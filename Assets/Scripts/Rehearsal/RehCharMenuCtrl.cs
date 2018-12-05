using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RehCharMenuCtrl : MonoBehaviour {

    //Gameobject locations
    [SerializeField]
    private GameObject buttonTemplate = null;

    public Dictionary<int, UnitDict> Units;

    private List<GameObject> buttons;

    public void creator()
    {//Start but triggered after information is passed in
        buttons = new List<GameObject>();

        if (buttons.Count > 0)
        { //Destroy buttons to allow for reload //TODO needed?
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        for (int i = 0; i < Units.Count; i++)
        {
            if (Units[i].Unlocked == true)
            {
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);

                button.GetComponent<CharMenuBtn>().SetText(Units[i].unitName);
                button.GetComponent<CharMenuBtn>().SetImage(Units[i].img);
                button.GetComponent<CharMenuBtn>().SetID(i);

                button.transform.SetParent(buttonTemplate.transform.parent, false);
            }
        }
    }


    public void ButtonClicked(int intID)
    { //TODO clean up protections
        GameObject.Find("InvController").GetComponent<InvController>().setImage(Units[intID].img);
        GameObject.Find("InvController").GetComponent<InvController>().loadInv(intID);
    }
}
