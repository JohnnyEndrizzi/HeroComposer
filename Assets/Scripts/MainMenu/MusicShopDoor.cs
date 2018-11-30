using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicShopDoor : MonoBehaviour {

    public Animator musicshopclose;
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

        musicshopclose.Play("MusicShopOpen");

    }

    void OnMouseExit()
    {

        musicshopclose.Play("MusicShopClose");
    }
}
