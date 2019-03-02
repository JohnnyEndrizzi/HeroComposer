using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour {

    //Door name
    public string doorName;

    //Animator
    private Animator animator;

    //Audio
    private AudioSource audioSource;
    private AudioClip doorOpening;
    private AudioClip doorClosing;

    //Used to enable/disable a door
    public bool doorEnabled { get; set; }

    //Called every frame
    void Update()
    {
               
    }

    // Use this for initialization
    void Start () {
        //Enable door
        doorEnabled = true;

        //Get animator
        animator = GetComponent<Animator>();
        
        //Get sound effects
        audioSource = GetComponent<AudioSource>();
        doorOpening = (AudioClip)Resources.Load("SoundEffects/menu_door_open");
        doorClosing = (AudioClip)Resources.Load("SoundEffects/menu_door_close");
    }

    //This function is called when the user clicks on a door
    protected void OnMouseDown()
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
        if (doorEnabled) { 
           SceneManagerWrapper.Instance.SwitchScene(doorName);
        }
    }

    //This function is called when the mouse hovers over a door
    void OnMouseEnter()
    {
        if (doorEnabled)
        {
            OpenDoor();
        }
    }

    //This function is called while the mouse hovers over a door
    private void OnMouseOver()
    {
        if (doorEnabled == true && animator.GetBool("IsOpen")==false)
        {
            OpenDoor();
        }
    }

    //This function is called when the mouse stops hovering over a door
    void OnMouseExit()
    {
        if (doorEnabled)
        {
            CloseDoor();
        }
    }

    //Play door opening animation
    public void OpenDoor() 
    {
        //Play sound effect
        audioSource.Pause();
        audioSource.PlayOneShot(doorOpening, 0.7F);

        //Play animation
        animator.SetBool("IsOpen", true);
    }

    //Play door closing animation
    public void CloseDoor()
    {
        //Play sound effect
        audioSource.Pause();
        audioSource.PlayOneShot(doorClosing, 0.7F);

        //Play animation
        animator.SetBool("IsOpen", false);
    }
}
