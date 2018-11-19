using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionDoor : MonoBehaviour {
    private Sprite[] sprites;
    public Animator auditionclose;
   // public Canvas selectcanvas;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnMouseOver()
    {
       // if (selectcanvas.enabled == false)
        //{
            
            auditionclose.Play("AuditionOpen");
       // }
    }

    void OnMouseExit()
    {
       
        auditionclose.Play("AuditionClose");
    }
}
