using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

//Classs to interact with game save files
public class GameFileHandler : MonoBehaviour  {
    //Load JSON file as (string, game object) dictionary
    public Dictionary<string, T> LoadJSONAsGameObjectDictionary<T>(string file)
    {
        //Load JSON as string
        string JSONString = File.ReadAllText(Application.streamingAssetsPath+"/GameFiles/"+file);
        return JsonConvert.DeserializeObject<Dictionary<string, T>>(JSONString);
    }

    //Load JSON as game object
    public T LoadJSONAsGameObject<T>(string file)
    {
        //Load JSON as string
        string JSONString = File.ReadAllText(Application.streamingAssetsPath+"/GameFiles/"+file);
        return JsonConvert.DeserializeObject<T>(JSONString);
    }

    //Save object as JSON
    public void SaveGameObjectAsJSON(object o, string file)
    {
        string JSONString = JsonConvert.SerializeObject(o, Formatting.Indented);
        File.WriteAllText(Application.streamingAssetsPath+"/GameFiles/"+file, JSONString);
    }
}