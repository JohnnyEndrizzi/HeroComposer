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
        SaveInv(true, true, true);
        //SaveScore();
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

            //write scores and associated names
            data += ("\"scoreNormal\": [");
            for (int j = 0; j < 5; j++)
            {
                data += (Lvls[i].scoreNormal[j] + ",");
            }
            data.Remove(data.Length - 1);
            data += ("\"nameNormal\": [");
            for (int j = 0; j < 5; j++)
            {
                data += ("\"" + Lvls[i].nameNormal[j] + "\",");
            }
            data.Remove(data.Length - 1);

            data += ("],\n" + "\"scoreHard\": [");
            for (int j = 0; j < 5; j++)
            {
                data += (Lvls[i].scoreHard[j] + ",");
            }
            data.Remove(data.Length - 1);
            data += ("],\n" + "\"nameHard\": [");
            for (int j = 0; j < 5; j++)
            {
                data += ("\"" + Lvls[i].nameHard[j] + "\",");
            }
            data.Remove(data.Length - 1);

            data += ("],\n" + "\"scoreExpert\": [");
            for (int j = 0; j < 5; j++)
            {
                data += (Lvls[i].scoreExpert[j] + ",");
            }
            data.Remove(data.Length - 1);
            data += ("],\n" + "\"nameExpert\": [");
            for (int j = 0; j < 5; j++)
            {
                data += ("\"" + Lvls[i].nameExpert[j] + "\",");
            }
            data.Remove(data.Length - 1);

            data += ("]\n");
        }

        data += "}]"; //file end

        File.WriteAllText("Assets/Resources/Metadata/testing.json", data);
    }
    
    //Callable For Inventory Saving
    public void SaveInv(bool Item, bool Team, bool money)
    {
        string data = "[{\n"; //file start

        if (Item) { data = SaveInvItemsJSON(data); }
        else      { data = SaveInvExceptRowJSON(data, 0); }
        data += ",\n";

        if (Team) { data = SaveInvTeamJSON(data); }
        else      { data = SaveInvExceptRowJSON(data, 1); }
        data += ",\n";

        if (money) { data = SaveInvCashJSON(data); }
        else { data = SaveInvExceptRowJSON(data, 2); }
        data += "\n}]"; //file end

        File.WriteAllText("Assets/Resources/Metadata/inventory.json", data);
    }
    
    
    private string SaveInvCashJSON(string data) //Save money
    {
        data += "\t\"Money\": ";
        data += GetComponent<StoredValues>().CashS().ToString();  
        
        return data;
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
        else if (row == 2)
        {
            data += "\t\"Money\":";
            data += inventory[0].Money;
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