using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

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


    public void Starter(){
        shopMenuCtrl.Dict = AllItems;
        shopMenuCtrl.shopItems = normalOffers;
        shopMenuCtrl.creator();

        spcMenuCtrl.Dict = AllItems;
        spcMenuCtrl.shopItems = specialOffers;
        spcMenuCtrl.creator();
    }


    public void OnClick(int invID, int itemID){
        FrontAndCentre = itemID;
        FrontAndCentreTile = invID;

        txtBoxTitle.text = AllItems[itemID].Title;
        txtBoxDesc.text = AllItems[itemID].Desc;
        //txtBoxStats

        if (itemID != 0) {txtBoxCurrent.text = "Already Owned: " + countItem(itemID).ToString();}
        else {txtBoxCurrent.text = "";}
    }


    public void Buy(){
        if (FrontAndCentreTile == 0) {
            //TODO play audio error
        }
        else if (FrontAndCentreTile > 0) {
            FrontAndCentreTile--;
            if (StoredValues.Cash < AllItems[FrontAndCentre].Cost) {
                //TODO play audio error
            }
            else {
                storedItems.Add(FrontAndCentre);
                StoredValues.Cash -= AllItems[FrontAndCentre].Cost;

                GameObject.Find("ShopBtn #" + FrontAndCentreTile).GetComponent<ShopMenuItem>().SetIcon(null);
                GameObject.Find("ShopBtn #" + FrontAndCentreTile).GetComponent<ShopMenuItem>().SetItemID(0);
                GameObject.Find("ShopBtn #" + FrontAndCentreTile).GetComponent<ShopMenuItem>().SetText("0");
                //TODO play audio success

                OnClick(0, 0);
                storedValues.passUp(storedItems); //TODO Fix
                storedValues.save();//TODO fix
            }
        }
        else {
            FrontAndCentreTile = FrontAndCentreTile*-1 -1;

            if (StoredValues.Cash < AllItems[FrontAndCentre].Cost) {
                //TODO play audio error
            }
            else {
                storedItems.Add(FrontAndCentre);
                StoredValues.Cash -= AllItems[FrontAndCentre].Cost;

                GameObject.Find("SpcBtn #" + FrontAndCentreTile).GetComponent<SpcMenuItem>().SetIcon(null);
                GameObject.Find("SpcBtn #" + FrontAndCentreTile).GetComponent<SpcMenuItem>().SetItemID(0);
                GameObject.Find("SpcBtn #" + FrontAndCentreTile).GetComponent<SpcMenuItem>().SetText("0");
                //TODO play audio success

                OnClick(0, 0);
                storedValues.passUp(storedItems);
                storedValues.save();
            }
        }
    }


    private int countItem(int item){
        int total = 0;

        for(int i = 0; i<Units.Count ;i++){
            if(Units[i].item1 == item){total++;}
            if(Units[i].item2 == item){total++;}
            if(Units[i].item3 == item){total++;}
        }
        for (int i = 0; i<storedItems.Count; i++) {
            if(storedItems[i] == item){total++;}
        }
        return total;
    }

    void Start () {   
        StoredValues.Cash += 5000; //TODO TEMP cash
        OnClick(0, 0);
    }
}