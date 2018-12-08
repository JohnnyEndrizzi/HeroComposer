using UnityEngine;

public class AtkMove : MonoBehaviour {
    //This script controls Animation movement 

    //Initial and final values
    public Vector3 StartPos;
    public Vector3 EndPos;
    private Vector3 StartSize;
    public Vector3 EndSize;

    public int custCmd;
    private float counter = 0f;
       
    public float atkLerpTime;        
    float currentLerpTime = 0;

    //Optional child mask
    private Transform mask;
    private Vector3 StartPosMask;
    private Vector3 EndPosMask;    
    
    void Start ()
    {
        StartSize = transform.localScale;

        /*The second level of special attacks which control animation masks */        
        if (this.transform.childCount == 1) { 
            mask = this.transform.GetChild(0);
            StartPosMask = mask.localPosition;
            if      (custCmd == 51) {EndPosMask = new Vector3(StartPosMask.x + 1, StartPosMask.y, StartPosMask.z);}
            else if (custCmd == 52) {EndPosMask = new Vector3(StartPosMask.x, StartPosMask.y * -1, StartPosMask.z);}
            else if (custCmd == 53) {EndPosMask = StartPosMask; StartPosMask.y = StartPosMask.y * -1;}
            else if (custCmd == 12) {EndPosMask = StartPosMask; counter = Random.Range(-5f, 5f);} //it's not a mask, it's a child! 
        }
    }

    void Update()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > atkLerpTime){
            currentLerpTime = atkLerpTime;
        }
                    
        float perc = currentLerpTime / atkLerpTime;

        //Move attack 
        transform.position = Vector2.Lerp(StartPos, EndPos, perc);
        transform.localScale = Vector3.Lerp(transform.localScale, EndSize, perc);

        //Face the target
        Vector3 targetDir = EndPos - StartPos;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg-180;
        transform.localRotation = Quaternion.Euler(Vector3.forward * angle);

        //Custom commands during movement
        /* Third level of special attacks which control unique timing patterns and spinning */
        if (custCmd == 4 & perc >= 1) { //replay in reverse
            custCmd = -1; perc = 0;
            currentLerpTime = 0;
            EndSize = EndSize * 0.25f;
        }
        if (custCmd == 3 & perc >= 1 & counter<2) { //triple move
            perc = 0; counter++;
            currentLerpTime = 0;
            transform.position = StartPos;
            transform.localScale = StartSize;
            EndPos.x = EndPos.x + Random.Range(-2f, 2f);
            EndPos.y = EndPos.y + Random.Range(-2f, 2f);
        }
        if (custCmd == 12) {//spin child object
            mask.localRotation = Quaternion.Euler(Vector3.right*counter);
        }

        if (perc >= 1) { //destroy When complete
            Destroy(this.gameObject);
        }
        if (mask != null) { //Move mask
            mask.localPosition = Vector2.Lerp(StartPosMask, EndPosMask, perc);
        }
    }
}