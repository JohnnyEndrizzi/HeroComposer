using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : DDOL {

    private Scene previousScene; 

    public void SwitchScene(string scene)
    {
        previousScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene);
    }

    public void LoadPreviousScene()
    {
        SwitchScene(previousScene.name);
    }
}