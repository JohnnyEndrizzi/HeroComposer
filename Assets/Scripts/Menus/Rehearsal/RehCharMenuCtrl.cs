using System.Collections.Generic;
using UnityEngine;

public class RehCharMenuCtrl : MonoBehaviour {
    //Controls Unit menu in Rehearsal

    //Gameobject locations
    [SerializeField]
    private GameObject buttonTemplate = null;

    public Dictionary<int, UnitDict> Units;
    public Dictionary<int, AllItemDict> AllItems;

    private List<GameObject> buttons;

    public void creator() //Start but triggered after information is passed in
    {
        buttons = new List<GameObject>();

        if (buttons.Count > 0) //Destroy buttons to allow for reload
        { 
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        for (int i = 0; i < Units.Count-1; i++) //Create a button for each unit (excluding null)
        {
            if (Units[i].Unlocked == true)
            {
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);

                button.name = ("RehCharBtn #" + i);
                button.GetComponent<RehCharMenuBtn>().SetText(Units[i].unitName);
                button.GetComponent<RehCharMenuBtn>().SetImage(Units[i].img);
                button.GetComponent<RehCharMenuBtn>().SetInvID(i);

                button.transform.SetParent(buttonTemplate.transform.parent, false);
            }
        }
    }

    public void LightsOut() //Turns off highlighting on all child objects
    {
        for(int i = 0; i< this.transform.GetChild(0).GetChild(0).childCount -1; i++){
            GameObject.Find("RehCharBtn #" + i).GetComponent<RehCharMenuBtn>().HighOff();    
        }
    }

    public void ButtonClicked(int intID)  //sub button Clicked
    { 
        LightsOut();
        GameObject.Find("RehCharBtn #" + intID).GetComponent<RehCharMenuBtn>().HighOn();    
        //GameObject.Find("RehController").GetComponent<RehController>().OnClickUnitMenu(intID);
    }
}