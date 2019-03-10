using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Enum for portait indexing
public enum CharacterPosition
{
    FrontRow,
    CentreLeft,
    CentreRight,
    BackRow
}

//Main controller for Rehearsal scene
public class RehearsalHandler : MonoBehaviour {

    /* Functional Requirement 
    * ID: 8.1 1-15
    * Description: The player must be able to customise their team.
    * 
    * This class controls the selection of team members */

    /* Functional Requirement 
    * ID: 8.1 1-19
    * Description: The system must be able to calculate the players current stats. 
    * 
    * This class allows players to view all stats including their equipted items */

    //UI Layer
    [SerializeField]
    private UILayer rehearsalUI = null;

    //Party location buttons
    [SerializeField]
    private Portrait FrontRowPortrait = null; 
    [SerializeField]
    private Portrait CentreLeftPortrait = null;
    [SerializeField]
    private Button CentreRightButton = null;
    [SerializeField]
    private Button BackRowButton = null;

    //Audio
    private AudioSource audioSource;
    private AudioClip addCharacter;
    private AudioClip removeCharacter;
    private AudioClip pickUpCharacter;
    private AudioClip errorSound;


    //Used for initialization
    void Start()
    {
        //Show UI Layer
        rehearsalUI.Show();

        //Load sound effects
        audioSource = GetComponent<AudioSource>();
        addCharacter = (AudioClip)Resources.Load("SoundEffects/rehersal_add_character_to_team");
        removeCharacter = (AudioClip)Resources.Load("SoundEffects/rehersal_remove_character_from_team");
        pickUpCharacter = (AudioClip)Resources.Load("SoundEffects/rehersal_pick_up_character");
        errorSound = (AudioClip)Resources.Load("SoundEffects/shop_not_enough_money");

        //Load characters
        DisplayCharactersInPortraits();
    }

    //Display all portraits of characters in party 
    private void DisplayCharactersInPortraits()
    {
        Dictionary<int, Character> charactersInParty = GameManager.Instance.gameDataManager.GetCharactersInParty();
        foreach(CharacterPosition position in System.Enum.GetValues(typeof(CharacterPosition)))
        {
            if (charactersInParty.ContainsKey((int)position)){
                DisplayCharacterInPortrait(charactersInParty[(int)position], position);
            }
        }
    }

    //Load character sprite into portrait
    private void DisplayCharacterInPortrait(Character character, CharacterPosition position)
    {

        if(position == CharacterPosition.FrontRow)
        {
            FrontRowPortrait.DisplayCharacter(character);
        }
        else if(position == CharacterPosition.CentreLeft)
        {
            CentreLeftPortrait.DisplayCharacter(character);
        }
        else if(position == CharacterPosition.CentreRight)
        {
            CentreRightButton.GetComponentInChildren<Portrait>().DisplayCharacter(character);
        }
        else if(position == CharacterPosition.BackRow)
        {
            BackRowButton.GetComponentInChildren<Portrait>().DisplayCharacter(character);
        }
    }

    //Remove character sprite from portrait
    private void RemoveCharacterInPortrait(Portrait portrait)
    {
        portrait.RemoveCharacter();
    }
}