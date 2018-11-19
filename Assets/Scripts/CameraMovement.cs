using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static bool cameraMovementFinished;

    private Vector3 start;
    private Vector3 end;
    private float lerp = 0f;

    // Use this for initialization
    void Start ()
    {
        start = transform.position;
        end = start;
        end.y = 0.95f;	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if ((transform.position == end) && (cameraMovementFinished == false))
        {
            cameraMovementFinished = true;
        }
        else
        {
            lerp += Time.deltaTime / 2;
            transform.position = Vector3.Lerp(start, end, lerp);
        }
    }
}
