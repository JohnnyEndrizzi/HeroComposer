using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickListener : MonoBehaviour
{
    public Sprite ATK_sprite;
    public Sprite DEF_sprite;
    public Sprite MGC_sprite;
    public Sprite ULT_sprite;

    public enum state {ATK, DEF, MGC, ULT};
    public static state menu_state;

    void Start()
    {
        menu_state = state.ATK;
    }

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("q")))
        {
            GetComponent<SpriteRenderer>().sprite = ATK_sprite;
            menu_state = state.ATK;
        }
        else if (Event.current.Equals(Event.KeyboardEvent("w")))
        {
            GetComponent<SpriteRenderer>().sprite = DEF_sprite;
            menu_state = state.DEF;
        }
        else if (Event.current.Equals(Event.KeyboardEvent("e")))
        {
            GetComponent<SpriteRenderer>().sprite = MGC_sprite;
            menu_state = state.MGC;
        }
        else if (Event.current.Equals(Event.KeyboardEvent("r")))
        {
            GetComponent<SpriteRenderer>().sprite = ULT_sprite;
            menu_state = state.ULT;
        }
    }
}
