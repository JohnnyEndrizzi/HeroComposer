using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Portrait : Draggable{

    //Image displayed in portrait
    private Image image = null;
    //Character displayed in portrait
    public Character character = null;

    //Called before start
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    //Display character sprite in portrait
    public void DisplayCharacter(Character character)
    {
        if (character != null)
        {
            this.character = character;
            Sprite characterSprite = Resources.Load<Sprite>(this.character.sprite);
            image.sprite = characterSprite;
            Color c = image.color;
            c.a = 1;
            image.color = c;
            if(transform.parent.GetComponent<PortraitBorder>() != null)
            {
                transform.parent.GetComponent<PortraitBorder>().RemoveIndicator();
            }
        }else{
            RemoveCharacter();
        }

    }

    //Remove character sprite from portrait
    public void RemoveCharacter()
    {
        character = null;
        image.sprite = null;
        Color c = image.color;
        c.a = 0;
        image.color = c;
    }

    //Return parent before being dragged
    public Transform GetStartParent() {
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
        //Display indicator if empty
        if (transform.parent.GetComponent<PortraitBorder>() != null && character == null)
        {
            transform.parent.GetComponent<PortraitBorder>().DisplayIndicator();
        }
    }
}
