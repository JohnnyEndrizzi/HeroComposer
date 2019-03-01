using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : DDOL {
    new void Awake()
    {
        //Destroy MainMenuHandler if one already exists
        if (GameObject.FindObjectsOfType<MainMenuHandler>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }           
    }

    void Update()
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
