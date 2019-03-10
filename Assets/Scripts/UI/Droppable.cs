using UnityEngine;
using UnityEngine.EventSystems;

public class Droppable : MonoBehaviour, IDropHandler
{
    public virtual void OnDrop(PointerEventData eventData)
    {
        Draggable.itemBeingDragged.transform.SetParent(transform);
        Draggable.itemBeingDragged.transform.localPosition = Vector3.zero;
    }
}
