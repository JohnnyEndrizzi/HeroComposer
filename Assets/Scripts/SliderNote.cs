using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderNote : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;

    public float id;

    public float approachRate;
    public float startTimeInBeats;
    public float endTimeInBeats;
    public float songPosInBeats;

    private bool donelerp;
    private bool doneScaleLerp1;
    private bool doneScaleLerp2;

    private float scaleLerpTime;
    private float lerpTime;

    private GameObject primaryNote;
    private GameObject secondaryNote;
    private Vector2 barSize;

    /* Use this for initialization */
    void Start ()
    {
        donelerp = false;
        doneScaleLerp1 = false;
        doneScaleLerp2 = false;

        barSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    /* Update is called once per frame */
    void Update ()
    {
        /* The middle bar between hold notes works as follows:
         *  1) Leading note of slider note spawns (PrimaryNote)
         *  2) Middle bar spawns with scale set to 0.1 and its anchor set to the right
         *  3) As PrimaryNote lerps along the NoteBar the middle bar will follow 
         *  4) While its lerping, the middle bar will increase its scale, keeping an end at the PrimaryNote and the other at the spawn point
         *  5) The ending note of the slider note spawns (SecondaryNote)
         *  6) Upon SecondaryNote spawning, the middle bar will stop increasing its scale (with an end at the PrimaryNote and the other at the SecondaryNote)
         *  7) The "combined" hold note will lerp together 
         *  8) PrimaryNote will eventually reach the kill point, but will stay there until SecondaryNote also arrives, dying at the same time.
         *  9) The middle bar will decrease its scale accordingly as the distance between PrimaryNote and SecondaryNote decrease 
         * 10) Once SecondaryNote arrives atthe kill point with PrimaryNote (middle bar should have a ~0.1 scale), the "combined" hold note will kill together */

        /* This is the logic that will lerp the middle bar from spawn to kill point using the current song's BPM value */
        if (donelerp == false)
        {
            lerpTime = ((approachRate - (startTimeInBeats - songPosInBeats)) / approachRate) >= 0 ?
                        (approachRate - (startTimeInBeats - songPosInBeats)) / approachRate :
                        ((approachRate - (startTimeInBeats - 0)) / approachRate);

            transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, endPos, lerpTime);

            if (transform.GetComponent<RectTransform>().anchoredPosition == endPos)
            {
                donelerp = true;
            }
        }

        /* Step 4 from above: The middle bar will increase its scale relative to the PrimaryNote and spawn point, until SecondaryNote spawns */
        if (doneScaleLerp1 == false)
        {
            /* Search for PrimaryNote with the same id as the middle bar */
            if (primaryNote == null)
            {
                GameObject[] possiblePrimaryNotes;
                possiblePrimaryNotes = GameObject.FindGameObjectsWithTag("PrimaryNote");

                for (int i = 0; i < possiblePrimaryNotes.Length; i++)
                {
                    if (possiblePrimaryNotes[i].GetComponent<CircleNote>().id == id)
                    {
                        primaryNote = possiblePrimaryNotes[i];
                    }
                }
            }

            /* Caclulate distance between PrimaryNote and spawn point (in world space) */
            if (primaryNote == null)
            {
                barSize.x = Mathf.Abs(328 - (-363));
            }
            else
            {
                barSize.x = Mathf.Abs(primaryNote.GetComponent<RectTransform>().anchoredPosition.x - (-363));
            }

            /* Resize middle bar until SecondaryNote with same id is instanitated */
            gameObject.GetComponent<RectTransform>().sizeDelta = barSize;

            /* Search for SecondaryNote with the same id as the middle bar and PrimaryNote */
            if (secondaryNote == null)
            {
                GameObject[] possibleSecondaryNotes;
                possibleSecondaryNotes = GameObject.FindGameObjectsWithTag("SecondaryNote");

                for (int i = 0; i < possibleSecondaryNotes.Length; i++)
                {
                    if (possibleSecondaryNotes[i].GetComponent<CircleNote>().id == id)
                    {
                        secondaryNote = possibleSecondaryNotes[i];
                        doneScaleLerp1 = true;
                    }
                }
            }
        }
        /* Step 9 from above: The middle bar will decrease its scale until SecondaryNote catches up PrimaryNote at the kill point */
        else if (doneScaleLerp2 == false && (doneScaleLerp1 && donelerp))
        {
            if (secondaryNote != null)
            {
                /* Caclulate distance between secondary note and set point (in world space) */
                barSize.x = Mathf.Abs(secondaryNote.GetComponent<RectTransform>().anchoredPosition.x - 328);

                /* Resize Bar until second object with share id is instanitated*/
                gameObject.GetComponent<RectTransform>().sizeDelta = barSize;
            }
            else
            {
                doneScaleLerp2 = true;
            }
        }

        /* Step 10 from above: PrimaryNote, SecondaryNote, and the middle bar will die together upon arriving at the kill point */
        if (donelerp && doneScaleLerp1 && doneScaleLerp2)
        {
            Destroy(transform.gameObject);
        }
    }

    /* This function is subscribed to the metronome's publishing, which will drive the note spawn/movement */
    public void UpdateSongPosition(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
