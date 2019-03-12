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

    /*Dictionary lists are passed in from the StoredVariables class on load,
    * they contain all information about all Units, what is equipt to who and all Items present in the game */
    //public Dictionary<int, UnitDict> Units;
    //public Dictionary<int, AllItemDict> AllItems;

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

    RectTransform rectTransform;
    Image image;
    Font myFont;
    
    //Local Values
    private Character FrontChar; //Save who is in front
    private Item[] shownItems = new Item[3]; //Save what is in front
    private Item HoldItem; //Save what is held

    /* Behaves similar to a start function but will only act after needed information has been passed in from above.
     * It passes any needed information to its sub-menus and tells them to start upon recieving their information */
    public void Starter() 
    {
        //storedValues = GameObject.Find("__app").GetComponent<StoredValues>();

        //Pass information to menus
        //InventoryMenu.AllItems = AllItems;
        //InventoryMenu.storedItems = storedItems;
        //InventoryMenu.Creator();

        //UnitMenu.Units = Units;
        //UnitMenu.Creator();

        //place first unit into centre portrait
        //loadInv(0);
        //setImage(Units[0].img);
    }


	void Start ()
    {
        //Show UI Layer
        soundChecklUI.Show();

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
        
        Btns[0] = BtnTop;
        Btns[1] = BtnMid;
        Btns[2] = BtnBottom;
}

    void Update()
    {        
        if (Input.GetMouseButtonUp(1))
        {
            loadInv(FrontChar);  //Drop held item and reload menus
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
        if (FrontChar != SelChar)
        {
            audioSource.PlayOneShot(pickUpChar, 0.5F);
        }

        FrontChar = SelChar;
        DropHeld();

        //Set equip buttons icons and information
        for (int i = 0; i<3; i++)
        {
            SetEquiptButtons(i , SelChar.name);
        }
        //TODO SelName int -> String

        
        

        //Pass and regenerate inventory menu
        //InventoryMenu.storedItems = storedItems;
        InventoryMenu.Creator();
    }

    void SetEquiptButtons(int slotNum, string SelName)
    {
        if (characters[SelName].equippedItems[slotNum.ToString()] != null)
        {
            SetShownItem(slotNum, characters[SelName].equippedItems[slotNum.ToString()].name);
        }
    }

    void SetShownItem(int slotNum, string itemName)
    {
        shownItems[slotNum] = invItems[itemName];
        Btns[slotNum].GetComponent<BtnEquipt>().SetIcon(Resources.Load<Sprite>(shownItems[slotNum].sprite));        
    }

 


    public void SaveInv() //takes currently displayed items and saves to global
    {
        for (int i = 0; i < 3; i++)
        {
            characters[FrontChar.name].equippedItems[i.ToString()] = shownItems[i];
        }

        GameManager.Instance.gameDataManager.SetCharacters(characters);

        //If character is in party, write to it as well.
        if (GameManager.Instance.gameDataManager.IsCharacterInParty(FrontChar))
        {
            Dictionary<int, Character> party = GameManager.Instance.gameDataManager.GetCharactersInParty();

            for (int j = 0; j < 4; j++)
            {
                if (party[j] != null && party[j].name == FrontChar.name) {
                    party[j] = characters[FrontChar.name];
                }
            }            
            GameManager.Instance.gameDataManager.SetCharactersInParty(party);
        }

        GameManager.Instance.gameDataManager.SaveInvItems(invItems);

        //TODO make sure FrontChar, char and SelChar are synced up when needed

        //Saves equipt items
        //Units[FrontAndCentre].item1 = it[1]; 
        //Units[FrontAndCentre].item2 = it[2];
        //Units[FrontAndCentre].item3 = it[3];

        /*
        for (int i = 0; i < Units.Count; i++)
        {
            CharacterScriptObject currentCharacterSO = (CharacterScriptObject)Resources.Load("ScriptableObjects/Characters/" + Units[i].unitName);
            if (Resources.Load("ScriptableObjects/Characters/" + Units[i].unitName))
            {
                currentCharacterSO.eqp1 = Units[i].item1;
                currentCharacterSO.eqp2 = Units[i].item2;
                currentCharacterSO.eqp3 = Units[i].item3;
            }
            else
            {
                // Debug.Log("HEY " + characters[i].name);
            }
        }
        */


        //Save inventory items
        //storedItems = InventoryMenu.GetStoredItems();
        //storedValues.passUp(storedItems);
        //storedValues.saveInv();
        //storedValues.saveChar();
    }



    void DropHeld() // Drop held item
    {
        if (HoldItem != null)
        {
            audioSource.PlayOneShot(place, 0.7F);
        }
        Drag.SetIcon(null);
        HoldItem = null;        
    }

    public void setImage(Sprite img) //Sets portrait image
    {
        UnitDisplay.GetComponent<Image>().sprite = img;
    }

    /*
    public void GridOnClick(int intID, Item itemID)  //Grid of Inventory items clicked
    {
        if (Drag.Dragging() == false) //pick up item 
        {          
            Drag.SetIcon(GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(null);
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(0);

            HoldItem = itemID;
            audioSource.PlayOneShot(pickUp, 0.7F);

        }
        else if(Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == false) //place item
        {
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(HoldItem);
            Drag.SetIcon(null);

            HoldItem = null;
            audioSource.PlayOneShot(place, 0.7F);
            SaveInv();
        }  
        else if (Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == true) //switch item
        { 
            Sprite tempS = GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon();

            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(HoldItem);
            Drag.SetIcon(tempS);

            HoldItem = itemID;
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

            HoldItem = shownItems[item];
            audioSource.PlayOneShot(pickUp, 0.7F);
            shownItems[item] = null;

        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == false) //place item
        {
            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(null);

            shownItems[item] = HoldItem;
            HoldItem = null;
            audioSource.PlayOneShot(equip, 0.7F);
            SaveInv();
        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == true) //switch item
        {
            Sprite tempS = origin.GetComponent<BtnEquipt>().GetIcon();
            Item tempN = HoldItem;

            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(tempS);

            HoldItem = shownItems[item];
            shownItems[item] = tempN;
            audioSource.PlayOneShot(equip, 0.7F);
            SaveInv();
        }
    }
    */

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

        if (itemID == 0) { origin = 0; }

        if (origin > 0) //hoverEnter - fade in
        {
            origin--;
            //ReWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            StartCoroutine(GraphicFader(0.1f, HoverTxt, 0f, 0.75f));
            StartCoroutine(GraphicFader(0.1f, txtBoxTitle, 0f, 1f));
            StartCoroutine(GraphicFader(0.1f, txtBoxDesc, 0f, 1f));
        }
        else if (origin < 0) //hoverExit - fade out
        {
            origin = Mathf.Abs(origin) - 1;
            //ReWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            StartCoroutine(GraphicFader(1f, HoverTxt, 0.75f, 0f));
            StartCoroutine(GraphicFader(1f, txtBoxTitle, 1f, 0f));
            StartCoroutine(GraphicFader(1f, txtBoxDesc, 1f, 0f));
        }
        else //catch
        {
            
        }
    }

    public void HoverText(string origin, int check) //An equip button has triggered OnPointerEnter or OnPointerExit - text is changed accordingly
    {
        HoverTxt.gameObject.SetActive(true); //starts disabled

        //Sets text to item in origin button
        //if (origin.Equals("ButtonTop")){       ReWriter(AllItems[it[1]].Title, AllItems[it[1]].Desc);}
        //else if(origin.Equals("ButtonMid")){   ReWriter(AllItems[it[2]].Title, AllItems[it[2]].Desc);}
        //else if(origin.Equals("ButtonBottom")){ReWriter(AllItems[it[3]].Title, AllItems[it[3]].Desc);}
        //else {Debug.Log("ERROR, hover origin unknown");}
        
        if (check > 0) //hoverEnter - fade in
        {
            StartCoroutine(GraphicFader(0.2f, HoverTxt, 0f, 0.75f));
            StartCoroutine(GraphicFader(0.2f, txtBoxTitle, 0f,1f));
            StartCoroutine(GraphicFader(0.2f, txtBoxDesc, 0f, 1f));
        }
        else if (check < 0) //hoverEnter - fade out
        {
            StartCoroutine(GraphicFader(1f, HoverTxt, 0.75f, 0f));
            StartCoroutine(GraphicFader(1f, txtBoxTitle, 1f,0f));
            StartCoroutine(GraphicFader(1f, txtBoxDesc, 1f, 0f));
        }
    }

    //Fade graphic in
    IEnumerator FadeGraphicIn(Graphic fadeObject)
    {
        for (float f = 0f; f <= 1; f += 0.1f)
        {
            Color c = fadeObject.material.color;
            c.a = (f + 0.1f > 1) ? 1 : f;
            fadeObject.material.color = c;
            yield return null;
        }
    }

    //Fade graphic out
    IEnumerator FadeGraphicOut(Graphic fadeObject)
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = fadeObject.material.color;
            c.a = (f - 0.1f < 0) ? 0 : f;
            fadeObject.material.color = c;
            yield return null;
        }
    }

    IEnumerator GraphicFader(float transitionTime, Graphic fadeObject, float fadeStart, float fadeEnd) //Graphic fading in or out
    { //0 to invisible, 1 to visible
        Color c = fadeObject.material.color;
        c.a = fadeStart;

        while (c.a < fadeStart + 0.1 && c.a > fadeEnd - 0.1 || c.a < fadeEnd + 0.1 && c.a > fadeStart - 0.1)
        {
            c.a = fadeObject.color.a - (Time.deltaTime / transitionTime) * fadeStart + (Time.deltaTime / transitionTime) * fadeEnd;
            fadeObject.material.color = c;
            yield return null;
        }
    }

    IEnumerator GraphicFader2(float transitionTime, Graphic fadeObject, float fadeStart, float fadeEnd) //Graphic fading in or out
    { //0 to invisible, 1 to visible
        fadeObject.color = new Color(fadeObject.color.r, fadeObject.color.g, fadeObject.color.b, fadeStart);
        while (fadeObject.color.a < fadeStart + 0.1 && fadeObject.color.a > fadeEnd - 0.1 || fadeObject.color.a < fadeEnd + 0.1 && fadeObject.color.a > fadeStart - 0.1)
        {
            fadeObject.color = new Color(fadeObject.color.r, fadeObject.color.g, fadeObject.color.b, fadeObject.color.a - (Time.deltaTime / transitionTime) * fadeStart + (Time.deltaTime / transitionTime) * fadeEnd);
            yield return null;
        }
    }

    
}