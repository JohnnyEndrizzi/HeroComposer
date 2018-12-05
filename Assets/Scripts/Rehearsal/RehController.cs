using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RehController : MonoBehaviour {

    //Gameobject locations
    [SerializeField]
    private Button UnitDisplay1 = null; //TODO Buttons vs Gameobject
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

    public Dictionary<int, UnitDict> Units;
    public Dictionary<int, AllItemDict> AllItems;

    //Text Locations  //TODO add one per character? or attached to UnitDisplay
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
    
    public void Starter()
    {
        

        //UnitMenu.Units = Units;
        //UnitMenu.AllItems = AllItems;
        //UnitMenu.creator();

        loadPortraits();
        //loadInv(0);
        //setImage(Units[0].img);
    }

    void Start()
    {
        //Font
        myFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        txtBoxTitle.fontSize = titleFontSize;
        txtBoxDesc.fontSize = descFontSize;

        //Equipted items buttons
        UnitDisplay1.onClick.AddListener(delegate { EqpOnClick("Disp1", 1); });
        UnitDisplay2.onClick.AddListener(delegate { EqpOnClick("Disp2", 2); });
        UnitDisplay3.onClick.AddListener(delegate { EqpOnClick("Disp3", 3); });
        UnitDisplay4.onClick.AddListener(delegate { EqpOnClick("Disp4", 4); });
    }
    void Update()
    {
        if (Input.GetMouseButtonUp(1))
        {
            //loadInv(FrontAndCentre);  //TODO ???
        }
    }

    public void loadPortraits()
    { //Loads last saved data and brings a selected Character to the front
      //FrontAndCentre = SelName;
      //DropHeld();

        for (int i=0; i < 4; i++)
        {
            if (Assets.Scripts.MainMenu.ApplicationModel.characters[i])
            {
                unitsInPortraits[i] = findKeyUnits(Assets.Scripts.MainMenu.ApplicationModel.characters[i].name);
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

        //Debug.Log(unitsInPortraits[0].ToString());
        //Debug.Log(unitsInPortraits[1].ToString());
        //Debug.Log(unitsInPortraits[2].ToString());
        //Debug.Log(unitsInPortraits[3].ToString());

        //it[1] = Units[SelName].item1;
        //it[2] = Units[SelName].item2;
        //it[3] = Units[SelName].item3;

        //InventoryMenu.storedItems = storedItems;
        //InventoryMenu.creator();
    }

    public int findKeyUnits(string name)
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

    public void setPortrait(int SelName, int target)
    { 
        switch (target)
        {
            case 1: UnitDisplay1.GetComponent<BtnUnit>().SetIcon(Units[SelName].img);
                break;
            case 2: UnitDisplay2.GetComponent<BtnUnit>().SetIcon(Units[SelName].img);
                break;
            case 3: UnitDisplay3.GetComponent<BtnUnit>().SetIcon(Units[SelName].img);
                break;
            case 4: UnitDisplay4.GetComponent<BtnUnit>().SetIcon(Units[SelName].img);
                break;
            default:
                break;
        }
    }

    public void OnClickUnitMenu()
    {

    }
    public void OnClickUnitDisp(Button origin, int item)
    {
        if (Drag.Dragging() == true)
        { //place
            origin.GetComponent<BtnUnit>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(null);

            unitsInPortraits[item] = HoldNum;
            HoldNum = 0;
        }        
    }

    public void EqpOnClick(string s, int i) //TODO Remove
    {

    }



}






/*



    public void loadInv(int SelName)
    { //Loads last saved data and brings a selected Character to the front
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

    public void saveInv()
    {
        Units[FrontAndCentre].item1 = it[1]; //TODO Update?
        Units[FrontAndCentre].item2 = it[2];
        Units[FrontAndCentre].item3 = it[3];

        storedItems = InventoryMenu.GetStoredItems();

        storedValues.passUp(storedItems);
        storedValues.save();
    }

    void DropHeld()
    {
        Drag.SetIcon(null);
        HoldNum = 0;
    }

    public void setImage(Sprite img)
    {
        UnitDisplay.GetComponent<Image>().sprite = img;
    }


    public void GridOnClick(int intID, int itemID)
    { //Grid of Inventory items
        if (Drag.Dragging() == false)
        { //pick up           
            Drag.SetIcon(GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(null);
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(0);

            HoldNum = itemID;

        }
        else if (Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == false)
        { //place
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(HoldNum);
            Drag.SetIcon(null);

            HoldNum = 0;
            saveInv();
        }
        else if (Drag.Dragging() == true && GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().HasIcon() == true)
        { //switch
            Sprite tempS = GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().GetIcon();

            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetIcon(Drag.GetIcon());
            GameObject.Find("InvBtn #" + intID).GetComponent<InvMenuBtn>().SetItemID(HoldNum);
            Drag.SetIcon(tempS);

            HoldNum = itemID;
            saveInv();
        }
    }

    void EqpOnClick(Button origin, int item)
    { //3 Equipt buttons
        if (Drag.Dragging() == false)
        { //pick up           
            Drag.SetIcon(origin.GetComponent<BtnEquipt>().GetIcon());
            origin.GetComponent<BtnEquipt>().SetIcon(null);

            HoldNum = it[item];
            it[item] = 0;

        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == false)
        { //place
            origin.GetComponent<BtnEquipt>().SetIcon(Drag.GetIcon());
            Drag.SetIcon(null);

            it[item] = HoldNum;
            HoldNum = 0;
            saveInv();
        }
        else if (Drag.Dragging() == true && origin.GetComponent<BtnEquipt>().HasIcon() == true)
        { //switch
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
