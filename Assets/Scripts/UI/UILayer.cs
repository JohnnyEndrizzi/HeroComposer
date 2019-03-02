using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UILayer : MonoBehaviour {
    //UI EventSystem 
    public EventSystem UIEventSystem;

    //Show the UI
    public void Show()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }
        if (UIEventSystem != null)
        {
            UIEventSystem.gameObject.SetActive(true);
        }
    }

    //Hide the UI
    public void Hide()
    {
        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }
        if (UIEventSystem != null)
        {
            UIEventSystem.gameObject.SetActive(false);
        }
    }
}
