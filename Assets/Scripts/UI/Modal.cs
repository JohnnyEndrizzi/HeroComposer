using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum ModalBanner
{
    Error,
    Success,
    Notice
}

public class Modal : MonoBehaviour {

    //Content text
    public Text content;
    //Banner
    public Image banner;

    //Banner sprites
    [SerializeField]
    private Sprite noticeBanner;
    [SerializeField]
    private Sprite errorBanner;
    [SerializeField]
    private Sprite successBanner;
    
    //Replace content with new message
    public void DisplayMessage(string text)
    {
        content.text = text;
    }

    //Change severity of banner
    public void ChangeBanner(ModalBanner banner)
    {
        Sprite newBanner = this.banner.sprite;
        switch (banner)
        {
            case ModalBanner.Notice:
                newBanner = noticeBanner;
                break;
            case ModalBanner.Error:
                newBanner = errorBanner;
                break;
            case ModalBanner.Success:
                newBanner = successBanner;
                break;
        }
        this.banner.sprite = newBanner;
    }

    //Close the modal
    public void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }
    //Allow animation of button rising back up to finish
    IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(this.gameObject);
    }
}
