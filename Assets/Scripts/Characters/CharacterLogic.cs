using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* This class is used to make a shallow and simplified copy of the character ScriptableObject 
 * which will be used during a level. This is done because only minimal information is required
 * to be used in a level, and the copy can then be deleted without risk. */
public class CharacterLogic : MonoBehaviour {

    public string sprite;
    public string sound;

    public int hp;
    public int currentHp;
    public int atk;
    public int def;
    public int rcv;
    public int mgc;
    public string attack;
    public int magicQueue = 0;
}









