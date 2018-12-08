using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainMovement : MonoBehaviour
{
    public Animator curtainAnim;
    public AudioClip applauseSFX;
    public SpriteRenderer MenuRender;
    public bool end = false;

    public GameObject spotlight1;
    public GameObject spotlight2;

    private AudioSource audioSource;
    private bool delayLock = false;

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

        yield return new WaitForSeconds(3.0f);

        /* After 3 seconds, start playing the applause sound */
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }

        /* Hide the opened curtains after the animation */
        GetComponent<SpriteRenderer>().enabled = false;

        delayLock = false;
    }

    /* The following coroutine plays the closing animation for the curtains upon finishing a level */
    private IEnumerator closeAnimation()
    {
        delayLock = true;

        GetComponent<SpriteRenderer>().enabled = true;

        /* Plays the apllause sound during the closing animation */
        audioSource.volume = 1;
        GetComponent<AudioSource>().PlayOneShot(applauseSFX, 0.7F);

        /* Closes the curtains */
        curtainAnim.Play("CurtainsClose");

        end = true;
        delayLock = false;

        yield return null;
    }

    /* Use this for initialization */
    void Start ()
    {
        delayLock = false;
        curtainAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().PlayOneShot(applauseSFX, 0.7F);
    }

    /* Update is called once per frame */
    void Update ()
    {
        if (!delayLock && !end)
        {
            /* Depending if the song is done or not, the corresponding coroutine will be called */
            if (GameLogic.songDone)
            {
                StartCoroutine(closeAnimation());
            }
            else
            {
                StartCoroutine(delayedOpenAnimation());
            }
        }
    }
}
