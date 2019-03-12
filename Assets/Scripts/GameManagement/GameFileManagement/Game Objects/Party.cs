//Serializable JSON object
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class Party {
    public Dictionary<string, Character> characters;

    public bool IsCharacterInParty(Character character)
    {
        Dictionary<string, bool> characterNames = characters.Values.ToDictionary(x => x.name, x => true);
        if (characterNames.ContainsKey(character.name))
        {
            return true;
        }
        else {
            return false;
        }
    }
}
