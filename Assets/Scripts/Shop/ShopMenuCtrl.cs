using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMenuCtrl : MonoBehaviour {
    //Controls on sale Item menu in Music Shop

    //Gameobject locations
    [SerializeField]
    private GameObject buttonTemplate = null;
    [SerializeField]
    private GridLayoutGroup gridGroup;

    private List<PlayerItem> shopInventory;
    private int maxBtns = 16;

    public List<int> shopItems;
    public Dictionary<int, AllItemDict> Dict;

    public void creator() //Generate/Regenerate List 
    { 
        shopInventory = new List<PlayerItem>();

        if (GameObject.Find("ShopBtn #0") != null) //Destroy buttons to allow for reload
        { 
            DestroyerOfLists();
        }     
        
        for (int i = 0; i<shopItems.Count; i++)  //Create a button for each item in sell list (excluding null)
        { 
            PlayerItem newItem = new PlayerItem();

            newItem.iconSprite = Dict[shopItems[i]].img;  
            newItem.itemID = shopItems[i];

            shopInventory.Add(newItem);
        }
        for (int i = shopItems.Count; i<=maxBtns-1; i++) //Fills remaining slots with empty buttons
        { 
            PlayerItem newItem = new PlayerItem();
 
            newItem.itemID = 0;

            shopInventory.Add(newItem);
        }
        GenInventory();
    }

    void GenInventory() //Create Buttons and sets values
    { 
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

    public List<int> GetStoredItems() //returns all item values
    {
        List<int> realValues = new List<int>();

        for (int i = 0; i < buttonTemplate.transform.parent.childCount - 1; i++)
        {
            realValues.Add(GameObject.Find("ShopBtn #" + i).GetComponent<InvMenuBtn>().GetItemID());
        }
        return realValues;
    }
    
    void DestroyerOfLists() //Destroys existing objects
    {
        for (int i = 0; i < buttonTemplate.transform.parent.childCount-1; i++)
        {
            Destroy(GameObject.Find("ShopBtn #" + i)); 
        }
    }

    public void ButtonClicked(int invID, int itemID) //sub button Clicked
    {
        GameObject.Find("ShopController").GetComponent<ShopController>().OnClick(invID + 1, itemID);
    }

    public class PlayerItem{
        public Sprite iconSprite;
        public int itemID;
    }
}