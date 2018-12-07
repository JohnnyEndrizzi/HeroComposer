using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {
    //Main controller for Music Shop scene

    // 8.1 1-9 he player must be able to purchase new equipment.

    //Gameobject locations
    [SerializeField]
    private ShopMenuCtrl shopMenuCtrl = null;
    [SerializeField]
    private SpcMenuCtrl spcMenuCtrl = null;
    [SerializeField]
    private StoredValues storedValues = null;

    [HideInInspector]
    public List<int> storedItems;  //Inventory Storage
    [HideInInspector]
    public List<int> specialOffers; //Specials List
    [HideInInspector]
    public List<int> normalOffers; //Normal Store List

    public Dictionary<int, UnitDict> Units;
    public Dictionary<int, AllItemDict> AllItems;

    //Text locations
    public Image HoverTxt;
    public Text txtBoxTitle;
    public Text txtBoxDesc;
    public Text txtBoxCurrent;
    public Text txtBoxStats;

    //TODO?
    //int titleFontSize = 16;
    //int descFontSize = 12;

    RectTransform rectTransform;
    Image image;
    Font myFont;

    //Local Values
    private int FrontAndCentre; //Save who is in front
    private int FrontAndCentreTile;
    [SerializeField]
    private GameObject ItemDisplay; //TODO show item


    public void Starter() //Start delayed until information is passed in
    {
        //Pass information to menus
        shopMenuCtrl.Dict = AllItems;
        shopMenuCtrl.shopItems = normalOffers;
        shopMenuCtrl.creator();

        spcMenuCtrl.Dict = AllItems;
        spcMenuCtrl.shopItems = specialOffers;
        spcMenuCtrl.creator();
    }

    void Start() //Start with nothing selected
    {
        OnClick(0, 0);
    }

    public void OnClick(int invID, int itemID) //Sale item clicked
    {
        //Set item text to show
        FrontAndCentre = itemID;
        FrontAndCentreTile = invID;

        //display text
        txtBoxTitle.text = AllItems[itemID].Title;
        txtBoxDesc.text = AllItems[itemID].Desc;
        //txtBoxStats

        if (itemID != 0) {txtBoxCurrent.text = "Already Owned: " + countItem(itemID).ToString();}
        else {txtBoxCurrent.text = "";}
    }


    public void Buy() //Purchase button clicked
    {
        if (FrontAndCentreTile == 0) //No item selected
        {
            //play audio error
        }
        else if (FrontAndCentreTile > 0) //Item is selected from normal shop display
        {
            FrontAndCentreTile--; //account for invID being +1
            if (StoredValues.Cash < AllItems[FrontAndCentre].Cost) //You don't have enough money
            {
                //play audio error
            }
            else //you can buy!
            {
                storedItems.Add(FrontAndCentre); //add item to inventory
                StoredValues.Cash -= AllItems[FrontAndCentre].Cost; //you pay for what you buy

                //remove bought item from shop
                GameObject.Find("ShopBtn #" + FrontAndCentreTile).GetComponent<ShopMenuItem>().SetIcon(null);
                GameObject.Find("ShopBtn #" + FrontAndCentreTile).GetComponent<ShopMenuItem>().SetItemID(0);
                GameObject.Find("ShopBtn #" + FrontAndCentreTile).GetComponent<ShopMenuItem>().SetText("0");
                
                //play audio success

                //remove bought item from display
                OnClick(0, 0);

                //Save Inventory
                storedValues.passUp(storedItems); 
                storedValues.save();
            }
        }
        else //item is selected from special shop display
        {
            FrontAndCentreTile = FrontAndCentreTile*-1 -1; //remove negative identifier from selected item

            if (StoredValues.Cash < AllItems[FrontAndCentre].Cost) //You don't have enough money
            {
                //play audio error
            }
            else //you can buy!
            {
                storedItems.Add(FrontAndCentre); //add item to inventory
                StoredValues.Cash -= AllItems[FrontAndCentre].Cost; //you pay for what you buy

                //remove bought item from shop
                GameObject.Find("SpcBtn #" + FrontAndCentreTile).GetComponent<SpcMenuItem>().SetIcon(null);
                GameObject.Find("SpcBtn #" + FrontAndCentreTile).GetComponent<SpcMenuItem>().SetItemID(0);
                GameObject.Find("SpcBtn #" + FrontAndCentreTile).GetComponent<SpcMenuItem>().SetText("0");

                //play audio success

                //remove bought item from display
                OnClick(0, 0);

                //Save Inventory
                storedValues.passUp(storedItems);
                storedValues.save();
            }
        }
    }
    
    private int countItem(int item) //counts total number of owned item, including equipted items
    {
        int total = 0;

        for(int i = 0; i<Units.Count-1 ;i++) //search equipted items
        {
            if(Units[i].item1 == item){total++;}
            if(Units[i].item2 == item){total++;}
            if(Units[i].item3 == item){total++;}
        }
        for (int i = 0; i<storedItems.Count; i++) //search inventory
        {
            if(storedItems[i] == item){total++;}
        }
        return total;
    }  
}