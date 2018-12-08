using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RehController : MonoBehaviour {
    //Main controller for Rehearsal scene

    /* Functional Requirement 
    * ID: 8.1 1-15
    * Description: The player must be able to customise their team.
    * 
    * This class controls the selection of team members */

    /* Functional Requirement 
    * ID: 8.1 1-15
    * Description: The system must be able to calculate the players current stats. //will be here before 19th
    * 
    * This class allows players to view all stats including their equipted items */

    //Gameobject locations
    [SerializeField]
    private Button UnitDisplay1 = null;  //Portraits
    [SerializeField]
    private Button UnitDisplay2 = null;
    [SerializeField]
    private Button UnitDisplay3 = null;
    [SerializeField]
    private Button UnitDisplay4 = null;

    [SerializeField]
    private DragImg Drag = null;
    [SerializeField]
    private RehCharMenuCtrl UnitMenu = null;

    /*Dictionary lists are passed in from the StoredVariables class on load,
    * they contain all information about all Units, what is equipt to who and all Items present in the game 
    * Items are required to calculate the affect on the character */
    public Dictionary<int, UnitDict> Units;
    public Dictionary<int, AllItemDict> AllItems;

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
    private int HoldNum; //Save what is held
    private int[] unitsInPortraits = new int[4];

    public void Starter() //Start after values are loaded in
    {
        //Pass information to menu
        UnitMenu.Units = Units;
        UnitMenu.AllItems = AllItems;
        UnitMenu.creator();

        LoadPortraits();
    }

    void Start()
    {
        //Font
        myFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        txtBoxTitle.fontSize = titleFontSize;
        txtBoxDesc.fontSize = descFontSize;

        //Add click listeners to Unit Displays
        UnitDisplay1.onClick.AddListener(delegate { OnClickUnitDisp(UnitDisplay1, 1); });
        UnitDisplay2.onClick.AddListener(delegate { OnClickUnitDisp(UnitDisplay2, 2); });
        UnitDisplay3.onClick.AddListener(delegate { OnClickUnitDisp(UnitDisplay3, 3); });
        UnitDisplay4.onClick.AddListener(delegate { OnClickUnitDisp(UnitDisplay4, 4); });
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            DropHeld();
        }
    }

    void DropHeld() //Drop held item
    {
        Drag.SetIcon(null);
        HoldNum = 0;
        UnitMenu.lightsOut();
    }

    private void passTeam() //set selected units to be used 
    {
        setTeam(new string[] {Units[unitsInPortraits[0]].unitName, Units[unitsInPortraits[1]].unitName, Units[unitsInPortraits[2]].unitName, Units[unitsInPortraits[3]].unitName});
    }

    public void setTeam(string[] chars) //Takes unit names - locates characterScripts - sets those to load on play
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
    }

    private void ClearTeam() //Set team to null before setting new team
    {
        Assets.Scripts.MainMenu.ApplicationModel.characters[0] = null;
        Assets.Scripts.MainMenu.ApplicationModel.characters[1] = null;
        Assets.Scripts.MainMenu.ApplicationModel.characters[2] = null;
        Assets.Scripts.MainMenu.ApplicationModel.characters[3] = null;
    }

    private void LoadPortraits() //Loads characters into portraits
    { 
         for (int i=0; i < 4; i++)
        {
            if (Assets.Scripts.MainMenu.ApplicationModel.characters[i])
            {
                unitsInPortraits[i] = FindKeyUnits(Assets.Scripts.MainMenu.ApplicationModel.characters[i].name);
            }
            else
            {
                unitsInPortraits[i] = -1;
            }
        }
        UnitDisplay1.GetComponent<BtnUnit>().SetIcon(Units[unitsInPortraits[0]].img);
        UnitDisplay2.GetComponent<BtnUnit>().SetIcon(Units[unitsInPortraits[1]].img);
        UnitDisplay3.GetComponent<BtnUnit>().SetIcon(Units[unitsInPortraits[2]].img);
        UnitDisplay4.GetComponent<BtnUnit>().SetIcon(Units[unitsInPortraits[3]].img);
    }

    private int FindKeyUnits(string name) //Find unit key from the unitName
    {
        foreach(int key in Units.Keys)
        {
            if (name.Equals(Units[key].unitName))
            {
                return key;
            }
        }
        return -1;
    }
    
    public void OnClickUnitMenu(int intID) //Unit Menu was clicked - attach selected unit to mouse
    {
        Drag.SetIcon(Units[intID].img);
        HoldNum = intID;
    }

    public void OnClickUnitDisp(Button origin, int item) //A portrait was clicked
    {
        if (Drag.Dragging() == true){ //place held unit
            int Check = DoubleCheck();
                        
            if (Check != item) {    
                switch (Check) { //switch switches units if the selected unit is already present
                    case 0:
                        break;
                    case 1:                        
                        UnitDisplay1.GetComponent<BtnUnit>().SetIcon(null);
                        unitsInPortraits[0] = -1;
                        break;
                    case 2:                        
                        UnitDisplay2.GetComponent<BtnUnit>().SetIcon(null);
                        unitsInPortraits[1] = -1;
                        break;
                    case 3:                        
                        UnitDisplay3.GetComponent<BtnUnit>().SetIcon(null);
                        unitsInPortraits[2] = -1;
                        break;
                    case 4:                        
                        UnitDisplay4.GetComponent<BtnUnit>().SetIcon(null);
                        unitsInPortraits[3] = -1;
                        break;

                    default:
                        break;                     
                }
                origin.GetComponent<BtnUnit>().SetIcon(Drag.GetIcon());
                Drag.SetIcon(null);
                unitsInPortraits[item - 1] = HoldNum;
                HoldNum = 0;
                UnitMenu.lightsOut();
            }
        }
        else { //clear selected portrait
            origin.GetComponent<BtnUnit>().SetIcon(null);
            unitsInPortraits[item - 1] = -1;
        }
        passTeam(); //commits team to load
    }

    private int DoubleCheck()  //better than a single check - finds if the held unit is already in a portrait
    {
        if (UnitDisplay1.GetComponent<BtnUnit>().HasIcon() && UnitDisplay1.GetComponent<BtnUnit>().GetIcon().name == Units[HoldNum].img.name) {
            return 1;
        }
        if (UnitDisplay2.GetComponent<BtnUnit>().HasIcon() && UnitDisplay2.GetComponent<BtnUnit>().GetIcon().name == Units[HoldNum].img.name) {
            return 2;
        }
        if (UnitDisplay3.GetComponent<BtnUnit>().HasIcon() && UnitDisplay3.GetComponent<BtnUnit>().GetIcon().name == Units[HoldNum].img.name) {
            return 3;
        }
        if (UnitDisplay4.GetComponent<BtnUnit>().HasIcon() && UnitDisplay4.GetComponent<BtnUnit>().GetIcon().name == Units[HoldNum].img.name) {
            return 4;
        }
        return 0;
    }
}





