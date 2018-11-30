using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkMove : MonoBehaviour {
     
    public Vector3 StartPos;
    public Vector3 EndPos;

    public int custCmd;
    private float counter = 0f;

    private Vector3 StartSize;
    public Vector3 EndSize;

    public float atkLerpTime;        
    float currentLerpTime = 0;

    private Transform mask;
    private Vector3 StartPosMask;
    private Vector3 EndPosMask;    


    void Start (){
        StartSize = transform.localScale;

        if (this.transform.childCount == 1) { 
            mask = this.transform.GetChild(0);
            StartPosMask = mask.localPosition;
            if      (custCmd == 51) {EndPosMask = new Vector3(StartPosMask.x + 1, StartPosMask.y, StartPosMask.z);}
            else if (custCmd == 52) {EndPosMask = new Vector3(StartPosMask.x, StartPosMask.y * -1, StartPosMask.z);}
            else if (custCmd == 53) {EndPosMask = StartPosMask; StartPosMask.y = StartPosMask.y * -1;}
            else if (custCmd == 12) {EndPosMask = StartPosMask; counter = Random.Range(-5f, 5f);}
        }
    }

    void Update(){
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > atkLerpTime){
            currentLerpTime = atkLerpTime;
        }
                    
        float perc = currentLerpTime / atkLerpTime;

        transform.position = Vector2.Lerp(StartPos, EndPos, perc);
        transform.localScale = Vector3.Lerp(transform.localScale, EndSize, perc);

        Vector3 targetDir = EndPos - StartPos;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg-180;
        transform.localRotation = Quaternion.Euler(Vector3.forward * angle);


        if (custCmd == 4 & perc >= 1) {
            custCmd = -1; perc = 0;
            currentLerpTime = 0;
            EndSize = EndSize * 0.25f;
        }
        if (custCmd == 3 & perc >= 1 & counter<2) {
            perc = 0; counter++;
            currentLerpTime = 0;
            transform.position = StartPos;
            transform.localScale = StartSize;
            EndPos.x = EndPos.x + Random.Range(-2f, 2f);
            EndPos.y = EndPos.y + Random.Range(-2f, 2f);
        }
        if (custCmd == 12) {//TODO fix
            mask.localRotation = Quaternion.Euler(Vector3.right*counter);
        }


        if (perc >= 1) {
            Destroy(this.gameObject);
        }
        if (mask != null) {
            mask.localPosition = Vector2.Lerp(StartPosMask, EndPosMask, perc);
        }
    }
}