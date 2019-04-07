using UnityEngine;
using UnityEngine.EventSystems;


public class BtnEquipBorder : EventTrigger
{
       
    public override void OnPointerEnter(PointerEventData data)
    {
        //To prevent call when hovering over button while holding its ItemSlot
        if (this.gameObject.transform.childCount == 1)
        {
            FindObjectOfType<InvController>().HoverTextFadeIn(GetComponentInChildren<ItemSlot>().item, GetIDNum());
        }
    }

    //Get which equip slot this is
    private int GetIDNum()
    {
        if (this.gameObject.name.Equals("ButtonTop")) { return 0; }
        else if (this.gameObject.name.Equals("ButtonMid")) { return 1; }
        else if (this.gameObject.name.Equals("ButtonBottom")) { return 2; }
        else { Debug.Log("ERROR " + this.gameObject.name); return -1; }        
    }

    public override void OnPointerExit(PointerEventData data)
    {
        FindObjectOfType<InvController>().HoverTextFadeOut();
    }

    public override void OnDrop(PointerEventData eventData)
    {
        ItemSlot displayedItemSlot = GetComponentInChildren<ItemSlot>();
        //TODO clean up null types
        if (displayedItemSlot != null && Draggable.itemBeingDragged.GetComponent<ItemSlot>().item != null && Draggable.itemBeingDragged.GetComponent<ItemSlot>().item.name != null)
        {         
            //If slot is empty
            if (displayedItemSlot.item == null)
            {
                //Display item
                ItemSlot iconBeingDragged = Draggable.itemBeingDragged.GetComponent<ItemSlot>();
                displayedItemSlot.DisplayItem(iconBeingDragged.item);                
                //Clear source item slot                    
                iconBeingDragged.DisplayItem(null);
                //Tell Controller of the event
                FindObjectOfType<InvController>().Equip();
            }
            else
            {
                //Swap items
                Item tempItem = displayedItemSlot.item;
                displayedItemSlot.DisplayItem(Draggable.itemBeingDragged.GetComponent<ItemSlot>().item);
                Draggable.itemBeingDragged.GetComponent<ItemSlot>().DisplayItem(tempItem);
                //Tell Controller of the event
                FindObjectOfType<InvController>().Equip();
            }          
        }
    }
}