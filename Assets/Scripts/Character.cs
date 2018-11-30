using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Character")]
public class Character : ScriptableObject
{
    public Sprite sprite = null;
    public string characterName = "Default";
    public int startingHp = 100;
    public int def = 5;
    public int atk = 5;

    public Magic characterAbilities;

}