using System.Collections.Generic;
using UnityEngine;

public class InvMenuCtrl : MonoBehaviour { 
    //Controls Item menu in Invantory

    //Gameobject locations
    [SerializeField]
    GameObject buttonTemplate = null;
    GameObject[] ItemSlots;
        
    private int maxItems = 98;

    public void GenInventory() //Create Buttons and sets values
    {        
        Dictionary<int, Item> storedItems = GameManager.Instance.gameDataManager.GetItemsInInventory();        
        List<Item> playerInventory = new List<Item>();        
        ItemSlots = new GameObject[maxItems];

        Item checkItem;

        for (int i = 0; i < maxItems; i++)
        {
            if (storedItems.TryGetValue(i, out checkItem))
            {
                playerInventory.Add(checkItem);
            }
            else
            {
                playerInventory.Add(null);
            }
        }

        //Generate Buttons
        int j = 0;
        foreach (var newItem in playerInventory) {
            ItemSlots[j] = Instantiate(buttonTemplate);
            ItemSlots[j].SetActive(true);

            ItemSlots[j].name = ("InvBtn #" + j);
            ItemSlots[j].GetComponentInChildren<ItemSlot>().DisplayItem(newItem);
            ItemSlots[j].transform.SetParent(buttonTemplate.transform.parent,false);
            j++;
        }
    }
           
    //Returns all item values
    public Dictionary<int, Item> GetStoredItems() 
    {
        Dictionary<int, Item> storedItems = new Dictionary<int, Item>();        

        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].GetComponentInChildren<ItemSlot>() && ItemSlots[i].GetComponentInChildren<ItemSlot>().item != null)
            {
                storedItems.Add(i, ItemSlots[i].GetComponentInChildren<ItemSlot>().item);
            }
        }
        return storedItems;
    }
}