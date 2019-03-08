using System.Collections.Generic;
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
    Item[] items;
    Magic[] magic;
    Levels[] levels;

    Dictionary<int, LevelDict> Lvls = new Dictionary<int, LevelDict>();

    // Load Unit data from file to CharacterScriptableObject for use in game and Unit Dictionary for use elsewhere
    public void LoadCharacters()
    {
        string jsonString = LoadResourceTextfileStreaming("JSON/characters.json");
        characters = JSONUtilityWrapper.getJsonArray<Character>(jsonString);

        //Debug.Log("TEST: " + jsonString);

        for (int i = 0; i < characters.Length; i++)         
        {
            CharacterScriptObject currentCharacterSO = (CharacterScriptObject)Resources.Load("ScriptableObjects/Characters/" + characters[i].charName);
            if (Resources.Load("ScriptableObjects/Characters/" + characters[i].charName))
            {
                //Debug.Log(characters[i].name);
                currentCharacterSO.charName = characters[i].charName;
                //Debug.Log(currentCharacterSO.name);
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

                //UnityEditor.EditorUtility.SetDirty(currentCharacterSO);
            }

            int[] eqp = {characters[i].eqp1, characters[i].eqp2, characters[i].eqp3};
            int[] stats = { characters[i].level, characters[i].hp, characters[i].atk, characters[i].def, characters[i].mgc, characters[i].rcv};

            gameObject.GetComponent<StoredValues>().importUnits(i, characters[i].charName, characters[i].desc, characters[i].sprite, characters[i].sound, eqp, characters[i].unlocked, stats, characters[i].mag_Eqp);
            gameObject.GetComponent<StoredValues>().nullUnit();
        }
    }

    // Load inventory and team data from file
    public void LoadInv()
    {
       /* string jsonString = LoadResourceTextfileStreaming("inventory.json");
        inventory = JSONUtilityWrapper.getJsonArray<Inventory>(jsonString);

        gameObject.GetComponent<StoredValues>().importInventory(inventory[0].StoredItems.Split(';'));
        gameObject.GetComponent<StoredValues>().CashL(inventory[0].Money);

        string[] tempNames = inventory[0].SelUnits.Split(';');*/

        /*
        Debug.Log(tempNames[0]);
        Debug.Log(tempNames[1]);
        Debug.Log(tempNames[2]);
        Debug.Log(tempNames[3]);
        */

        /*
        for (int i = 0; i < tempNames.Length; i++)
        {
            CharacterScriptObject currentCharacterSO = (CharacterScriptObject)Resources.Load("ScriptableObjects/Characters/" + characters[i].charName);
            if (Resources.Load("ScriptableObjects/Characters/" + characters[i].charName))
            {
                if (currentCharacterSO.charName == tempNames[0])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[0] = currentCharacterSO;
                }
                else if (currentCharacterSO.charName == tempNames[1])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[1] = currentCharacterSO;
                }
                else if (currentCharacterSO.charName == tempNames[2])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[2] = currentCharacterSO;
                }
                else if (currentCharacterSO.charName == tempNames[3])
                {
                    Assets.Scripts.MainMenu.ApplicationModel.characters[3] = currentCharacterSO;
                }
            }
        }   
        */
    }       

    // Load Item data from file to Item Dictionary
    public void LoadItems()
    {
        string jsonString = LoadResourceTextfile("items.json");
        items = JSONUtilityWrapper.getJsonArray<Item>(jsonString);

        GameObject.Find("__app").GetComponent<StoredValues>().nullItem();
        for (int j = 0; j < items.Length; j++)
        {
            GameObject.Find("__app").GetComponent<StoredValues>().importItems(j+1, items[j].NameKey, items[j].NameTitle, items[j].Desc, "Items/" + items[j].sprite, items[j].cost);
            //"SoundEffects/" + items[j].sound                
        }
    }

    // Load Attack sprites and sound effects from file to AttackAnimator
    public void LoadMagic()
    {
       // TODO
    }

    public void LoadLevels()
    {
        string jsonString = LoadResourceTextfileStreaming("levels.json");
        levels = JSONUtilityWrapper.getJsonArray<Levels>(jsonString);

        for (int i = 0; i < levels.Length; i++)
        {
            Lvls.Add(i, new LevelDict(levels[i].LevelName, levels[i].LevelSong, levels[i].Enemy, levels[i].Terrain, levels[i].scoreNormal, levels[i].scoreHard, levels[i].scoreExpert, levels[i].nameNormal, levels[i].nameHard, levels[i].nameExpert));
        }
    }

    //Load file to string from path
    public static string LoadResourceTextfile(string path)
    {
        string filePath = "Metadata/" + path.Replace(".json", "");
        
        TextAsset targetFile = Resources.Load<TextAsset>(filePath);

        return targetFile.text;
    }

    public static string LoadResourceTextfileStreaming(string path)
    {    
        string fileText = File.ReadAllText(Application.streamingAssetsPath + "/" + path);        
        return fileText;
    }
}


public class LevelDict
{ //All level and scores Dictionary 
    public string LevelName; 
    public string LevelSong;    
    public string Enemy;
    public string Terrain;

    public int[] scoreNormal = new int[5];
    public string[] nameNormal = new string[5];
    public int[] scoreHard = new int[5];
    public string[] nameHard = new string[5];
    public int[] scoreExpert = new int[5];
    public string[] nameExpert = new string[5];

    public LevelDict(string LevelNameX, string LevelSongX, string EnemyX, string TerrainX, int[] scoreNormalX, int[] scoreHardX, int[] scoreExpertX, string[] nameNormalX, string[] nameHardX, string[] nameExpertX)
    {
        LevelName = LevelNameX;
        LevelSong = LevelSongX;
        Enemy = EnemyX;
        Terrain = TerrainX;
        scoreNormal = scoreNormalX;
        nameNormal = nameNormalX;
        scoreHard = scoreHardX;
        nameHard = nameHardX;
        scoreExpert = scoreExpertX;
        nameExpert = nameExpertX;
    }
}