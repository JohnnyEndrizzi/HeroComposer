using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class DoorHandler : MonoBehaviour
{
	private Sprite[] sprites;
	public Animator closeddoor;
	private int doorstatus;
	public Canvas selectcanvas;
	public Button play;

    void Start()
    {
        /* Deserializes all information from their corresponding JSON into local copies */
        if (GetComponent<LoadData>() != null && Assets.Scripts.MainMenu.ApplicationModel.loadedCharacters == false)
        {
            Assets.Scripts.MainMenu.ApplicationModel.loadedCharacters = true;
            GetComponent<LoadData>().LoadCharacters();
            GetComponent<LoadData>().LoadItems();
            GetComponent<LoadData>().LoadInv();
            /* TODO */
            //GetComponent<LoadData>().LoadMagic();
        }

        selectcanvas.enabled = false;
        StoredValues.Cash += 5000;
    }

    /* Update is called once per frame */
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
                    play.onClick.AddListener(PlaybuttonOnClick);
                }
            }     
		}
    }

    /* This function is called when the mouse hovers over the doors on the main menu */
	void OnMouseOver()
	{	
		if (selectcanvas.enabled == false)
        {
			doorstatus = 1;

            /* The corresponding door's open animation will play */
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

    /* This function is called when the mouse stops hovering over the doors on the main menu */
    void OnMouseExit()
    {
        doorstatus = 0;

        /* The corresponding door's close animation will play */
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

    /* This function is called when the mouse clicks on a door on the main menu */
    void OnClick()
    {
        string tempName = this.name.Replace("DoorClose", "");

        /* Functional Requirement
         * ID: 8.1.1-8
         * Description: The system must display the inventory screen.
         * 
         * Functional Requirement
         * ID: 8.1.1-20
         * Description: The system must preserve insight about character and party customization between level completions and menu selections.
         * 
         * The scene corresponding to the door will be loaded, as long as its not the play door (which is covered in the function below) */

        if (tempName.Equals("Audition")) //Temporary until Audition scene is completed
        {
            MenuSceneSwitch("Menu");
        }
        else if (!tempName.Equals("Play")) //Play button has a more complex function attached
        {
            MenuSceneSwitch(tempName);
        }
    }

    /* This function is called when the mouse clicks on the play door on the main menu (for starting the gameplay) */
    void PlaybuttonOnClick()
	{
        string songTitle = GameObject.Find("Song Dropdown").GetComponent<Dropdown>().captionText.text;
        string songDifficulty = GameObject.Find("Difficulty Dropdown").GetComponent<Dropdown>().captionText.text;

        /* Functional Requirement
         * ID: 8.2-1
         * Description: The player must be able to choose a level.
         * 
         * Creates a path to the selected song using the provided name and difficulty, and saves it in ApplicationModel */
        Assets.Scripts.MainMenu.ApplicationModel.songName = Regex.Replace(songTitle, @"\s+", "");
        Assets.Scripts.MainMenu.ApplicationModel.songPathName = Regex.Replace(songTitle, @"\s+", "") + "_" + songDifficulty;

        /* Preserves the main menu as the last scene */
        //LastScene.instance.prevScene = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene("Scenes/main", LoadSceneMode.Single);
        MenuSceneSwitch("main");
    }

    /* Preserves the main menu as the last scene and loads the new one */
    private void MenuSceneSwitch(string sceneNew)
    {
        GameObject.Find("sceneSwitcher").GetComponent<SceneSwitcher>().sceneSwitchCurtains(sceneNew);
    }
}