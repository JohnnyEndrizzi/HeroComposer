using UnityEngine;
using UnityEngine.UI;

public class UIItem : MonoBehaviour {

    public Item item;

    public void SetItem(Item item)
    {
        this.item = item;
        GetComponent<Image>().sprite = Resources.Load<Sprite>(item.sprite);
    }
}
