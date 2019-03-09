using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Keep track of sibling index in heirarchy
    private int originalSiblingIndex;
    //Keep track of original position
    private Vector3 originalPosition;

    //Called when first start dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Store current sibling index
        originalSiblingIndex = transform.GetSiblingIndex();
        //Store current position
        originalPosition = transform.position;
    }

    //Called while dragging
    public void OnDrag(PointerEventData eventData)
    {
        //Temporarily set as last sibling so it is rendered on top 
        transform.SetAsLastSibling();
        transform.position = Input.mousePosition;
    }

    //Called when finished dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        //Reset sibling index
        transform.SetSiblingIndex(originalSiblingIndex);
        //Snap back to original position
        transform.position = originalPosition;
    }
}
