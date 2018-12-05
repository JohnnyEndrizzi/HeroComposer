using UnityEngine;
using System.Collections;

/* Used to store (de)serialized JSON data */

[System.Serializable]
public class Character
{
    public string name;
    public string desc;
    public string sprite;
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