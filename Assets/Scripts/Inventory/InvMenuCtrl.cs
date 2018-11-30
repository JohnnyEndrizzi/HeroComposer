﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvMenuCtrl : MonoBehaviour { //TODO rework

    //Gameobject locations
    [SerializeField]
    private GameObject buttonTemplate = null;
    [SerializeField]
    private GridLayoutGroup gridGroup;

    private List<PlayerItem> playerInventory;
    public List<int> storedItems;
    private int maxBtns = 99;


    public Dictionary<int, AllItemDict> AllItems;

    public void creator(){ //Generate/Regenerate List 
        playerInventory = new List<PlayerItem>();
            
        if (GameObject.Find("InvBtn #0") != null) { //Destroy buttons to allow for reload
            DestroyerOfLists();
        }     
    

        for (int i = 0; i<storedItems.Count; i++){ 
            PlayerItem newItem = new PlayerItem();

            newItem.iconSprite = AllItems[storedItems[i]].img;// img[stored[i]];   
            newItem.itemID = storedItems[i];

            playerInventory.Add(newItem);
        }
        for (int i = storedItems.Count; i<=maxBtns-1; i++){ 
            PlayerItem newItem = new PlayerItem();

            newItem.iconSprite = AllItems[0].img;   
            newItem.itemID = 0;

            playerInventory.Add(newItem);
        }
        genInventory();
    }


    void genInventory(){ //Create Buttons and sets values
        int i = 0;
        foreach (PlayerItem newItem in playerInventory) { 
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.name = ("InvBtn #" + i);
            button.GetComponent<InvMenuBtn>().SetIcon(newItem.iconSprite);
            button.GetComponent<InvMenuBtn>().SetInvID(i);
            button.GetComponent<InvMenuBtn>().SetItemID(newItem.itemID);
            button.transform.SetParent(buttonTemplate.transform.parent,false);

            i++;
        }
    }

    public List<int> GetStoredItems(){ //returns all item values
        List<int> realValues  = new List<int>();         

        for (int i = 0; i < buttonTemplate.transform.parent.childCount-1; i++) {
            realValues.Add(GameObject.Find("InvBtn #" + i).GetComponent<InvMenuBtn>().GetItemID());            
        }
        return realValues;
    }

    void DestroyerOfLists(){ //Boom
        for (int i = 0; i < buttonTemplate.transform.parent.childCount-1; i++) {
            Destroy(GameObject.Find("InvBtn #" + i)); 
        }
    }

    public void ButtonClicked(int invID, int itemID){ 
        GameObject.Find("InvController").GetComponent<InvController>().GridOnClick(invID, itemID);      
    }

    public class PlayerItem{
        public Sprite iconSprite;
        public int itemID;
    }
}