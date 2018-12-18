using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenLoader : MonoBehaviour
{
    [SerializeField]
    private Button startBtn;
    [SerializeField]
    private Text loadingText;
    [SerializeField]
    private float delay = 1f; //audio fade out time

    private bool clicked = false; // track user input
    private bool activated = false; //is button enabled (enables once curtains open)

    AsyncOperation async;

    public void Start()
    {
        //StartCoroutine(Next());  
    }

    public void Click()
    {
        if (activated)
        {
            try { GameObject.Find("GObtn").transform.SetAsFirstSibling(); } catch { }
            StartCoroutine(Next2());


            clicked = true;
            Debug.Log("C " + clicked);
        }
    }

    IEnumerator Next2()
    {
        yield return StartCoroutine(AudioFadeOut()); //fade out audio before transition
               
        MenuSceneSwitch("Menu"); //switch scenes
    }

    public void activation()
    {
        activated = true;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(TextFader(1f, loadingText, 1, 0)); //disables "Loading..."
    }

    //Broken/disabled Async Code
    IEnumerator Next()
    {
        yield return StartCoroutine(LoadNewScene()); //loads next scene in background
        yield return StartCoroutine(TextFader(0.5f, loadingText, 1, 0)); //disables "Loading..."
        startBtn.GetComponent<Button>().enabled = true;

        yield return StartCoroutine(Clicked());      //waits until the button has been clicked once
        yield return StartCoroutine(AudioFadeOut()); //fade out audio before transition

        async.allowSceneActivation = true; //load async scene?
        MenuSceneSwitch("Menu"); //switch scenes?

        yield return null;
    }    

    IEnumerator Clicked()
    {
        while (!clicked)
            yield return null;
    }
    
    // The coroutine runs on its own at the same time as Update()
    IEnumerator LoadNewScene()
    {
        StartCoroutine(TextFader(0.5f, loadingText, 0, 1)); //enables "Loading..."
        startBtn.GetComponent<Button>().enabled = false;
                
        async = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
        async.allowSceneActivation = false; //stops async progress at 0.9 - doesnt run scripts

        while (async.progress < 0.9f)  
        {
            yield return null;
        }
    }
    //Broken/disabled Async Code mostly ends here
    
    IEnumerator TextFader(float t, Text i, float fade1, float fade2) //Controls the text fading in or out for text 
    {
        //0 to invisible, 1 to visible - fader2 is opposite of fader
        //fade1 = start alpha, fade2 = end alpha

        i.color = new Color(i.color.r, i.color.g, i.color.b, fade1);

        while (i.color.a < fade1 + 0.1 && i.color.a > fade2 - 0.1 || i.color.a < fade2 + 0.1 && i.color.a > fade1 - 0.1)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t) * fade1 + (Time.deltaTime / t) * fade2);
            yield return null;
        }
    }

    IEnumerator AudioFadeOut()
    {
        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
    }

    private void MenuSceneSwitch(string sceneNew) //SceneSwitch with curtains
    {
        GameObject.Find("sceneSwitcher").GetComponent<SceneSwitcher>().sceneSwitchCurtains(sceneNew);
    }
}