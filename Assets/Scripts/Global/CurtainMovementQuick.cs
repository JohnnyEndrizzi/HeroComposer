using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurtainMovementQuick : MonoBehaviour
{  
    //Open/Close curtains without fanciness (long applause, lights, delay)

    public Animator curtainAnim;
    public AudioClip opening;

    private AudioSource audioSource;

    void Start()
    {
        curtainAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<AudioSource>().PlayOneShot(opening, 0.7F);

        StartCoroutine(OpenAnimation());
    }

    private IEnumerator OpenAnimation() //Open curtains
    { 
        curtainAnim.Play("CurtainsOpen");
        
        //volume fade out
        float startVolume = audioSource.volume; 
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }
    }

    private IEnumerator closeAnimation(string sceneNew) //Close curtains and switch scene once complete
    {
        audioSource.volume = 1;
        GetComponent<AudioSource>().PlayOneShot(opening, 0.7F);

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
        yield return null;
    }    
    
    public void closeCurtains(string sceneNew)  //Run close curtains animation
    {        
        StartCoroutine(closeAnimation(sceneNew));
    }        
}