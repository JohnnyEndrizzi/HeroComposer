using System.Collections.Generic;
using UnityEngine;

public class AttackAnimator : MonoBehaviour
{ //This class recieves, creates animations as required.
    
    /* Functional Requirement 
    * ID: 8.1-2
    * Description: The player’s battle commands must invoke the proper attack animations as a response.
    * 
    * This class recieves, creates animations as required */

    //Prefab Sprites 
    private Transform arrow;
    private Transform fireball;
    private Transform slash;
    private Transform shield;
    private Transform heal;
    private Transform snow;

    //Audio
    private AudioSource audioSource;
    private AudioClip arrowSound;
    private AudioClip bigArrowSound;
    private AudioClip fireSound;
    private AudioClip iceSound;
    private AudioClip healSound;

    //Animation Times
    public float fastshot = 0.2f;
    public float slowshot = 1.0f;

    //Dictionary lists to store all attack and location information
    Dictionary<string, Locations> LocationMap = new Dictionary<string, Locations>();
    Dictionary<string, Attacks> AttackMap = new Dictionary<string, Attacks>();
       
    void Start()
    {
        LoadResources();

        /* List of all targetable locations on the field.  
         * These can be referenced to from elsewhere whenever a location is required. 
         * It also provides a central location from which locations can be efficiently added or modified if needed. */
        LocationMap.Add("P1", new Locations(3.29f, 1.75f, -4.8f));   //P1
        LocationMap.Add("P2", new Locations(1.02f, 0.33f, -5.1f)); //P2
        LocationMap.Add("P3", new Locations(2.94f, -0.82f, -5.5f)); //P3
        LocationMap.Add("P4", new Locations(5.31f, 0.32f, -5.1f)); //P4    
        LocationMap.Add("P5", new Locations(6.5f, 0, 1)); //Centre of team

        LocationMap.Add("C", new Locations(0, 0, 1)); //CentrePoint
        LocationMap.Add("CP", new Locations(6.5f, 0, 1)); //Centre of team

        LocationMap.Add("E1", new Locations(-4, 3, 1)); //E1
        LocationMap.Add("E2", new Locations(-4, -3, 1)); //E2
        LocationMap.Add("E3", new Locations(-9, 4, 1)); //E3
        LocationMap.Add("E4", new Locations(-9, -4, 1)); //E4
        LocationMap.Add("E5", new Locations(-3.5f, 1.16f, -5.5f)); //Boss

        LocationMap.Add("HL", new Locations(-5f, 20, 1)); //High Left
        LocationMap.Add("HR", new Locations(2.5f, 10, 1)); //High Right
        
        /* List of all attacks
         * This contains all the information required to launch any magical or ranged attack
         * This includes sprites, behaviour, offsets, number of attacks and timing
         * Ideally new attacks can be implemented in a single line.
         * Below is a guide legend of the various behavior types and special actions so all relevent information 
         * is located here for ease of use. */
        //AttackMap.Add("name", new Attacks(int attackCode, Transform prefab, intintfloat SpawnOffset, int scaleFactor, int customCmd, audioClip sound));
        AttackMap.Add("heal", new Attacks(0, heal, 0, 0, 0.0f, 3, 4));
        AttackMap.Add("shield", new Attacks(1, shield, -1, 0, 0.0f, 1, 51)); //TODO make spherical sprite
        AttackMap.Add("slash1", new Attacks(1, slash, -1, 0, 0.0f, 1, 52));
        AttackMap.Add("slash2", new Attacks(1, slash, -1, 0, 0.0f, 1, 53));
        AttackMap.Add("fireball", new Attacks(3, fireball, 0, 0, 0.0f, 5, fireSound));
        AttackMap.Add("arrow", new Attacks(3, arrow, 0, 0, 0.0f, 1, arrowSound));
        AttackMap.Add("healTeam", new Attacks(4, heal, 0, 0, 0.0f, 7, 4, healSound));
        AttackMap.Add("arrowHail", new Attacks(6, arrow, 0, 0, 0.0f, 0.25f, 11, bigArrowSound));
        AttackMap.Add("meteor", new Attacks(6, fireball, 0, 0, 0.0f, 10, 2, fireSound));
        AttackMap.Add("fire3", new Attacks(2, fireball, 0, 0, 0.0f, 2, 3, fireSound));
        AttackMap.Add("blizzard", new Attacks(2, snow, 0, 0, 0.0f, 1, 12, iceSound));

        AttackMap["arrow"].sound = arrowSound;
        AttackMap["fireball"].sound = fireSound;
        
        // Attack Code List //
        //0 - Single Character
        //1 - Single Char Wave
        //2 - A to B 
        //3 - A to B Long
        //4 - Whole Team
        //5 - A to B Team
        //6 - Aerial Boss attack
        //7 - Aerial Player attack

        // Custom Cmd Code List
        //0 - Colour overlay - vector4(r,g,b,a)
        //11 - multishot w/ random #10
        //12 - multishot w/ random, spin #20
        //2 - slow shot   
        //3 - triple shot              
        //4 - revert              
        //51 - mask Left
        //52 - mask Up
        //53 - mask Down
    }

