using DummyFiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour {
    public string characterId;
    private CharacterType type;
    private string characterName;
    public List<GameItems> equippedItems;
    private string characterBackstory;
    private int characterCost;
    private object character;
    private int attack;
    private int defense;
    private int magic;

    public static Characters GenerateCharacter()
    {
        return new Characters { } ;
    }

    public Characters getCharacterInfo(Characters character)
    {
        return new Characters { };
    }

    public void equipItem(GameItems item)
    {

    }


    
    
	

}

public enum CharacterType
{
    Attacker = 1,
    Defender = 2,
    Mage = 3,
    Healer = 4
}

