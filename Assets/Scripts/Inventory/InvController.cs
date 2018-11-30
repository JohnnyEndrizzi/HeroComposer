﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvController : MonoBehaviour {

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

    //Button Locations
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

    ////Graphics
    //Characters
    //Items
    //
    //Button BG
    //World BG
    //Door
    //Inventory and top bar BG

    public void Starter(){
        InventoryMenu.AllItems = AllItems;
        InventoryMenu.storedItems = storedItems;
        InventoryMenu.creator();

        UnitMenu.Units = Units;
        UnitMenu.creator();

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

    void Update(){        
        if (Input.GetMouseButtonUp(1)) {
            loadInv(FrontAndCentre);
        }
    }
        
    public void loadInv(int SelName){ //Loads last saved data and brings a selected Character to the front
        FrontAndCentre = SelName;
        DropHeld();
        BtnTop.GetComponent<BtnEquipt>().SetIcon(AllItems[Units[SelName].item1].img);
        BtnMid.GetComponent<BtnEquipt>().SetIcon(AllItems[Units[SelName].item2].img);
        BtnBottom.GetComponent<BtnEquipt>().SetIcon(AllItems[Units[SelName].item3].img);


        it[1] = Units[SelName].item1;
        it[2] = Units[SelName].item2;
        it[3] = Units[SelName].item3;

        InventoryMenu.storedItems = storedItems;
        InventoryMenu.creator();
    }

    public void saveInv(){
        Units[FrontAndCentre].item1 = it[1]; //TODO Update?
        Units[FrontAndCentre].item2 = it[2];
        Units[FrontAndCentre].item3 = it[3];

        storedItems = InventoryMenu.GetStoredItems();

        storedValues.passUp(storedItems);
        storedValues.save();
    }

    void DropHeld(){
        Drag.SetIcon(null);
        HoldNum = 0;
    }

    public void setImage(Sprite img){
        UnitDisplay.GetComponent<Image>().sprite = img;
    }


    public void GridOnClick(int intID, int itemID){ //Grid of Inventory items
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

    void EqpOnClick(Button origin, int item){ //3 Equipt buttons
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

    int stringLength(string s, int size){
        int totalLength = 0;
        CharacterInfo characterInfo = new CharacterInfo();

        char[] chars = s.ToCharArray();

        foreach(char c in chars){
            myFont.RequestCharactersInTexture(c.ToString(), size, txtBoxTitle.fontStyle);
            myFont.GetCharacterInfo(c, out characterInfo, size);
            totalLength += characterInfo.advance;
        }
        return totalLength;
    }

    public void HoverText(int origin, int itemID){
        HoverTxt.gameObject.SetActive(true);



        if (origin > 0) {    
            origin--;
            reWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            StartCoroutine(textFader(1f, txtBoxTitle, txtBoxDesc, 1f, 0f)); //TODO make update per frame 
        }
        else if (origin < 0) {
            origin = Mathf.Abs(origin) - 1;
            reWriter(AllItems[itemID].Title, AllItems[itemID].Desc);
            HoverTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200);
            StartCoroutine(textFader(3f, txtBoxTitle, txtBoxDesc, 0f, 1f));
        }else {
            reWriter(AllItems[origin].Title, AllItems[origin].Desc);
        }
    }

    public void HoverText(string origin, int check){
        HoverTxt.gameObject.SetActive(true);

        if(origin.Equals("ButtonTop")){        reWriter(AllItems[it[1]].Title, AllItems[it[1]].Desc);}
        else if(origin.Equals("ButtonMid")){   reWriter(AllItems[it[2]].Title, AllItems[it[2]].Desc);}
        else if(origin.Equals("ButtonBottom")){reWriter(AllItems[it[3]].Title, AllItems[it[3]].Desc);}
        else {Debug.Log("ERROR, hover origin unknown");}


        if (check > 0) {           
            StartCoroutine(textFader(1f, txtBoxTitle, txtBoxDesc, 1f,0f)); //TODO make update per frame 
        }
        else if (check < 0){
            HoverTxt.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 200);
            StartCoroutine(textFader(3f, txtBoxTitle, txtBoxDesc, 0f,1f));
        }
    }

    IEnumerator textFader(float t, Text i, Text i2, float fader, float fader2){ //0 to invisible, 1 to visible
        i.color = new Color(i.color.r, i.color.g, i.color.b, fader);
        i2.color = new Color(i2.color.r, i2.color.g, i2.color.b, fader);
        while (i.color.a < 1.0f && i.color.a > 0.0f) {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t)*fader + (Time.deltaTime / t)*fader);
            i2.color = new Color(i2.color.r, i2.color.g, i2.color.b, i2.color.a - (Time.deltaTime / t)*fader + (Time.deltaTime / t)*fader);
            yield return null;
        }
    }
}