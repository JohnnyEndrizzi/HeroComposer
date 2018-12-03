using UnityEngine;
using System.Collections;
using System;

public class CharacterSelector : MonoBehaviour
{
    public GameObject player;
    public Vector3 playerSpawnPosition = new Vector3(0, 1, -7);
    public int currentIndex = 0;

    Character[] characters;
    Character selectedCharacter;

    /*
    public void OverwriteCharacter()
    {
        player.GetComponent<CharacterLogic>().name = selectedCharacter.name;
        player.GetComponent<CharacterLogic>().unlocked = selectedCharacter.unlocked;
        player.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Characters/" + selectedCharacter.sprite);
        player.GetComponent<CharacterLogic>().level = selectedCharacter.level;
        player.GetComponent<CharacterLogic>().hp = selectedCharacter.hp;
        player.GetComponent<CharacterLogic>().atk = selectedCharacter.atk;
        player.GetComponent<CharacterLogic>().def = selectedCharacter.def;
        player.GetComponent<CharacterLogic>().mgc = selectedCharacter.mgc;
        player.GetComponent<CharacterLogic>().rcv = selectedCharacter.rcv;
        player.GetComponent<CharacterLogic>().mgc_animation = selectedCharacter.mgc_animation;
    }

    public void ReadFromJson()
    {
        Debug.Log("Updating Scriptable Object");

        player.GetComponent<CharacterLogic>().level = 10;



        Debug.Log(JsonUtility.ToJson(characters[currentIndex]));

        OverwriteCharacter();
    }

    public static string LoadResourceTextfile(string path)
    {
        string filePath = "Metadata/" + path.Replace(".json", "");
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }
    */

    void Start()
    {
        //string jsonString = LoadResourceTextfile("characters.json");
        //characters = JsonHelper.getJsonArray<Character>(jsonString);
    }

    public void OnCharacterSelect(int characterChoice)
    {
        selectedCharacter = characters[characterChoice];

        currentIndex = characterChoice;

        //OverwriteCharacter();
        player.name = "character_1";
    }
}

/*
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

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}
*/