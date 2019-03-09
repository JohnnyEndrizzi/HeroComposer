using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class JSONFiles
{
    public const string Characters = "characters.json";
    public const string Items = "items.json";
    public const string Inventory = "inventory.json";
    public const string Party = "party.json";
}

public class GameDataManager : MonoBehaviour {

    //GameFileHandler for interacting with game files
    public GameFileHandler gameFileHandler;

    //GameObjects
    //Characters
    private Dictionary<string,Character> characters;
    //Items
    private Dictionary<string,Item> items;
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
        items = gameFileHandler.LoadJSONAsGameObjectDictionary<Item>(JSONFiles.Items);
        inventory = gameFileHandler.LoadJSONAsGameObject<Inventory>(JSONFiles.Inventory);
        party = gameFileHandler.LoadJSONAsGameObject<Party>(JSONFiles.Party);
    }

    //Return the current characters in players party
    public Dictionary<int,Character> GetCharactersInParty()
    {
        return party.characters.Keys.ToDictionary(x => int.Parse(x), x => party.characters[x]);
    }
    
}
