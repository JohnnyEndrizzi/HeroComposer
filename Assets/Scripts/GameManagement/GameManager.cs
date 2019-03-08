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

    //GameData
    public GameDataManager gameDataManager;

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

    //Called every frame
    void Update()
    {
        //Enter debug mode
        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            EnterDebugMode();
        }    
    }

    //Enter debug mode (skip to gameplay) 
    private void EnterDebugMode()
    {
        Debug.Log("Entering DEBUG mode.");
        //Add code here
    }
}
