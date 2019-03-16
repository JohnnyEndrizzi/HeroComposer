using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class JSONFiles
{
    public const string Characters = "characters.json";
    public const string MagicAbilities = "magicAbilities.json";
    public const string Items = "items.json";
    public const string Levels = "levels.json";
    public const string Inventory = "inventory.json";
    public const string Party = "party.json";
}

public class GameDataManager : MonoBehaviour {

    //GameFileHandler for interacting with game files
    public GameFileHandler gameFileHandler;

    //GAME OBJECTS
    //Characters
    private Dictionary<string,Character> characters;
    //Magic Abilities
    private Dictionary<string, MagicAbility> magicAbilities;
    //Items
    private Dictionary<string,Item> items;
    //Levels
    private Dictionary<string, Level> levels;
    //Inventory
    private Inventory inventory;
    //Party
    private Party party;

    // Use this for initialization
    void Start () {
        //Load any data needed at the start of the game
        InitializeGameData();
    }   

    //Load all necessary game data when the game starts
    private void InitializeGameData()
    {
        characters = gameFileHandler.LoadJSONAsGameObjectDictionary<Character>(JSONFiles.Characters);
        magicAbilities = gameFileHandler.LoadJSONAsGameObjectDictionary<MagicAbility>(JSONFiles.MagicAbilities);
        items = gameFileHandler.LoadJSONAsGameObjectDictionary<Item>(JSONFiles.Items);
        levels = gameFileHandler.LoadJSONAsGameObjectDictionary<Level>(JSONFiles.Levels);
        inventory = gameFileHandler.LoadJSONAsGameObject<Inventory>(JSONFiles.Inventory);
        party = gameFileHandler.LoadJSONAsGameObject<Party>(JSONFiles.Party);
    }

    //Set level to unlocked
    public void unlockLevel(int level)
    {
        var newlevel = levels[Convert.ToString(level)];
        newlevel.locked = false;
        levels[Convert.ToString(level)] = newlevel;
        gameFileHandler.SaveGameObjectAsJSON(levels, JSONFiles.Levels);
    }

    //Get specific level
    public Level getSpecificLevel(int level)
    {
        return levels[Convert.ToString(level)];
    }

    public void setCutsceneOfLeveltoSeen(int level)
    {
        var newlevel = levels[Convert.ToString(level)];
        newlevel.cutsceneDisplayed = true;
        levels[Convert.ToString(level)] = newlevel;
        gameFileHandler.SaveGameObjectAsJSON(levels, JSONFiles.Levels);
    }

    //Get the locked levels
    public Dictionary<int, Level> getLockedLevels()
    {
        return levels.Where(kvp => kvp.Value.locked).ToDictionary(x => Convert.ToInt32(x.Key), x => x.Value);
    }

    //Return all characters
    public Dictionary<string, Character> GetCharacters()
    {
        return characters;
    }

    //Check if character is in party 
    public bool IsCharacterInParty(Character character)
    {
        return party.IsCharacterInParty(character);
    }

    //Set characters in party
    public void SetCharactersInParty(Dictionary<int, Character> party)
    {
        this.party.characters = party.Keys.ToDictionary(x => x.ToString(), x => party[x]);
        gameFileHandler.SaveGameObjectAsJSON(this.party,JSONFiles.Party);
    }

    //Return the current characters in players party
    public Dictionary<int,Character> GetCharactersInParty()
    {
        return party.characters.Keys.ToDictionary(x => int.Parse(x), x => party.characters[x]);
    }
    
}
