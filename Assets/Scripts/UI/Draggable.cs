using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //Keep track of item being dragged
    public static GameObject itemBeingDragged;
    //Original positions
    protected Vector3 startPosition;
    //Original parent
    protected Transform startParent;
    //Original hierarchy position
    protected int startSiblingIndex;
    //UIContainer parent transform
    protected Transform UIContainerTransform; 

    //Called every frame
    void Update()
    {
        //If another object is being dragged, disable raycasting for this object
        if (itemBeingDragged != null && itemBeingDragged != this)
        {
            GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Enable raycasting 
        }else{
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }

    //Used to initialize
    void Start()
    {
        gameObject.AddComponent<CanvasGroup>();
    }

    //Called when first start dragging
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        //Store item being dragged, original position and parent
        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        //Disable blocking raycasts so drop event can fire
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        //Set as last sibling on UI so its rendered on top
        startSiblingIndex = transform.GetSiblingIndex();
        UIContainerTransform = FindObjectOfType<UIContainer>().transform;
        transform.SetParent(UIContainerTransform);
        transform.SetAsLastSibling();
    }

    //Called while dragging
    public void OnDrag(PointerEventData eventData)
    {
        //Update position to mouse location
        transform.position = Input.mousePosition;
    }

    //Called when finished dragging
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        //Reset draggable component
        itemBeingDragged = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
