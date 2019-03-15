using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Down : MonoBehaviour {
	private bool movement_lock;
	private int direction;
	private float init_position;
	private float deltay;
	// Use this for initialization
	void Start () {
		movement_lock = false;
        init_position = this.transform.position.y;
        deltay = 0.0f;
        /*Randomly pick a starting direction*/
        if(Random.value<0.5f)
            direction=1;
        else
            direction=-1;

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform.position, Vector3.up);
		if (Input.GetKey("up"))
        {
        	//if (!movement_lock){
        		
        	//}
            

            //transform.Translate(0, 1*Time.deltaTime, 0);
            //transform.Translate(0, Mathf.SmoothStep(0, Mathf.Sin(Random.Range(-20.0f, 20.0f)*Time.deltaTime), 4), 0);
            //Debug.Log("going up");
        }
        else if (Input.GetKey("down"))
        {
            //updown();
            //Debug.Log("going back");
        }
        if (Input.GetKey("left"))
        {
            //updown();
            //Debug.Log("going left");
        }
        else if (Input.GetKey("right"))
        {
            //updown();
            //Debug.Log("going right");
        }
        updown();
	}

	float movement(float x, int direction)
	{
		float y = 0;
		if (direction == 1 && x < 3 && x>=0){
			y = 0.1f;
		}
		else if (direction == 1 && x >= 3){
			y = -0.1f;
			direction = -1;
		}
		else if (direction == -1 && x >0){
			y = -0.1f;
		}
		else if (direction == -1 && x <=0){
			y = 0.1f;
			direction = 1;
		}

		return y;
		//Mathf.SmoothStep(0, Mathf.Sin(Random.Range(-50, 50)*Time.deltaTime), 99)

	}

	void updown() 
	{
		//movement_lock = true;
	   	//for (float f = 1f; f >= 0; f -= 0.5f) 
	    //{
		//while (counter < duration)
        //{
		Debug.Log("current is  " + this.transform.position.y + " init is " + init_position + " deltay is " + deltay);
    	
    	float newy = this.transform.position.y + movement(deltay,direction);
    	deltay = this.transform.position.y - init_position;
        transform.position = new Vector3(transform.position.x, newy, transform.position.z); //Mathf.Sin(Random.Range(-100.0f, 100.0f)*Time.deltaTime)
        //Debug.Log("The deltay is "+ deltay + " the y position is " + newy + " the direction is " + direction);
        //counter = counter + 0.1f;

        //movement_lock = false;

	    //}
	    //}
	    

	    /*
	    while (counter < duration)
        {
            moveNoteScore(counter, duration, score, spawnPoint, toPosition);

            counter += Time.deltaTime;
            yield return null;
        }
	    */
	}
}
