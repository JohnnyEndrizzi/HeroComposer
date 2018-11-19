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
    public int test;

    void Start()
    {
        menu_state = state.ATK;
        test = 0;
    }

    public state GetMenuState()
    {
        return menu_state;
    }

    public void ChangeMenuState(Sprite s, state state)
    {
        GetComponent<SpriteRenderer>().sprite = s;
        menu_state = state;
    }

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("q")))
        {
            ChangeMenuState(ATK_sprite, state.ATK);
        }
        else if (Event.current.Equals(Event.KeyboardEvent("w")))
        {
            ChangeMenuState(DEF_sprite, state.DEF);
        }
        else if (Event.current.Equals(Event.KeyboardEvent("e")))
        {
            ChangeMenuState(MGC_sprite, state.MGC);
        }
        else if (Event.current.Equals(Event.KeyboardEvent("r")))
        {
            ChangeMenuState(ULT_sprite, state.ULT);
        }
    }
}
