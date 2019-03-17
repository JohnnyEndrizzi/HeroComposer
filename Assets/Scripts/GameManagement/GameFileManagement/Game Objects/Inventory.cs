//Serializable JSON object
using System.Collections.Generic;

[System.Serializable]
public class Inventory{
    public int money;
    public List<Item> items;

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