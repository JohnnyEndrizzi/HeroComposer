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

    void Start ()
    {
        donelerp = false;
        doneScaleLerp1 = false;
        doneScaleLerp2 = false;

        barSize = gameObject.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update ()
    {
        //Debug.Log("Current = " + transform.GetComponent<RectTransform>().anchoredPosition);
        //Debug.Log("End     = " + endPos);

        if (donelerp == false)
        {
            lerpTime = ((approachRate - (startTimeInBeats - songPosInBeats)) / approachRate) >= 0 ?
                        (approachRate - (startTimeInBeats - songPosInBeats)) / approachRate :
                        ((approachRate - (startTimeInBeats - 0)) / approachRate);

            //transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, endPos, lerpTime);
            transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, endPos, lerpTime);

            if (transform.GetComponent<RectTransform>().anchoredPosition == endPos)
            {
                donelerp = true;
            }
        }

        if (doneScaleLerp1 == false)
        {
            /* Search for the primary note with the same id */

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

            /* Caclulate distance between primary note and set point (in world space) */
            barSize.x = Mathf.Abs(primaryNote.GetComponent<RectTransform>().anchoredPosition.x - (-363));

            /* Resize Bar until second object with share id is instanitated*/
            gameObject.GetComponent<RectTransform>().sizeDelta = barSize;

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
                Debug.Log("DONE");
                doneScaleLerp2 = true;
            }
        }
        
        if (donelerp && doneScaleLerp1 && doneScaleLerp2)
        {
            Debug.Log("Delete Bar:  " + id);
            Destroy(transform.gameObject);
        }
    }

    public void UpdateSongPosition(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
