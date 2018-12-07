﻿using UnityEngine;
using System.Collections;

/* This class is used to store the (de)serialized character data form the JSON file */
[System.Serializable]
public class Character
{
    public string name;
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