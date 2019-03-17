using System.Collections.Generic;
using UnityEngine;

public class LimitedTimeItems : MonoBehaviour {
    //Item prefab
    [SerializeField]
    private GameObject itemPrefab;

    // Use this for initialization
    void Start () {
        DisplayLimitedTimeItems();
    }

    //Display limited time items
    private void DisplayLimitedTimeItems()
    {
        Dictionary<int, Item> items = GameManager.Instance.gameDataManager.GetItems();
        GameObject UIItem = Instantiate(itemPrefab, this.transform);
        int randomItem = 0;
        Item item = items[randomItem];
        UIItem.GetComponent<UIItem>().SetItem(item);
    }
}
