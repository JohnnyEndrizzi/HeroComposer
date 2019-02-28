using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class DoorHandler : MonoBehaviour
{
	private Sprite[] sprites;
	public Animator closeddoor;
	private int doorstatus;
	public Canvas selectcanvas;
	public Button play;

    //Audio
    private AudioSource audioSource;
    private AudioClip opening;
    private AudioClip closing;

    private bool clearToProceed = false;
    private bool doorsUnlocked = false;

    /* TEMP MESSAGE
     * To change b/n map and game on click 
     * 
     * change comments on:
     * ~ line 171 in PlaybuttonOnClick()
     */

    void Start()
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }

        audioSource = GetComponent<AudioSource>();
        opening = (AudioClip)Resources.Load("SoundEffects/menu_door_open");
        closing = (AudioClip)Resources.Load("SoundEffects/menu_door_close");
        audioSource.mute = true;

        selectcanvas.enabled = false;    
    }
        
    void Update()
    {
		if (Input.GetMouseButtonDown (0) && doorstatus == 1 )
        {
            OnClick();
           
            if (play != null)
            {
                if (!selectcanvas.enabled)
                {
                    mute();
                    selectcanvas.enabled = true;
                    play.onClick.AddListener(PlaybuttonOnClick);                    
                } 
            }     
		}

        if (Input.GetKeyDown(KeyCode.Escape) && this.name.Equals("PlayDoorClose"))
        {
            if (selectcanvas.enabled && play != null)
            {
                unMute();
                selectcanvas.enabled = false;
                play.onClick.RemoveListener(PlaybuttonOnClick);
            }
            else if (!selectcanvas.enabled)
            {
                //TODO open pause menu
                MenuSceneSwitch("StartMenu");
            }
        }        
    }

    /* This function is called when the mouse hovers over the doors on the main menu */
	void OnMouseEnter()
	{	
		if (selectcanvas.enabled == false && clearToProceed == true)
        {
            doorstatus = 1;
            StartCoroutine(Doors());
            //StartCoroutine(DoorOpen()); //The old code
        }
	}

    /* This function is called when the mouse stops hovering over the doors on the main menu */
    void OnMouseExit()
    {
        doorstatus = 0;
        //StartCoroutine(DoorClose()); //The old code
    }

    /*Single check for if the mouse is already hovering over a door when the doors are unlocked*/
    void OnMouseOver()
    {
        if(doorsUnlocked == true)
        {
            doorsUnlocked = false;
            doorstatus = 1;
            StartCoroutine(Doors());
        }        
    }  
    
    public void DoorUnlocker()
    {
        clearToProceed = true;
        doorsUnlocked = true;
    }

    IEnumerator Doors()
    {
        StartCoroutine(DoorOpen());

        if (doorstatus != 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        
        yield return StartCoroutine(IsDoorClosed());


        StartCoroutine(DoorClose());

        yield return null;
    }

    IEnumerator Doors2() //WIP
    {
        yield return StartCoroutine(D1());
        yield return StartCoroutine(D2());

        yield return null;
    }

    IEnumerator D1() //WIP
    {
        if (doorstatus != 0)
        {
            yield return null;
        }
        yield return null;
    }

    IEnumerator D2() //WIP
    {       
        yield return new WaitForSeconds(0.5f);
        yield return null;
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
            this.transform.GetComponent<ComingSoon>().clicked();
        }
        else if (!tempName.Equals("Play")) //Play button has a more complex function attached
        {
            mute();
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

        MenuSceneSwitch("main");
        //MenuSceneSwitch("map");
    }

    /* Preserves the main menu as the last scene and loads the new one */
    private void MenuSceneSwitch(string sceneNew)
    {
        GameObject.Find("sceneSwitcher").GetComponent<SceneSwitcher>().sceneSwitchCurtains(sceneNew);
    }

    public void mute()
    {
        GameObject.Find("PlayDoorClose").GetComponent<AudioSource>().mute = true;
        GameObject.Find("InventoryDoorClose").GetComponent<AudioSource>().mute = true;
        GameObject.Find("AuditionDoorClose").GetComponent<AudioSource>().mute = true;
        GameObject.Find("ShopDoorClose").GetComponent<AudioSource>().mute = true;
        GameObject.Find("RehersalDoorClose").GetComponent<AudioSource>().mute = true;
    }

    public void unMute()
    {
        GameObject.Find("PlayDoorClose").GetComponent<AudioSource>().mute = false;
        GameObject.Find("InventoryDoorClose").GetComponent<AudioSource>().mute = false;
        GameObject.Find("AuditionDoorClose").GetComponent<AudioSource>().mute = false;
        GameObject.Find("ShopDoorClose").GetComponent<AudioSource>().mute = false;
        GameObject.Find("RehersalDoorClose").GetComponent<AudioSource>().mute = false;
    }

    IEnumerator IsDoorClosed() //OnHoverExit
    {
        while (doorstatus != 0)
        {
            yield return null;
        }
    }

    IEnumerator DoorOpen() //Plays Door Opening Animation
    {
        audioSource.Pause();
        audioSource.PlayOneShot(opening, 0.7F);

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
        yield return null;
    }

    IEnumerator DoorClose() //Plays Door Closing Animation
    {
        audioSource.Pause();
        audioSource.PlayOneShot(closing, 0.7F);

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
        yield return null;
    }
}