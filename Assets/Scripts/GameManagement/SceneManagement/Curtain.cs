using UnityEngine;
using System.Collections;

public class Curtain : UILayer
{
    //Spotlights
    public GameObject spotlightPrefab;
    private GameObject spotlight1;
    private GameObject spotlight2;

    //Animator
    private Animator animator;

    //Audio
    private AudioSource audioSource;
    private AudioClip curtainOpening;
    private AudioClip curtainClosing;
    private AudioClip applauseSFX;

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
        applauseSFX = (AudioClip)Resources.Load("Songs/SFX/Applause");        
    }

    //Enable input when curtain is finished opening
    private void EnableInput()
    {
        GameManager.Instance.IsInputEnabled = true;
    }

    //Disable input when curtain is closing/closed
    private void DisableInput()
    {
        GameManager.Instance.IsInputEnabled = false;
    }

    //Open curtain
    public void Open()
    {
        animator.SetBool("IsOpen", true);
        audioSource.PlayOneShot(curtainOpening, 0.7F);
    }

    //Close curtain
    public void Close()
    {
        animator.SetBool("IsOpen", false);
        audioSource.PlayOneShot(curtainClosing, 0.7f);
    }
    /// <summary>
    /// //
    /// </summary>

    //Open curtain with Intro effects
    public void OpenIntro()
    {
        audioSource.PlayOneShot(applauseSFX, 0.7F);
    }

    public void SpotlightCreate()
    {
        Vector3 stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        spotlight1 = Instantiate(spotlightPrefab, new Vector3(-20f, -20f, 1f), transform.rotation);
        spotlight2 = Instantiate(spotlightPrefab, new Vector3(stageDimensions.x+20, -20f, 1f), transform.rotation);
        spotlight2.GetComponent<SpotlightBehavior>().right_side = true;
    }

    public void SpotlightDestroy()
    {
        Destroy(spotlight1);
        Destroy(spotlight2);
    }

    //Open curtain then Grow with into effects
    public void OpenThenGrow()
    {
        StartCoroutine(OpenThenGrowIE());
    }
    private IEnumerator OpenThenGrowIE()
    {
        //Start spotlights
        SpotlightCreate();
        //Wait for spotlights
        yield return new WaitForSeconds(3.0f);
        SpotlightDestroy();
        Open();
        yield return new WaitForSeconds(2.0f);
        yield return StartCoroutine(Grow(2));                
    }

    //Shrin then close curtains
    public void ShrinkThenClose()
    {
        StartCoroutine(ShrinkThenCloseIE());   
    }
    private IEnumerator ShrinkThenCloseIE()
    {
        yield return StartCoroutine(Grow(0.5f));        
        Close();
        yield return null;
    }

    //TODO used?
    //Grow Curtains
    public void Grow()
    {
        StartCoroutine(Grow(2));
    }

    //Shrink Curtains
    public void Shrink()
    {
        StartCoroutine(Grow(0.5f));
    }

    //Grow and shrink curtains
    private IEnumerator Grow(float scale)
    {
        float LerpTime = 3f;
        float currentLerpTime = 0;
        Vector3 local = transform.localScale;

        while (currentLerpTime < LerpTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, local * scale, (currentLerpTime / LerpTime));
            currentLerpTime += Time.deltaTime;
            yield return null;
        }
    }
}