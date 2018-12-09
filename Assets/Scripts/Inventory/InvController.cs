using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    //Gameobject locations
    [SerializeField]
    private GameObject UnitDisplay = null;
    [SerializeField]
    private DragImg Drag = null; 
    [SerializeField]
    private StoredValues storedValues = null;
    [SerializeField]
    private InvMenuCtrl InventoryMenu = null;
    [SerializeField]
    private CharMenuCtrl UnitMenu = null;

    //Item list is passed in from the StoredVariables class on load
    [HideInInspector]
    public List<int> storedItems;  //Inventory Storage

    /*Dictionary lists are passed in from the StoredVariables class on load,
    * they contain all information about all Units, what is equipt to who and all Items present in the game */
    public Dictionary<int, UnitDict> Units;
    public Dictionary<int, AllItemDict> AllItems;

    //Equip Button Locations
    [SerializeField]
    private Button BtnTop = null;
    [SerializeField]
    private Button BtnMid = null;
    [SerializeField]
    private Button BtnBottom = null;

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

    //Text Size Variables
    int titleFontSize = 18;
    int descFontSize = 14;

    RectTransform rectTransform;
    Image image;
    Font myFont;
    
    //Local Values
    private int FrontAndCentre; //Save who is in front
    private int[] it = new int[]{ 0, 0, 0, 0 }; //Save what is in front
    private int HoldNum; //Save what is held

    /* Behaves similar to a start function but will only act after needed information has been passed in from above.
     * It passes any needed information to its sub-menus and tells them to start upon recieving their information */
    public void Starter() 
    {
        //Pass information to menus
        InventoryMenu.AllItems = AllItems;
        InventoryMenu.storedItems = storedItems;
        InventoryMenu.creator();

        UnitMenu.Units = Units;
        UnitMenu.creator();

        //place first unit into centre portrait
        loadInv(0);
        setImage(Units[0].img);
    }


	void Start ()
    {        
        //Font
        myFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        txtBoxTitle.fontSize = titleFontSize;
        txtBoxDesc.fontSize = descFontSize;

        //Equipted items buttons
        BtnTop.onClick.AddListener(delegate {EqpOnClick(BtnTop, 1);});	
        BtnMid.onClick.AddListener(delegate {EqpOnClick(BtnMid, 2);});
        BtnBottom.onClick.AddListener(delegate {EqpOnClick(BtnBottom, 3);});

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
            loadInv(FrontAndCentre);  //Drop held item and reload menus
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LastScene.instance.prevScene = "Inventory";
            GameObject.Find("CurtainsOpenTransition").GetComponent<CurtainMovementQuick>().closeCurtains("Menu");
        }
    }
        
    public void loadInv(int SelName) //Loads last saved data and brings a selected Character to the front
    {        
        if (FrontAndCentre != SelName)
        {
            audioSource.PlayOneShot(pickUpChar, 0.5F);
        }

        FrontAndCentre = SelName;
        DropHeld();

        //Set equip buttons icons
        BtnTop.GetComponent<BtnEquipt>().SetIcon(AllItems[Units[SelName].item1].img);
        BtnMid.GetComponent<BtnEquipt>().SetIcon(AllItems[Units[SelName].item2].img);
        BtnBottom.GetComponent<BtnEquipt>().SetIcon(AllItems[Units[SelName].item3].img);

        //Local save of currently displayed equipt items
        it[1] = Units[SelName].item1;
        it[2] = Units[SelName].item2;
        it[3] = Units[SelName].item3;

        //Pass and regenerate inventory menu
        InventoryMenu.storedItems = storedItems;
        InventoryMenu.creator();
    }

    public void saveInv() //takes currently displayed items and saves to global
    {
        //Saves equipt items
        Units[FrontAndCentre].item1 = it[1]; 
        Units[FrontAndCentre].item2 = it[2];
        Units[FrontAndCentre].item3 = it[3];

        //Save inventory items
        storedItems = InventoryMenu.GetStoredItems();
        storedValues.passUp(storedItems);
        storedValues.save();
    }

    void DropHeld() // Drop held item
    {
        if (HoldNum != 0)
        {
            audioSource.PlayOneShot(place, 0.7F);
        }
        Drag.SetIcon(null);
        HoldNum = 0;        
    }

    public void setImage(Sprite img) //Sets portrait image
    {
        UnitDisplay.GetComponent<Image>().sprite = img;
    }


    public void GridOnClick(int intID, int itemID)  //Grid of Inventory items clicked
    {
        if (Drag.Dragging() == false) //pick up item 
        {          
            Drag.SetIcon(GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(null);
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(0);

            HoldNum = itemID;
            audioSource.PlayOneShot(pickUp, 0.7F);

        }
        else if(Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == false) //place item
        {
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(HoldNum);
            Drag.SetIcon(null);

            HoldNum = 0;
            audioSource.PlayOneShot(place, 0.7F);
            saveInv();
        }  
        else if (Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == true) //switch item
        { 
            Sprite tempS = GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon();

            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(HoldNum);
            Drag.SetIcon(tempS);

            HoldNum = itemID;
            audioSource.PlayOneShot(place, 0.7F);
            saveInv();
        }         
    }

    void EqpOnClick(Button origin, int item) //One of Equipt buttons clicked
    {
        if (Drag.Dragging() == false) //pick up item    
        {      
            Drag.SetIcon(origin.GetComponent<BtnEquipt>().GetIcon());
            origin.GetComponent<BtnEquipt>().SetIcon(null);  

            HoldNum = it[item];
            audioSource.PlayOneShot(pickUp, 0.7F);
            it[item] = 0;

        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == false) //place item
        {
            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(null);

            it[item] = HoldNum;
            HoldNum = 0;
            audioSource.PlayOneShot(equip, 0.7F);
            saveInv();
        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == true) //switch item
        {
            Sprite tempS = origin.GetComponent<BtnEquipt>().GetIcon();
            int tempN = HoldNum;

            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(tempS);

            HoldNum = it[item];
            it[item] = tempN;
            audioSource.PlayOneShot(equip, 0.7F);
            saveInv();
        }
    }

    //Text control  
    void ReWriter(string title, string desc) //Update Text
    {
        txtBoxTitle.text = title;
        txtBoxDesc.text = desc;             

        rectTransform = HoverTxt.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(170, 200);

        if (StringLength(desc, descFontSize) == 0 && StringLength(title, titleFontSize) == 0){ 
            rectTransform.sizeDelta = new Vector2(0, 200);
        }        
    }

    int StringLength(string s, int size) //get the amount of size the string will take up using 
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

    public void HoverText(int origin, int itemID) //An item has triggered OnPointerEnter or OnPointerExit - text is changed accordingly
    {
        HoverTxt.gameObject.SetActive(true); //starts disabled

        if (origin > 0) //hoverEnter - fade in
        {    
            origin--;
            ReWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            StartCoroutine(TextFader(1f, txtBoxTitle, txtBoxDesc, 1f, 0f)); 
        }
        else if (origin < 0) //hoverExit - fade out
        {
            origin = Mathf.Abs(origin) - 1;
            ReWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            HoverTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200);
            StartCoroutine(TextFader(3f, txtBoxTitle, txtBoxDesc, 0f, 1f));
        }
        else //catch
        {
            ReWriter(AllItems[origin].Title, AllItems[origin].Desc);
        }
    }

    public void HoverText(string origin, int check) //An equip button has triggered OnPointerEnter or OnPointerExit - text is changed accordingly
    {
        HoverTxt.gameObject.SetActive(true); //starts disabled

        //Sets text to item in origin button
        if (origin.Equals("ButtonTop")){       ReWriter(AllItems[it[1]].Title, AllItems[it[1]].Desc);}
        else if(origin.Equals("ButtonMid")){   ReWriter(AllItems[it[2]].Title, AllItems[it[2]].Desc);}
        else if(origin.Equals("ButtonBottom")){ReWriter(AllItems[it[3]].Title, AllItems[it[3]].Desc);}
        else {Debug.Log("ERROR, hover origin unknown");}


        if (check > 0) //hoverEnter - fade in
        {           
            StartCoroutine(TextFader(1f, txtBoxTitle, txtBoxDesc, 1f,0f)); 
        }
        else if (check < 0) //hoverEnter - fade out
        {
            HoverTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200);
            StartCoroutine(TextFader(3f, txtBoxTitle, txtBoxDesc, 0f,1f));
        }
    }

    IEnumerator TextFader(float t, Text i, Text i2, float fader, float fader2) //Controls the text fading in or out for 2 text objects
    { //0 to invisible, 1 to visible - fader2 is opposite of fader
        i.color = new Color(i.color.r, i.color.g, i.color.b, fader);
        i2.color = new Color(i2.color.r, i2.color.g, i2.color.b, fader);
        while (i.color.a < 1.0f && i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t)*fader + (Time.deltaTime / t)*fader);
            i2.color = new Color(i2.color.r, i2.color.g, i2.color.b, i2.color.a - (Time.deltaTime / t)*fader + (Time.deltaTime / t)*fader);
            yield return null;
        }
    }
}