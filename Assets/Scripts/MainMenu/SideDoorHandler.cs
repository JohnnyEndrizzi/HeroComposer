using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class SideDoorHandler : MonoBehaviour {
    private Sprite[] sprites;
    public Animator closeddoor;
    private int doorstatus;
    public Canvas selectcanvas;
    public Button play;

    void Start()
    {
        selectcanvas.enabled = false; 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown (0) && doorstatus == 1 )
        {
            OnClick();

            if (play != null) {
                play.onClick.AddListener(PlaybuttonOnClick);
                if (!selectcanvas.enabled) {
                    selectcanvas.enabled = true;
                    play.onClick.AddListener(PlaybuttonOnClick);
                }
                else {
                    //Debug.Log("canvas is enabled");
                }
            }
        }
    }

    void OnMouseOver()
    {   
        if (selectcanvas.enabled == false)
        {
            doorstatus = 1;
            closeddoor.Play("door_open");
        }
    }

    void OnMouseExit()
    {
        doorstatus = 0;
        closeddoor.Play("door_animation");
    }
        
    void OnClick(){
        string tempName = this.name.Replace("DoorClose", "");

        if (!tempName.Equals("Play")) {
            //TODO close curtains here?
            //MenuSceneSwitch(this.name.Replace("DoorClose",""));
            MenuSceneSwitch(tempName);
        }
    }
    void PlaybuttonOnClick(){
        string songTitle = GameObject.Find("Song Dropdown").GetComponent<Dropdown>().captionText.text;
        string songDifficulty = GameObject.Find("Difficulty Dropdown").GetComponent<Dropdown>().captionText.text;

        Assets.Scripts.MainMenu.ApplicationModel.songPathName = Regex.Replace(songTitle, @"\s+", "") + "_" + songDifficulty;

        SceneManager.LoadScene("Scenes/main", LoadSceneMode.Single);
    }

    public void MenuSceneSwitch(string sceneNew){     
        SceneManager.LoadScene(sceneNew, LoadSceneMode.Single);
    }
}