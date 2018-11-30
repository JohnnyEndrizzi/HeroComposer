using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class DoorHandler : MonoBehaviour
{
	private Sprite[] sprites;
	public Animator closeddoor;
	private int doorstatus;
	public Canvas selectcanvas;
	public Button play;

    public Character character1;
    public Character character2;
    public Character character3;
    public Character character4;

    void Start()
    {
        selectcanvas.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown (0) && doorstatus == 1 )
        {
			if(!selectcanvas.enabled)
            {
				selectcanvas.enabled = true;
                play.onClick.AddListener(PlaybuttonOnClick);
			}
			else
            {
				//Debug.Log("canvas is enabled");
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

	void PlaybuttonOnClick()
	{
        Assets.Scripts.MainMenu.ApplicationModel.characters.Add(character1);
        Assets.Scripts.MainMenu.ApplicationModel.characters.Add(character2);
        Assets.Scripts.MainMenu.ApplicationModel.characters.Add(character3);
        Assets.Scripts.MainMenu.ApplicationModel.characters.Add(character4);

        string songTitle = GameObject.Find("Song Dropdown").GetComponent<Dropdown>().captionText.text;
        string songDifficulty = GameObject.Find("Difficulty Dropdown").GetComponent<Dropdown>().captionText.text;

        Assets.Scripts.MainMenu.ApplicationModel.songPathName = Regex.Replace(songTitle, @"\s+", "") + "_" + songDifficulty;

        SceneManager.LoadScene("Scenes/main", LoadSceneMode.Single);
    }
}