    public enum CmdCode //WIP
    {
        ColorOverlay = 0, //plus vector4(r,g,b,a)
        multishot10 = 11,
        multishotSpin20 = 12,
        slowShotAtk = 2,
        tripleShotAtk = 3,
        revertMove = 4,
        maskL = 51,
        maskU = 52,
        maskD = 53
    }

    public enum AtkCode //WIP
    {
        SingleChar = 0,
        SingleCharWave = 1,
        AtoB = 2,
        AtoBLong = 3,
        WholeTeam = 4,
        AtoBTeam = 5,
        AerialBossAtk = 6,
        AerialPlayerAtk = 7
    }

    private void LoadResources() //Load assets 
    {
        //Setting Source and paths
        audioSource = GetComponent<AudioSource>();
        string soundPath = "SoundEffects/Attacks/";
        string spritePath = "Prefab/Attacks/";

        //Prefab transforms
        arrow = Resources.Load<Transform>(spritePath + "arrow1");
        fireball = Resources.Load<Transform>(spritePath + "fireball1");
        slash = Resources.Load<Transform>(spritePath + "slash1");
        shield = Resources.Load<Transform>(spritePath + "shield1");
        heal = Resources.Load<Transform>(spritePath + "heal1");
        snow = Resources.Load<Transform>(spritePath + "Blizzard");

        //Sound effect clips
        arrowSound = (AudioClip)Resources.Load(soundPath + "magic_arrow_edit");
        fireSound = (AudioClip)Resources.Load(soundPath + "magic_fire_edit");
        iceSound = (AudioClip)Resources.Load(soundPath + "magic_ice_edit");
        healSound = (AudioClip)Resources.Load(soundPath + "magic_heal_edit");
        bigArrowSound = (AudioClip)Resources.Load(soundPath + "boss_arrow_volley");
    }

