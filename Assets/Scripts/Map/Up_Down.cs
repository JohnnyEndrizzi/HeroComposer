using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Down : MonoBehaviour {
	private bool movement_lock;
	private int direction;
	private float init_position;
	private float deltay;
	public float delay_time;
	// Use this for initialization
	void Start () {
		movement_lock = false;
        init_position = this.transform.position.y;
        deltay = 0.0f;
        /*Randomly pick a starting direction*/
        direction=1;


	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(Camera.main.transform.position, Vector3.up);
		if (Input.GetKey("up"))
        {
 
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
        //updown();
        StartCoroutine(updown(delay_time));
	}

	float movement(float x, int direction)
	{
		float y = 0;
		if (direction == 1 && x < 5){
			y = 0.1f;
		}
		else if (direction == 1 && x >= 5){
			y = -0.1f;
			this.direction = -1;
			Debug.Log("change direction");
		}
		else if (direction == -1 && x >0){

			y = -0.1f;
		}
		else if (direction == -1 && x <=0){
			y = 0.1f;
			this.direction = 1;
			Debug.Log("change direction");
		}

		return y;
		//Mathf.SmoothStep(0, Mathf.Sin(Random.Range(-50, 50)*Time.deltaTime), 99)

	}

	//void updown()
	 IEnumerator updown(float time)
	{
	yield return new WaitForSeconds(time);
		Debug.Log("current is  " + this.transform.position.y + " init is " + init_position + " deltay is " + deltay + " direction is " + direction);
		
		float newy = this.transform.position.y + movement(deltay,direction);
		deltay = this.transform.position.y - init_position;
	    this.transform.position = new Vector3(transform.position.x, newy, transform.position.z);
	 
	} 


}
