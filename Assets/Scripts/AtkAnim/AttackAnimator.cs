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

    //TODO 
    //Lightning
    //energy
    //snow
    //music
    //arrow hail
    //meteor - detailed fireball
    //colours

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
        //soundwave

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


    // Update is called once per frame
    void Update()
    {// temp for manual input

        // Demo inputs
        //        if(Input.GetKeyDown(KeyCode.Keypad1)){ATTACK("heal",4,1);}
        //        if(Input.GetKeyDown(KeyCode.Keypad2)){ATTACK("slash1",1,1);}
        //        if(Input.GetKeyDown(KeyCode.Keypad3)){ATTACK("slash2",1,1);}
        //        if(Input.GetKeyDown(KeyCode.Keypad4)){ATTACK("fireball",3,4);}
        //        if(Input.GetKeyDown(KeyCode.Keypad5)){ATTACK("arrow",2,5);}
        //        if(Input.GetKeyDown(KeyCode.Keypad6)){ATTACK("healTeam", 1, 2);}
        //        if(Input.GetKeyDown(KeyCode.Keypad7)){ATTACK("arrowHail", 2, 5);}
        //        if(Input.GetKeyDown(KeyCode.Keypad8)){ATTACK("meteor", 2, 4);}
        //        if(Input.GetKeyDown(KeyCode.Keypad9)){ATTACK("fire3", 2, 5);}
        //        if(Input.GetKeyDown(KeyCode.Keypad0)){ATTACK("blizzard", 1, 5);}
    }

    public void ATTACK(string AtkName, int summoner, int target)
    { //calls an attack
        Vector3 Pos1, Pos2;
        if (!AttackMap.ContainsKey(AtkName)) { Debug.Log("ERROR, ATTACK, Attack not in Dictionary " + AtkName); return; }


        //Position Selector //TODO make switch
        if (AttackMap[AtkName].targetNum == 0) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["P" + summoner].Spawn; }
        else if (AttackMap[AtkName].targetNum == 1) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["P" + summoner].Spawn + AttackMap[AtkName].offset; }
        else if (AttackMap[AtkName].targetNum == 2) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["E" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 3) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["E" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 4) { Pos1 = LocationMap["CP"].Spawn; Pos2 = LocationMap["CP"].Spawn; }
        else if (AttackMap[AtkName].targetNum == 5) { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["P" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 6) { Pos1 = LocationMap["HL"].Spawn; Pos2 = LocationMap["P" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 7) { Pos1 = LocationMap["HR"].Spawn; Pos2 = LocationMap["E" + target].Spawn; }

        else { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["E" + target].Spawn; Debug.Log("ERROR, Attack, Attack does not exist"); }

        if (AttackMap[AtkName].customCmd == 11) { BOOMRandomizer(AtkName, Pos1, Pos2, 10, fastshot); }//multishot w/ random
        else if (AttackMap[AtkName].customCmd == 12) { BOOMRandomizer(AtkName, Pos1, Pos2, 20, slowshot); }//multishot w/ spin random
        else if (AttackMap[AtkName].customCmd == 2) { BOOM(AtkName, Pos1, Pos2, slowshot); }//slowshot
        else { BOOM(AtkName, Pos1, Pos2, fastshot); }//default


    }

    public void ATTACKdemo(string AtkName, int summoner, int target)
    { //calls an attack
        Vector3 Pos1, Pos2;
        if (!AttackMap.ContainsKey(AtkName)) { Debug.Log("ERROR, ATTACK, Attack not in Dictionary" + AtkName); return; }


        //Position Selector //TODO make switch
        if (AttackMap[AtkName].targetNum == 0) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["AD" + summoner].Spawn; }
        else if (AttackMap[AtkName].targetNum == 1) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["AD" + summoner].Spawn + AttackMap[AtkName].offset; }
        else if (AttackMap[AtkName].targetNum == 2) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["ADB" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 3) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["ADB" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 4) { Pos1 = LocationMap["AD1"].Spawn; Pos2 = LocationMap["AD1"].Spawn; }
        else if (AttackMap[AtkName].targetNum == 5) { Pos1 = LocationMap["AD" + summoner].Spawn; Pos2 = LocationMap["ADB" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 6) { Pos1 = LocationMap["ADHL"].Spawn; Pos2 = LocationMap["ADC" + target].Spawn; }
        else if (AttackMap[AtkName].targetNum == 7) { Pos1 = LocationMap["ADHR"].Spawn; Pos2 = LocationMap["ADB" + target].Spawn; }

        else { Pos1 = LocationMap["P" + summoner].Spawn; Pos2 = LocationMap["E" + target].Spawn; Debug.Log("ERROR, Attack, Attack does not exist"); }


        if (AttackMap[AtkName].customCmd == 11) { BOOMRandomizer(AtkName, Pos1, Pos2, 10, fastshot); }//multishot w/ random
        else if (AttackMap[AtkName].customCmd == 12) { BOOMRandomizer(AtkName, Pos1, Pos2, 20, slowshot); }//multishot w/ spin random
        else if (AttackMap[AtkName].customCmd == 2) { BOOM(AtkName, Pos1, Pos2, slowshot); }//slowshot
        else { BOOM(AtkName, Pos1, Pos2, fastshot); }//default


    }

    void BOOM(string AtkName, Vector3 Pos1, Vector3 Pos2, float lerpTime)
    {
        //Creation of attack, and passing in attack values
        var obj = Instantiate(AttackMap[AtkName].trans, Pos1 + AttackMap[AtkName].offset, transform.rotation);
        obj.gameObject.SetActive(false);
        obj.gameObject.AddComponent<AtkMove>();

        var tempScript = obj.gameObject.GetComponent<AtkMove>();
        tempScript.StartPos = Pos1 + AttackMap[AtkName].offset;
        tempScript.EndPos = Pos2 + AttackMap[AtkName].offset;
        tempScript.atkLerpTime = 0.2f;
        //tempScript.atkLerpTime = AttackMap[AtkName].delay   //TODO, variable timing delay?

        tempScript.EndSize = obj.localScale * AttackMap[AtkName].scaleFactor;
        tempScript.custCmd = AttackMap[AtkName].customCmd;
        obj.gameObject.SetActive(true);
    }


    void BOOMRandomizer(string AtkName, Vector3 pos1, Vector3 pos2, int num, float speed)
    {
        Vector3[] pos2s = new Vector3[num];

        for (int i = 0; i < num; i++)
        {
            pos2s[i].x = pos2.x + Random.Range(-5f, 5f);
            pos2s[i].y = pos2.y + Random.Range(-5f, 5f);
        }
        for (int j = 0; j < num; j++)
        {
            BOOM(AtkName, pos1, pos2s[j], speed);
        }
    }




    void Select(int charSelect)
    { //temp for manual input
        Debug.Log(charSelect.ToString());

        if (InputCounter[0] == 0)
        { //caster
            InputCounter[1] = charSelect;
            InputCounter[0]++;
        }
        else if (InputCounter[0] == 1)
        { //spell
            InputCounter[2] = charSelect;
            InputCounter[0]++;

            if (AttackMap[numString(charSelect)].targetNum == 0)
            {
                ATTACK(numString(InputCounter[2]), InputCounter[1], -1);
                InputCounter[0] = 0;
            }
            else if (AttackMap[numString(charSelect)].targetNum == 1)
            {
                ATTACK(numString(InputCounter[2]), InputCounter[1], -1);
                InputCounter[0] = 0;
            }
            else if (AttackMap[numString(charSelect)].targetNum == 4)
            {
                ATTACK(numString(InputCounter[2]), InputCounter[1], -2);
                InputCounter[0] = 0;
            }
        }
        else if (InputCounter[0] == 2)
        { //target
            InputCounter[3] = charSelect;
            InputCounter[0] = 0;
            ATTACK(numString(InputCounter[2]), InputCounter[1], InputCounter[3]);
        }
        //0 - Single Character
        //1 - Single Char Wave
        //2 - A to B 
        //3 - A to B Long
        //4 - Whole Team
        //5 - Colour Overlay

    }

    string numString(int Num)
    { //temp for manual input
        switch (Num)
        {
            case 1:
                return "heal";
            case 2:
                return "shield";
            case 3:
                return "fireball";
            case 4:
                return "arrow";
            case 5:
                return "healTeam";
            case 6:
                return "arrowHail";
            case 7:
                return "meteor";

            default:
                Debug.Log("ERROR: numString, ATTACK DOES NOT EXIST");
                return "heal";
        }
    }
}




public class Locations
{ //Location Dictionary 
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

public class Attacks
{ //Attack Dictionary 
    public int targetNum;
    public Transform trans;
    public Vector3 offset;
    public float scaleFactor;
    public int customCmd;
    public Color overlay;

    public Attacks(int targetNumX, Transform transformX, Vector3 offsetX, float scaleFactorX)
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = offsetX;
        scaleFactor = scaleFactorX;
    }
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX)
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
    }
    public Attacks(int targetNumX, Transform transformX, Vector3 offsetX, float scaleFactorX, int customCmdX)
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = offsetX;
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
    }
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, int customCmdX)
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
    }

    public Attacks(int targetNumX, Transform transformX, Vector3 offsetX, float scaleFactorX, int customCmdX, Color overlayX)
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = offsetX;
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
        overlay = overlayX;
    }
    public Attacks(int targetNumX, Transform transformX, int SX, int SY, float SZ, float scaleFactorX, int customCmdX, Color overlayX)
    {
        targetNum = targetNumX;
        trans = transformX;
        offset = new Vector3(SX, SY, SZ);
        scaleFactor = scaleFactorX;
        customCmd = customCmdX;
        overlay = overlayX;
    }
}