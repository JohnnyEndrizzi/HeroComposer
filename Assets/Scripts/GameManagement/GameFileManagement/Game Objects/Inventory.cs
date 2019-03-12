//Serializable JSON object
using System.Collections.Generic;

[System.Serializable]
public class Inventory{
    public int money;
    public Dictionary<string, Item> items;    
}