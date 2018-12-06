using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadData : MonoBehaviour
{
    Character[] characters;
    Items[] items;
    Magic[] magic;
    Inventory[] inventory;

    public void LoadCharacters()
    {
        string jsonString = LoadResourceTextfile("characters.json");
        characters = JsonHelper.getJsonArray<Character>(jsonString);

        string[] guids = AssetDatabase.FindAssets("t:CharacterScriptObject");  

        for (int i = 0; i < guids.Length; i++)         
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);

            for (int j = 0; j < characters.Length; j++)
            {
                if (AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).name == characters[j].name)
                {
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).name = characters[j].name;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).desc = characters[j].desc;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).sprite = "Characters/" + characters[j].sprite;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).headshot = "Headshots/" + characters[j].headshot;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).sound = "SoundEffects/" + characters[j].sound;

                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).unlocked = characters[j].unlocked;
                    
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).level = characters[j].level;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).hp = characters[j].hp;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).atk = characters[j].atk;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).def = characters[j].def;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).mgc = characters[j].mgc;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).rcv = characters[j].rcv;

                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).eqp1 = characters[j].eqp1;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).eqp2 = characters[j].eqp2;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).eqp3 = characters[j].eqp3;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).mag_Eqp = characters[j].mag_Eqp;
                }
                int[] eqp = {characters[j].eqp1, characters[j].eqp2, characters[j].eqp3};
                int[] stats = { characters[j].level, characters[j].hp, characters[j].atk, characters[j].def, characters[j].mgc, characters[j].rcv};
                GameObject.Find("Values").GetComponent<StoredValues>().importUnits(j, characters[j].name, characters[j].desc, "Characters/" + characters[j].sprite, "SoundEffects/" + characters[j].sound, eqp, characters[j].unlocked, stats, characters[j].mag_Eqp);
            }
            GameObject.Find("Values").GetComponent<StoredValues>().nullUnit();
        }
    }

    public void LoadInv()
    {
        string jsonString = LoadResourceTextfile("inventory.json");
        inventory = JsonHelper.getJsonArray<Inventory>(jsonString);

        string[] guids = AssetDatabase.FindAssets("t:CharacterScriptObject");

        GameObject.Find("Values").GetComponent<StoredValues>().importInventory(inventory[0].StoredItems.Split(';'));
        string[] tempNames = inventory[0].SelUnits.Split(';');

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);

            if (AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).name == tempNames[0])
            {Assets.Scripts.MainMenu.ApplicationModel.characters[0] = AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path);}
            else if (AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).name == tempNames[1])
            { Assets.Scripts.MainMenu.ApplicationModel.characters[1] = AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path); }
            else if (AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).name == tempNames[2])
            { Assets.Scripts.MainMenu.ApplicationModel.characters[2] = AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path); }
            else if (AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).name == tempNames[3])
            { Assets.Scripts.MainMenu.ApplicationModel.characters[3] = AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path); }
        }   
    }
    
    public void LoadItems()
    {
        string jsonString = LoadResourceTextfile("items.json");
        items = JsonHelper.getJsonArray<Items>(jsonString);
        string path = AssetDatabase.GUIDToAssetPath("Resources/Items");

        GameObject.Find("Values").GetComponent<StoredValues>().nullItem();
        for (int j = 0; j < items.Length; j++)
        {
            GameObject.Find("Values").GetComponent<StoredValues>().importItems(j+1, items[j].NameKey, items[j].NameTitle, items[j].Desc, "Items/" + items[j].sprite, items[j].cost);
            //"SoundEffects/" + items[j].sound                
        }
    }

    public void LoadMagic()
    {
        string jsonString = LoadResourceTextfile("characters.json");
        characters = JsonHelper.getJsonArray<Character>(jsonString);

        string[] guids = AssetDatabase.FindAssets("t:CharacterScriptObject");
        CharacterScriptObject[] a = new CharacterScriptObject[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path);
            Debug.Log(a[i].unlocked);

            for (int j = 0; j < characters.Length; j++)
            {
                if (AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).name == characters[j].name)
                {
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).name = characters[j].name;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).desc = characters[j].desc;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).sprite = "Characters/" + characters[j].sprite;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).sound = "SoundEffects/" + characters[j].sound;

                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).unlocked = characters[j].unlocked;

                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).level = characters[j].level;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).hp = characters[j].hp;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).atk = characters[j].atk;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).def = characters[j].def;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).mgc = characters[j].mgc;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).rcv = characters[j].rcv;

                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).eqp1 = characters[j].eqp1;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).eqp2 = characters[j].eqp2;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).eqp3 = characters[j].eqp3;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).mag_Eqp = characters[j].mag_Eqp;
                    //AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).mgc_animation = characters[j].mgc_animation;
                }
            }
        }
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