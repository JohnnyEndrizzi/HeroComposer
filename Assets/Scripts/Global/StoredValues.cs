using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class StoredValues : MonoBehaviour {

    Dictionary<int, UnitDict> Units = new Dictionary<int, UnitDict>();
    Dictionary<int, AllItemDict> AllItems = new Dictionary<int, AllItemDict>();


    //Sprites
    [SerializeField]
    private Sprite[] UnitImg = null;
    [SerializeField]
    private Sprite[] ItemImg = null;
     
    [SerializeField]
    private List<int> storedItems; 

    [SerializeField]
    private ShopController ShopCtrl = null; 
    [SerializeField]
    private InvController InvCtrl = null; 

    private List<int> spcOffers = new List<int>();
    private List<int> normOffers = new List<int>();

    //Universal values
    private static int cash;

    public static int Cash{
        get{return cash;}
        set{cash = value;}
    }

    public void Start(){
        load();

        if (ShopCtrl != null) {
            ShopCtrl.Units = Units;
            ShopCtrl.AllItems = AllItems;
            ShopCtrl.storedItems = storedItems;

            findOffers();

            ShopCtrl.specialOffers = spcOffers;
            ShopCtrl.normalOffers = normOffers;

            ShopCtrl.Starter();
        }
        if (InvCtrl != null) {
            InvCtrl.Units = Units;
            InvCtrl.AllItems = AllItems;
            InvCtrl.storedItems = storedItems;

            InvCtrl.Starter();
        }
    }

    private void findOffers(){ //TODO tie into game
        spcOffers.Add(4);
        spcOffers.Add(4);
        spcOffers.Add(3);
        spcOffers.Add(2);
        spcOffers.Add(0);
        spcOffers.Add(0);

        for (int i = 0; i < 16; i++) {
            normOffers.Add(Random.Range(0,5));
        }
    }

    public void passUp(List<int> NewStoredItems){
        storedItems = NewStoredItems;
    }


    public void backup(){
        string pathR = "Assets/storedValues.txt";
        string pathW = "Assets/storedValuesBUP.txt";
        string temp;

        StreamReader reader = new StreamReader(pathR, true);
        StreamWriter writer = new StreamWriter(pathW, true);

        temp = reader.ReadLine();

        while (temp != null) {
            writer.WriteLine(temp);
            temp = reader.ReadLine();
        }
        reader.Close();
        writer.Close();
    }


    private void emptyTxt(){
        string path = "Assets/storedValues.txt";
        using (var stream = new FileStream(path, FileMode.Truncate)){
            using (var writer = new StreamWriter(stream)){
                writer.Write("");
            }
        }
    }


    public void save(){
        //backup(); //TODO Needed?
        emptyTxt();

        string path = "Assets/storedValues.txt";
        string temp;
        int zeroes = 0;

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("UNIT LIST");
        for (int i = 0; i < Units.Count; i++) {
            temp  = Units[i].unitName + ";";
            temp += Units[i].unitDesc + ";";
            temp += Units[i].Unlocked + ";";
            temp += Units[i].item1 + ";";
            temp += Units[i].item2 + ";";
            temp += Units[i].item3 + ";";
            //stats;

            writer.WriteLine(temp);
        }

        writer.WriteLine("NEXT");
        writer.WriteLine("ITEM LIST");
        for (int i = 0; i < AllItems.Count; i++) {
            temp  = AllItems[i].itemName + ";";
            temp += AllItems[i].Title + ";";
            temp += AllItems[i].Desc + ";";
            temp += AllItems[i].Cost + ";";
//            for (int j = 0; j < AllItems[i].itemStats.Length; j++) { //TODO Stats
//                temp += AllItems[i].itemStats[j] + ";";
//            }
            writer.WriteLine(temp);
        }
        writer.WriteLine("NEXT");

        temp = "";
        for (int i = 0; i < storedItems.Count; i++) {
            if (storedItems[i] == 0) {
                zeroes++;
            }
            else {
                for (int j = 0; j < zeroes; j++) {
                    temp += 0 + ";";
                }
                temp += storedItems[i] + ";";
                zeroes = 0;
            }
           
           
         }
         writer.WriteLine(temp);

         writer.Close();
    }


    public void load(){
        string path = "Assets/storedValues.txt";
        string temp;
        string[] tempA;
        int i = 0;

        StreamReader reader = new StreamReader(path, true);

        temp = reader.ReadLine();
        temp = reader.ReadLine();

        //UNIT LIST
        while (temp != "NEXT") {
            tempA = temp.Split(';');
            Units.Add(i,new UnitDict(tempA[0], tempA[1], tempA[2], tempA[3], tempA[4], tempA[5],UnitImg[i]));

            i++;
            temp = reader.ReadLine();
        }
        i = 0;           

        temp = reader.ReadLine();
        temp = reader.ReadLine();

        //ITEM LIST
        while (temp != "NEXT") {
            tempA = temp.Split(';'); 
            AllItems.Add(i, new AllItemDict(tempA[0], tempA[1], tempA[2],ItemImg[i], tempA[3])); //TODO expand with stats

            i++;
            temp = reader.ReadLine();
        }
        i = 0;

        temp = reader.ReadLine();
        tempA = temp.Split(';'); 

        for(int j=0; j<tempA.Length-1; j++){           
            storedItems.Add(int.Parse(tempA[j]));
        }
        reader.Close();
    }
}
            
 
public class UnitDict { //All Units Dictionary 
    public string unitName;
    public string unitDesc;

