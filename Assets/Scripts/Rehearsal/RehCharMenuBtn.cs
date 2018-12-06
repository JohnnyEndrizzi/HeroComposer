using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RehCharMenuBtn : MonoBehaviour {

    //Gameobject locations
    [SerializeField]
    private Text btnText = null;
    [SerializeField]
    private Image imgIcon = null;
    [SerializeField]
    private Image highlighter = null;

    private string btnTextTxt;

    private int invID; //Location ID
    private int itemID; //Held item ID

    void Start(){
        this.GetComponent<Button>().onClick.AddListener(delegate {OnClick();});

        try{highlighter.enabled = false;}catch{}

        btnText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        btnText.fontSize = 16;
    }

    public void SetText(string textString) { //Set Text
        btnText.text = textString; 
        btnTextTxt = textString;
    }

    public void SetInvID(int ID){invID = ID;} //Set ID value   
    public int GetInvID(){return invID; } //Get ID value
    public void SetItemID(int ID){itemID = ID;} //Set ID value
    public int GetItemID(){return itemID; } //Get ID value

    public void SetImage(Sprite image){ //Set Sprite
        this.GetComponent<Image>().sprite = image;
    }

    public void SetIcon(Sprite mySprite) { //Set Icon Sprite
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

    public void toggleHigh(){
        if (highlighter.enabled) {
            highlighter.enabled = false;
        }
        else {
            highlighter.enabled = true;
        }
    }
    public void HighOff(){
        highlighter.enabled = false;
    }
    public void HighOn(){
        highlighter.enabled = true;
    }

    void OnClick(){ //Gets Clicked
        //To find who called this sceipt (different parents use this child script)
        if(GameObject.Find("InvController") != null){
            this.transform.parent.parent.parent.GetComponent<CharMenuCtrl>().ButtonClicked(invID);

        }
        else if(GameObject.Find("RehController") != null){
            this.transform.parent.parent.parent.GetComponent<RehCharMenuCtrl>().ButtonClicked(invID);
        }
    }
}



