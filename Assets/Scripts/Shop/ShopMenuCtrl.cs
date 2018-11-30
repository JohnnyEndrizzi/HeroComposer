﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuCtrl : MonoBehaviour {

    //Gameobject locations
    [SerializeField]
    private GameObject buttonTemplate = null;
    [SerializeField]
    private GridLayoutGroup gridGroup;

    private List<PlayerItem> shopInventory;
    private int maxBtns = 16;

    public List<int> shopItems;
    public Dictionary<int, AllItemDict> Dict;

    public void creator(){ //Generate/Regenerate List 
        shopInventory = new List<PlayerItem>();

        if (GameObject.Find("ShopBtn #0") != null) { //Destroy buttons to allow for reload
            DestroyerOfLists();
        }     


        for (int i = 0; i<shopItems.Count; i++){ 
            PlayerItem newItem = new PlayerItem();

            newItem.iconSprite = Dict[shopItems[i]].img;// img[stored[i]];   
            newItem.itemID = shopItems[i];

            shopInventory.Add(newItem);
        }
        for (int i = shopItems.Count; i<=maxBtns-1; i++){ 
            PlayerItem newItem = new PlayerItem();

            newItem.iconSprite = Dict[shopItems[0]].img;   
            newItem.itemID = 0;

            shopInventory.Add(newItem);
        }
        genInventory();
    }

    void genInventory(){ //Create Buttons and sets values
        int i = 0;
        foreach (PlayerItem newItem in shopInventory) { 
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.name = ("ShopBtn #" + i);
            button.GetComponent<ShopMenuItem>().SetIcon(newItem.iconSprite);
            button.GetComponent<ShopMenuItem>().SetInvID(i);
            button.GetComponent<ShopMenuItem>().SetItemID(newItem.itemID);
            button.GetComponent<ShopMenuItem>().SetText(Dict[shopItems[i]].Cost.ToString());
            button.transform.SetParent(buttonTemplate.transform.parent,false);

            i++;
        }
    }

    public void ButtonClicked(int invID, int itemID){ 
        GameObject.Find("ShopController").GetComponent<ShopController>().OnClick(invID+1, itemID);      
    }

    void DestroyerOfLists(){ //Boom
        for (int i = 0; i < buttonTemplate.transform.parent.childCount-1; i++) {
            Destroy(GameObject.Find("ShopBtn #" + i)); 
        }
    }

    public class PlayerItem{
        public Sprite iconSprite;
        public int itemID;
    }
}