using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenLoader : MonoBehaviour
{
    private bool loadScene = false;

    public GameObject menu;

    [SerializeField]
    private int scene;

    [SerializeField]
    private Text loadingText;

    [SerializeField]
    private float delay = 1f;

    public void test()
    {
        loadScene = true;
        //loadingText.text = "Loading...";

        menu.SetActive(false);

        StartCoroutine(LoadNewScene());



        if (loadScene == true)
        {
            //loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

    // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);

        float elapsedTime = 0;
        float currentVolume = AudioListener.volume;

        while (elapsedTime < delay)
        {
            elapsedTime += Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(currentVolume, 0, elapsedTime / delay);
            yield return null;
        }
        
        AsyncOperation async = Application.LoadLevelAsync(scene);
        while (!async.isDone)
        {
            yield return null;

        }
    }
}