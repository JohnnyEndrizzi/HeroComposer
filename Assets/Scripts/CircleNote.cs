using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleNote : MonoBehaviour
{
    /* Variables used for spawning and killing the circle note */
    public Vector2 startPos;
    public Vector2 endPos;

    /* Variables used for lerping the circle note */
    public float approachRate;
    public float startTimeInBeats;
    public float songPosInBeats;
    public float beatNumber;
    public int defendTarget;

    /* Variables used for identifying the note */
    public float id;
    public bool firstNoteOfSlider;
    public bool secondNoteOfSlider;
    public float endTimeOfSlider;

    /* Update is called once per frame */
    void Update ()
    {
        /* A standard circle note will spawn at startPos and lerp to endPos before dying */
        if (transform.GetComponent<RectTransform>().anchoredPosition != endPos)
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, endPos, (approachRate - (startTimeInBeats - songPosInBeats)) / approachRate);
        }
        else
        {
            /* If the circle note is the leading note of a hold note pair, it will wait for the duration of the hold note 
             * to end (when the end of the hold note arrives at the kill point) before dying. */
            if (firstNoteOfSlider)
            {
                if (songPosInBeats >= endTimeOfSlider)
                {
                    Destroy(transform.gameObject);
                }
            }
            /* If the circle note is independant or is the ending note of a hold note pair it will delete immediately */
            else
            {
                /* If this was a note where the boss was queued to attack, the attack coroutine is called */
                if (this.name == "defendNoteEnd")
                {
                    GameObject.Find("Menu").GetComponent<BossLogic>().BossAttack(defendTarget);
                }

                Destroy(transform.gameObject);
            }
        }
    }

    /* This is the function that is subscribed to Metronome's publishing */
    public void UpdateSongPosition(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
