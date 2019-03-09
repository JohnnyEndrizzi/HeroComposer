using UnityEngine;
using UnityEngine.EventSystems;

public class UILayer : MonoBehaviour {
    //UI EventSystem 
    public EventSystem UIEventSystem;

    //Show the UI
    public void Show()
    {
        this.gameObject.SetActive(true);
        if (UIEventSystem != null)
        {
            UIEventSystem.gameObject.SetActive(true);
        }
    }

    //Hide the UI
    public void Hide()
    {
        this.gameObject.SetActive(false);
        if (UIEventSystem != null)
        {
            UIEventSystem.gameObject.SetActive(false);
        }
    }
}
