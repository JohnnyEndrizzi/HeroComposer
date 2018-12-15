using UnityEngine;
using System.IO;

public class LoadData : MonoBehaviour
{

    /* Functional Requirement 
   * ID: 8.2-9
   * Description: The player must be able to save the game. 
   * 
   * This class controls the exporting of data to save files  */

    /* Functional Requirement 
    * ID: 8.2-10
    * Description: The player must be able to load a saved game.
    * 
    * This class controls the loading of all data from to save files */

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

            int[] eqp = {characters[i].eqp1, characters[i].eqp2, characters[i].eqp3};
            int[] stats = { characters[i].level, characters[i].hp, characters[i].atk, characters[i].def, characters[i].mgc, characters[i].rcv};

            GameObject.Find("Values").GetComponent<StoredValues>().importUnits(i, characters[i].name, characters[i].desc, characters[i].sprite, characters[i].sound, eqp, characters[i].unlocked, stats, characters[i].mag_Eqp);
            GameObject.Find("Values").GetComponent<StoredValues>().nullUnit();
        }
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
            GameObject.Find("Values").GetComponent<StoredValues>().importItems(j+1, items[j].NameKey, items[j].NameTitle, items[j].Desc, "Items/" + items[j].sprite, items[j].cost);
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

/* The following is a wrapper class used for JSON (de)serialization.
 * Unity's JsonUtility doesn't support JSON arrays properly, so the wrapper
 * is required to make things easier. */
public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}