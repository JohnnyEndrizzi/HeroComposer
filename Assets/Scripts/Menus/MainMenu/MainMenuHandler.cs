using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : DDOL {
    public void Update()
    {
        //Escape key pressed 
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //Delete main menue handler if returning to title screen
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Destroy(this.gameObject);
            }
            //Return to previous scene
            GameObject.Find("SceneManagerWrapper").GetComponent<SceneManagerWrapper>().LoadPreviousScene();
        }
    }
}
