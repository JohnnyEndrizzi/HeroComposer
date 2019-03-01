using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    //Door name
    public string doorName;

    //Animator
    public Animator animator;

    //Audio
    private AudioSource audioSource;
    private AudioClip doorOpening;
    private AudioClip doorClosing;

    // Use this for initialization
    void Start () {
        //Load audio files
        AudioListener.volume = 1;
        audioSource = GetComponent<AudioSource>();
        audioSource.mute = true;
        doorOpening = (AudioClip)Resources.Load("SoundEffects/menu_door_open");
        doorClosing = (AudioClip)Resources.Load("SoundEffects/menu_door_close");
    }
	
	// Update is called once per frame
	void Update () {
    }

    //This function is called when the user clicks on a door
    void OnClick()
    {
        /* Functional Requirement
        * ID: 8.1.1-8
        * Description: The system must display the inventory screen.
        * 
        * Functional Requirement
        * ID: 8.1.1-20
        * Description: The system must preserve insight about character and party customization between level completions and menu selections.
        * 
        * The scene corresponding to the door will be loaded, as long as its not the play door (which is covered in the function below) */
        if(doorName != "Play")
        {
            
        }
        else
        {

        }
    }

    //This function is called when the mouse hovers over a door
    void OnMouseEnter()
    {
        StartCoroutine(OpenDoor());
    }

    //This function is called when the mouse stops hovering over a door
    void OnMouseExit()
    {
        StartCoroutine(CloseDoor());
    }

    //Play door opening animation
    IEnumerator OpenDoor() 
    {
        //Play sound effect
        audioSource.Pause();
        audioSource.PlayOneShot(doorOpening, 0.7F);

        //Play animation
        animator.SetBool("IsOpen", true);

        yield return null;
    }

    //Play door closing animation
    IEnumerator CloseDoor()
    {
        //Play sound effect
        audioSource.Pause();
        audioSource.PlayOneShot(doorClosing, 0.7F);

        //Play animation
        animator.SetBool("IsOpen", false);

        yield return null;
    }
}
