using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastScene : MonoBehaviour {
    public static LastScene instance = null;

    public string prevScene = null; //TODO

	void Awake () {
        if (!instance) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
	}
}
