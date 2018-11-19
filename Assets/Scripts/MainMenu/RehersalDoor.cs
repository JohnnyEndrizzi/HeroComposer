using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RehersalDoor : MonoBehaviour {
    public Animator rehersalclose;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseOver()
    {
        // if (selectcanvas.enabled == false)
        //{

        rehersalclose.Play("RehersalOpen");
        // }
    }

    void OnMouseExit()
    {

        rehersalclose.Play("RehersalClose");
    }
}
