using UnityEngine;

public class LevelStart : Interactable {

    public string songName = "";

    string difficulty = "Normal";

    public override void Interact()
    {
        /* Functional Requirement
        * ID: 8.2-1
        * Description: The player must be able to choose a level.
        * 
        * Creates a path to the selected song using the provided name and difficulty, and saves it in ApplicationModel */
        Assets.Scripts.MainMenu.ApplicationModel.songName = songName;
        Assets.Scripts.MainMenu.ApplicationModel.songPathName = songName + "_" + difficulty;

        GameObject.Find("sceneSwitcher").GetComponent<SceneSwitcher>().sceneSwitchHidden("main");
    }
}
