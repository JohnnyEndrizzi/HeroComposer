using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour {

    GameObject PauseMenu;
    GameObject VolumeMenu;
    GameObject ControlsMenu;
    GameObject HowToPlayMenu;
    GameObject GotoMM;
    GameObject ExitMenu1;
    GameObject ExitMenu2;

    private AudioSource audioSource;
    private AudioSource audioSource2;
    private AudioClip confirm;
    private AudioClip back;
    private AudioClip crowd;

    //Audio Volumes
    private float volC = 0.5F;
    private float volB = 0.5F;
    private float volA = 0.3F;

    //Temporary Keybindings
    KeyCode Char1Code; //Top
    KeyCode Char2Code; //Left 
    KeyCode Char3Code; //Down
    KeyCode Char4Code; //Right

    KeyCode AtkCode;
    KeyCode DefCode;
    KeyCode MgcCode;
    char UserInput;

    //Backup Audio
    float MusicLvl;
    float SFXLvl;
    float BGLvl;

    // Use this for initialization
    void Start () {
        PauseMenu = GameObject.Find("SettingsUI");
        VolumeMenu = GameObject.Find("VolumeUI");
        ControlsMenu = GameObject.Find("ControlUI");
        HowToPlayMenu = GameObject.Find("HowToPlayUI");
        GotoMM = GameObject.Find("MainMenuUI");
        ExitMenu1 = GameObject.Find("QuitConfirmUI");
        ExitMenu2 = GameObject.Find("QuitConfirmUI2");

        audioSource = GetComponent<AudioSource>(); //for non-looping audio
        audioSource2 = GameObject.Find("CrowdAudio").GetComponent<AudioSource>(); //for looping audio
        confirm = (AudioClip)Resources.Load("SoundEffects/inventory_pick_up_item");
        back = (AudioClip)Resources.Load("SoundEffects/inventory_place_item_back");
        //crowd = (AudioClip)Resources.Load("SoundEffects/crowd_murmur");
    }
        
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.GetComponent<Canvas>().enabled)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //Pausing/Unpausing Game
    public void PauseGame()
    {
        PauseMenu.GetComponent<Canvas>().enabled = true;
        audioSource.PlayOneShot(back, volB);
        //audioSource2.PlayOneShot(crowd, volA);
    }
    public void UnPauseGame()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        audioSource2.Stop();
        audioSource.PlayOneShot(back, volB);        
    }

    //SubMenu Selections
    public void VolumeSelect()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        VolumeMenu.GetComponent<Canvas>().enabled = true;
        audioSource.PlayOneShot(confirm, volC);
        //TODO save vol levels to backup
    }
    public void ControlSelect()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        ControlsMenu.GetComponent<Canvas>().enabled = true;
        audioSource.PlayOneShot(confirm, volC);
        //TODO load control listings
    }
    public void HowToPlaySelect()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        HowToPlayMenu.GetComponent<Canvas>().enabled = true;
        audioSource.PlayOneShot(confirm, volC);
    }
    public void MMSelect()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        GotoMM.GetComponent<Canvas>().enabled = true;
        audioSource.PlayOneShot(confirm, volC);
    }
    public void QuitSelect()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        ExitMenu1.GetComponent<Canvas>().enabled = true;
        audioSource.PlayOneShot(confirm, volC);
    }

    //Quitting or MainMenu
    public void QuitConfirm()
    {
        ExitMenu1.GetComponent<Canvas>().enabled = false;
        ExitMenu2.GetComponent<Canvas>().enabled = true;
        audioSource.PlayOneShot(confirm, volC);
    }
    public void QuitGame()
    {
        PauseMenu.GetComponent<Canvas>().enabled = false;
        audioSource.PlayOneShot(confirm, volC);
        Application.Quit();
    }
    public void MainMenuConfirm()
    {
        GotoMM.GetComponent<Canvas>().enabled = false;
        audioSource.PlayOneShot(confirm, volC);
        GameObject.Find("sceneSwitcher").GetComponent<SceneSwitcher>().sceneSwitchHiddenCurtains("Menu");
    }

    //Confirming/Back Buttons
    public void ConfirmChanges()
    {
        PauseMenu.GetComponent<Canvas>().enabled = true;
        VolumeMenu.GetComponent<Canvas>().enabled = false;
        ControlsMenu.GetComponent<Canvas>().enabled = false;
        audioSource.PlayOneShot(confirm, volC);
        //TODO save keybindings         
    }
    public void BackButton()
    {
        PauseMenu.GetComponent<Canvas>().enabled = true;
        VolumeMenu.GetComponent<Canvas>().enabled = false;
        ControlsMenu.GetComponent<Canvas>().enabled = false;
        HowToPlayMenu.GetComponent<Canvas>().enabled = false;
        GotoMM.GetComponent<Canvas>().enabled = false;
        ExitMenu1.GetComponent<Canvas>().enabled = false;
        ExitMenu2.GetComponent<Canvas>().enabled = false;
        audioSource.PlayOneShot(back, volB);
        //TODO Load volume BUP
    }

    //Control Style Buttons
    public void Style1()
    {
        audioSource.PlayOneShot(confirm, volC);
        //TODO change visual keys
    }
    public void Style2()
    {
        audioSource.PlayOneShot(confirm, volC);
        //TODO change visual keys
    }
    public void Style3()
    {
        audioSource.PlayOneShot(confirm, volC);
        //TODO change visual keys
    }


    //WIP//
    //Control Keybindings 

    //Volume Sliders

    public void WaitForKey(int buttonLocation)
    {
        StartCoroutine(WaitingForKey());
        SetKey(buttonLocation);
    }

    IEnumerator WaitingForKey()
    {
        //while (!Input.anyKeyDown) { }
        //UserInput = Input.inputString[0];

        yield return null;
    }

    private void SetKey(int buttonLocation)
    {
        //UserInput
    }

    public void LoadKeys()
    {
        KeyCode Char1Code; //Top
        KeyCode Char2Code; //Left 
        KeyCode Char3Code; //Down
        KeyCode Char4Code; //Right

        KeyCode AtkCode;
        KeyCode DefCode;
        KeyCode MgcCode;
    }
    public void SaveKeys()
    {

    }

    public void LoadAudioLevels()
    {
        //MusicLvl;
        //SFXLvl;
        //BGLvl;
    }
    public void SaveAudioLevels()
    {

    }

    public void GetInput()
    {
        //Input.GetKey
    }

}