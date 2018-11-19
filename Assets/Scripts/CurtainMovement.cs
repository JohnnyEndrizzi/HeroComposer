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

    private IEnumerator delayedOpenAnimation()
    {
        delayLock = true;

        yield return new WaitForSeconds(3.0f);

        curtainAnim.Play("CurtainsOpen");
        Destroy(spotlight1);
        Destroy(spotlight2);

        yield return new WaitForSeconds(3.0f);

        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }

        GetComponent<SpriteRenderer>().enabled = false;

        delayLock = false;
    }

    private IEnumerator closeAnimation()
    {
        delayLock = true;

        GetComponent<SpriteRenderer>().enabled = true;

        audioSource.volume = 1;
        GetComponent<AudioSource>().PlayOneShot(applauseSFX, 0.7F);

        curtainAnim.Play("CurtainsClose");

        end = true;
        delayLock = false;

        yield return null;
    }

    // Use this for initialization
    void Start ()
    {
        delayLock = false;
        curtainAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().PlayOneShot(applauseSFX, 0.7F);
    }

    // Update is called once per frame
    void Update ()
    {
        if (!delayLock && !end)
        {
            Debug.Log(GameLogic.songDone);
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
