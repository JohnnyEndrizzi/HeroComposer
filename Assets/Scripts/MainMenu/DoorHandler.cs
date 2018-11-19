﻿using UnityEngine;
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
    public string i;
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
            //uncommon later
            //closeddoor.Play("door_open");
            openeddoorAnimation();
		}
	}

	void OnMouseExit()
	{
        doorstatus = 0;
		closeddoor.Play("door_animation");
    }

	void PlaybuttonOnClick()
	{
        string songTitle = GameObject.Find("Song Dropdown").GetComponent<Dropdown>().captionText.text;
        string songDifficulty = GameObject.Find("Difficulty Dropdown").GetComponent<Dropdown>().captionText.text;

        Assets.Scripts.MainMenu.ApplicationModel.songPathName = Regex.Replace(songTitle, @"\s+", "") + "_" + songDifficulty;

        SceneManager.LoadScene("Scenes/main", LoadSceneMode.Single);
    }

    public void openeddoorAnimation()
    {
        Debug.Log("the clip 1 is " + closeddoor.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        //closeddoor.Play("door_open");
        GameObject.Find("PlayDoorClose").GetComponent<Animator>().Play("door_open");
        Debug.Log("the clip 2 is " + GameObject.Find("PlayDoorClose").GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name);
        i = closeddoor.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }


}

