/* Used to store (de)serialized JSON data */

[System.Serializable]
public class Levels
{
    public string LevelName;
    public string LevelSong;
    public string Enemy;
    public string Terrain;
    
    public int[] scoreNormal;
    public string[] nameNormal;    
    public int[] scoreHard;
    public string[] nameHard;
    public int[] scoreExpert;
    public string[] nameExpert;
}