    /* This function is used to call a magical attack from anywhere.
     * All information needed to launch an attack is located here, the only outside input required
     * is 'what is the attack?', 'where did it come from?' and 'where is it going?'
     * The selector will use the behaviour type of the attack to calculate which set of positions will be used. 
     * It then plugs in the source and target numbers and applies those to the preset locations to determing the 
     * coordinates to start and finish the animation at.
     * 
     * It will then apply the first level of special attacks which control number of items and timing of attacks. */
    public void ATTACK(string AtkName, int summoner, int target)
    {
        Vector3 Pos1, Pos2;
        if (!AttackMap.ContainsKey(AtkName)) { Debug.Log("ERROR, ATTACK, Attack not in Dictionary " + AtkName); return; }

        //Debug.Log("new TargetedAtk: " + AtkName);        //works
        //Debug.Log("Soun: " + bigArrowSound.ToString()); //works
        //Debug.Log("Soun: " + AttackMap[AtkName].trans.ToString()); //works
        //Debug.Log("Soun: " + AttackMap["arrowHail"].sound.ToString()); //doesnt work
        //Debug.Log("Soun: " + AttackMap[AtkName].sound.name); //doesnt work
                        
        Attacks triggeredAttack = AttackMap[AtkName];
        //triggeredAttack.sound = AttackMap[AtkName].sound; //doesnt work

        //Debug.Log("Tran: " + triggeredAttack.trans.name.ToString()); 
        //Debug.Log("Soun: " + triggeredAttack.sound.ToString()); //doesnt work

        //try { audioSource.PlayOneShot(bigArrowSound, 0.7f); } catch { Debug.Log("no sound1"); } //works
        //try { audioSource.PlayOneShot(AttackMap["arrow"].sound, 0.7f); } catch { Debug.Log("no sound2"); } //works
        //try { audioSource.PlayOneShot(triggeredAttack.sound, 0.7f); } catch { Debug.Log("no sound3"); } //works
        
        
        //Position Selector using attack codes
        switch (triggeredAttack.targetNum)
        {
            case 0:
                Pos1 = LocationMap["P" + summoner].Spawn;
                Pos2 = Pos1;
                break;
            case 1:
                Pos1 = LocationMap["P" + summoner].Spawn;
                Pos2 = Pos1 + AttackMap[AtkName].offset;
                break;
            case 2:
                Pos1 = LocationMap["P" + summoner].Spawn;
                Pos2 = LocationMap["E" + target].Spawn;
                break;
            case 3:
                Pos1 = LocationMap["P" + summoner].Spawn;
                Pos2 = LocationMap["E" + target].Spawn;
                break;
            case 4:
                Pos1 = LocationMap["CP"].Spawn;
                Pos2 = Pos1;
                break;
            case 5:
                Pos1 = LocationMap["P" + summoner].Spawn;
                Pos2 = LocationMap["P" + target].Spawn;
                break;
            case 6:
                Pos1 = LocationMap["HL"].Spawn;
                Pos2 = LocationMap["P" + target].Spawn;
                break;
            case 7:
                Pos1 = LocationMap["HR"].Spawn;
                Pos2 = LocationMap["E" + target].Spawn;
                break;
            default:
                Pos1 = LocationMap["P" + summoner].Spawn;
                Pos2 = LocationMap["E" + target].Spawn;
                Debug.Log("ERROR, ATTACK, Attack does not exist");
                break;
        }

        //Uses Custom Commands to control timing/randomizers/number spawned etc.
        switch (triggeredAttack.customCmd)
        {
            case 11: //multishot w/ random
                BOOMRandomizer(AtkName, triggeredAttack, Pos1, Pos2, 10, fastshot);
                break;
            case 12: //multishot w/ spin random
                BOOMRandomizer(AtkName, triggeredAttack, Pos1, Pos2, 20, slowshot);
                break;
            case 2: //slowshot
                BOOM(AtkName, triggeredAttack, Pos1, Pos2, slowshot);
                break;
            default: //default
                BOOM(AtkName, triggeredAttack, Pos1, Pos2, fastshot);
                break;
        }
    }

    /* This function spawns the desired attack animation, and passes in relevant data.
     * This includes start and end positions, timing information, scale changes, 
     * and optionally a special attack identifier. */
    private void BOOM(string AtkName, Attacks triggeredAttack, Vector3 Pos1, Vector3 Pos2, float lerpTime)  //Basic Attack spawner
    {
        //Creation of attack
        var obj = Instantiate(triggeredAttack.trans, Pos1 + triggeredAttack.offset, transform.rotation);
        obj.gameObject.SetActive(false);
        obj.gameObject.AddComponent<AtkMove>();

        //Passing in attack values
        var tempScript = obj.gameObject.GetComponent<AtkMove>();
        tempScript.StartPos = Pos1 + triggeredAttack.offset;
        tempScript.EndPos = Pos2 + triggeredAttack.offset;
        tempScript.atkLerpTime = 0.2f;       

        tempScript.EndSize = obj.localScale * triggeredAttack.scaleFactor;
        tempScript.custCmd = triggeredAttack.customCmd;
        tempScript.name = AtkName;
        //tempScript.transform.SetParent(this.transform);
        obj.gameObject.SetActive(true);
        
        if (triggeredAttack.sound && triggeredAttack.customCmd != 11 && triggeredAttack.customCmd != 12)
        {//Block multishot audio from playing rapidly
            PewPew(triggeredAttack);
        }      
    }

