using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleNote : MonoBehaviour {

    public Vector3 startPos;
    public Vector3 endPos;

    public float approachRate;
    public float startTimeInBeats;
    public float songPosInBeats;
    public int beatNumber;

	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(startPos, endPos, (approachRate - (startTimeInBeats - songPosInBeats)) / approachRate);
        if (transform.position == endPos)
        {
            //Debug.Log(string.Format("Deleting note {0}.", beatNumber));
            Destroy(transform.root.gameObject);
            GameLogic.hitIndex++;
        }
    }

    public void UpdateSongPosition(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
