using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnEquipt : EventTrigger {

    //Gameobject locations
    [SerializeField]
    private Image imgIcon = null;

    public void SetIcon(Sprite image){ //Set Sprite
        if (image == null){imgIcon.GetComponent<Image>().enabled = false;}
        else{imgIcon.GetComponent<Image>().enabled = true;} 

        imgIcon.sprite = image;
    }      

    public Sprite GetIcon() { //Get Sprite
        return imgIcon.sprite;
    }

    public bool HasIcon() { //Is spot filled?
        if (imgIcon.sprite == true) {return true;}
        else{return false;}
    }

    public override void OnPointerEnter(PointerEventData data){
        GameObject.Find("InvController").GetComponent<InvController>().HoverText(this.name, 1);   
    }

    public override void OnPointerExit(PointerEventData data){
        GameObject.Find("InvController").GetComponent<InvController>().HoverText(this.name, -1);   
    }
}