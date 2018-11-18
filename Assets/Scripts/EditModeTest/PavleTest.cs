using DummyFiles;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PavleTest : MonoBehaviour {


    // ==== Shop Room ====
    [UnityTest]
    public IEnumerator Instrument1UIExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Instrument1");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Instrument1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Instrument2UIExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Instrument2");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Instrument2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Defence1UIExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Defence1");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Defence1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Defence2UIExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Defence2");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Defence2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator PowerUp1UIExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("PowerUp1");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("PowerUp1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator PowerUp2UIExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("PowerUp2");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        if (GameObject.FindGameObjectWithTag("PowerUp2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator CurrentCashUIExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("CurrentCash");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        if (GameObject.FindGameObjectWithTag("CurrentCash").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator InventoryUIExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("Inventory");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        if (GameObject.FindGameObjectWithTag("Inventory").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BackGroundImageShopExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("BackGroundShop");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        
        Assert.Pass();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BackGroundMusicShopExists()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        try
        {
            AudioSource audio = GameObject.FindGameObjectWithTag("BackGroundShop").GetComponent<AudioSource>();
            if (audio.clip.name != "shopMusic" && audio.isPlaying == false)
            {
                Assert.Fail();
            }

        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BuyItemSuccess()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        GlobalUser user = new GlobalUser();
        StoreLogic.BuyItem(GameItems.Instrument1);

        try
        {
            GameObject.FindGameObjectsWithTag("ItemBought");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        List<GameItems> test = user.GetItemList();
        Assert.That(test.Count == 1);
        yield return null;
    }
    [UnityTest]
    public IEnumerator BuyItemFail()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        GlobalUser user = new GlobalUser();
        StoreLogic.BuyItem(GameItems.Instrument1);

        try
        {
            GameObject.FindGameObjectsWithTag("FailedToBuy");
        }
        catch(Exception e)
        {
            Assert.Fail();
        }

        List<GameItems> test = user.GetItemList();
        Assert.That(test.Count == 0);
        
        yield return null;
    }

    [UnityTest]
    public IEnumerator AccessInventory()
    {
        GlobalUser user = new GlobalUser();
        user.AddItem(GameItems.Instrument1);
        user.AddItem(GameItems.Shield2);


        List<GameItems> test = user.GetItemList();
        Assert.That(test.Count == 2);

        yield return null;
    }


    // ==== Recruiting Room ====
    [UnityTest]
    public IEnumerator Recruit1UIExists()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Recruit1");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Recruit1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator Recruit2UIExists()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Recruit2");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Recruit2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator Recruit3UIExists()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Recruit3");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Recruit3").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator Recruit4UIExists()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Recruit4");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Recruit4").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator CurrentCashRecruitUIExists()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("CurrentCash");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        if (GameObject.FindGameObjectWithTag("CurrentCash").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BackGroundImageRecruitExists()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("BackGroundRecruit");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BackGroundMusicRecruitExists()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");

        try
        {
            AudioSource audio = GameObject.FindGameObjectWithTag("BackGroundRecruit").GetComponent<AudioSource>();
            if (audio.clip.name != "recruitMusic" && audio.isPlaying == false)
            {
                Assert.Fail();
            }

        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }


    [UnityTest]
    public IEnumerator InventoryUIExistsInRecruit()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("Inventory");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        if (GameObject.FindGameObjectWithTag("Inventory").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }


    [UnityTest]
    public IEnumerator BuyCharacterSuccess()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");

        GlobalUser user = new GlobalUser();
        RecruitLogic.BuyCharacter(Characters.GenerateCharacter());

        try
        {
            GameObject.FindGameObjectsWithTag("CharacterRecruited");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        List<Characters> test = user.GetCharacterList();
        Assert.That(test.Count == 1);
        yield return null;
    }
    [UnityTest]
    public IEnumerator BuyCharacterFail()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        GlobalUser user = new GlobalUser();
        RecruitLogic.BuyCharacter(Characters.GenerateCharacter());

        try
        {
            GameObject.FindGameObjectsWithTag("FailedToRecruit");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        List<Characters> test = user.GetCharacterList();
        Assert.That(test.Count == 0);

        yield return null;
    }

    [UnityTest]
    public IEnumerator AccessCharacterInventory()
    {
        GlobalUser user = new GlobalUser();
        user.CharacterList = new List<Characters> { Characters.GenerateCharacter(), Characters.GenerateCharacter() };


        List<Characters> test = user.GetCharacterList();
        Assert.That(test.Count == 2);

        yield return null;
    }

    // ==== Line Up Room ====

    [UnityTest]
    public IEnumerator Position1UIExists()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Position1");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Position1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator Position2UIExists()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Position2");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Position2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator Position3UIExists()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Position3");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Position3").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator Position4UIExists()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("Position4");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("Position4").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator EditLineUpUIExists()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("EditLineup");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        if (GameObject.FindGameObjectWithTag("EditLineup").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BackGroundImageLineupExists()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("BackGroundLineup");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BackGroundMusicLineupExists()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");

        try
        {
            AudioSource audio = GameObject.FindGameObjectWithTag("BackGroundLineup").GetComponent<AudioSource>();
            if (audio.clip.name != "lineupMusic" && audio.isPlaying == false)
            {
                Assert.Fail();
            }

        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    [UnityTest]
    public IEnumerator ChangeLineup()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");

        GlobalLogic globalState = new GlobalLogic();
        globalState.changeLineup(new Characters[4] { Characters.GenerateCharacter(), Characters.GenerateCharacter(), Characters.GenerateCharacter(), Characters.GenerateCharacter() });

        try
        {
            GameObject.FindGameObjectsWithTag("LineupChanged");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        int count = 0;
        foreach (Characters character in globalState.Lineup)
        {
            Assert.That(character.characterId != null);
        }

        yield return null;
    }

    // ==== Main Menu Selection Logic ====
    [UnityTest]
    public IEnumerator SelectShopOnMainMenu()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.chooseShopScene();
        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "shop");

        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectRecruitOnMainMenu()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.chooseShopScene();
        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "Recruit");

        yield return null;
    }

    [UnityTest]
    public IEnumerator SelectLineupOnMainMenu()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.chooseShopScene();
        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "Lineup");

        yield return null;
    }

    // ==== Settings ====
    [UnityTest]
    public IEnumerator AdjustMusicVolumeExists()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("AdjustMusicVolume");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("AdjustMusicVolume").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustMusicVolume()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustMusic(90);

        Assert.That(globalLogic.musicVolume == 90);

        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustVolumeExists()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("AdjustSoundVolume");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("AdjustSoundVolume").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustSoundVolume()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustSound(90);

        Assert.That(globalLogic.soundVolume == 90);

        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustDisplaySettingsExists()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("AdjustDisplaySettings");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("AdjustDisplaySettings").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustDisplaySettings()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustVideoSettings("fov", "resolution", "detail level");

        Assert.That(globalLogic.videoSettings[0] == "fov");
        Assert.That(globalLogic.videoSettings[0] == "resolution");
        Assert.That(globalLogic.videoSettings[0] == "detail level");

        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustKeyBindsExists()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("KeyBinds");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("KeyBinds").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustKeyBindsSuccess()
    {
        KeyBinds keyBinds = new KeyBinds();

        keyBinds.rebindKey("r", "k");

        bool keyFound = false;
        foreach(string key in keyBinds.keybinds)
        {
            Assert.That(key != "r");

            if(key == "k")
            {
                keyFound = true;
            }
        }

        if(!keyFound)
        {
            Assert.Fail();
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustKeyBindsFailure()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");

        KeyBinds keyBinds = new KeyBinds();

        keyBinds.rebindKey("r", "k");
        keyBinds.rebindKey("q", "k");

        try
        {
            GameObject.FindGameObjectsWithTag("KeyBindFailed");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        foreach (string key in keyBinds.keybinds)
        {
            if (key == "q")
            {
                Assert.Pass();
            }
        }

        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustAudioLatencyExists()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("AudioLatency");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("AudioLatency").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustAudioLatency()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustAudioLatency(90);

        Assert.That(globalLogic.audioLatency == 90);

        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustVideoLatencyExists()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("VideoLatency");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("VideoLatency").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator AdjustVideoLatency()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustVideoLatency(90);

        Assert.That(globalLogic.videoLatency == 90);

        yield return null;
    }

    [UnityTest]
    public IEnumerator BackGroundImageSettingsExists()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("BackGroundSettings");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BackGroundMusicSettingsExists()
    {
        SetupCoreScene("Assets/Scenes/Settings.unity");

        try
        {
            AudioSource audio = GameObject.FindGameObjectWithTag("BackGroundSettings").GetComponent<AudioSource>();
            if (audio.clip.name != "SettingsMusic" && audio.isPlaying == false)
            {
                Assert.Fail();
            }

        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }


    // ==== WorldMap ====
    [UnityTest]
    public IEnumerator WorldMapExists()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        try
        {
            GameObject.FindGameObjectsWithTag("WorldMap");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    [UnityTest]
    public IEnumerator WorldMapLevelSelected()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.loadBattleLevel("Level One", new Characters[] { Characters.GenerateCharacter() });

        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "main");
        
        yield return null;
    }






    void SetupCoreScene(string s)
    {

        //AudioSource x = GameObject.FindGameObjectWithTag("Menu").GetComponent<AudioSource>();
        EditorSceneManager.OpenScene(s);
    }
}
