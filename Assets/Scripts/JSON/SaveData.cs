using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    /* Functional Requirement 
   * ID: 8.2-9
   * Description: The player must be able to save the game. 
   * 
   * This class controls the exporting of data to save files  */

    Inventory[] inventory;
    Levels[] levels;
    Dictionary<int, LevelDict> Lvls = new Dictionary<int, LevelDict>();


    public void SaveAll()
    {
        SaveCharacters();
        SaveInv(true, true);
        //SaveScore();
        //SaveMoney?();
    }

    public void SaveCharacters()
    {
        CharacterScriptObject[] characters = Resources.LoadAll<CharacterScriptObject>("ScriptableObjects/Characters");

        string data = "[";
        for (int i = 0; i < characters.Length; i++)
        {
            data += JsonUtility.ToJson(characters[i], true);
            if (i < characters.Length - 1) data += ",";
        }
        data += "]";

        File.WriteAllText("Assets/Resources/Metadata/characters.json", data);
    }

    public void SaveScores() //Save scores to JSON  //mostly UNTESTED
    {
        string jsonString = LoadResourceTextfile("levels.json");
        levels = JsonHelper.getJsonArray<Levels>(jsonString);

        string data = "[{\n"; //file start

        for (int i = 0; i < levels.Length; i++)
        {
            //read only
            data += ("\"LevelName\": \"" + levels[i].LevelName + "\",\n");
            data += ("\"LevelSong\": \"" + levels[i].LevelSong + "\",\n");
            data += ("\"Enemy\": \""     + levels[i].Enemy + "\",\n");
            data += ("\"Terrain\": \""   + levels[i].Terrain + "\",\n");

            //write scores
            data += ("\"scoreNormal\": [");
            for (int j = 0; j < 5; j++)
            {
                data += (Lvls[i].scoreNormal[j] + ",");
            }
            data.Remove(data.Length - 1);
            data += ("],\n" + "\"scoreHard\": [");
            for (int j = 0; j < 5; j++)
            {
                data += (Lvls[i].scoreHard[j] + ",");
            }
            data.Remove(data.Length - 1);
            data += ("],\n" + "\"scoreExpert\": [");
            for (int j = 0; j < 5; j++)
            {
                data += (Lvls[i].scoreExpert[j] + ",");
            }
            data.Remove(data.Length - 1);
            data += ("],\n");
        }
        data.Remove(data.Length - 1); //remove trailing \n
        data.Remove(data.Length - 1); //remove trailing ","
        data += "\n}]"; //file end

        File.WriteAllText("Assets/Resources/Metadata/testing.json", data);
    }
    
    //Callable For Inventory Saving
    public void SaveInv(bool Item, bool Team)
    {
        string data = "[{\n"; //file start

        if (Item) { data = SaveInvItemsJSON(data); }
        else      { data = SaveInvExceptRowJSON(data, 0); }
        data += ",\n";

        if (Team) { data = SaveInvTeamJSON(data); }
        else      { data = SaveInvExceptRowJSON(data, 1); }
        data += "\n}]"; //file end

        File.WriteAllText("Assets/Resources/Metadata/inventory.json", data);
    }

    private string SaveInvItemsJSON(string data) //Saved stored inventory
    {
        int[] storedItems = GetComponent<StoredValues>().GetStoredItems();
        int zeroes = 0; //to crop trailing zeroes

        data += "\t\"StoredItems\":\"";
        for (int i = 0; i < storedItems.Length; i++)
        {
            if (storedItems[i] == 0)
            {
                zeroes++;
            }
            else
            {
                for (int j = 0; j < zeroes; j++)
                {
                    data += 0 + ";";
                }
                data += storedItems[i] + ";";
                zeroes = 0;
            }
        }
        if (storedItems.Length > 0) { data = data.Remove(data.Length - 1); } //crop trailing ";"
        data += "\"";
        return data;
    }

    private string SaveInvTeamJSON(string data) //Save selected units
    {
        data += "\t\"SelUnits\":\"";
        for (int i = 0; i < 4; i++)
        {
            if (Assets.Scripts.MainMenu.ApplicationModel.characters[i])
            {
                data += Assets.Scripts.MainMenu.ApplicationModel.characters[i].name;
            }
            else
            {
                data += "0";
            }
            if (i != 3) { data += ";"; }
        }
        data += "\"";
        return data;
    }

    private string SaveInvExceptRowJSON(string data, int row)
    {
        string jsonString = LoadResourceTextfile("inventory.json");
        inventory = JsonHelper.getJsonArray<Inventory>(jsonString);

        if (row == 0)
        {
            data += "\t\"StoredItems\":\"";
            data += inventory[0].StoredItems;
        }
        else if (row == 1)
        {
            data += "\t\"SelUnits\":\"";
            data += inventory[0].SelUnits;
        }
        data += "\"";
        return data;
    }

    //Load file to string from path
    public static string LoadResourceTextfile(string path)
    {
        string filePath = "Metadata/" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }
}


