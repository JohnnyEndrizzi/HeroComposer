﻿using System.Collections;
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
        if (UIContainer.Instance != null)
        {
            UIContainer.Instance.ClearUILayers();
        }
        SwitchScene(scene);
        if (openCurtainsAfter)
        {
            Curtain.Instance.Open();
        }
    }

    //Load previous screen using curtains
    public void LoadPreviousScreenWithCurtains()
    {
        LoadPreviousScene();
    }

    //Switch to new scene
    private void SwitchScene(string scene)
    {
        previousScene = GetCurrentScene();
        SceneManager.LoadScene(scene);
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