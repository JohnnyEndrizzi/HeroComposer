using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurtainMovementQuick : MonoBehaviour
{  
    //Open/Close curtains without fanciness (long applause, lights, delay)

    public Animator curtainAnim;
    private AudioClip opening;
    private AudioClip closing;

    private SpriteRenderer MenuRender;
    private Image MenuRender2;
    private AudioSource audioSource;

    private bool isAnim = false;
    

    void Start()
    {             
        curtainAnim = GetComponent<Animator>();
        
        opening = (AudioClip)Resources.Load("SoundEffects/menu_curtain_open");
        closing = (AudioClip)Resources.Load("SoundEffects/menu_curtain_close");              

        MenuRender = GetComponent<SpriteRenderer>();
        MenuRender2 = GetComponent<Image>();

        
        
         if (LastScene.instance.prevScene.Equals("-1")) //Game Load
        {
            StartCoroutine(SplashScreen());
        }
        else
        {
            StartCoroutine(OpenAnimation());            
        }        
    }
    
    /* Update is called once per frame */
    void Update()
    {
        if (isAnim) //converts animation sprites to UI images during animation to make use of canvas ordering
        {
            MenuRender2.sprite = MenuRender.sprite;
        }        
    }

    private IEnumerator SplashScreen() //Splash Screen
    {
        Image Logo = this.transform.GetChild(0).GetComponentInChildren<Image>(); //Why does GetComponentInChildren search the parent first??
        Text txt = GetComponentInChildren<Text>();

        yield return new WaitForSeconds(1.0f);
               
        //fade in logo
        Logo.enabled = true;
        yield return StartCoroutine(ImageFader(1f, Logo, 0,1));

        yield return new WaitForSeconds(1.0f);

        //fade out logo
        yield return StartCoroutine(ImageFader(1f, Logo, 1,0));

        yield return new WaitForSeconds(0.1f);

        //pulse "Press to Continue" Text       
        txt.enabled = true;

        StartCoroutine(FadePulse(txt));
        yield return StartCoroutine(WaitForKeyDown()); //Wait for Input
          
        //End text pulse
        txt.enabled = false;

        yield return StartCoroutine(OpenAnimation());

        try { GameObject.Find("ScreenLoader").GetComponent<ScreenLoader>().activation(); } catch { }
        try { GameObject.Find("GObtn").transform.SetAsLastSibling(); } catch { }

        yield return null;
    }

    IEnumerator FadePulse(Text txt)
    {
        while (txt.IsActive())
        {
            yield return StartCoroutine(TextFader(1f, txt, 0, 1));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(TextFader(1f, txt, 1, 0));
            yield return new WaitForSeconds(0.5f);
        }
        yield return null;
    }

    IEnumerator ImageFader(float t, Image i, float fade1, float fade2) //Controls the Image fading in or out for Image 
    { 
        //0 to invisible, 1 to visible
        //fade1 = start alpha, fade2 = end alpha   

        i.color = new Color(i.color.r, i.color.g, i.color.b, fade1);
        while (i.color.a < fade1+0.1 && i.color.a > fade2-0.1 || i.color.a < fade2 + 0.1 && i.color.a > fade1 - 0.1)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t) * fade1 + (Time.deltaTime / t) * fade2);
            yield return null;
        }
    }

    IEnumerator TextFader(float t, Text i, float fade1, float fade2) //Controls the text fading in or out for text 
    {
        //0 to invisible, 1 to visible - fader2 is opposite of fader
        //fade1 = start alpha, fade2 = end alpha

        i.color = new Color(i.color.r, i.color.g, i.color.b, fade1);
        
        while (i.color.a < fade1 + 0.1 && i.color.a > fade2 - 0.1 || i.color.a < fade2 + 0.1 && i.color.a > fade1 - 0.1)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t) * fade1 + (Time.deltaTime / t) * fade2);
            try {
                Outline O1 = i.transform.GetComponents<Outline>()[0];
                O1.effectColor = new Color(O1.effectColor.r, O1.effectColor.g, O1.effectColor.b, i.color.a);
                try{
                    Outline O2 = i.transform.GetComponents<Outline>()[1];
                    O2.effectColor = new Color(O2.effectColor.r, O2.effectColor.g, O2.effectColor.b, i.color.a);
                }
            catch { }
        } catch { }           
            yield return null;
        }
    }

    IEnumerator WaitForKeyDown()
    {
        while (!Input.anyKeyDown)
            yield return null;
    }

    private IEnumerator OpenAnimation() //Open curtains
    {
        isAnim = true;

        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(opening, 0.7F);

        curtainAnim.Play("CurtainsOpen");
        
        //volume fade out
        float startVolume = audioSource.volume; 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }
        isAnim = false;

        try { UnFreezeDoors(); } catch { } //Enable Buttons
        yield return null;
    }

    private IEnumerator CloseAnimation(string sceneNew) //Close curtains and switch scene once complete
    {
        isAnim = true;

        audioSource.volume = 1;
        GetComponent<AudioSource>().PlayOneShot(closing, 0.7F);

        curtainAnim.Play("CurtainsClose");

        //volume fade out
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {            
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneNew,LoadSceneMode.Single);

        isAnim = false;
        yield return null;
    }    
    
    public void CloseCurtains(string sceneNew)  //Run close curtains animation
    {        
        StartCoroutine(CloseAnimation(sceneNew));
    }

    private void UnFreezeDoors()
    {
        GameObject.Find("PlayDoorClose").GetComponent<AudioSource>().mute = false;
        GameObject.Find("InventoryDoorClose").GetComponent<AudioSource>().mute = false;
        GameObject.Find("AuditionDoorClose").GetComponent<AudioSource>().mute = false;
        GameObject.Find("ShopDoorClose").GetComponent<AudioSource>().mute = false;
        GameObject.Find("RehersalDoorClose").GetComponent<AudioSource>().mute = false;

        GameObject.Find("PlayDoorClose").GetComponent<DoorHandler>().clearToProceed = true;
        GameObject.Find("InventoryDoorClose").GetComponent<DoorHandler>().clearToProceed = true;
        GameObject.Find("AuditionDoorClose").GetComponent<DoorHandler>().clearToProceed = true;
        GameObject.Find("ShopDoorClose").GetComponent<DoorHandler>().clearToProceed = true;
        GameObject.Find("RehersalDoorClose").GetComponent<DoorHandler>().clearToProceed = true;
    }
}