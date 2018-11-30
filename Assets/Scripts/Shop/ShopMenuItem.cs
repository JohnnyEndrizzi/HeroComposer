using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuItem : MonoBehaviour {

    //Gameobject locations
    [SerializeField]
    private ShopMenuCtrl shopMenuCtrl = null;
    [SerializeField]
    private Image imgIcon = null;
    [SerializeField]
    private Text iconText = null;
    [SerializeField]
    private Image textBG = null;

    private string iconTextTxt;
    private int invID; //cell number
    private int itemID; //cell contents
       
    void Start(){
        this.GetComponent<Button>().onClick.AddListener(delegate {OnClick();});

        iconText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        iconText.fontSize = 14;
    }

    public void SetInvID(int ID){invID = ID;} //Set ID value
    public void SetItemID(int ID){itemID = ID;} //Set ID value

    public void SetText(string textString) { //Set Text
        iconText.text = textString; 
        iconTextTxt = textString;

        if (textString.Equals("0")) {iconText.text = "";}            
    }

    public void SetIcon(Sprite mySprite) { //Set Sprite
        if (mySprite == null){            
            imgIcon.GetComponent<Image>().enabled = false;
            textBG.GetComponent<Image>().enabled = false;
        }else{
            imgIcon.GetComponent<Image>().enabled = true;
            textBG.GetComponent<Image>().enabled = true;
        } 
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
        shopMenuCtrl.ButtonClicked(invID, itemID);
    }
}