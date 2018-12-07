﻿using UnityEngine;

public class Wiggle : MonoBehaviour {
    //wiggles things like signs
    
    float lerpTime = 3f;
    float currentLerpTime;

    private float moveSpeed=5;
    private float moveLoc;
    private Quaternion Rotate_From;
    private Quaternion Rotate_To;
	
	void Update ()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float perc = currentLerpTime / lerpTime;

        if(perc >= 1) //done wiggle
        {
            moveLoc = moveSpeed*next(moveLoc/moveSpeed);
            currentLerpTime = 0;
        }

        Rotate_From = transform.rotation;
        Rotate_To = Quaternion.Euler (0,0,moveLoc);

        transform.rotation = Quaternion.Lerp (Rotate_From, Rotate_To, Time.deltaTime*0.2f);
	}

    private int next(float last) //pick next angle to wiggle to
    {
        int rnd = Random.Range(-2, 5);

        if (rnd > 2) {
            return (int)last;  //do nothing - makes the wiggle a little gentler and more random
        }
        else {
            return  rnd;
        }
    }
}

