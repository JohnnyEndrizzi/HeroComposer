using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurtainMovement : MonoBehaviour
{
    public Animator curtainAnim;
    public AudioClip applauseSFX;

    public bool end = false;
    private bool delayLock = false;

    public GameObject spotlight1;
    public GameObject spotlight2;

    private SpriteRenderer MenuRender;
    private Image MenuRender2;
    private AudioSource audioSource;


    /* The following coroutine plays the opening animation for the curtains upon starting a level */
    private IEnumerator delayedOpenAnimation()
    {
        delayLock = true;

        /* The spotlights and applause will be active for 3 seconds, so the coroutine will wait */
        yield return new WaitForSeconds(3.0f);

        /* The spotlights disappear upon the curtains opening */
        curtainAnim.Play("CurtainsOpen");
        Destroy(spotlight1);
        Destroy(spotlight2);

        yield return new WaitForSeconds(2.0f);

        /* Hide the opened curtains after the animation */  
        yield return StartCoroutine(Grow(2));
        
        //yield return new WaitForSeconds(1.0f);

        /* After 3 seconds, start playing the applause sound */
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }

        delayLock = false;
    }
    
    public void ClosingTime()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        //StartCoroutine(closeAnimation());
    }

    /* The following coroutine plays the closing animation for the curtains upon finishing a level */
    private IEnumerator closeAnimation()
    {
        delayLock = true;

        /* Show the opened curtains before the animation */
        yield return StartCoroutine(Grow(0.5f));

        /* Plays the apllause sound during the closing animation */
        audioSource.volume = 1;
        audioSource.PlayOneShot(applauseSFX, 0.7F);

        /* Closes the curtains */
        curtainAnim.Play("CurtainsClose");

        end = true;
        delayLock = false;

        yield return StartCoroutine(Grow(1.0f));

        SceneManager.LoadScene("Menu", LoadSceneMode.Single);

        yield return null;
    }

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

    /* Use this for initialization */
    void Start()
    {
        RectTransform rectTransform = this.GetComponent<RectTransform>();
        RectTransform rectTransformCanvas = GameObject.Find("Healthbar").GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransformCanvas.rect.width, rectTransformCanvas.rect.height);

        delayLock = false;
        curtainAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(applauseSFX, 0.7F);

        MenuRender = GetComponent<SpriteRenderer>();
        MenuRender2 = GetComponent<Image>();

        //end = true;
    }

    /* Update is called once per frame */
    void Update()
    {
        if (!delayLock && !end)
        {
            /* Depending if the song is done or not, the corresponding coroutine will be called */
            if (GameLogic.songDone)
            {
                //StartCoroutine(closeAnimation());
            }
            else
            {
                StartCoroutine(delayedOpenAnimation());
            }
        }
        if (delayLock)
        {
            MenuRender2.sprite = MenuRender.sprite;
        }
    }
}