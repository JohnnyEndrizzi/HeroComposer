using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour {
    public void UpdateProgress(float progress)
    {
        GetComponent<Slider>().value = progress;
    }
    public void Show()
    {
       
       GetComponent<CanvasGroup>().alpha = 1;
    }
    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
