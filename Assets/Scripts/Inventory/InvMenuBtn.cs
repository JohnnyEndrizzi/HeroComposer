using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvMenuBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    //Gameobject locations
    [SerializeField]
    private InvMenuCtrl invMenuCtrl = null;
    [SerializeField]
    private Image imgIcon = null;

    private int invID; //Location ID
    private int itemID; //Held item ID

    void Start(){
        this.GetComponent<Button>().onClick.AddListener(delegate {OnClick();});
    }
        
    public void SetInvID(int ID){invID = ID;} //Set ID value   
    public int GetInvID(){return invID; } //Get ID value
    public void SetItemID(int ID){itemID = ID;} //Set ID value
    public int GetItemID(){return itemID; } //Get ID value

    public void SetIcon(Sprite mySprite) { //Set Sprite
        if (mySprite == null){imgIcon.GetComponent<Image>().enabled = false;}
        else{imgIcon.GetComponent<Image>().enabled = true;} 
            
        imgIcon.sprite = mySprite; 
    }
    public Sprite GetIcon() { //Get Sprite
        return imgIcon.sprite;
    }

    public bool HasIcon() { //Is spot filled?
        if (imgIcon.sprite == true) {return true;}
        else{return false;}
    }

    void OnClick(){ //Gets clicked
        invMenuCtrl.ButtonClicked(invID, itemID);
    }      

    public void OnPointerEnter(PointerEventData data){        
        GameObject.Find("InvController").GetComponent<InvController>().HoverText(invID+1, itemID);   
    }

    public void OnPointerExit(PointerEventData data){
        GameObject.Find("InvController").GetComponent<InvController>().HoverText(-(invID+1), itemID);   
    }
}
