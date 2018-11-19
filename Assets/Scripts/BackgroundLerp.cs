using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLerp : MonoBehaviour {

    int counter;

	// Use this for initialization
	void Start ()
    {
        counter = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 start = transform.position;
        Vector3 end = start;
        end.x =- 55;

        transform.position = Vector3.Lerp(start, end, Time.deltaTime / transform.parent.GetComponent<AudioSource>().clip.length);
    }
}