/*
    //Imported types from JSON
    Character[] characters;
    Inventory[] inventory;
    Items[] items;
    Magic[] magic;

    // Load Unit data from file to CharacterScriptableObject for use in game and Unit Dictionary for use elsewhere
    public void LoadCharacters()
    {
        string jsonString = LoadResourceTextfile("characters.json");
        characters = JsonHelper.getJsonArray<Character>(jsonString);

        Debug.Log("TEST: " + jsonString);

        for (int i = 0; i < characters.Length; i++)
        {
            CharacterScriptObject currentCharacterSO = (CharacterScriptObject)Resources.Load("ScriptableObjects/Characters/" + characters[i].name);
            if (Resources.Load("ScriptableObjects/Characters/" + characters[i].name))
            {
                Debug.Log(characters[i].name);
                currentCharacterSO.name = characters[i].name;
                currentCharacterSO.desc = characters[i].desc;
                currentCharacterSO.sprite = characters[i].sprite;
                currentCharacterSO.headshot = characters[i].headshot;
                currentCharacterSO.sound = characters[i].sound;

                currentCharacterSO.unlocked = characters[i].unlocked;

                currentCharacterSO.level = characters[i].level;
                currentCharacterSO.hp = characters[i].hp;
                currentCharacterSO.atk = characters[i].atk;
                currentCharacterSO.def = characters[i].def;
                currentCharacterSO.mgc = characters[i].mgc;
                currentCharacterSO.rcv = characters[i].rcv;

                currentCharacterSO.eqp1 = characters[i].eqp1;
                currentCharacterSO.eqp2 = characters[i].eqp2;
                currentCharacterSO.eqp3 = characters[i].eqp3;
                currentCharacterSO.mag_Eqp = characters[i].mag_Eqp;

                UnityEditor.EditorUtility.SetDirty(currentCharacterSO);
            }
            else
            {
                Debug.Log("HEY " + characters[i].name);
            }

            int[] eqp = { characters[i].eqp1, characters[i].eqp2, characters[i].eqp3 };
            int[] stats = { characters[i].level, characters[i].hp, characters[i].atk, characters[i].def, characters[i].mgc, characters[i].rcv };

            GameObject.Find("Values").GetComponent<StoredValues>().importUnits(i, characters[i].name, characters[i].desc, characters[i].sprite, characters[i].sound, eqp, characters[i].unlocked, stats, characters[i].mag_Eqp);
            GameObject.Find("Values").GetComponent<StoredValues>().nullUnit();
        }
    }



    // Load inventory and team data from file
    public void LoadInv()
    {
        string jsonString = LoadResourceTextfile("inventory.json");
        inventory = JsonHelper.getJsonArray<Inventory>(jsonString);

        GameObject.Find("Values").GetComponent<StoredValues>().importInventory(inventory[0].StoredItems.Split(';'));
        string[] tempNames = inventory[0].SelUnits.Split(';');

        for (int i = 0; i < characters.Length - 1; i++)
        {
            CharacterScriptObject currentCharacterSO = (CharacterScriptObject)Resources.Load("ScriptableObjects/Characters/" + characters[i].name);
            if (Resources.Load("ScriptableObjects/Characters/" + characters[i].name))
            {
                if (currentCharacterSO.name == tempNames[0])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[0] = currentCharacterSO;
                }
                else if (currentCharacterSO.name == tempNames[1])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[1] = currentCharacterSO;
                }
                else if (currentCharacterSO.name == tempNames[2])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[2] = currentCharacterSO;
                }
                else if (currentCharacterSO.name == tempNames[3])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[3] = currentCharacterSO;
                }
            }
        }
    }

    

    // Load Item data from file to Item Dictionary
    public void LoadItems()
    {
        string jsonString = LoadResourceTextfile("items.json");
        items = JsonHelper.getJsonArray<Items>(jsonString);

        GameObject.Find("Values").GetComponent<StoredValues>().nullItem();
        for (int j = 0; j < items.Length; j++)
        {
            GameObject.Find("Values").GetComponent<StoredValues>().importItems(j + 1, items[j].NameKey, items[j].NameTitle, items[j].Desc, "Items/" + items[j].sprite, items[j].cost);
            //"SoundEffects/" + items[j].sound                
        }
    }

    // Load Attack sprites and sound effects from file to AttackAnimator
    public void LoadMagic()
    {
        // TODO
    }

    //Load file to string from path
    public static string LoadResourceTextfile(string path)
    {
        string filePath = "Metadata/" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }
}

*/
