using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItem : MonoBehaviour, IPointerClickHandler {

    //Item displayed
    public Item item;

    //Set item
    public void SetItem(Item item)
    {
        this.item = item;
        GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
    }

    //Fire on click
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
