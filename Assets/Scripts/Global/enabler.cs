﻿using UnityEngine;

public class enabler : MonoBehaviour {
    //Quality of life script to make object visible on load (for objects disabled on editor as they block the view)
    void Start ()
    {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}
}