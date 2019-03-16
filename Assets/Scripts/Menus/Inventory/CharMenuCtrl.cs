using System.Collections.Generic;
using UnityEngine;

public class CharMenuCtrl : MonoBehaviour {
    //Controls Unit Menu in Inventory

    //Gameobject locations
    [SerializeField]
    private GameObject buttonTemplate = null;

    //Characters in list
    private Dictionary<string, bool> charactersInList;

    //Called before start
    private void Awake()
    {
        charactersInList = new Dictionary<string, bool>();
        LoadCharacterList();
    }

    //Populate character list scrollbox
    private void LoadCharacterList()
    {
        Dictionary<string, Character> characters = GameManager.Instance.gameDataManager.GetCharacters();
        foreach (Character character in characters.Values)
        {            
            //Only add character to list if unlocked
            if (character.unlocked)
            {
                //Add character to list
                GameObject characterPortrait = Instantiate(buttonTemplate);
                characterPortrait.SetActive(true);
                characterPortrait.GetComponent<CharMenuBtn>().DisplayCharacter(character);
                characterPortrait.transform.SetParent(buttonTemplate.transform.parent, false);

                charactersInList.Add(character.name, true);
            }            
        }
    }
}