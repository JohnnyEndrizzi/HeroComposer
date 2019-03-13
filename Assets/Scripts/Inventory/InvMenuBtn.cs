using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvMenuBtn : Droppable, IPointerEnterHandler, IPointerExitHandler {
    //Menu cell for Items in Inventory

    //Gameobject locations
    [SerializeField]
    private InvMenuCtrl invMenuCtrl = null;
    [SerializeField]
    private Image imgIcon = null;

    private int invID;  //Location ID
    private string itemID; //Held item ID

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate {OnClick();});
    }
        
    public void SetInvID(int ID){invID = ID;}   //Set invID value   
    public int  GetInvID(){return invID; }      //Get invID value
    public void SetItemID(string ID){itemID = ID;} //Set itemID value
    public string  GetItemID(){return itemID; }    //Get itemID value

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
        if (!itemID.Equals("0"))
        {
            GameObject.Find("InvController").GetComponent<InvController>().HoverTextFadeIn(itemID);
        }        
    }

    public void OnPointerExit(PointerEventData data) //Turns off text on hoverExit
    {
        if (!itemID.Equals("0"))
        {
            GameObject.Find("InvController").GetComponent<InvController>().HoverTextFadeOut(itemID);
        }            
    }

    public override void OnDrop(PointerEventData eventData)
    {


        /*

        InvMenuBtnIcon displayedItemSlot = GetComponentInChildren<InvMenuBtnIcon>();
        if (displayedItemSlot != null)
        {
            //If portrait was from character list and dropped on an empty slot
            CharacterList characterList = FindObjectOfType<CharacterList>();
            if (Draggable.itemBeingDragged.GetComponent<Portrait>().GetStartParent() == characterList.GetComponent<ScrollRect>().content.transform && displayedItemSlot.character == null)
            {
                //Display character from portrait being draged in this portrait
                InvMenuBtnIcon iconBeingDragged = Draggable.itemBeingDragged.GetComponent<InvMenuBtnIcon>();
                displayedItemSlot.DisplayItem(iconBeingDragged.item);
                //Remove portrait being dragged
                characterList.RemoveItem(iconBeingDragged.item);
                Destroy(iconBeingDragged.gameObject);
                //Portrait was from anywhere or was dropped on a non-empty slot
            }
            else
            {
                //If portrait was from character list
                if (Draggable.itemBeingDragged.GetComponent<Portrait>().GetStartParent() == characterList.GetComponent<ScrollRect>().content.transform)
                {
                    //Remove from character list
                    characterList.RemoveCharacter(Draggable.itemBeingDragged.GetComponent<Portrait>().character);
                }
                //Swap portraits
                Character tempCharacter = displayedItemSlot.character;
                displayedItemSlot.DisplayCharacter(Draggable.itemBeingDragged.GetComponent<Portrait>().character);
                Draggable.itemBeingDragged.GetComponent<Portrait>().DisplayCharacter(tempCharacter);
            }
        }  */
    }
}


