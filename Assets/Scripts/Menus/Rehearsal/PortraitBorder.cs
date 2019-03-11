using UnityEngine.EventSystems;

public class PortraitBorder : Droppable
{

    public override void OnDrop(PointerEventData eventData)
    {
        Portrait displayedPortrait = GetComponentInChildren<Portrait>();
        if (displayedPortrait != null)
        {
            Character tempCharacter = displayedPortrait.character;
            displayedPortrait.DisplayCharacter(Draggable.itemBeingDragged.GetComponent<Portrait>().character);
            Draggable.itemBeingDragged.GetComponent<Portrait>().DisplayCharacter(tempCharacter);
        }
    }
}
