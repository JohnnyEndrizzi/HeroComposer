using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : MonoBehaviour
{
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
        GameManager.Instance.IsInputEnabled = false;
        Curtain.Instance.Close();
        yield return new WaitForSeconds(2.0f);
        SwitchScene(scene);
        if (openCurtainsAfter)
        {
            Curtain.Instance.Open();
        }
    }

    //Switch to new scene
    private void SwitchScene(string scene)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    public void LoadPreviousScreenWithCurtains()
    { 
        LoadPreviousScene();
    }

    //Load previous scene
    private void LoadPreviousScene()
    {
        SwitchSceneWithCurtains(previousScene, true);
    }
}