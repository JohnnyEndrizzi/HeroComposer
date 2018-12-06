using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnUnit : EventTrigger{  
    //Unity Bug? - set imgIcon as Monobehaviour, then turn into EventTrigger keeps values but prevents editor changes

    //Gameobject locations
    [SerializeField]
    private Image imgIcon = null;

    public void SetIcon(Sprite image){ //Set Sprite
        if (image == null) { imgIcon.GetComponent<Image>().enabled = false; }
        else { imgIcon.GetComponent<Image>().enabled = true; }

        imgIcon.sprite = image;
        imgIcon.preserveAspect = true;
    }

    public Sprite GetIcon()
    { //Get Sprite
        return imgIcon.sprite;
    }

    public bool HasIcon()
    { //Is spot filled?
        if (imgIcon.sprite == true) { return true; }
        else { return false; }
    }

    public override void OnPointerEnter(PointerEventData data)
    {

    }

    public override void OnPointerExit(PointerEventData data)
    {

    }
}

