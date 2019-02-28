using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StoredValues : MonoBehaviour
{
    /* Persistant script used to pass information to where it is needed
     * Used to create Unit and Item Dictionaries
     * Passes information to scene controllers
     * Passes information to save files
     * Keeps track of your money, inventory items, and shop lists */

    /* Functional Requirement 
    * ID: 8.2-9
    * Description: The player must be able to save the game. 
    * 
    * This class controls the saving of all data during runtime as well as exporting to save files  */

    /* Functional Requirement 
    * ID: 8.2-10
    * Description: The player must be able to load a saved game.
    * 
    * This class controls the loading of all data during runtime as well as exporting to save files */

    //Game Dictionaries for Units and Item
    Dictionary<int, UnitDict> Units = new Dictionary<int, UnitDict>();
    Dictionary<int, AllItemDict> AllItems = new Dictionary<int, AllItemDict>();
     
    //Inventory 
    [SerializeField]
    private List<int> storedItems; 

    //Scene Controllers
    private ShopController ShopCtrl = null; 
    private InvController InvCtrl = null;
    private RehController RehCtrl = null;

    //Shop lists
    private List<int> spcOffers = new List<int>();
    private List<int> normOffers = new List<int>();

    public static StoredValues instance = null;

    //Universal values
    private static int cash;

    //Get/Set money
    public static int Cash{
        get{return cash;}
        set{cash = value;}
    }

    public void CashL(int value)
    {       
        cash = value;
    }
    public int CashS()
    {
        return cash;
    }

    //Persist and prevent multiple instances
    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        
    }

    private void Start()
    {
        cash += 100000; //TODO remove free money :(

        /* Deserializes all information from their corresponding JSON into local copies */
        if (GetComponent<LoadData>() != null && Assets.Scripts.MainMenu.ApplicationModel.loadedCharacters == false)
        {
            Assets.Scripts.MainMenu.ApplicationModel.loadedCharacters = true;
            GetComponent<LoadData>().LoadCharacters();
            GetComponent<LoadData>().LoadItems();
            GetComponent<LoadData>().LoadInv();
            GetComponent<LoadData>().LoadLevels();
            /* TODO */
            //GetComponent<LoadData>().LoadMagic();
        }
    }

    /* Currently using depreciated code, this will be replaced by current code in future
     * Acts the same as Start(), but runs whenever a new scene is loaded
     * Both start and awake will only run once for a persistant object */
    private void OnLevelWasLoaded() 
    {
        Starter();
    }

    /* Finds any sceneControllers present in a scene and passes information 
     * into them and calling their start functions */
    private void Starter() //Start
    {  
        //load();

        //Find if any scene controllers are present
        try
        { InvCtrl = GameObject.Find("InvController").GetComponent<InvController>(); }
        catch { }
        try
        { ShopCtrl = GameObject.Find("ShopController").GetComponent<ShopController>(); }
        catch { }
        try
        { RehCtrl = GameObject.Find("RehController").GetComponent<RehController>(); }
        catch { }

        //Pass information and start any existing scene controllers
        if (ShopCtrl != null) {
            ShopCtrl.Units = Units;
            ShopCtrl.AllItems = AllItems;
            ShopCtrl.storedItems = storedItems;

            findOffers(); 

            ShopCtrl.specialOffers = spcOffers;
            ShopCtrl.normalOffers = normOffers;

            ShopCtrl.Starter();            
        }
        else if (InvCtrl != null) {
            InvCtrl.Units = Units;
            InvCtrl.AllItems = AllItems;
            InvCtrl.storedItems = storedItems;

            InvCtrl.Starter();
        }
        else if (RehCtrl != null)
        {
            RehCtrl.Units = Units;
            RehCtrl.AllItems = AllItems;

            RehCtrl.Starter();
        }
    }
    
    private void findOffers(){ //Temporary setting of shop lists //TODO
        spcOffers.Clear();
        spcOffers.Add(9);
        spcOffers.Add(0);
        spcOffers.Add(10);
        spcOffers.Add(0);
        spcOffers.Add(11);
        spcOffers.Add(0);

        normOffers.Clear();
        normOffers.Add(1);
        normOffers.Add(1);
        normOffers.Add(2);
        normOffers.Add(2);
        normOffers.Add(3);
        normOffers.Add(3);
        normOffers.Add(4);
        normOffers.Add(4);
        normOffers.Add(5);
        normOffers.Add(5);
        normOffers.Add(6);
        normOffers.Add(6);
        normOffers.Add(0);
        normOffers.Add(0);

        /*for (int i = 0; i < 16; i++) {
            normOffers.Add(Random.Range(0,5));
        }*/
    }

    public void passUp(List<int> NewStoredItems) //Allows subscripts to modify storedItems
    {
        storedItems = NewStoredItems;
    }
    public int[] GetStoredItems() //Allows save script to save inventory
    {
        return storedItems.ToArray();
    }

    //Import Unit information from JSON Save files
    public void importUnits(int i, string unitName, string unitDesc, string unitSprite, string unitSound, int[] eqp, bool unlock, int[] stats, string mag)
    {
        if (!Units.ContainsKey(i))
        {         
            Units.Add(i, new UnitDict(unitName, unitDesc, unlock, eqp[0], eqp[1], eqp[2], Resources.Load<Sprite>(unitSprite), Resources.Load<AudioClip>(unitSound), stats, mag));
        }        
    }

    //Import Item information from JSON Save files
    public void importItems(int i, string itemName, string itemTitle, string itemDesc, string itemSprite, int itemCost)
    {
        if (!AllItems.ContainsKey(i))
        {
            AllItems.Add(i, new AllItemDict(itemName, itemTitle, itemDesc, Resources.Load<Sprite>(itemSprite), itemCost)); //TODO expand with stats        
        }
    }
    //Import Inventory information from JSON Save files
    public void importInventory(string[] inventoryLoad)
    {
        for (int i = 0; i < inventoryLoad.Length; i++){
            storedItems.Add(int.Parse(inventoryLoad[i]));
        }
    }

    //Create an empty item for any item slot that contains nothing
    public void nullItem()
    {
        if (!AllItems.ContainsKey(0))
        {
            AllItems.Add(0, new AllItemDict("", "", "", "0", null));             
        }
    }

    //Create an empty unit for any unit slot that contains nothing
    public void nullUnit()  
    {
        if (!Units.ContainsKey(-1)) 
        {
            Units.Add(-1, new UnitDict("", "", false, null, null));  
        }
    }

    public void saveInv()
    {        
        GetComponent<SaveData>().SaveInv(true, false, true);
    }
    public void saveChar()
    {
        GetComponent<SaveData>().SaveCharacters();
    }

    public void SetTeam(string[] chars) //Takes unit names - locates characterScripts - sets those to load on play
    {
        ClearTeam();

        Object[] characters = Resources.LoadAll("ScriptableObjects/Characters/", typeof(CharacterScriptObject));
        for (int i = 0; i < characters.Length; i++)
        {
            CharacterScriptObject currentCharacterSO = (CharacterScriptObject)characters[i];
            if (characters[i])
            {
                if (currentCharacterSO.name == chars[0])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[0] = currentCharacterSO;
                }
                else if (currentCharacterSO.name == chars[1])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[1] = currentCharacterSO;
                }
                else if (currentCharacterSO.name == chars[2])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[2] = currentCharacterSO;
                }
                else if (currentCharacterSO.name == chars[3])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[3] = currentCharacterSO;
                }
            }
        }
        GetComponent<SaveData>().SaveInv(false, true, false);
    }

    private void ClearTeam() //Set team to null before setting new team
    {
        Assets.Scripts.MainMenu.ApplicationModel.characters[0] = null;
        Assets.Scripts.MainMenu.ApplicationModel.characters[1] = null;
        Assets.Scripts.MainMenu.ApplicationModel.characters[2] = null;
        Assets.Scripts.MainMenu.ApplicationModel.characters[3] = null;
    }
}


