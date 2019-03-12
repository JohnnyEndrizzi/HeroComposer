//Serializable JSON object
using System.Collections.Generic;

[System.Serializable]
public class Character
{
    //Meta information
    public string name;
    public string description;
    public string sprite;
    public string headshot;
    public string soundEffect;

    //If character has been unlocked by player
    public bool unlocked;

    //Items equipped to character
    public Dictionary<string, Item> equippedItems;

    //Magic ability equipped to character
    public MagicAbility magicAbility;

    //Characer stats
    public int level;
    public int hp;
    public int atk;
    public int def;
    public int rcv;
    public int mgc;
}