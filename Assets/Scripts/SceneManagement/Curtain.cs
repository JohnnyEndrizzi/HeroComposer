using UnityEngine;


public class Curtain : UILayer
{ 
    //Animator
    private Animator animator;

    //Audio
    private AudioSource audioSource;
    private AudioClip curtainOpening;
    private AudioClip curtainClosing;

    //Singleton instance
    public static Curtain Instance { get; private set; }

    //Called before start
    void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            //Save singleton instance
            Instance = this;
            //Dont destroy between scenes
            DontDestroyOnLoad(gameObject);
        //Destroy this instance
        }else{
            Destroy(this.gameObject);
        }
    }

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