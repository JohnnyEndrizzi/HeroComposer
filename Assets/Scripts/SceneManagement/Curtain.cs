using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Curtain : MonoBehaviour
{ 
    //Animator
    private Animator animator;

    //Audio
    private AudioSource audioSource;
    private AudioClip curtainOpening;
    private AudioClip curtainClosing;
    
    //Use this for initialization 
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        curtainOpening = (AudioClip)Resources.Load("SoundEffects/menu_curtain_open");
        curtainClosing = (AudioClip)Resources.Load("SoundEffects/menu_curtain_close");                
    }

    //Open curtain
    public void Open()
    {
        audioSource.PlayOneShot(curtainOpening, 0.7F);
        animator.SetBool("IsOpen", true);
    }

    //Close curtain
    public void Close()
    {
        audioSource.PlayOneShot(curtainClosing, 0.7f);
        animator.SetBool("IsOpen", false);
    }
}