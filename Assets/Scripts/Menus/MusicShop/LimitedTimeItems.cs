using System.Collections.Generic;
using UnityEngine;

public class LimitedTimeItems : MonoBehaviour {
    //Item prefab
    [SerializeField]
    private GameObject itemPrefab;

    Dictionary<int, Item> items;


    // Use this for initialization
    void Start () {
        items = GameManager.Instance.gameDataManager.GetItems();

        DispItems(0);
        DispItems(1);
        DispItems(2);
        DispItems(3);
        DispItems(0);
        DispItems(1);
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

    //Temp
    private void DispItems(int itemNum)
    {
        GameObject UIItem = Instantiate(itemPrefab, this.transform);
        Item item = items[itemNum];
        UIItem.GetComponent<UIItem>().SetItem(item);
    }
}
