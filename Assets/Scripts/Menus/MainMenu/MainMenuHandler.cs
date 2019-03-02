using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour {
    //Singleton instance
    public static MainMenuHandler Instance { get; private set; }

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
        }else{
            Destroy(this.gameObject);
        }

    }

    //This is called every frame
    void Update()
    {
        //Escape key pressed 
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //If at the main menu
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                //Return to title screen 
                Destroy(this.gameObject);
                SceneManagerWrapper.Instance.SwitchSceneWithCurtains("StartScreen", false);
            }
            else
            {
                //Return to previous scene
                SceneManagerWrapper.Instance.LoadPreviousScene();
            }
        }
    }

    public static void EnableDoors()
    {
        foreach (Door d in GameObject.FindObjectsOfType<Door>())
        {
            d.doorEnabled = true;
        }
    }

    public static void DisableDoors()
    {
        foreach (Door d in GameObject.FindObjectsOfType<Door>())
        {
            d.doorEnabled = false;
        }
    }
}
