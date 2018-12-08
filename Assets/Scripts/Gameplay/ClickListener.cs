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

    /* This function will return the current state of the menu (ATK, DEF, MGC, or ULT) */
    public state GetMenuState()
    {
        return menu_state;
    }

    /* Functional Requirement
     * ID: 8.1-3
     * Description: The player must be able to input battle commands.
     * 
     * This function will alter the current state of the menu (ATK, DEF, MGC, or ULT) */
    public void ChangeMenuState(Sprite s, state state)
    {
        GetComponent<SpriteRenderer>().sprite = s;
        menu_state = state;
    }

    void OnGUI()
    {
        /* Functional Requirement
         * ID: 8.1-3
         * Description: The player must be able to input battle commands.
         *
         * The current keyboard layout for changing the menu input is WASD. We believed this will provide a much more
         * intuitive mapping from keyboard to game space. This will reduce the amount of time the player will look at 
         * the keyboard while they're playing, allowing them to focus on the gameplay instead.
         * 
         * W - ATK
         * A - DEF
         * S - ULT
         * D - MGC */
        if (Event.current.Equals(Event.KeyboardEvent("w")))
        {
            ChangeMenuState(ATK_sprite, state.ATK);
        }
        else if (Event.current.Equals(Event.KeyboardEvent("a")))
        {
            ChangeMenuState(DEF_sprite, state.DEF);
        }
        else if (Event.current.Equals(Event.KeyboardEvent("d")))
        {
            ChangeMenuState(MGC_sprite, state.MGC);
        }
        else if (Event.current.Equals(Event.KeyboardEvent("s")))
        {
            ChangeMenuState(ULT_sprite, state.ULT);
        }
    }
}
