using System.Collections.Generic;
using UnityEngine;

public class CharMenuCtrl : MonoBehaviour {
    //Controls Unit Menu in Inventory

    //Gameobject locations
    [SerializeField]
    private GameObject buttonTemplate = null;

    public Dictionary<int, UnitDict> Units;

    private List<GameObject> buttons;   

    public void Creator() //Start but triggered after information is passed in
    {
        buttons = new List<GameObject>();

        if (buttons.Count > 0) //Destroy buttons to allow for reload
        { 
            foreach (GameObject button in buttons){
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }            

        for (int i = 0; i<Units.Count-1; i++) //Create a button for each unit (excluding null)
        {            
            if (Units[i].Unlocked == true) {
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);

                button.GetComponent<CharMenuBtn>().SetText(Units[i].unitName);
                button.GetComponent<CharMenuBtn>().SetImage(Units[i].img);
                button.GetComponent<CharMenuBtn>().SetInvID(i);

                button.transform.SetParent(buttonTemplate.transform.parent, false);
            }
        }
    }
    
    public void ButtonClicked(int intID) //sub button Clicked
    { 
        GameObject.Find("InvController").GetComponent<InvController>().setImage(Units[intID].img);
        GameObject.Find("InvController").GetComponent<InvController>().loadInv(intID);
    }
}