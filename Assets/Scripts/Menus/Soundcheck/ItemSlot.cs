using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : Draggable
{

    //Image to display to
    public Image image = null;
    //Item displayed in portrait
    public Item item = null;

    //Called before start
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    //Display character sprite in portrait
    public void DisplayItem(Item item)
    {
        if (item != null)
        {
            this.item = item;
            Sprite ItemSprite = Resources.Load<Sprite>(this.item.sprite);
            image.sprite = ItemSprite;
            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
        else
        {
            RemoveItem();
        }
    }

    //Remove character sprite from portrait
    public void RemoveItem()
    {
        item = null;
        image.sprite = null;
        Color c = image.color;
        c.a = 0;
        image.color = c;
    }

    //Return parent before being dragged
    public Transform GetStartParent()
    {
        return startParent;
    }

    
    public override void OnBeginDrag(PointerEventData eventData)
    {
        //Reset Draggable interface
        base.OnBeginDrag(eventData);
        //Tell Controller of the event
        if (item != null)
        {
            FindObjectOfType<InvController>().Pickup(item);
        }
    }    

    //Called when portrait is stopped being dragged
    public override void OnEndDrag(PointerEventData eventData)
    {
        //Reset Draggable interface
        base.OnEndDrag(eventData);        
        //Reset object location
        transform.SetParent(startParent);
        transform.SetSiblingIndex(startSiblingIndex);
        transform.position = startPosition;
    }
}