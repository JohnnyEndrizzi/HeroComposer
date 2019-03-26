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
    private Portrait frontRowPortrait = null; 
    [SerializeField]
    private Portrait centreLeftPortrait = null;
    [SerializeField]
    private Portrait centreRightPortrait = null;
    [SerializeField]
    private Portrait backRowPortrait = null;

    //Character list
    [SerializeField]
    private CharacterList characterList; 

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

        //Load characters in party
        DisplayParty();

        //Load character list
        LoadCharacterList();
    }

    //Populate character list scrollbox
    private void LoadCharacterList()
    {
        Dictionary<string, Character> characters = GameManager.Instance.gameDataManager.GetCharacters();
        foreach(Character character in characters.Values)
        {
            //Only add character to list if not in party
            if (!GameManager.Instance.gameDataManager.IsCharacterInParty(character))
            {
                //Only add character to list if unlocked
                if (character.unlocked)
                {
                    characterList.AddCharacter(character);
                }
            }
        }
    }

    //Display all portraits of characters in party 
    private void DisplayParty()
    {
        Dictionary<int, Character> charactersInParty = GameManager.Instance.gameDataManager.GetCharactersInParty();
        foreach(CharacterPosition position in System.Enum.GetValues(typeof(CharacterPosition)))
        {
            Character character = null;
            if (charactersInParty.ContainsKey((int)position)){
                character = charactersInParty[(int)position];
            }

            if (position == CharacterPosition.FrontRow)
            {
                frontRowPortrait.DisplayCharacter(character);
            }
            else if (position == CharacterPosition.CentreLeft)
            {
                centreLeftPortrait.DisplayCharacter(character);
            }
            else if (position == CharacterPosition.CentreRight)
            {
                centreRightPortrait.DisplayCharacter(character);
            }
            else if (position == CharacterPosition.BackRow)
            {
                backRowPortrait.DisplayCharacter(character);
            }
        }
    }

    //Save party to local game data and JSON file
    public void SaveParty()
    {
        //Check if party is empty
        if (frontRowPortrait.character == null && centreLeftPortrait.character == null && centreRightPortrait.character == null && backRowPortrait.character == null)
        {
            //Display message
            Modal messageModal = Instantiate(GameManager.Instance.prefabManager.simpleMessageModal, FindObjectOfType<UIContainer>().transform);
            messageModal.DisplayMessage("Party cannot be empty! Please try again.");
            messageModal.ChangeBanner(ModalBanner.Error);
        }
        //Save party
        else
        {
            Dictionary<int, Character> party = new Dictionary<int, Character>();
            if (frontRowPortrait.character != null)
                party[(int)CharacterPosition.FrontRow] = frontRowPortrait.character;
            if (centreLeftPortrait.character != null)
                party[(int)CharacterPosition.CentreLeft] = centreLeftPortrait.character;
            if (centreRightPortrait.character != null)
                party[(int)CharacterPosition.CentreRight] = centreRightPortrait.character;
            if (backRowPortrait.character != null)
                party[(int)CharacterPosition.BackRow] = backRowPortrait.character;
            GameManager.Instance.gameDataManager.SetCharactersInParty(party);
            //Display message
            Modal messageModal = Instantiate(GameManager.Instance.prefabManager.simpleMessageModal, FindObjectOfType<UIContainer>().transform);
            messageModal.DisplayMessage("Party saved succesfully!");
            messageModal.ChangeBanner(ModalBanner.Success);
        }
    }
}