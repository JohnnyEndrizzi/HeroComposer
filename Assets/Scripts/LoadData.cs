using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LoadData : MonoBehaviour
{
    Character[] characters;

    public void LoadCharacters()
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
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).unlocked = characters[j].unlocked;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).sprite = "Characters/" + characters[j].sprite;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).level = characters[j].level;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).hp = characters[j].hp;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).atk = characters[j].atk;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).def = characters[j].def;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).mgc = characters[j].mgc;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).rcv = characters[j].rcv;
                    AssetDatabase.LoadAssetAtPath<CharacterScriptObject>(path).mgc_animation = characters[j].mgc_animation;
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
