using UnityEngine;
using UnityEngine.UI;

public class SpcMenuItem : MonoBehaviour {
    //Menu cell for Items on special shelf in Music Shop

    //Gameobject locations
    [SerializeField]
    private SpcMenuCtrl spcMenuCtrl = null;
    [SerializeField]
    private Image imgIcon = null;
    [SerializeField]
    private Text iconText = null;
    [SerializeField]
    private Image textBG = null;

    private string iconTextTxt;

    private int invID;  //Location ID
    private int itemID; //Held item ID

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate {OnClick();});

        iconText.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        iconText.fontSize = 14;
    }

    public void SetInvID(int ID) {invID = ID;}   //Set invID value   
    public int  GetInvID() {return invID;}       //Get invID value
    public void SetItemID(int ID) {itemID = ID;} //Set itemID value
    public int  GetItemID() {return itemID;}     //Get itemID value

    public void SetText(string textString) //Set Text
    { 
        iconText.text = textString; 
        iconTextTxt = textString;

        if (textString.Equals("0")) {iconText.text = "";}            
    }

    public void SetIcon(Sprite mySprite) //Set Sprite on icon child object
    { 
        if (mySprite == null){
            imgIcon.GetComponent<Image>().enabled = false;
            textBG.GetComponent<Image>().enabled = false;
        }else{
            imgIcon.GetComponent<Image>().enabled = true;
            textBG.GetComponent<Image>().enabled = true;
        } 
        imgIcon.sprite = mySprite; 
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

    void OnClick() //Gets clicked
    { 
        spcMenuCtrl.ButtonClicked(invID, itemID);
    }         
}