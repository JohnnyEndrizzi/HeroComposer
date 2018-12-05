using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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

    private bool Done = false;

    public CharacterScriptObject character1;
    public CharacterScriptObject character2;
    public CharacterScriptObject character3;
    public CharacterScriptObject character4;

    void Start()
    {
        if (GetComponent<LoadData>() != null && Done == false)
        {
            Done = true;
            GetComponent<LoadData>().LoadCharacters();   //This code runs 5 times
            GetComponent<LoadData>().LoadItems();
            GetComponent<LoadData>().LoadInv();
            //GetComponent<LoadData>().LoadMagic(); //TODO
        }

        selectcanvas.enabled = false;

        StoredValues.Cash += 5000;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown (0) && doorstatus == 1 )
        {
            OnClick();

            if (play != null)
            {
                
                if (!selectcanvas.enabled)
                {
                    selectcanvas.enabled = true;
                    charSet();
                    play.onClick.AddListener(PlaybuttonOnClick);
                }
                else
                {
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

            if (this.name.Equals("InventoryDoorClose"))
            {
                closeddoor.Play("SoundCheckOpen");
            }
            else if (this.name.Equals("ShopDoorClose"))
            {
                closeddoor.Play("MusicShopOpen");
            }
            else if (this.name.Equals("PlayDoorClose"))
            {
                closeddoor.Play("door_open");
            }
            else
            {
                string tempName = this.name.Replace("DoorClose", "");
                closeddoor.Play(tempName + "Open");
            }            
        }
	}

    void OnMouseExit()
    {
        doorstatus = 0;

        if (this.name.Equals("InventoryDoorClose"))
        {
            closeddoor.Play("SoundCheckClose");
        }
        else if (this.name.Equals("ShopDoorClose"))
        {
            closeddoor.Play("MusicShopClose");
        }
        else if (this.name.Equals("PlayDoorClose"))
        {
            closeddoor.Play("door_animation");
        }
        else
        {
            string tempName = this.name.Replace("DoorClose", "");
            closeddoor.Play(tempName + "Close");
        }
    }

    void OnClick()
    {
        string tempName = this.name.Replace("DoorClose", "");

        if (!tempName.Equals("Play"))
        {
            //TODO close curtains here?
            //MenuSceneSwitch(this.name.Replace("DoorClose",""));
            MenuSceneSwitch(tempName);
        }
    }

    void charSet()
    {
        //Assets.Scripts.MainMenu.ApplicationModel.characters.Add(character1);
        //Assets.Scripts.MainMenu.ApplicationModel.characters.Add(character2);
        //Assets.Scripts.MainMenu.ApplicationModel.characters.Add(character3);
        //Assets.Scripts.MainMenu.ApplicationModel.characters.Add(character4);
    }

	void PlaybuttonOnClick()
	{
        string songTitle = GameObject.Find("Song Dropdown").GetComponent<Dropdown>().captionText.text;
        string songDifficulty = GameObject.Find("Difficulty Dropdown").GetComponent<Dropdown>().captionText.text;

        Assets.Scripts.MainMenu.ApplicationModel.songPathName = Regex.Replace(songTitle, @"\s+", "") + "_" + songDifficulty;

        LastScene.instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Scenes/main", LoadSceneMode.Single);
    }

    public void MenuSceneSwitch(string sceneNew)
    {
        LastScene.instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneNew, LoadSceneMode.Single);
    }
}

