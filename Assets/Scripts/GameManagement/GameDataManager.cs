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
    //Acitve Level
    private Level activeLevel;

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

    //Set active level
    public void SetActiveLevel(string songName)
    {
        activeLevel = levels[songName];
    }

    //Get active level
    public Level GetActiveLevel()
    {
        return activeLevel;
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
