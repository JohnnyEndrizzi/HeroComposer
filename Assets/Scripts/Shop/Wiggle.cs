using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wiggle : MonoBehaviour {
    
    float lerpTime = 3f;
    float currentLerpTime;

    private float moveSpeed=5;
    private float moveLoc;
    private Quaternion Rotate_From;
    private Quaternion Rotate_To;
	
	void Update () {

        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }
        float perc = currentLerpTime / lerpTime;

        if(perc >= 1){
            moveLoc = moveSpeed*next(moveLoc/moveSpeed);
            currentLerpTime = 0;
        }

        Rotate_From = transform.rotation;
        Rotate_To = Quaternion.Euler (0,0,moveLoc);

        transform.rotation = Quaternion.Lerp (Rotate_From, Rotate_To, Time.deltaTime*0.2f);
	}

    private int next(float last){
        int rnd = Random.Range(-2, 5);

        if (rnd > 2) {
            return (int)last;
        }
        else {
            return  rnd;
        }
    }
}

