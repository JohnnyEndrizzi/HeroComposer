using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvMenuCtrl : MonoBehaviour { 
    //Controls Item menu in Invantory

    //Gameobject locations
    [SerializeField]
    private GameObject buttonTemplate = null;
    [SerializeField]
    private GridLayoutGroup gridGroup;

    private List<PlayerItem> playerInventory;
    private int maxBtns = 98;

    Dictionary<string, Item> allItems;
    Dictionary<string, Item> invItems;
    public List<string> storedItems;

    public void Creator() //Generate/Regenerate List 
    {
        playerInventory = new List<PlayerItem>();
                
        if (GameObject.Find("InvBtn #0") != null) //Destroy existing buttons to allow for reload
        { 
            DestroyerOfLists();
        }
                
        for (int i = 0; i<storedItems.Count; i++) //Fill inventory slots with owned items
        {
            PlayerItem newItem = new PlayerItem
            {
                //iconSprite = AllItems[storedItems[i]].img,
                itemID = storedItems[i]
            };

            playerInventory.Add(newItem);
        }                
        for (int i = storedItems.Count; i<=maxBtns-1; i++) //Fill remaining inventory slots with empty items
        {
            PlayerItem newItem = new PlayerItem
            {
                itemID = "0"
            };

            playerInventory.Add(newItem);
        }
        GenInventory();
    }
    
    void GenInventory() //Create Buttons and sets values
    { 
        int i = 0;
        foreach (PlayerItem newItem in playerInventory) { 
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);

            button.name = ("InvBtn #" + i);
            button.GetComponent<InvMenuBtn>().SetIcon(newItem.iconSprite);
            button.GetComponent<InvMenuBtn>().SetInvID(i);
            button.GetComponent<InvMenuBtn>().SetItemID(newItem.itemID);
            //add comp<dragable> TODO
            button.transform.SetParent(buttonTemplate.transform.parent,false);

            i++;
        }
    }

    public List<string> GetStoredItems() //returns all item values
    { 
        List<string> realValues = new List<string>();         

        for (int i = 0; i < buttonTemplate.transform.parent.childCount-1; i++)
        {
            realValues.Add(GameObject.Find("InvBtn #" + i).GetComponent<InvMenuBtn>().GetItemID());            
        }
        return realValues;
    }

    void DestroyerOfLists() //Destroys existing objects
    {
        for (int i = 0; i < buttonTemplate.transform.parent.childCount-1; i++)
        {
            Destroy(GameObject.Find("InvBtn #" + i)); 
        }
    }

    public void ButtonClicked(int invID, string itemID) //sub button Clicked
    { 
        GameObject.Find("InvController").GetComponent<InvController>().GridOnClick(invID, itemID);       //TODO
    }

    public class PlayerItem{
        public Sprite iconSprite;
        public string itemID;
    }
}