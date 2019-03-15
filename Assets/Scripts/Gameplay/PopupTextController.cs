using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupTextController : MonoBehaviour
{
    //static methods require static references
    private static GameObject popupText;
    private static RectTransform parent;
    private static float min = -.5f, max = .5f;

    private void Start()
    {
        //set the parent to the ui group
        parent = transform.Find("UI").GetComponent<RectTransform>();
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        //find if null
        if (popupText == null)
        {
            popupText = Resources.Load("Prefabs/UI/PopupText") as GameObject;
        }

        GameObject instance = Instantiate(popupText);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(
        new Vector2(location.position.x + Random.Range(min, max), location.position.y));
        
        instance.transform.SetParent(parent, false);
        instance.transform.position = screenPos;
        instance.GetComponent<Text>().text = text;

        Animator anim = instance.GetComponent<Animator>();
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        Destroy(instance, clipInfo[0].clip.length);
    }
}