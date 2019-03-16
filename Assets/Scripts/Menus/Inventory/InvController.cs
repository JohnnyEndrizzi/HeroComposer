using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Enum for portait indexing
public enum SlotPosition
{
    Top,
    Middle,
    Bottom
}

public class InvController : MonoBehaviour {
    //Main controller for Inventory scene


    /* Functional Requirement 
    * ID: 8.1 1-8
    * Description: The system must display the inventory screen.
    * 
    * This class controls the Inventory screen, it does anything required for the scene to run including 
    * deciding what to display, where to display and any movements of items*/

    /* Functional Requirement 
    * ID: 8.1 1-10
    * Description: The player must be able to customise individual characters.
    * 
    * This class allows players to apply items to their individual characters as desired */

    /* Functional Requirement 
    * ID: 8.1 1-11
    * Description: The system must display the current equipment that each character has.
    * 
    * This class allows players to see what items their characters have equipt */

    //UI Layer
    [SerializeField]
    private UILayer soundChecklUI = null;

    //Gameobject locations
    [SerializeField]
    private GameObject UnitDisplay = null;
    
    //[SerializeField]
    private InvMenuCtrl InventoryMenu = null;
    [SerializeField]
    private CharMenuCtrl UnitMenu = null;

    //Item list is passed in from the StoredVariables class on load
    //[HideInInspector]
    //public List<int> storedItems;  //Inventory Storage

    Dictionary<string, Item> allItems;
    Dictionary<string, Item> invItems;
    Dictionary<string, Character> characters;


    //Equip Button Locations
    [SerializeField]
    private Button BtnTop = null;
    [SerializeField]
    private Button BtnMid = null;
    [SerializeField]
    private Button BtnBottom = null;

    private Button[] Btns = new Button[3];

    //Audio
    private AudioSource audioSource;
    private AudioClip equip;
    private AudioClip pickUp;
    private AudioClip place;
    private AudioClip pickUpChar;

    //Text Locations
    [SerializeField]
    private Image HoverTxt = null;
    [SerializeField]
    private Text txtBoxTitle = null;
    [SerializeField]
    private Text txtBoxDesc = null;

    [SerializeField]
    private DragImg Drag = null;

    //Text Size Variables
    int titleFontSize = 18;
    int descFontSize = 14;

    //RectTransform rectTransform;
    Image image;
    Font myFont;
    
    //Local Values
    private Character frontChar; //Save who is in front
    private Item[] shownItems = new Item[3]; //Save what is in front
    private Item holdItem; //Save what is held


	void Start ()
    {
        //Show UI Layer
        soundChecklUI.Show();

        allItems = GameManager.Instance.gameDataManager.GetItemList();

        //Font
        myFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        txtBoxTitle.fontSize = titleFontSize;
        txtBoxDesc.fontSize = descFontSize;

        //Assign buttons to group
        Btns[0] = BtnTop;
        Btns[1] = BtnMid;
        Btns[2] = BtnBottom;

        //Equipted items buttons
        Btns[0].onClick.AddListener(delegate {EqpOnClick(Btns[0], 1);});
        Btns[1].onClick.AddListener(delegate {EqpOnClick(Btns[1], 2);});
        Btns[2].onClick.AddListener(delegate {EqpOnClick(Btns[2], 3);});

        //Import Audio
        audioSource = GetComponent<AudioSource>();
        equip = (AudioClip)Resources.Load("SoundEffects/inventory_equip_item");
        pickUp = (AudioClip)Resources.Load("SoundEffects/inventory_pick_up_item");
        place = (AudioClip)Resources.Load("SoundEffects/inventory_place_item_back");
        pickUpChar = (AudioClip)Resources.Load("SoundEffects/rehersal_pick_up_character");        
    }

    void Update()
    {        
        if (Input.GetMouseButtonUp(1))
        {
            loadInv(frontChar);  //Drop held item and reload menus on right click
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LastScene.instance.prevScene = "Inventory";
            GameObject.Find("CurtainsOpenTransition").GetComponent<Curtain>().Close();
        }
    }

    //Populate character list scrollbox
    private void LoadInvList()
    {        
        characters = GameManager.Instance.gameDataManager.GetCharacters();

        invItems = GameManager.Instance.gameDataManager.GetInvItems();
        foreach (Item item in invItems.Values)
        {
            //TODO stuff
        }

        //InventoryMenu.storedItems = storedItems;
        InventoryMenu.Creator();
    }

    public void loadInv(Character SelChar) //Loads last saved data and brings a selected Character to the front
    {        
        if (frontChar != SelChar)
        {
            audioSource.PlayOneShot(pickUpChar, 0.5F);
        }

        frontChar = SelChar;
        DropHeld();

        //Set equip buttons icons and information
        for (int i = 0; i<3; i++)
        {
            SetEquiptButtons(i , SelChar.name);
        }
        //TODO SelName int -> String

        //InventoryMenu.storedItems = storedItems;
        InventoryMenu.Creator();
    }

    void SetEquiptButtons(int slotNum, string SelName)
    {
        if (characters[SelName].equippedItems[slotNum.ToString()] != null)
        {
            shownItems[slotNum] = invItems[characters[SelName].equippedItems[slotNum.ToString()].name];
            Btns[slotNum].GetComponent<BtnEquipt>().SetIcon(Resources.Load<Sprite>(shownItems[slotNum].sprite));
        }
    }
 


