//Serializable JSON object
using System.Collections.Generic;

[System.Serializable]
public class Inventory{
    //Player's available money
    public int money;

    //Player's inventory of items
    public Dictionary<string, Item> invItems;    
}