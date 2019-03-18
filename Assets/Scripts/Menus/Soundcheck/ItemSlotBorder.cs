using UnityEngine.EventSystems;

public class ItemSlotBorder : EventTrigger
{

    public override void OnPointerEnter(PointerEventData data) //Turns on text on hoverEnter 
    {
        //To prevent call when hovering over button while holding its ItemSlot
        if (this.gameObject.transform.childCount == 1)
        {
            FindObjectOfType<InvController>().HoverTextFadeIn(GetComponentInChildren<ItemSlot>().item);
        }
    }

    public override void OnPointerExit(PointerEventData data) //Turns off text on hoverExit
    {
        FindObjectOfType<InvController>().HoverTextFadeOut();
    }

    public override void OnDrop(PointerEventData eventData)
    {        
        ItemSlot displayedItemSlot = GetComponentInChildren<ItemSlot>();
        //TODO clean up null types
        if (displayedItemSlot != null && Draggable.itemBeingDragged.GetComponent<ItemSlot>() && Draggable.itemBeingDragged.GetComponent<ItemSlot>().item != null && Draggable.itemBeingDragged.GetComponent<ItemSlot>().item.name != null)
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
                FindObjectOfType<InvController>().Place();
            }
            else
            {
                //Swap items
                Item tempItem = displayedItemSlot.item;   
                displayedItemSlot.DisplayItem(Draggable.itemBeingDragged.GetComponent<ItemSlot>().item);
                Draggable.itemBeingDragged.GetComponent<ItemSlot>().DisplayItem(tempItem);
                //Tell Controller of the event
                FindObjectOfType<InvController>().Place();
            }            
        }  
    }
}