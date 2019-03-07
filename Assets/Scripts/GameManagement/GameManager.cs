using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    //Singleton instance
    public static GameManager Instance { get; private set; }

    //Enable or disable input
    public bool IsInputEnabled { get; set; }

    //SceneManager
    public SceneManagerWrapper sceneManager;

    //Called before start
    void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            //Save singleton instance
            Instance = this;
            //Dont destroy between scenes
            DontDestroyOnLoad(gameObject);
            //Destroy this instance
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        IsInputEnabled = true;
	}
}
