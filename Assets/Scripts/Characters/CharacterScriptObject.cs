﻿using UnityEngine;

[CreateAssetMenu (menuName = "Character")]
/* This ScriptableObject will be used to store all local representations of our characters while
 * doing menu actions. It will be deserialized from the JSON file, and will be shallow copied
 * before starting a level. */
public class CharacterScriptObject : ScriptableObject
{
    public string charName;
    public string desc;    
    public string sprite;
    public string headshot;
    public string sound;

    public bool unlocked;

    public int level;
    public int hp;
    public int atk;
    public int def;
    public int rcv;
    public int mgc;
    
    public int eqp1;
    public int eqp2;
    public int eqp3;
    public string mag_Eqp;
}