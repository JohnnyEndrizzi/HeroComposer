﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvMenuBtnIcon : Draggable {
    
    //Image to display to
    private Image image = null;
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
            Sprite characterSprite = Resources.Load<Sprite>(this.item.sprite);
            image.sprite = characterSprite;
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