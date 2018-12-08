using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnEquipt : EventTrigger {
    //Independant Button containing an item
    //Unity Bug - set imgIcon as Monobehaviour, then turn into EventTrigger keeps values but prevents editor changes
    //a more reliable solution will be applied

    //Gameobject locations
    [SerializeField]
    private Image imgIcon = null;

    public void SetIcon(Sprite image) //Set Sprite on icon child object
    {
        if (image == null){imgIcon.GetComponent<Image>().enabled = false;}
        else{imgIcon.GetComponent<Image>().enabled = true;} 

        imgIcon.sprite = image;
        imgIcon.preserveAspect = true;
    }      

    public Sprite GetIcon() //Get Sprite from child object
    {
        return imgIcon.sprite;
    }

    public bool HasIcon() //Is child object icon spot filled?
    {
        if (imgIcon.sprite == true) {return true;}
        else{return false;}
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        GameObject.Find("InvController").GetComponent<InvController>().HoverText(this.name, 1);   
    }

    public override void OnPointerExit(PointerEventData data)
    {
        GameObject.Find("InvController").GetComponent<InvController>().HoverText(this.name, -1);   
    }
}