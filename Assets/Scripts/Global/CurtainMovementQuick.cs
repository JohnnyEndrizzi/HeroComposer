using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurtainMovementQuick : MonoBehaviour {
    public Animator curtainAnim;
    public AudioClip opening;

    private AudioSource audioSource;

    private IEnumerator OpenAnimation(){
        curtainAnim.Play("CurtainsOpen");

        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }
    }

    private IEnumerator closeAnimation(string sceneNew){
        audioSource.volume = 1;
        GetComponent<AudioSource>().PlayOneShot(opening, 0.7F);

        curtainAnim.Play("CurtainsClose");

        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {            
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(sceneNew,LoadSceneMode.Single);
        yield return null;
    }
        
    void Start (){                
        curtainAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().PlayOneShot(opening, 0.7F);

        StartCoroutine(OpenAnimation());
    }

    public void closeCurtains(string sceneNew){        
        StartCoroutine(closeAnimation(sceneNew));
    }        
}