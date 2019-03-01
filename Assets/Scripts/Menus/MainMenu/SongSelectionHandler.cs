using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SongSelectionHandler : MonoBehaviour {
    //Song selection canvas
    public Canvas songSelectionCanvas;

    //This is for initilization
    public void Start()
    {
        songSelectionCanvas.enabled = false;
    }

    //Start the song that was selected
    public void StartSong() {
        /* Functional Requirement
        * ID: 8.2-1
        * Description: The player must be able to choose a level.
        * 
        * Creates a path to the selected song using the provided name and difficulty, and saves it in ApplicationModel */
        string songTitle = GameObject.Find("Song Dropdown").GetComponent<Dropdown>().captionText.text;
        string songDifficulty = GameObject.Find("Difficulty Dropdown").GetComponent<Dropdown>().captionText.text;

        Assets.Scripts.MainMenu.ApplicationModel.songName = Regex.Replace(songTitle, @"\s+", "");
        Assets.Scripts.MainMenu.ApplicationModel.songPathName = Regex.Replace(songTitle, @"\s+", "") + "_" + songDifficulty;

        GameObject.Find("SceneManagerWrapper").GetComponent<SceneManagerWrapper>().SwitchScene("main");
    }

    public void OpenSongSelectionMenu()
    {
        songSelectionCanvas.enabled = true;
    }
}
