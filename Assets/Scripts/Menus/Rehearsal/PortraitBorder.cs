using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PortraitBorder : Droppable
{

    public override void OnDrop(PointerEventData eventData)
    {
        Portrait displayedPortrait = GetComponentInChildren<Portrait>();
        if (displayedPortrait != null)
        {
            //If portrait was from character list and dropped on an empty slot
            CharacterList characterList = FindObjectOfType<CharacterList>();
            if (Draggable.itemBeingDragged.GetComponent<Portrait>().GetStartParent() == characterList.GetComponent<ScrollRect>().content.transform && displayedPortrait.character == null)
            {
                //Display character from portrait being draged in this portrait
                Portrait portraitBeingDragged = Draggable.itemBeingDragged.GetComponent<Portrait>();
                displayedPortrait.DisplayCharacter(portraitBeingDragged.character);
                //Remove portrait being dragged
                characterList.RemoveCharacter(portraitBeingDragged.character);
                Destroy(portraitBeingDragged.gameObject);
            //Portrait was from anywhere or was dropped on a non-empty slot
            }else{
                //If portrait was from character list
                if (Draggable.itemBeingDragged.GetComponent<Portrait>().GetStartParent() == characterList.GetComponent<ScrollRect>().content.transform)
                {
                    //Remove from character list
                    characterList.RemoveCharacter(Draggable.itemBeingDragged.GetComponent<Portrait>().character);
                }
                //Swap portraits
                Character tempCharacter = displayedPortrait.character;
                displayedPortrait.DisplayCharacter(Draggable.itemBeingDragged.GetComponent<Portrait>().character);
                Draggable.itemBeingDragged.GetComponent<Portrait>().DisplayCharacter(tempCharacter);
            }
        }
    }
}
