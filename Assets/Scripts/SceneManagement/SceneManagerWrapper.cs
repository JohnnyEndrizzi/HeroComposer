using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerWrapper : DDOL {

    private string previousScene; 

    public void SwitchScene(string scene)
    {
        previousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    public void LoadPreviousScene()
    {
        SwitchScene(previousScene);
    }
}