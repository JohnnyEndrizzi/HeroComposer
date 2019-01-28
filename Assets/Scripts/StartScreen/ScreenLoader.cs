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

    private bool activated = false; //is button enabled (enables once curtains open)

    AsyncOperation async;

    public void activation()
    {
        activated = true;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(TextFader(1f, loadingText, 1, 0));
        StartCoroutine(TimerNext());
    }

    IEnumerator TimerNext()
    {
        yield return StartCoroutine(LoadNewScene());
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(AudioFadeOut());
        yield return StartCoroutine(TextFader(0.5f, loadingText, 1, 0));

        async.allowSceneActivation = true;
        MenuSceneSwitch("Menu");

        yield return null;
    }

    IEnumerator LoadNewScene()
    {
        async = SceneManager.LoadSceneAsync("Menu");
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        yield return null;
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

    IEnumerator TextFader(float t, Text i, float fade1, float fade2)
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

    private void MenuSceneSwitch(string sceneNew)
    {
        GameObject.Find("sceneSwitcher").GetComponent<SceneSwitcher>().sceneSwitchCurtains(sceneNew);
    }    
}