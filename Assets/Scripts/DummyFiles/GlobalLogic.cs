using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLogic : MonoBehaviour {

    public Characters[] Lineup = new Characters[4];
    public int musicVolume = 0;
    public int soundVolume = 0;
    public int audioLatency = 0;
    public int videoLatency = 0;
    public string[] videoSettings = new string[3];

    public GlobalUser getGlobalUser()
    {
        GlobalUser user = new GlobalUser();
        return user;
    }

    public void changeLineup(Characters[] characters)
    {

    }

    public static void chooseShopScene()
    {

    }

    public static void chooseLineupScene()
    {

    }

    public static void chooseRecruitScene()
    {

    }

    public static void loadBattleLevel(string levelName, Characters[] Lineup)
    {
    }

    public  void adjustMusic(int value)
    {

    }

    public  void adjustSound(int value)
    {

    }

    public void adjustAudioLatency(int value)
    {

    }

    public void adjustVideoLatency(int value)
    {

    }

    public  void adjustVideoSettings(string value1, string value2, string value3)
    {

    }
}
