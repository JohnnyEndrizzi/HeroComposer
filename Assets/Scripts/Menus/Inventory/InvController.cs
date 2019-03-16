using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

//Enum for portait indexing
public enum EqpSlotPos
{
    ButtonTop,
    ButtonMiddle,
    ButtonBottom
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

    //Object to display to
    [SerializeField]
    private GameObject UnitDisplay = null;    
    //Menu of all inventory items
    private InvMenuCtrl InventoryMenu;

    //Local dictionary of characters    
    Dictionary<string, Character> characters;
    //Save which character is selected
    private Character frontChar;

    //Equip Button Locations
    [SerializeField]
    private Button BtnTop = null;
    [SerializeField]
    private Button BtnMid = null;
    [SerializeField]
    private Button BtnBottom = null;
    
    //To keep track of button ItemSlots while they are not attached to their buttons
    private ItemSlot[] BtnsItemSlot = new ItemSlot[3];

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
    private int titleFontSize = 18;
    private int descFontSize = 14;
    Font myFont;
        
	void Start ()
    {
        //Show UI Layer
        soundChecklUI.Show();

        characters = GameManager.Instance.gameDataManager.GetCharacters();
        InventoryMenu = FindObjectOfType<InvMenuCtrl>();
        
        //Font
        myFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        txtBoxTitle.fontSize = titleFontSize;
        txtBoxDesc.fontSize = descFontSize;

        //Assign buttons to group
        BtnsItemSlot[0] = BtnTop.GetComponentInChildren<ItemSlot>();
        BtnsItemSlot[1] = BtnMid.GetComponentInChildren<ItemSlot>();
        BtnsItemSlot[2] = BtnBottom.GetComponentInChildren<ItemSlot>();

        //Import Audio
        audioSource = GetComponent<AudioSource>();
        equip = (AudioClip)Resources.Load("SoundEffects/inventory_equip_item");
        pickUp = (AudioClip)Resources.Load("SoundEffects/inventory_pick_up_item");
        place = (AudioClip)Resources.Load("SoundEffects/inventory_place_item_back");
        pickUpChar = (AudioClip)Resources.Load("SoundEffects/rehersal_pick_up_character");

        //Populate character list scrollbox
        InventoryMenu.GenInventory();

        //Sets first char to the front
        LoadInv(characters[characters.Keys.ToArray()[0]]); 
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //TODO transitions
        }
    }

    //Loads last saved data and brings a selected Character to the front
    public void LoadInv(Character SelChar) 
    {        
        if (frontChar != SelChar)
        {
            audioSource.PlayOneShot(pickUpChar, 0.5F);
        }

        frontChar = SelChar;        
        UnitDisplay.GetComponent<Image>().sprite = Resources.Load<Sprite>(SelChar.sprite);

        //Set equip buttons icons and information
        for (int i = 0; i<3; i++)
        {           
            BtnsItemSlot[i].DisplayItem(SelChar.equippedItems[i.ToString()]);
        }       
    }

    //takes currently displayed items and saves to global
    public void SaveInv()
    {
        //Check equipt items
        for (int i = 0; i < 3; i++)
        {                       
            characters[((int)Enum.Parse(typeof(CharacterIDs), frontChar.name)).ToString()].equippedItems[i.ToString()] = BtnsItemSlot[i].item;
        }

        GameManager.Instance.gameDataManager.SetCharacters(characters);

        //If character is in party, write to it as well.
        if (GameManager.Instance.gameDataManager.IsCharacterInParty(frontChar))
        {
            Dictionary<int, Character> party = GameManager.Instance.gameDataManager.GetCharactersInParty();
            
            for (int j = 0; j < 4; j++)
            {
                if (party.ContainsKey(j) && party[j].name == frontChar.name) {
                    party[j] = characters[((int)Enum.Parse(typeof(CharacterIDs), frontChar.name)).ToString()];                  
                }
            }            
            GameManager.Instance.gameDataManager.SetCharactersInParty(party);
        }
        //Save inventory list
        GameManager.Instance.gameDataManager.SaveInvItems(InventoryMenu.GetStoredItems());       
    }
           
    //Audio and save triggers on drags and drops
    public void Pickup()
    {
        audioSource.PlayOneShot(pickUp, 0.7F);        
    }        
    public void Equip()
    {
        audioSource.PlayOneShot(equip, 0.7F);
        SaveInv();
    }
    public void Place()
    {
        audioSource.PlayOneShot(place, 0.7F);
        SaveInv();
    }
       
    //Update Text
    void ReWriter(Item itemInfo) 
    {
        if(itemInfo == null)
        {
            itemInfo = new Item{name = "", description = ""};
        }

        txtBoxTitle.text = itemInfo.name;
        txtBoxDesc.text = itemInfo.description;             

        RectTransform rectTransform = HoverTxt.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(170, 200);

        if (StringLength(itemInfo.description, descFontSize) == 0 && StringLength(itemInfo.name, titleFontSize) == 0){ 
            rectTransform.sizeDelta = new Vector2(0, 200);
        }        
    }

    //get the amount of physical space the string will take up 
    int StringLength(string s, int size) 
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

    public void HoverTextFadeIn(Item itemInfo)
    {
        ReWriter(itemInfo);

        StopAllCoroutines();

        StartCoroutine(GraphicFader(1f, HoverTxt, 0.75f));
        StartCoroutine(GraphicFader(1f, txtBoxTitle, 1f));
        StartCoroutine(GraphicFader(1f, txtBoxDesc, 1f));
    }
    public void HoverTextFadeOut()
    {
        StopAllCoroutines();

        StartCoroutine(GraphicFader(0.5f, HoverTxt, 0f));
        StartCoroutine(GraphicFader(0.5f, txtBoxTitle, 0f));
        StartCoroutine(GraphicFader(0.5f, txtBoxDesc, 0f));
    }

    //Graphic fading in or out
    IEnumerator GraphicFader(float transitionTime, Graphic fadeObject, float fadeEnd) 
    { //0 to invisible, 1 to visible
        Color c = fadeObject.color;
        float fadeStart = c.a;

        int mult = (int)Mathf.Sign(fadeStart - fadeEnd);

        for (float f = fadeStart; f >= fadeEnd * mult; f -= 0.1f * transitionTime)
        {
            c.a = (f - 0.1f < fadeEnd * mult) ? fadeEnd : f * mult;
            fadeObject.color = c;
            yield return null;
        }
    }
}