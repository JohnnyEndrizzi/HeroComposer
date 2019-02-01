﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLabel : MonoBehaviour {

    public float rotationSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
