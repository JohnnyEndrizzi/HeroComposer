using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Keep track of item being dragged
    public static GameObject itemBeingDragged;
    //Original positions
    Vector3 startPosition;
    //Original parent
    Transform startParent;
    //Original hierarchy position
    int startSiblingIndex;

    //Called when first start dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Store item being dragged, original position and parent
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        //Disable blocking raycasts so drop event can fire
        gameObject.AddComponent<CanvasGroup>();
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Set as last sibling so its rendered on top
        startSiblingIndex = transform.GetSiblingIndex();
        transform.SetAsLastSibling();
    }

    //Called while dragging
    public void OnDrag(PointerEventData eventData)
    {
        //Update position to mouse location
        transform.position = Input.mousePosition;
    }

    //Called when finished dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        //Reset draggable component
        itemBeingDragged = null;
        Destroy(GetComponent<CanvasGroup>());
        //If component was not dropped anywhere, reset to orignal position
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
            transform.SetSiblingIndex(startSiblingIndex);
        }
    }
}
