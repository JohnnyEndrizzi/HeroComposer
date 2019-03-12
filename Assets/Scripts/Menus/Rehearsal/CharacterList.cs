using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterList : Droppable
{
    //Character portrait prefab
    [SerializeField]
    private GameObject characterPortraitPrefab;

    //Characters in list
    private Dictionary<string, bool> charactersInList;

    //Called before start
    private void Awake()
    {
        charactersInList = new Dictionary<string, bool>();
    }

    //Add character to list
    public void AddCharacter(Character character)
    {
        GameObject characterPortrait = Instantiate(characterPortraitPrefab, GetComponent<ScrollRect>().content.transform);
        characterPortrait.GetComponent<Portrait>().DisplayCharacter(character);
        charactersInList.Add(character.name, true);
    }

    //Remove character from list
    public void RemoveCharacter(Character character)
    {
        charactersInList.Remove(character.name);
    }

    //Called when character is dropped over list
    public override void OnDrop(PointerEventData eventData)
    {
        //Get character being dragged
        Character characterBeingDragged = Draggable.itemBeingDragged.GetComponent<Portrait>().character;
        //Add character to list if it doesn't already exist
        if (!charactersInList.ContainsKey(characterBeingDragged.name))
        {
            AddCharacter(characterBeingDragged);
            Draggable.itemBeingDragged.GetComponent<Portrait>().RemoveCharacter();
        }
    }
}
