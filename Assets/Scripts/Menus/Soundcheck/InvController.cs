using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    //Save which item is being held
    Item holdItem = new Item();

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
    private AudioClip errorSound;

    //Text Locations
    [SerializeField]
    private Text txtBoxTitle = null;
    [SerializeField]
    private Text txtBoxDesc = null;
    [SerializeField]
    private Text statsWindowL = null;
    [SerializeField]
    private Text statsWindowR = null;

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
        errorSound = (AudioClip)Resources.Load("SoundEffects/shop_not_enough_money");

        //Populate character list scrollbox
        InventoryMenu.GenInventory();

        //Sets first char to the front
        LoadInv(characters[characters.Keys.ToArray()[0]]);

        //Pre-fill stat window                    
        StatsWindow(new Item(), new Item(), -1);
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

        GameManager.Instance.gameDataManager.SaveCharacters(characters);

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
        GameManager.Instance.gameDataManager.SaveInventory(InventoryMenu.GetStoredItems());               
    }
           
    //Audio, save and text triggers on drags and drops
    public void Pickup(Item heldItem)
    {
        audioSource.PlayOneShot(pickUp, 0.7F);        
        holdItem = heldItem;
        StatsWindow(holdItem, new Item(), -1);
    }
    public void Equip()
    {
        audioSource.PlayOneShot(equip, 0.7F);
        SaveInv();
        holdItem = new Item();
        StatsWindow(new Item(), new Item(), -1);
    }
    public void Place()
    {
        audioSource.PlayOneShot(place, 0.7F);
        SaveInv();
        holdItem = new Item();
    }
    public void LockedChar()
    {
        audioSource.PlayOneShot(errorSound, 0.7F);       
    }

    public void HoverTextFadeIn(Item itemInfo, int SlotNum)
    {
        InfoWindow(itemInfo);
        StatsWindow(holdItem, itemInfo, SlotNum);        

        StopAllCoroutines();

        //StartCoroutine(GraphicFader(1f, HoverTxt, 0.75f));
        StartCoroutine(GraphicFader(1f, txtBoxTitle, 1f));
        StartCoroutine(GraphicFader(1f, txtBoxDesc, 1f));
    }
    public void HoverTextFadeOut()
    {
        StopAllCoroutines();

        //StartCoroutine(GraphicFader(0.5f, HoverTxt, 0f));
        StartCoroutine(GraphicFader(0.5f, txtBoxTitle, 0f));
        StartCoroutine(GraphicFader(0.5f, txtBoxDesc, 0f));

        StatsWindow(holdItem, new Item(),-1);
    }

    //Update Item info box Text
    void InfoWindow(Item itemInfo)
    {
        if (itemInfo == null)
        {
            itemInfo = new Item { name = "", description = "" };
        }

        //Row Names
        string[] statName = new string[] { "Level", "HP   ", "ATK  ", "DEF  ", "RCV  ", "MGC  " };
        int[] itemStat = ToStats(itemInfo);
        string desc = itemInfo.description;

        for (int i = 0; i < itemStat.Length; i++)
        {
            if (itemStat[i] != 0)
            {
                desc += "\n" + statName[i] + ": " + ((Math.Sign(itemStat[i]) > 0) ? "<color=green><size=20>+" : "<color=red><size=20>-") + itemStat[i] + "</size></color>";
            }
        }
        txtBoxTitle.text = itemInfo.name;
        txtBoxDesc.text = desc;
    }

    public void StatsWindow(Item holdItem, Item hoverItem, int hoverSlot)
    {
        //name == null for new Item(), hoverItem == null for empty item slots
        if (holdItem.name == null)
        {
            holdItem = new Item { name = "", description = "" };
        }
        if (hoverItem == null || hoverItem.name == null)
        {
            hoverItem = new Item { name = "", description = "" };
        }

        if (!holdItem.name.Equals(""))
        {
            StatCall(holdItem, hoverSlot);
        }
        else  if (!hoverItem.name.Equals(""))
        {
            if (hoverSlot < 0)
            {
                StatCall(hoverItem, hoverSlot);
            }
            else
            {
                StatCall(new Item(), hoverSlot);
            }
        }
        else
        {
            StatCall(holdItem, -1);
        }
    }

    void StatCall(Item newItem, int replacedItem)
    {
        string rightBox = "", leftBox = "", director = "";
        
        //Row Names
        string[] statName = new string[] { "Level", "HP   ", "ATK  ", "DEF  ", "RCV  ", "MGC  " };

        //Char base stats
        int[] baseStat = ToStats(frontChar);

        //Equipt item stats
        int[][] equiptStats = new int[3][];
        equiptStats[0] = ToStats(frontChar.equippedItems["0"]);
        equiptStats[1] = ToStats(frontChar.equippedItems["1"]);
        equiptStats[2] = ToStats(frontChar.equippedItems["2"]);

        //Char stats with Equipt Item Stats
        int[] eqpStat = baseStat.Select((x, index) => x + equiptStats[0][index]).ToArray(); 
        eqpStat = eqpStat.Select((x, index) => x + equiptStats[1][index]).ToArray(); 
        eqpStat = eqpStat.Select((x, index) => x + equiptStats[2][index]).ToArray();

        //new item stats 
        int[] itemStat = ToStats(newItem);    
        
        //New Stats = Base stats + Equipted items + New Item
        int[] newStat = eqpStat.Select((x, index) => x + itemStat[index]).ToArray();

        //New Stats -= Replaced Equipt Item
        if(replacedItem >= 0) newStat = newStat.Select((x, index) => x - equiptStats[replacedItem][index]).ToArray();

        int statNum = 6; //how many stats are shown
        for (int i = 0; i < statNum; i++)
        {
            director = statName[i] + " =  <size=18>" + eqpStat[i] + "</size>";
            if (newStat[i] > eqpStat[i])
            {
                director += AllignTextSpaces(baseStat[i]) + " <size=14>>></size> <size=18><color=green>" + newStat[i] + "</color></size>";
            }
            else if (newStat[i] < eqpStat[i])
            {
                director += AllignTextSpaces(baseStat[i]) + " <size=14>>></size> <size=18><color=red>" + newStat[i] + "</color></size>";
            }

            if (i % 2 == 0)
            {                
                leftBox += director + (i == statNum-2 ? "" : "\n\n");
            }
            else
            {
                rightBox += director + (i == statNum-1 ? "" : "\n\n");
            }
        }
        statsWindowL.text = leftBox;
        statsWindowR.text = rightBox;
    }

    //Graphic fading in or out
    IEnumerator GraphicFader(float transitionTime, Graphic fadeObject, float fadeEnd)
    { //0 to invisible, 1 to visible
        Color c = fadeObject.color;
        float fadeStart = c.a;
        fadeObject.canvasRenderer.SetAlpha(c.a);

        int mult = (int)Mathf.Sign(fadeStart - fadeEnd);

        for (float f = fadeStart; f >= fadeEnd * mult; f -= 0.1f * transitionTime)
        {
            c.a = (f - 0.1f < fadeEnd * mult) ? fadeEnd : f * mult;
            fadeObject.color = c;
            fadeObject.canvasRenderer.SetAlpha(c.a);
            yield return null;
        }
    }

    public string AllignTextSpaces(int L)
    {
        int DesiredLength = 4;
        string outStr = "";
        for (int i = L.ToString().Count(); i < DesiredLength; i++) { outStr += " "; }
        return outStr;
    }  

    public int[] ToStats(Character character)
    {         
        int[] stats = new int[]
        {
            character.level,
            character.hp,
            character.atk,
            character.def,
            character.rcv,
            character.mgc
        };
        return stats;
    }

    public int[] ToStats(Item item)
    {
        if (item == null)
        {
            return new int[] { 0, 0, 0, 0, 0, 0 };            
        }
        int[] stats = new int[]
        {
            item.level,
            item.hp,
            item.atk,
            item.def,
            item.rcv,
            item.mgc
        };
        return stats;
    }
}