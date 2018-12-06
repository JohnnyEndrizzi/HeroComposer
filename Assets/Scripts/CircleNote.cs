using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleNote : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 endPos;

    public float approachRate;
    public float startTimeInBeats;
    public float songPosInBeats;
    public float beatNumber;
    public int defendTarget;

    public float id;
    public bool firstNoteOfSlider;
    public bool secondNoteOfSlider;
    public float endTimeOfSlider;

    // Update is called once per frame
    void Update ()
    {
        if (transform.GetComponent<RectTransform>().anchoredPosition != endPos)
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, endPos, (approachRate - (startTimeInBeats - songPosInBeats)) / approachRate);
        }
        else
        {
            if (firstNoteOfSlider)
            {
                if (songPosInBeats >= endTimeOfSlider)
                {
                    //Debug.Log("Delete Secondary Note: " + id);
                    Destroy(transform.gameObject);
                }
            }
            else
            {
                //Debug.Log("Delete Primary Note: " + id);
                if (this.name == "defendNoteEnd")
                {
                    GameObject.Find("Menu").GetComponent<BossLogic>().BossAttack(defendTarget);
                }
                Destroy(transform.gameObject);
            }
        }
    }

    public void UpdateSongPosition(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
