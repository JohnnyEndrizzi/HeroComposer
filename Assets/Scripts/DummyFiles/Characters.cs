using DummyFiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour {
    public string characterId;
    public float currentHealth;
    public float defenseValue;
    public int currentLevel;
    public float currentXP;
    public float currentBoundedXP;
    public float specialGaugeValue;
    private CharacterType type;
    private string characterName;
    public List<string> upgrades;
    public List<GameItems> equippedItems;
    private string characterBackstory;
    private int characterCost;
    private object character;
    private int attack;
    private int defense;
    private int magic;

    public void calculateLevelUpXP()
    {
        /* Uses the current level to derive how much XP is needed to level up (Default is 0 for now). */
        currentBoundedXP = 0;
    }

    public static Characters GenerateCharacter()
    {
        return new Characters { } ;
    }

    public Characters getCharacterInfo(Characters character)
    {
        return new Characters { };
    }

    public void equipItem(GameItems item)
    {

    }

    public List<string> getUpgrades()
    {
        return new List<string> { };
    }

    public void upgradeCharacter(string Upgrade, GlobalUser User)
    {

    }
}

public enum CharacterType
{
    Attacker = 1,
    Defender = 2,
    Mage = 3,
    Healer = 4
}

