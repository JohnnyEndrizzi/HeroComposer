using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {
    //Main controller for Music Shop scene

    /* Functional Requirement 
    * ID: 8.1 1-9
    * Description: The player must be able to purchase new equipment
    * 
    * This class controls all purchasing of items and equipment, it then adds them to the global
    * owned items list */

    //Gameobject locations
    [SerializeField]
    private ShopMenuCtrl shopMenuCtrl = null;
    [SerializeField]
    private SpcMenuCtrl spcMenuCtrl = null;
    [SerializeField]
    private StoredValues storedValues = null;

    //Item lists are passed in from the StoredVariables class on load
    [HideInInspector]
    public List<int> storedItems;  //Inventory Storage
    [HideInInspector]
    public List<int> specialOffers; //Specials List
    [HideInInspector]
    public List<int> normalOffers; //Normal Store List

    /*Dictionary lists are passed in from the StoredVariables class on load,
     * they contain all information about all Units and Items present in the game */ 
    public Dictionary<int, UnitDict> Units;
    public Dictionary<int, AllItemDict> AllItems;

    //Text gameobject locations
    public Image HoverTxt;
    public Text txtBoxTitle;
    public Text txtBoxDesc;
    public Text txtBoxCurrent;
    public Text txtBoxStats;

    //Audio
    private AudioSource audioSource;
    private AudioClip error;
    private AudioClip normalPurchase;
    private AudioClip specialPurchase;
    private AudioClip pickUp;

    //Font size 
    int titleFontSize = 16;
    int descFontSize = 12;

    RectTransform rectTransform;
    Image image;
    Font myFont;

    //Local Values
    private int FrontAndCentre; //Save who is in front
    private int FrontAndCentreTile; //Save where the front item comes from
    [SerializeField]
    private GameObject ItemDisplay; //Shown item image display location

    /* Behaves similar to a start function but will only act after needed information has been passed in from above.
     * It passes any needed information to its sub-menus and tells them to start upon recieving their information */ 
    public void Starter() 
    {
        storedValues = GameObject.Find("__app").GetComponent<StoredValues>();

        shopMenuCtrl.Dict = AllItems;
        shopMenuCtrl.shopItems = normalOffers;
        shopMenuCtrl.creator();

        spcMenuCtrl.Dict = AllItems;
        spcMenuCtrl.shopItems = specialOffers;
        spcMenuCtrl.creator();
    }

    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            LastScene.instance.prevScene = "shop";
            GameObject.Find("CurtainsOpenTransition").GetComponent<CurtainMovementQuick>().closeCurtains("Menu");
        }
    }

    void Start() //Start with nothing selected
    {
        OnClick(0, 0);

        //Import Audio
        audioSource = GetComponent<AudioSource>();
        error = (AudioClip)Resources.Load("SoundEffects/shop_not_enough_money");
        normalPurchase = (AudioClip)Resources.Load("SoundEffects/shop_purchase_regular_item");
        specialPurchase = (AudioClip)Resources.Load("SoundEffects/shop_purchase_special_item");
        pickUp = (AudioClip)Resources.Load("SoundEffects/inventory_pick_up_item");
    }

    public void OnClick(int invID, int itemID) //Sale item clicked
    {
        if (FrontAndCentreTile != invID)
        {
            audioSource.PlayOneShot(pickUp, 0.7F);
        }

        //Set item text to show
        FrontAndCentre = itemID;
        FrontAndCentreTile = invID;

        //display text
        txtBoxTitle.text = AllItems[itemID].Title;
        txtBoxDesc.text = AllItems[itemID].Desc;
        //txtBoxStats

        //Display how many of that item the player currently owns
        if (itemID != 0) {txtBoxCurrent.text = "Already Owned: " + countItem(itemID).ToString();}
        else {txtBoxCurrent.text = "";}
    }
    
    public void Buy() //Purchase button clicked
    {
        if (FrontAndCentreTile == 0) //No item selected
        {
            //play audio error
            audioSource.PlayOneShot(error, 0.7F);
        }
        else if (FrontAndCentreTile > 0) //Item is selected from normal shop display
        {
            FrontAndCentreTile--; //account for invID being +1
            if (StoredValues.Cash < AllItems[FrontAndCentre].Cost) //You don't have enough money
            {
                //play audio error
                audioSource.PlayOneShot(error, 0.7F);
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
                audioSource.PlayOneShot(normalPurchase, 0.7F);

                //remove bought item from display
                OnClick(0, 0);

                //Save Inventory
                storedValues.passUp(storedItems);
                storedValues.saveInv();
            }
        }
        else //item is selected from special shop display
        {
            FrontAndCentreTile = FrontAndCentreTile*-1 -1; //remove negative identifier from selected item

            if (StoredValues.Cash < AllItems[FrontAndCentre].Cost) //You don't have enough money
            {
                //play audio error
                audioSource.PlayOneShot(error, 0.7F);
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
                audioSource.PlayOneShot(specialPurchase, 0.7F);

                //remove bought item from display
                OnClick(0, 0);

                //Save Inventory
                storedValues.passUp(storedItems);
                storedValues.saveInv();
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