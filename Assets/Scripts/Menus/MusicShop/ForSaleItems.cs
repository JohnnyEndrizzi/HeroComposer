﻿using System.Collections.Generic;
using UnityEngine;

public class ForSaleItems : MonoBehaviour {
    //Item prefab
    [SerializeField]
    private GameObject itemPrefab;

    // Use this for initialization
    void Start () {
        DisplayForSaleItems();
	}

    private void DisplayForSaleItems()
    {
        Dictionary<int, Item> items = GameManager.Instance.gameDataManager.GetItems();
        GameObject UIItem = Instantiate(itemPrefab, this.transform);
        int randomItem = 1;
        Item item = items[randomItem];
        UIItem.GetComponent<UIItem>().SetItem(item);
    }
}