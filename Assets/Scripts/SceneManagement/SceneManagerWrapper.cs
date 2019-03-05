using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : MonoBehaviour
{
    //Singleton instance
    public static SceneManagerWrapper Instance { get; private set; }

    //Previous scene
    private string previousScene;

    //Called before start
    void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            //Save singleton instance
            Instance = this;
            //Dont destroy between scenes
            DontDestroyOnLoad(gameObject);
        //Destroy this instance
        }else{
            Destroy(this.gameObject);
        }
    }

    //Switch scene by closing/opening curtains
    public void SwitchSceneWithCurtains(string scene, bool openCurtainsAfter)
    {
        StartCoroutine(SwitchSceneWithCurtainsCoroutine(scene, openCurtainsAfter));
    }
    //Coroutine to swtich scenes with curtain transition
    IEnumerator SwitchSceneWithCurtainsCoroutine(string scene, bool openCurtainsAfter)
    {
        Curtain.Instance.Close();
        yield return new WaitForSeconds(2.0f);
        SwitchScene(scene);
        if (openCurtainsAfter)
        {
            Curtain.Instance.Open();
        }
    }

    //Switch to new scene
    public void SwitchScene(string scene)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    //Load previous scene
    public void LoadPreviousScene()
    {
        SwitchScene(previousScene);
    }

    //Switch to new scene without saving previousScene - allows the back button to ignore certain scenes
    public void SwitchSceneHidden(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}