using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundCheckDoor : MonoBehaviour {

    public Animator soundcheckclose;
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

        soundcheckclose.Play("SoundCheckOpen");

    }

    void OnMouseExit()
    {

        soundcheckclose.Play("SoundCheckClose");
    }
}
