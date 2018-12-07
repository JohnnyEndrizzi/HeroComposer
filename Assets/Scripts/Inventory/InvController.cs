using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvController : MonoBehaviour {
    //Main controller for Inventory scene

    // 8.1 1-8 The system must display the inventory screen.

    // 8.1 1-10 The player must be able to customise individual characters. //partially done

    // 8.1 1-11 The system must display the current equipment that each character has.

    // 8.1 1-12 The player must be able view a character’s skill tree. //will be here someday
    // 8.1 1-13 The system must be able to update a character’s skill tree.



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

    [HideInInspector]
    public List<int> storedItems;  //Inventory Storage

    public Dictionary<int, UnitDict> Units;
    public Dictionary<int, AllItemDict> AllItems;

    //Equip Button Locations
    [SerializeField]
    private Button BtnTop = null;
    [SerializeField]
    private Button BtnMid = null;
    [SerializeField]
    private Button BtnBottom = null;

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

    public void Starter() //Start delayed until information is passed in
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

	void Start () {        
        //Font
        myFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        txtBoxTitle.fontSize = titleFontSize;
        txtBoxDesc.fontSize = descFontSize;

        //Equipted items buttons
        BtnTop.onClick.AddListener(delegate {EqpOnClick(BtnTop, 1);});	
        BtnMid.onClick.AddListener(delegate {EqpOnClick(BtnMid, 2);});
        BtnBottom.onClick.AddListener(delegate {EqpOnClick(BtnBottom, 3);});
	}

    void Update()
    {        
        if (Input.GetMouseButtonUp(1)) {
            loadInv(FrontAndCentre);  //Drop held item and reload menus
        }
    }
        
    public void loadInv(int SelName) //Loads last saved data and brings a selected Character to the front
    {
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
        Drag.SetIcon(null);
        HoldNum = 0;
    }

    public void setImage(Sprite img) //Sets portrait image
    {
        UnitDisplay.GetComponent<Image>().sprite = img;
    }


    public void GridOnClick(int intID, int itemID)  //Grid of Inventory items clicked
    {
        if (Drag.Dragging() == false){ //pick up           
            Drag.SetIcon(GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(null);
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(0);

            HoldNum = itemID;

        }else if(Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == false){ //place
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(HoldNum);
            Drag.SetIcon(null);

            HoldNum = 0;
            saveInv();
        }  
        else if (Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == true) { //switch
            Sprite tempS = GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon();

            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(HoldNum);
            Drag.SetIcon(tempS);

            HoldNum = itemID;
            saveInv();
        }         
    }

    void EqpOnClick(Button origin, int item){ //One of Equipt buttons clicked
        if (Drag.Dragging() == false) { //pick up           
            Drag.SetIcon(origin.GetComponent<BtnEquipt>().GetIcon());
            origin.GetComponent<BtnEquipt>().SetIcon(null);  

            HoldNum = it[item];
            it[item] = 0;

        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == false) { //place
            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(null);

            it[item] = HoldNum;
            HoldNum = 0;
            saveInv();
        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == true) { //switch
            Sprite tempS = origin.GetComponent<BtnEquipt>().GetIcon();
            int tempN = HoldNum;

            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(tempS);

            HoldNum = it[item];
            it[item] = tempN;

            saveInv();
        }
    }

    //Text control  
    void reWriter(string title, string desc){ //Update Text
        txtBoxTitle.text = title;
        txtBoxDesc.text = desc;             

        rectTransform = HoverTxt.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(170, 200);

        if (stringLength(desc, descFontSize) == 0 && stringLength(title, titleFontSize) == 0) { 
            rectTransform.sizeDelta = new Vector2(0, 200);
        }        
    }

    int stringLength(string s, int size) //get the amount of size the string will take up using 
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
            reWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            StartCoroutine(textFader(1f, txtBoxTitle, txtBoxDesc, 1f, 0f)); 
        }
        else if (origin < 0) //hoverExit - fade out
        {
            origin = Mathf.Abs(origin) - 1;
            reWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            HoverTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200);
            StartCoroutine(textFader(3f, txtBoxTitle, txtBoxDesc, 0f, 1f));
        }
        else //catch
        {
            reWriter(AllItems[origin].Title, AllItems[origin].Desc);
        }
    }

    public void HoverText(string origin, int check) //An equip button has triggered OnPointerEnter or OnPointerExit - text is changed accordingly
    {
        HoverTxt.gameObject.SetActive(true); //starts disabled

        //Sets text to item in origin button
        if (origin.Equals("ButtonTop")){       reWriter(AllItems[it[1]].Title, AllItems[it[1]].Desc);}
        else if(origin.Equals("ButtonMid")){   reWriter(AllItems[it[2]].Title, AllItems[it[2]].Desc);}
        else if(origin.Equals("ButtonBottom")){reWriter(AllItems[it[3]].Title, AllItems[it[3]].Desc);}
        else {Debug.Log("ERROR, hover origin unknown");}


        if (check > 0) //hoverEnter - fade in
        {           
            StartCoroutine(textFader(1f, txtBoxTitle, txtBoxDesc, 1f,0f)); 
        }
        else if (check < 0) //hoverEnter - fade out
        {
            HoverTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200);
            StartCoroutine(textFader(3f, txtBoxTitle, txtBoxDesc, 0f,1f));
        }
    }

    IEnumerator textFader(float t, Text i, Text i2, float fader, float fader2) //Controls the text fading in or out for 2 text objects
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