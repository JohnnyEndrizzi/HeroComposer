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
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        RectTransform rectTransformCanvas = this.transform.parent.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransformCanvas.rect.width, rectTransformCanvas.rect.height);
             
        curtainAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        opening = (AudioClip)Resources.Load("SoundEffects/menu_curtain_open");
        closing = (AudioClip)Resources.Load("SoundEffects/menu_curtain_close");
        audioSource.PlayOneShot(opening, 0.7F);


        MenuRender = GetComponent<SpriteRenderer>();
        MenuRender2 = GetComponent<Image>();

        StartCoroutine(OpenAnimation());
    }

    /* Update is called once per frame */
    void Update()
    {
        if (isAnim) //converts animation sprites to UI images during animation to make use of canvas ordering
        {
            MenuRender2.sprite = MenuRender.sprite;
        }
    }

    private IEnumerator OpenAnimation() //Open curtains
    {
        isAnim = true;

        curtainAnim.Play("CurtainsOpen");
        
        //volume fade out
        float startVolume = audioSource.volume; 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }
        isAnim = false;
        yield return null;
    }

    private IEnumerator closeAnimation(string sceneNew) //Close curtains and switch scene once complete
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
    
    public void closeCurtains(string sceneNew)  //Run close curtains animation
    {        
        StartCoroutine(closeAnimation(sceneNew));
    }        
}