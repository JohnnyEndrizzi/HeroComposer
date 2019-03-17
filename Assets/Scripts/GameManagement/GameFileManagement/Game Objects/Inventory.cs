//Serializable JSON object
using System.Collections.Generic;

[System.Serializable]
public class Inventory{
    //Player's available money
    public int money;
    //Player's inventory of items
    public List<Item> items;

    //Add item to inventory
    public void AddItem(Item item)
    {
        //Create list if it doesn't exist yet
        if(items == null)
        {
            items = new List<Item>();
        }
        items.Add(item);
    }
}