    public bool Unlocked;

    public int[] stats;

    public int item1;
    public int item2;
    public int item3;

    public Sprite img;

    public UnitDict(string unitNameX, string unitDescX, bool unlockedX, int item1X, int item2X, int item3X) {
        unitName = unitNameX;
        unitDesc = unitDescX;

        Unlocked = unlockedX;

        item1 = item1X;
        item2 = item2X;
        item3 = item3X;
    }

    public UnitDict(string unitNameX, string unitDescX, string unlockedX, string item1X, string item2X, string item3X, Sprite imgX) {
        unitName = unitNameX;
        unitDesc = unitDescX;

        Unlocked = unlockedX == "True";

        item1 = int.Parse(item1X);
        item2 = int.Parse(item2X);
        item3 = int.Parse(item3X);

        img = imgX;
    }


    public UnitDict(bool unlockedX, int item1X, int item2X, int item3X) {
        Unlocked = unlockedX;
        item1 = item1X;
        item2 = item2X;
        item3 = item3X;
    }

    public UnitDict(string UnitNameX, bool unlockedX) {
        Unlocked = unlockedX;
    }

    public UnitDict(string UnitNameX, int item1X, int item2X, int item3X) {
        item1 = item1X;
        item2 = item2X;
        item3 = item3X;
    }
}

public class AllItemDict { //All game items Dictionary 
    public string itemName;
    public string Title;
    public string Desc;
    public int Cost;
    public Sprite img;
    public int[] itemStats;

    public AllItemDict(string itemNameX, string TitleX, string DescX, string CostX) {
        itemName = itemNameX;
        Title = TitleX;
        Desc = DescX;
    }

    public AllItemDict(string itemNameX, string TitleX, string DescX) { //TODO REMOVE
        itemName = itemNameX;
        Title = TitleX;
        Desc = DescX;
    }

    public AllItemDict(string itemNameX, string TitleX, string DescX, Sprite imgX, string CostX) {
        itemName = itemNameX;
        Title = TitleX;
        Desc = DescX;
        img = imgX;
        Cost = int.Parse(CostX);
    }
    public AllItemDict(string itemNameX, string TitleX, Sprite imgX) {
        itemName = itemNameX;
        Title = TitleX;
        img = imgX;
    }
}