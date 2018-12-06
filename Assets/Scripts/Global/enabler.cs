using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enabler : MonoBehaviour {
	void Start () {
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
	}
}