    public void SaveInv() //takes currently displayed items and saves to global
    {
        for (int i = 0; i < 3; i++)
        {
            characters[frontChar.name].equippedItems[i.ToString()] = shownItems[i];
        }

        GameManager.Instance.gameDataManager.SetCharacters(characters);

        //If character is in party, write to it as well.
        if (GameManager.Instance.gameDataManager.IsCharacterInParty(frontChar))
        {
            Dictionary<int, Character> party = GameManager.Instance.gameDataManager.GetCharactersInParty();

            for (int j = 0; j < 4; j++)
            {
                if (party[j] != null && party[j].name == frontChar.name) {
                    party[j] = characters[frontChar.name];
                }
            }            
            GameManager.Instance.gameDataManager.SetCharactersInParty(party);
        }

        GameManager.Instance.gameDataManager.SaveInvItems(invItems);

        //TODO make sure FrontChar, char and SelChar are synced up when needed       
    }

    public void SetImage(Sprite img) //Sets portrait image
    {
        UnitDisplay.GetComponent<Image>().sprite = img;
    }




    void DropHeld() // Drop held item
    {
        if (holdItem != null)
        {
            audioSource.PlayOneShot(place, 0.7F);
        }
        Drag.SetIcon(null);
        holdItem = null;        
    }
    
    public void GridOnClick(int intID, string itemID)  //Grid of Inventory items clicked
    {
        Item SelectedItem = allItems[itemID];

        if (Drag.Dragging() == false) //pick up item 
        {          
            Drag.SetIcon(GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(null);
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(null);

            holdItem = SelectedItem;
            audioSource.PlayOneShot(pickUp, 0.7F);

        }
        else if(Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == false) //place item
        {
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(holdItem.name);
            Drag.SetIcon(null);

            holdItem = null;
            audioSource.PlayOneShot(place, 0.7F);
            SaveInv();
        }  
        else if (Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == true) //switch item
        { 
            Sprite tempS = GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon();

            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(holdItem.name);
            Drag.SetIcon(tempS);

            holdItem = SelectedItem;
            audioSource.PlayOneShot(place, 0.7F);
            SaveInv();
        }         
    }

    void EqpOnClick(Button origin, int item) //One of Equipt buttons clicked
    {
        if (Drag.Dragging() == false) //pick up item    
        {      
            Drag.SetIcon(origin.GetComponent<BtnEquipt>().GetIcon());
            origin.GetComponent<BtnEquipt>().SetIcon(null);  

            holdItem = shownItems[item];
            audioSource.PlayOneShot(pickUp, 0.7F);
            shownItems[item] = null;

        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == false) //place item
        {
            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(null);

            shownItems[item] = holdItem;
            holdItem = null;
            audioSource.PlayOneShot(equip, 0.7F);
            SaveInv();
        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == true) //switch item
        {
            Sprite tempS = origin.GetComponent<BtnEquipt>().GetIcon();
            Item tempN = holdItem;

            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(tempS);

            holdItem = shownItems[item];
            shownItems[item] = tempN;
            audioSource.PlayOneShot(equip, 0.7F);
            SaveInv();
        }
    }


    

    //Text control  
    void ReWriter(Item itemInfo) //Update Text
    {
        txtBoxTitle.text = itemInfo.name;
        txtBoxDesc.text = itemInfo.description;             

        RectTransform rectTransform = HoverTxt.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(170, 200);

        if (StringLength(itemInfo.description, descFontSize) == 0 && StringLength(itemInfo.name, titleFontSize) == 0){ 
            rectTransform.sizeDelta = new Vector2(0, 200);
        }        
    }

    int StringLength(string s, int size) //get the amount of physical space the string will take up 
    {
        int totalLength = 0;
        CharacterInfo characterInfo = new CharacterInfo();

        if (s.Equals(null)) { s = ""; }
        char[] chars = s.ToCharArray();

        foreach(char c in chars){
            myFont.RequestCharactersInTexture(c.ToString(), size, txtBoxTitle.fontStyle);
            myFont.GetCharacterInfo(c, out characterInfo, size);
            totalLength += characterInfo.advance;
        }
        return totalLength;
    }

    public void HoverTextFadeIn(string origin)
    {
        if (origin.Equals("ButtonTop")) {       ReWriter(allItems[shownItems[1].name]); }
        else if (origin.Equals("ButtonMid")) {  ReWriter(allItems[shownItems[2].name]); }
        else if (origin.Equals("ButtonBottom")) { ReWriter(allItems[shownItems[3].name]); }
        else { ReWriter(allItems[origin]); }
        
        StartCoroutine(GraphicFader(0.2f, HoverTxt, 0.75f));
        StartCoroutine(GraphicFader(0.2f, txtBoxTitle, 1f));
        StartCoroutine(GraphicFader(0.2f, txtBoxDesc, 1f));
    }
    public void HoverTextFadeOut(string origin)
    {
        StartCoroutine(GraphicFader(1f, HoverTxt, 0f));
        StartCoroutine(GraphicFader(1f, txtBoxTitle, 0f));
        StartCoroutine(GraphicFader(1f, txtBoxDesc, 0f));

        if (origin.Equals("ButtonTop")) {       ReWriter(allItems[shownItems[1].name]); }
        else if (origin.Equals("ButtonMid")) {  ReWriter(allItems[shownItems[2].name]); }
        else if (origin.Equals("ButtonBottom")) { ReWriter(allItems[shownItems[3].name]); }
        else { ReWriter(allItems[origin]); }
    }
       
    IEnumerator GraphicFader(float transitionTime, Graphic fadeObject, float fadeEnd) //Graphic fading in or out
    { //0 to invisible, 1 to visible
        Color c = fadeObject.material.color;
        float fadeStart = c.a;

        int mult = (int)Mathf.Sign(fadeStart - fadeEnd);

        for (float f = fadeStart; f >= fadeEnd * mult; f -= 0.1f * transitionTime)
        {
            c.a = (f - 0.1f < fadeEnd * mult) ? fadeEnd : f * mult;
            fadeObject.material.color = c;
            yield return null;
        }
    }
}