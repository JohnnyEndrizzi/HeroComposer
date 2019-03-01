using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickListener : MonoBehaviour
{
    public Sprite ATK_sprite;
    public Sprite DEF_sprite;
    public Sprite MGC_sprite;

    public enum state {ATK, DEF, MGC};
    public static state menu_state;
    public int test;

    void Start()
    {
        menu_state = state.ATK;
        test = 0;
    }

    /* This function will return the current state of the menu (ATK, DEF, or MGC) */
    public state GetMenuState()
    {
        return menu_state;
    }

    /* Functional Requirement
     * ID: 8.1-3
     * Description: The player must be able to input battle commands.
     * 
     * This function will alter the current state of the menu (ATK, DEF, or MGC) */
    public void ChangeMenuState(Sprite s, state state)
    {
        
        if (state == state.ATK)
        {
            GameObject.Find("SwordSymbol").GetComponent<SpriteRenderer>().enabled = true;

            GameObject.Find("ShieldSymbol").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("MagicSymbol").GetComponent<SpriteRenderer>().enabled = false;
        }
        
        else if (state == state.DEF)
        {
            GameObject.Find("ShieldSymbol").GetComponent<SpriteRenderer>().enabled = true;

            GameObject.Find("SwordSymbol").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("MagicSymbol").GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GameObject.Find("MagicSymbol").GetComponent<SpriteRenderer>().enabled = true;

            GameObject.Find("SwordSymbol").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("ShieldSymbol").GetComponent<SpriteRenderer>().enabled = false;
        }
        
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
         * S - MGC */
        if (Input.GetKey(KeyCode.A))
        {
            ChangeMenuState(DEF_sprite, state.DEF);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            ChangeMenuState(MGC_sprite, state.MGC);
        }
        else
        {
            ChangeMenuState(ATK_sprite, state.ATK);
        }
    }
}
