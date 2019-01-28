using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BGmusic : MonoBehaviour
{
    //Persistant script to play background music where applicable
    public static BGmusic instance = null;

    //Audio
    private AudioSource audioSource;
    private AudioClip menuBG1;
    public bool soundScene = true;

    private bool isFade = false;

    //Persist and prevent multiple instances
    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        menuBG1 = (AudioClip)Resources.Load("SoundEffects/menu_background_music");
    }

    private void Update()
    {
        if (!audioSource.isPlaying && soundScene) //move to separate sound file
        {
            audioSource.PlayOneShot(menuBG1, 0.3f);
        }
    }

    /* Currently using depreciated code, this will be replaced by current code in future
    * Acts the same as Start(), but runs whenever a new scene is loaded
    * Both start and awake will only run once for a persistant object */
    private void OnLevelWasLoaded()
    {
        //Debug.Log("loaded BGMusic");
        if (SceneManager.GetActiveScene().name.Equals("main") || SceneManager.GetActiveScene().name.Equals("StartMenu"))
        {
            soundScene = false;
            StartCoroutine(fadeOut());            
        }
        else
        {
            soundScene = true;
        }
    }


    private IEnumerator fadeOut() 
    {
        isFade = true;
                
        //volume fade out
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }
        audioSource.Pause();
        audioSource.volume = startVolume;
        isFade = false;
        yield return null;
    }
}