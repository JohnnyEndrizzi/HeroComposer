using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelLabel : MonoBehaviour {

    public float rotationSpeed = 10.0f;
    public int level = 0;

	// Use this for initialization
	void Start () {
		var lockedLevels = GameManager.Instance.gameDataManager.getLockedLevels();
        var gameobject = GetComponent<TMP_Text>();
        if (lockedLevels.ContainsKey(level)|| level == 0)
        {
            gameobject.fontSize = 50f;
            gameobject.SetText("LOCKED");
        }
        else
        {
            gameobject.fontSize = 65f;
            gameobject.SetText("Level " + level);
        }
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
	}
}
