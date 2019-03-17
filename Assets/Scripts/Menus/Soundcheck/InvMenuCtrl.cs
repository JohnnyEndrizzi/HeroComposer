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
        List<Item> storedItems = GameManager.Instance.gameDataManager.GetItemsInInventory();        
        List<Item> playerInventory = new List<Item>();
        ItemSlots = new GameObject[maxItems];

        //Fill inventory slots with owned items
        for (int i = 0; i < storedItems.Count; i++) 
        {
            playerInventory.Add(storedItems[i]);
        }
        //Fill remaining inventory slots with empty items
        for (int i = storedItems.Count; i <= maxItems - 1; i++) 
        {
            playerInventory.Add(null);
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
    public List<Item> GetStoredItems() 
    { 
        List<Item> realValues = new List<Item>();         
        for (int i = 0; i < ItemSlots.Length; i++)
        {
            if (ItemSlots[i].GetComponentInChildren<ItemSlot>() != null)
            {
                realValues.Add(ItemSlots[i].GetComponentInChildren<ItemSlot>().item);
            }                     
        }
        return realValues;
    }
}