/*
    TODO text stats

    //Text control  
    void reWriter(string title, string desc)
    { //Update Text
        txtBoxTitle.text = title;
        txtBoxDesc.text = desc;

        rectTransform = HoverTxt.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(170, 200);

        if (stringLength(desc, descFontSize) == 0 && stringLength(title, titleFontSize) == 0)
        {
            rectTransform.sizeDelta = new Vector2(0, 200);
        }
    }

    int stringLength(string s, int size)
    {
        int totalLength = 0;
        CharacterInfo characterInfo = new CharacterInfo();

        if (s.Equals(null)) { s = ""; }
        char[] chars = s.ToCharArray();

        foreach (char c in chars)
        {
            myFont.RequestCharactersInTexture(c.ToString(), size, txtBoxTitle.fontStyle);
            myFont.GetCharacterInfo(c, out characterInfo, size);
            totalLength += characterInfo.advance;
        }
        return totalLength;
    }

    public void HoverText(int origin, int itemID)
    {
        HoverTxt.gameObject.SetActive(true);



        if (origin > 0)
        {
            origin--;
            reWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            StartCoroutine(textFader(1f, txtBoxTitle, txtBoxDesc, 1f, 0f)); //TODO make update per frame 
        }
        else if (origin < 0)
        {
            origin = Mathf.Abs(origin) - 1;
            reWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            HoverTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200);
            StartCoroutine(textFader(3f, txtBoxTitle, txtBoxDesc, 0f, 1f));
        }
        else
        {
            reWriter(AllItems[origin].Title, AllItems[origin].Desc);
        }
    }

    public void HoverText(string origin, int check)
    {
        HoverTxt.gameObject.SetActive(true);

        if (origin.Equals("ButtonTop")) { reWriter(AllItems[it[1]].Title, AllItems[it[1]].Desc); }
        else if (origin.Equals("ButtonMid")) { reWriter(AllItems[it[2]].Title, AllItems[it[2]].Desc); }
        else if (origin.Equals("ButtonBottom")) { reWriter(AllItems[it[3]].Title, AllItems[it[3]].Desc); }
        else { Debug.Log("ERROR, hover origin unknown"); }


        if (check > 0)
        {
            StartCoroutine(textFader(1f, txtBoxTitle, txtBoxDesc, 1f, 0f)); //TODO make update per frame 
        }
        else if (check < 0)
        {
            HoverTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200);
            StartCoroutine(textFader(3f, txtBoxTitle, txtBoxDesc, 0f, 1f));
        }
    }

    IEnumerator textFader(float t, Text i, Text i2, float fader, float fader2)
    { //0 to invisible, 1 to visible
        i.color = new Color(i.color.r, i.color.g, i.color.b, fader);
        i2.color = new Color(i2.color.r, i2.color.g, i2.color.b, fader);
        while (i.color.a < 1.0f && i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t) * fader + (Time.deltaTime / t) * fader);
            i2.color = new Color(i2.color.r, i2.color.g, i2.color.b, i2.color.a - (Time.deltaTime / t) * fader + (Time.deltaTime / t) * fader);
            yield return null;
        }
    }
}


    */
