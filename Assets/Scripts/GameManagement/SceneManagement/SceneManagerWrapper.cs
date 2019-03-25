using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerWrapper : MonoBehaviour
{
    //Loading bar
    [SerializeField]
    private LoadingBar loadingBar; 

    //Previous scene
    private string previousScene;

    //Switch scene by closing/opening curtains
    public void SwitchSceneWithCurtains(string scene, bool openCurtainsAfter)
    {
        StartCoroutine(SwitchSceneWithCurtainsCoroutine(scene, openCurtainsAfter));
    }
    //Coroutine to switch scenes with curtain transition
    IEnumerator SwitchSceneWithCurtainsCoroutine(string scene, bool openCurtainsAfter)
    {
        //Disable input while switching scenes
        GameManager.Instance.IsInputEnabled = false;
        //Close the curtain
        Curtain.Instance.Close();
        yield return new WaitForSeconds(2.0f);
        //Clear all UI elements except for the curtain
        if (UIContainer.Instance != null)
        {
            UIContainer.Instance.ClearUILayers();
        }
        //Switch scene (async) 
        previousScene = GetCurrentScene();
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        //Loading bar
        loadingBar.Show();
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/.9f);
            Debug.Log(progress);
            loadingBar.UpdateProgress(progress);
            yield return null;
        }
        loadingBar.Hide();
        //Open curtains after switching scene 
        if (openCurtainsAfter)
        {
            Curtain.Instance.Open();
        }
        //Delay to allow curtains to open
        yield return new WaitForSeconds(2.0f);
    }

    //Load previous screen using curtains
    public void LoadPreviousScreenWithCurtains()
    {
        LoadPreviousScene();
    }

    //Load previous scene
    private void LoadPreviousScene()
    {
        SwitchSceneWithCurtains(previousScene, true);
    }

    //Return current scene
    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}