    /* This function is used to creates multiple attacks in a single animation, each with their own randomized path 
     * to produce a spread centred on the target. */
    private void BOOMRandomizer(string AtkName, Attacks triggeredAttack, Vector3 pos1, Vector3 pos2, int num, float speed)
    {
        Vector3 pos2new = new Vector3();

        for (int i = 0; i < num; i++)
        {
            pos2new.x = pos2.x + Random.Range(-5f, 5f);
            pos2new.y = pos2.y + Random.Range(-5f, 5f);
            BOOM(AtkName, triggeredAttack, pos1, pos2new, speed);
        }
        PewPew(triggeredAttack);
    }

    public void PewPew(Attacks triggeredAttackB)
    {
        //Debug.Log("Pew1: " + triggeredAttack.sound.ToString());
        try { audioSource.PlayOneShot(triggeredAttackB.sound, 0.7f); } catch { Debug.Log(triggeredAttackB.trans.name + " no sound"); }
    }
    public void PewPew(string AtkName) //calls from AtkMove children; children do not know their sound or lookup number. Only currently used with tripleshot
    {
        //Debug.Log("Pew2: " + AtkName);
        try { audioSource.PlayOneShot(AttackMap[AtkName].sound, 0.7f); } catch { Debug.Log(AtkName + " no sound"); }        
    }
}

public class Locations //Location Dictionary 
{
    public Vector3 Spawn;

    //Overloads accept input from Vector3, integer or float values
    public Locations(Vector3 SpawnX)
    {
        Spawn = SpawnX;
    }
    public Locations(int SX, int SY, float SZ)
    {
        Spawn = new Vector3(SX, SY, SZ);
    }
    public Locations(float SX, int SY, float SZ)
    {
        Spawn = new Vector3(SX, SY, SZ);
    }
    public Locations(int SX, float SY, float SZ)
    {
        Spawn = new Vector3(SX, SY, SZ);
    }
    public Locations(float SX, float SY, float SZ)
    {
        Spawn = new Vector3(SX, SY, SZ);
    }
}

public class Attacks //Dictionary for all magical, special and ranged attacks
{
    public int targetNum;
    public Transform trans;
    public Vector3 offset;
    public float scaleFactor;
    public int customCmd;
    public Color overlay;
    public AudioClip sound;

    //Attacks do not need a customCmd but may have one
    //Not all attacks have a sound yet.
    //Colour is not yet implemented; when it it commented functions will replace the uncommented ones    
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, int customCmdX) //custom
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
    }
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, int customCmdX, AudioClip soundX) //custom, sound
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
        sound = soundX;
    }
    //public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, int customCmdX, Color overlayX) //custom, colour 
    //{
    //    targetNum = targetNumX;
    //    trans = transformX;
    //    offset = new Vector3(SX, SY, SZ);
    //    scaleFactor = scaleFactorX;
    //    customCmd = customCmdX;
    //    overlay = overlayX;
    //}
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, AudioClip soundX) //sound
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
    }
    //public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, int customCmdX, AudioClip soundX, Color overlayX) //custom, sound, colour
    //{
    //    targetNum = targetNumX;
    //    trans = transformX;
    //    offset = new Vector3(SX, SY, SZ);
    //    scaleFactor = scaleFactorX;
    //    customCmd = customCmdX;
    //    overlay = overlayX;
    //    sound = soundX;
    //}
}