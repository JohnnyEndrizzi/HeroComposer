//Serializable JSON object
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Party {
    public Dictionary<string, Character> characters;

    public bool IsCharacterInParty(Character character)
    {
        Dictionary<string, bool> characterNames = characters.Values.ToDictionary(x => x.charName, x => true);
        if (characterNames.ContainsKey(character.charName))
        {
            return true;
        }
        else {
            return false;
        }
    }
}
