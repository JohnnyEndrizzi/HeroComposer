using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvMenuBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    //Menu cell for Items in Inventory

    //Gameobject locations
    [SerializeField]
    private InvMenuCtrl invMenuCtrl = null;
    [SerializeField]
    private Image imgIcon = null;

    private int invID;  //Location ID
    private int itemID; //Held item ID

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate {OnClick();});
    }
        
    public void SetInvID(int ID){invID = ID;}   //Set invID value   
    public int  GetInvID(){return invID; }      //Get invID value
    public void SetItemID(int ID){itemID = ID;} //Set itemID value
    public int  GetItemID(){return itemID; }    //Get itemID value

    public void SetIcon(Sprite mySprite) //Set Sprite on icon child object
    { 
        if (mySprite == null){imgIcon.GetComponent<Image>().enabled = false;}
        else{imgIcon.GetComponent<Image>().enabled = true;} 
            
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
        invMenuCtrl.ButtonClicked(invID, itemID);
    }      

    public void OnPointerEnter(PointerEventData data) //Turns on text on hoverEnter 
    {        
        GameObject.Find("InvController").GetComponent<InvController>().HoverText(invID+1, itemID);   
    }

    public void OnPointerExit(PointerEventData data) //Turns on text on hoverExit
    {
        GameObject.Find("InvController").GetComponent<InvController>().HoverText(-(invID+1), itemID);   
    }
}