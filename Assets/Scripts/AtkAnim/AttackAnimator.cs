using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimator : MonoBehaviour
{
    //Prefab Sprites
    public Transform arrow;
    public Transform fireball;
    public Transform slash;
    public Transform shield;
    public Transform heal;
    public Transform snow;

    //Animation Times
    public float fastshot = 0.2f;
    public float slowshot = 1.0f;

    //Temp for manual input;
    int[] InputCounter = new int[] { 0, -1, -1, -1 };  //inputNum, caster, spell, target   

    Dictionary<string, Locations> LocationMap = new Dictionary<string, Locations>();
    Dictionary<string, Attacks> AttackMap = new Dictionary<string, Attacks>();

    void Start()
    {
        //List of all targetable locations
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

        //Art Demo Locations
        LocationMap.Add("AD1", new Locations(0, 0.5f, 1));   //P1
        LocationMap.Add("ADC1", new Locations(4, 0, 1));   //Centrepoint
        LocationMap.Add("ADB1", new Locations(-4, 1, 1));  //Boss
        LocationMap.Add("ADHL", new Locations(-6, 6, 1));   //High Left


        //List of all attacks and specs
        //AttackMap.Add("name", new Attacks(int targetNum, Transform prefab, Vector3 SpawnOffset, int scaleFactor, int customCmd));
        AttackMap.Add("heal", new Attacks(0, heal, 0, 0, 0.0f, 3, 4));
        AttackMap.Add("shield", new Attacks(1, shield, -1, 0, 0.0f, 1, 51)); //TODO make spherical sprite
        AttackMap.Add("slash1", new Attacks(1, slash, -1, 0, 0.0f, 1, 52));
        AttackMap.Add("slash2", new Attacks(1, slash, -1, 0, 0.0f, 1, 53));
        AttackMap.Add("fireball", new Attacks(3, fireball, 0, 0, 0.0f, 5));
        AttackMap.Add("arrow", new Attacks(3, arrow, 0, 0, 0.0f, 1));
        AttackMap.Add("healTeam", new Attacks(4, heal, 0, 0, 0.0f, 7, 4));
        AttackMap.Add("arrowHail", new Attacks(6, arrow, 0, 0, 0.0f, 0.25f, 11));
        AttackMap.Add("meteor", new Attacks(6, fireball, 0, 0, 0.0f, 10, 2));
        AttackMap.Add("fire3", new Attacks(2, fireball, 0, 0, 0.0f, 2, 3));
        AttackMap.Add("blizzard", new Attacks(2, snow, 0, 0, 0.0f, 1, 12));
        
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
        //51 - mask Lelt
        //52 - mask Up
        //53 - mask Down
    }

    public void ATTACK(string AtkName, int summoner, int target) //Calls an attack 
    { 
        Vector3 Pos1, Pos2;
        if (!AttackMap.ContainsKey(AtkName)) { Debug.Log("ERROR, ATTACK, Attack not in Dictionary " + AtkName); return; }
        
        //Position Selector using attack codes
        if      (AttackMap[AtkName].targetNum == 0) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["P" + summoner].Spawn; }
        else if (AttackMap[AtkName].targetNum == 1) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["P" + summoner].Spawn + AttackMap[AtkName].offset; }
        else if (AttackMap[AtkName].targetNum == 2) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["E" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 3) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["E" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 4) { Pos1 = LocationMap["CP"].Spawn;           Pos2 = LocationMap["CP"].Spawn; }
        else if (AttackMap[AtkName].targetNum == 5) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["P" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 6) { Pos1 = LocationMap["HL"].Spawn;           Pos2 = LocationMap["P" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 7) { Pos1 = LocationMap["HR"].Spawn;           Pos2 = LocationMap["E" + target].Spawn; }

        else { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["E" + target].Spawn; Debug.Log("ERROR, Attack, Attack does not exist"); }

        //Uses Custom Commands to control timing/randomizers/number spawned etc.
        if (AttackMap[AtkName].customCmd == 11) { BOOMRandomizer(AtkName, Pos1, Pos2, 10, fastshot); }//multishot w/ random
        else if (AttackMap[AtkName].customCmd == 12) { BOOMRandomizer(AtkName, Pos1, Pos2, 20, slowshot); }//multishot w/ spin random
        else if (AttackMap[AtkName].customCmd == 2) { BOOM(AtkName, Pos1, Pos2, slowshot); }//slowshot
        else { BOOM(AtkName, Pos1, Pos2, fastshot); }//default
    }

    public void ATTACKdemo(string AtkName, int summoner, int target) //Calls an attack for artShow - identical but uses different mapped locations //TODO rename demo->art
    { 
        Vector3 Pos1, Pos2;
        if (!AttackMap.ContainsKey(AtkName)) { Debug.Log("ERROR, ATTACK, Attack not in Dictionary" + AtkName); return; }

        //Position Selector using attack codes
        if      (AttackMap[AtkName].targetNum == 0) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["AD" + summoner].Spawn; }
        else if (AttackMap[AtkName].targetNum == 1) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["AD" + summoner].Spawn + AttackMap[AtkName].offset; }
        else if (AttackMap[AtkName].targetNum == 2) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["ADB" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 3) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["ADB" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 4) { Pos1 = LocationMap["AD1"].Spawn;           Pos2 = LocationMap["AD1"].Spawn; }
        else if (AttackMap[AtkName].targetNum == 5) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["ADB" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 6) { Pos1 = LocationMap["ADHL"].Spawn;          Pos2 = LocationMap["ADC" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 7) { Pos1 = LocationMap["ADHR"].Spawn;          Pos2 = LocationMap["ADB" + target].Spawn; }

        else { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["E" + target].Spawn; Debug.Log("ERROR, Attack, Attack does not exist"); }

        //Uses Custom Commands to control timing/randomizers/number spawned etc.
        if (AttackMap[AtkName].customCmd == 11) { BOOMRandomizer(AtkName, Pos1, Pos2, 10, fastshot); }//multishot w/ random
        else if (AttackMap[AtkName].customCmd == 12) { BOOMRandomizer(AtkName, Pos1, Pos2, 20, slowshot); }//multishot w/ spin random
        else if (AttackMap[AtkName].customCmd == 2) { BOOM(AtkName, Pos1, Pos2, slowshot); }//slowshot
        else { BOOM(AtkName, Pos1, Pos2, fastshot); }//default
    }

    private void BOOM(string AtkName, Vector3 Pos1, Vector3 Pos2, float lerpTime)  //Basic Attack spawner
    {
        //Creation of attack
        var obj = Instantiate(AttackMap[AtkName].trans, Pos1 + AttackMap[AtkName].offset, transform.rotation);
        obj.gameObject.SetActive(false);
        obj.gameObject.AddComponent<AtkMove>();

        //Passing in attack values
        var tempScript = obj.gameObject.GetComponent<AtkMove>();
        tempScript.StartPos = Pos1 + AttackMap[AtkName].offset;
        tempScript.EndPos = Pos2 + AttackMap[AtkName].offset;
        tempScript.atkLerpTime = 0.2f;
       
        tempScript.EndSize = obj.localScale * AttackMap[AtkName].scaleFactor;
        tempScript.custCmd = AttackMap[AtkName].customCmd;
        obj.gameObject.SetActive(true);
    }

    private void BOOMRandomizer(string AtkName, Vector3 pos1, Vector3 pos2, int num, float speed) //Creates num attacks with random spread
    {
        //Vector3[] pos2s = new Vector3[num];  //TODO Test if the same
        Vector3 pos2new = new Vector3();

        //for (int i = 0; i < num; i++)
        //{
        //    pos2s[i].x = pos2.x + Random.Range(-5f, 5f);
        //    pos2s[i].y = pos2.y + Random.Range(-5f, 5f);
        //}
        //for (int j = 0; j < num; j++)
        //{
        //    BOOM(AtkName, pos1, pos2s[j], speed);
        //}

        for (int i = 0; i < num; i++)
        {
            pos2new.x = pos2.x + Random.Range(-5f, 5f);
            pos2new.y = pos2.y + Random.Range(-5f, 5f);
            BOOM(AtkName, pos1, pos2new, speed);
        }
    }
}


public class Locations //Location Dictionary 
{ 
    public Vector3 Spawn;

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
    public Color overlay;  //TODO remove/move (also remove Dict Add)

    public Attacks(int targetNumX, Transform transformX, Vector3 offsetX, float scaleFactorX) //Basic w/ Vector3 offset
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = offsetX;
        scaleFactor = scaleFactorX;
    }
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX) //Basic w/ int,int,float offset
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
    }
    public Attacks(int targetNumX, Transform transformX, Vector3 offsetX, float scaleFactorX, int customCmdX) //custom w/ Vector3 offset
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = offsetX;
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
    }
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, int customCmdX) //custom w/ int,int,float offset
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
    }

    public Attacks(int targetNumX, Transform transformX, Vector3 offsetX, float scaleFactorX, int customCmdX, Color overlayX) //custom w/ colour and Vector3 offset
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = offsetX;
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
        overlay = overlayX;
    }
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, int customCmdX, Color overlayX) //custom w/ colour and int,int,float offset
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
        overlay = overlayX;
    }
}
