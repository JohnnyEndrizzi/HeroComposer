using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuditionDoor: MonoBehaviour
{
    public Animator auditionclose;
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

        auditionclose.Play("AuditionOpen");

    }

    void OnMouseExit()
    {

        auditionclose.Play("AuditionClose");
    }
}
