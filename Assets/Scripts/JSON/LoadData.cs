using UnityEngine;

public class LoadData : MonoBehaviour
{

    /* Functional Requirement 
    * ID: 8.1 1-8
    * Description: The system must display the inventory screen.
    * 
    * This class controls the Inventory screen, it does anything required for the scene to run including 
    * deciding what to display, where to display and any movements of items*/

    Character[] characters;
    Items[] items;
    Magic[] magic;
    Inventory[] inventory;

    public void LoadCharacters()
    {
        string jsonString = LoadResourceTextfile("characters.json");
        characters = JsonHelper.getJsonArray<Character>(jsonString);

        for (int i = 0; i < characters.Length; i++)         
        {
            CharacterScriptObject currentCharacterSO = (CharacterScriptObject)Resources.Load("ScriptableObjects/Characters/" + characters[i].name);
            if (Resources.Load("ScriptableObjects/Characters/" + characters[i].name))
            {
                currentCharacterSO.name = characters[i].name;
                currentCharacterSO.desc = characters[i].desc;
                currentCharacterSO.sprite = "Characters/" + characters[i].sprite;
                currentCharacterSO.headshot = "Headshots/" + characters[i].headshot;
                currentCharacterSO.sound = "SoundEffects/" + characters[i].sound;

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
            }

            int[] eqp = {characters[i].eqp1, characters[i].eqp2, characters[i].eqp3};
            int[] stats = { characters[i].level, characters[i].hp, characters[i].atk, characters[i].def, characters[i].mgc, characters[i].rcv};

            GameObject.Find("Values").GetComponent<StoredValues>().importUnits(i, characters[i].name, characters[i].desc, "Characters/" + characters[i].sprite, "SoundEffects/" + characters[i].sound, eqp, characters[i].unlocked, stats, characters[i].mag_Eqp);
            GameObject.Find("Values").GetComponent<StoredValues>().nullUnit();
        }
    }

    public void LoadInv()
    {
        string jsonString = LoadResourceTextfile("inventory.json");
        inventory = JsonHelper.getJsonArray<Inventory>(jsonString);

        GameObject.Find("Values").GetComponent<StoredValues>().importInventory(inventory[0].StoredItems.Split(';'));
        string[] tempNames = inventory[0].SelUnits.Split(';');

        for (int i = 0; i < characters.Length; i++)
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

    public void LoadMagic()
    {
       // TODO
    }

    public static string LoadResourceTextfile(string path)
    {
        string filePath = "Metadata/" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }
}

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.array = array;
        return JsonUtility.ToJson(wrapper);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}