public class UnitDict { //All Units Dictionary 
    public string unitName;
    public string unitDesc;

    //Is unit unlocked?
    public bool Unlocked;

    public int[] stats;

    //Equipted items
    public int item1;
    public int item2;
    public int item3;

    //Equipted magic skill
    public string mag_eqp;

    public Sprite img;
    public AudioClip sound;

    public UnitDict(string unitNameX, string unitDescX, bool unlockedX, int item1X, int item2X, int item3X, Sprite imgX, AudioClip soundX, int[] statsX, string mag_eqpX)
    {
        unitName = unitNameX;
        unitDesc = unitDescX;
        Unlocked = unlockedX ;

        item1 = item1X;
        item2 = item2X;
        item3 = item3X;

        img = imgX;
        sound = soundX;

        stats = statsX;
        mag_eqp = mag_eqpX;
    }
    public UnitDict(string unitNameX, string unitDescX, bool unlockedX, Sprite imgX, AudioClip soundX) //nullUnit
    {
        unitName = unitNameX;
        unitDesc = unitDescX;
        Unlocked = unlockedX;

        img = imgX;
        sound = soundX;
    }
}

public class AllItemDict { //All game items Dictionary 
    public string itemName; //keyName
    public string Title;    //Display Name
    public string Desc;     
    public int Cost;
    public Sprite img;
    public int[] Stats;

    public AllItemDict(string itemNameX, string TitleX, string DescX, Sprite imgX, int CostX, int[] statsX)
    {
        itemName = itemNameX;
        Title = TitleX;
        Desc = DescX;
        img = imgX;
        Cost = CostX;
        Stats = statsX;
    }
    public AllItemDict(string itemNameX, string TitleX, string DescX, string CostX, Sprite imgX) {  //nullItem
        itemName = itemNameX;
        Title = TitleX;
        Desc = DescX;
        img = imgX;
    }
    public AllItemDict(string itemNameX, string TitleX, string DescX, Sprite imgX, string CostX) { //Remove
        itemName = itemNameX;
        Title = TitleX;
        Desc = DescX;
        img = imgX;
        Cost = int.Parse(CostX);
    }
    public AllItemDict(string itemNameX, string TitleX, string DescX, Sprite imgX, int CostX){
        itemName = itemNameX;
        Title = TitleX;
        Desc = DescX;
        img = imgX;
        Cost = CostX;
    }
}        