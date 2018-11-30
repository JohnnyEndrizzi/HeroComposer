using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharMenuBtn : MonoBehaviour {

    //Gameobject locations
    [SerializeField]
    private CharMenuCtrl charMenuCtrl = null;
    [SerializeField]
    private Text btnText = null;

    private string btnTextTxt;
    private int intID;

    void Start(){
        this.GetComponent<Button>().onClick.AddListener(delegate {OnClick();});

        btnText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        btnText.fontSize = 16;
    }
       
    public void SetText(string textString) { //Set Text
        btnText.text = textString; 
        btnTextTxt = textString;
	}

    public void SetID(int ID){ //Set ID value
        intID = ID;
    }

    public void SetImage(Sprite image){ //Set Sprite
        this.GetComponent<Image>().sprite = image;
    }

    void OnClick(){ //Gets Clicked
        charMenuCtrl.ButtonClicked(intID);
    }
}