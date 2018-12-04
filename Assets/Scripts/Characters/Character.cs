using UnityEngine;
using System.Collections;

/* Used to store (de)serialized JSON data */

[System.Serializable]
public class Character
{
    public string name;
    public bool unlocked;
    public string sprite;
    public int level;
    public int hp;
    public int atk;
    public int def;
    public int rcv;
    public int mgc;
    public string mgc_animation;
}