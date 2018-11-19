using DummyFiles;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PavleTest : MonoBehaviour {


    // ==== Shop Room ====
    //UI has not been created so therefore placeholder positions are currently set
    //1 This tests if the first instrument ui is present in the store and in correct location (item is a placeholder)
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
    //2 This tests if the second instrument ui is present in the store and in correct location (item is a placeholder)
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
    //3 This tests if the first defence item ui is present in the store and in correct location (item is a placeholder)
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
    //4 This tests if the second defence item ui is present in the store and in correct location (item is a placeholder)
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
    //5 This tests if the first powerup item ui is present in the store and in correct location (item is a placeholder)
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
    //6 This tests if the second powerup item ui is present in the store and in correct location (item is a placeholder)
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
    //7 This tests that the current cash amount UI to the user is present and in correct position
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
    //8 This tests that the current inventory of items UI is present and in correct location
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
    //9 This tests that the background image for the shop exits
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

    //10 This tests that correct background song for the shop is being played
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

    //11 This tests that when a user selects an item the correct info is outputed
    [UnityTest]
    public IEnumerator SelectItem()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        StoreLogic.ShowInformation(GameItems.Instrument1);

        try
        {
            GameObject.FindGameObjectsWithTag("ShopItemInformation");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }
    
    //12 This tests that when a user successfully purchases an item that it gets added to the user
    [UnityTest]
    public IEnumerator BuyItemSuccess()
    {
        SetupCoreScene("Assets/Scenes/shop.unity");

        GlobalUser user = new GlobalUser();
        StoreLogic.BuyItem(GameItems.Instrument1);

        
        try
        {
            GameObject ItemBought = GameObject.FindGameObjectWithTag("ItemBought");
            Assert.That(ItemBought.GetComponent<AudioSource>().isPlaying == true);
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        
       
        List<GameItems> test = user.GetItemList();
        Assert.That(test.Count == 1);
        yield return null;
    }

    //13 This tests that a failure scenario where the user doesn't have enough money to buy an item
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

    //14 This tests that the user can access the inventory of items they have bought
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
    //15 This tests if the first recruitable character ui is present in the store and in correct location (item is a placeholder)
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

    //16 This tests if the second recruitable character ui is present in the store and in correct location (item is a placeholder)
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

    //17 This tests if the third recruitable character ui is present in the store and in correct location (item is a placeholder)
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

    //18 This tests if the fourth recruitable character ui is present in the store and in correct location (item is a placeholder)
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

    //19 This tests that the current cash amount UI to the user is present and in correct position on the recruiting scene
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

    //20 This tests that the background image for the recruiting exits
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

    //21 This tests that correct background song for the recruit scene is being played
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

    //22 This tests that the character inventory UI exits
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

    //23 This test ensures that an info screen pops up when selecting a recruitable character
    [UnityTest]
    public IEnumerator SelectRecruit()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");

        RecruitLogic.ShowCharacterInfo(Characters.GenerateCharacter());

        try
        {
            GameObject.FindGameObjectsWithTag("RecruitInformation");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    //24 This tests when a user buys a character successfully
    [UnityTest]
    public IEnumerator BuyCharacterSuccess()
    {
        SetupCoreScene("Assets/Scenes/Recruit.unity");

        GlobalUser user = new GlobalUser();
        RecruitLogic.BuyCharacter(Characters.GenerateCharacter());

        try
        {
            //Display and accompanying sound affect
            GameObject characterRecruited = GameObject.FindGameObjectWithTag("CharacterRecruited");
            Assert.That(characterRecruited.GetComponent<AudioSource>().isPlaying == true);
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        List<Characters> test = user.GetCharacterList();
        Assert.That(test.Count == 1);
        yield return null;
    }

    //25 This tests when a user fails to buy a character
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

    //26 This tests that the user can access the inventory of characters they have bought
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
    //27 This tests if the first position in the lineup UI exists and in correct position 
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

    //28 This tests if the second position in the lineup UI exists and in correct position 
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

    //29 This tests if the third position in the lineup UI exists and in correct position 
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

    //30 This tests if the fourth position in the lineup UI exists and in correct position 
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

    //31 This tests if the edit button UI exists and in correct position
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

    //32 This tests that the background image for the rehearsal exists
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

    //33 This tests that correct background song for the rehearsal scene is being played
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

    //34 This tests that a user is able to change the lineup of characters they want to go to battle with
    [UnityTest]
    public IEnumerator ChangeLineup()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");

        GlobalLogic globalState = new GlobalLogic();
        globalState.changeLineup(new Characters[4] { new Characters { name = "test" }, new Characters { name = "test" }, new Characters { name = "test" }, new Characters { name = "test" } });

        try
        {
            GameObject.FindGameObjectsWithTag("LineupChanged");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        //Checks to make each positons was filled with the change
        int count = 0;
        foreach (Characters character in globalState.Lineup)
        {
            Assert.That(character.name == "test");
        }

        yield return null;
    }

    //35 This tests that a user is able to select a character and get information on them
    [UnityTest]
    public IEnumerator SelectCharacter()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");
        
        LineupLogic.showCharacterInfo(Characters.GenerateCharacter());

        try
        {
            GameObject.FindGameObjectsWithTag("CharacterInformation");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    //36 This tests that a user can equip a character with a piece of equipment
    [UnityTest]
    public IEnumerator EquipCharacter()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");

        Characters character = Characters.GenerateCharacter();

        Assert.That(character.equippedItems.Count == 0);

        character.equipItem(GameItems.Instrument1);

        Assert.That(character.equippedItems.Count == 1);
        yield return null;
    }

    //37 This tests that a user can select an upgrade on a characters upgrade path and get info on it
    [UnityTest]
    public IEnumerator SelectUpgadePath()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");
        Characters character = Characters.GenerateCharacter();

        List<string> listOfUpgrades = character.getUpgrades();

        try
        {
            GameObject.FindGameObjectsWithTag("UpgradePath");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Upgrade.getSpecificUpgradeInfo(listOfUpgrades.ToArray()[0]);

        try
        {
            GameObject.FindGameObjectsWithTag("UpgradeDescription");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        yield return null;
    }

    //38 This tests if a user successfully made an upgrade for a character
    [UnityTest]
    public IEnumerator UpgradeSuccessful()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");
        Characters character = Characters.GenerateCharacter();
        GlobalUser user = new GlobalUser();
        user.XP = 100;

        List<string> listOfUpgrades = character.getUpgrades();

        try
        {
            GameObject.FindGameObjectsWithTag("UpgradePath");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Upgrade.getSpecificUpgradeInfo(listOfUpgrades.ToArray()[0]);

        try
        {
            GameObject.FindGameObjectsWithTag("UpgradeDescription");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        character.upgradeCharacter(listOfUpgrades.ToArray()[0], user);

        try
        {
            GameObject.FindGameObjectsWithTag("UpgradeSuccess");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.That(character.getUpgrades().Count == listOfUpgrades.Count + 1);

        yield return null;
    }

    //39 This tests the scenario when a user doesn't have enough xp to upgrade a character
    [UnityTest]
    public IEnumerator UpgradeFailed()
    {
        SetupCoreScene("Assets/Scenes/Lineup.unity");
        Characters character = Characters.GenerateCharacter();
        GlobalUser user = new GlobalUser();

        List<string> listOfUpgrades = character.getUpgrades();

        try
        {
            GameObject.FindGameObjectsWithTag("UpgradePath");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Upgrade.getSpecificUpgradeInfo(listOfUpgrades.ToArray()[0]);

        try
        {
            GameObject.FindGameObjectsWithTag("UpgradeDescription");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        character.upgradeCharacter(listOfUpgrades.ToArray()[0], user);

        try
        {
            GameObject.FindGameObjectsWithTag("UpgradeFailed");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.That(character.getUpgrades().Count == listOfUpgrades.Count);

        yield return null;
    }

    // ==== Main Menu Selection Logic ====
    //40 This tests that Shop selection on the menu goes to the correct scene
    [UnityTest]
    public IEnumerator SelectShopOnMainMenu()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.chooseShopScene();
        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "shop");

        yield return null;
    }

    //41 This tests that Recruit selection on the menu goes to the correct scene
    [UnityTest]
    public IEnumerator SelectRecruitOnMainMenu()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.chooseShopScene();
        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "Recruit");

        yield return null;
    }

    //43 This tests that Rehearsal selection on the menu goes to the correct scene
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
    //44 This tests that ui for music volume ajustment exists and is located in the proper place
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

    //45 This tests that a user is able to adjust the volume of the music
    [UnityTest]
    public IEnumerator AdjustMusicVolume()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustMusic(90);

        Assert.That(globalLogic.musicVolume == 90);

        yield return null;
    }

    //46 This tests that ui for sound volume ajustment exists and is located in the proper place
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

    //47 This tests that a user is able to adjust the volume of the sound
    [UnityTest]
    public IEnumerator AdjustSoundVolume()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustSound(90);

        Assert.That(globalLogic.soundVolume == 90);

        yield return null;
    }

    //48 This tests that ui for diplay settings ajustment exists and is located in the proper place
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

    //49 This tests that a user is able to adjust the display settings 
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

    //50 This tests that ui for key bind adjustment exists and is located in the proper place
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

    //51 This tests that a user is able to successfully customize their keybind
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

    //52 This tests a failure scenario where a user tries to rebind two keys to the same key
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

    //53 This tests that ui for audio latency adjustment exists and is located in the proper place
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

    //52 This tests that a user is able to adjust the audio latency
    [UnityTest]
    public IEnumerator AdjustAudioLatency()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustAudioLatency(90);

        Assert.That(globalLogic.audioLatency == 90);

        yield return null;
    }

    //54 This tests that ui for video latency adjustment exists and is located in the proper place
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

    //55 This tests that a user is able to adjust the video latency
    [UnityTest]
    public IEnumerator AdjustVideoLatency()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustVideoLatency(90);

        Assert.That(globalLogic.videoLatency == 90);

        yield return null;
    }

    //56 This tests that the background image for the settings exists
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

    //57 This tests that correct background song for the settings scene is being played
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
    //58 This tests if the world map ui exists
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

    //59 This tests if the user can choose a level on the world map
    [UnityTest]
    public IEnumerator WorldMapLevelSelected()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.loadBattleLevel("Level One", new Characters[] { Characters.GenerateCharacter() });

        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "main");
        
        yield return null;
    }

    // ==== Cutscenes ====

    //60 This tests if the first cutscene is exists and in correct location
    [UnityTest]
    public IEnumerator CutScene1Exists()
    {
        SetupCoreScene("Assets/Scenes/Cutscene.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("CutScene1");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("CutScene1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    //61 This tests if the second cutscene is exists and in correct location
    [UnityTest]
    public IEnumerator CutScene2Exists()
    {
        SetupCoreScene("Assets/Scenes/Cutscene.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("CutScene2");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("CutScene2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    //62 This tests if the third cutscene is exists and in correct location
    [UnityTest]
    public IEnumerator CutScene3Exists()
    {
        SetupCoreScene("Assets/Scenes/Cutscene.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("CutScene3");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("CutScene3").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    //63 This tests if the third cutscene is exists and in correct location
    [UnityTest]
    public IEnumerator CutScene4Exists()
    {
        SetupCoreScene("Assets/Scenes/Cutscene.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("CutScene4");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("CutScene4").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    //64 This tests if the fourth cutscene is exists and in correct location
    [UnityTest]
    public IEnumerator CutScene5Exists()
    {
        SetupCoreScene("Assets/Scenes/Cutscene.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("CutScene5");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("CutScene5").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    //65 This tests if the skip button during a cutscene exists and in correct location
    [UnityTest]
    public IEnumerator SkipUIExists()
    {
        SetupCoreScene("Assets/Scenes/Cutscene.unity");
        try
        {
            GameObject.FindGameObjectsWithTag("SkipScene");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        if (GameObject.FindGameObjectWithTag("SkipScene").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    //66 This tests that a cutscene will be played for the user
    [UnityTest]
    public IEnumerator PlayCutscene()
    {
        CutsceneLogic cutScene = new CutsceneLogic();

        cutScene.playCutscene(1);

        Assert.That(cutScene.currentCutscene == 1);
        Assert.That(cutScene.isCutScenePlaying == true);


        yield return null;
    }

    //67 This tests if a user can pause and unpause a cutscene
    [UnityTest]
    public IEnumerator PauseAndUnPauseCutscene()
    {
        CutsceneLogic cutScene = new CutsceneLogic();

        cutScene.playCutscene(1);

        Thread.Sleep(100);

        cutScene.pauseCutscene();


        Assert.That(cutScene.isCutScenePaused == true);
        Assert.That(cutScene.isCutScenePlaying == false);

        cutScene.unPauseCutscene();

        Assert.That(cutScene.isCutScenePaused == false);
        Assert.That(cutScene.isCutScenePlaying == true);


        yield return null;
    }

    //68 This tests if a user can skip a cutscene
    [UnityTest]
    public IEnumerator SkipCutscene()
    {
        CutsceneLogic cutScene = new CutsceneLogic();

        cutScene.playCutscene(1);

        Assert.That(cutScene.currentCutscene == 1);
        Assert.That(cutScene.isCutScenePlaying == true);

        cutScene.pauseCutscene();
        cutScene.skipCutscene();

        Assert.That(cutScene.isCutScenePlaying == false);
        Assert.That(cutScene.currentCutscene == 0);

        yield return null;
    }

    // ==== Saving and Loading Games ====
    //69 This tests if a user can save the game
    [UnityTest]
    public IEnumerator ManualSave()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");
        SaveState savestate = new SaveState();
        savestate.selectSavePath("test");
        savestate.saveGameState(false);

        try
        {
            GameObject.FindGameObjectsWithTag("DoYouWantToSave");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }


        Assert.That(savestate.saveState.Length != 0);

        savestate.confirmSave();

        try
        {
            GameObject.FindGameObjectsWithTag("SaveSuccessful");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        yield return null;
    }


    [UnityTest]
    //70 This tests if the system can automatically save for the user
    public IEnumerator AutomaticSave()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");
        SaveState savestate = new SaveState();
        savestate.selectSavePath("test");
        savestate.saveGameState(true);

        try
        {
            GameObject.FindGameObjectsWithTag("SaveSuccessFullAuto");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.That(savestate.saveState.Length != 0);
        yield return null;
    }

    //71 This tests if a user selects an empty save slot
    [UnityTest]
    public IEnumerator SelectEmptySavePath()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");
        SaveState savestate = new SaveState();
        savestate.selectSavePath("test");
        Assert.That(savestate.savePath == "test");
        yield return null;
    }

    //72 This tests if a user selects an occupied save slot
    [UnityTest]
    public IEnumerator SelectOccupiedSavePath()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");
        SaveState savestate = new SaveState();
        savestate.selectSavePath("test");

        try
        {
            GameObject.FindGameObjectsWithTag("PathOccupied");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        savestate.confirmOverwrite();

        Assert.That(savestate.savePath== "test");
        yield return null;
    }

    //73 This tests when a user loads a saved game
    [UnityTest]
    public IEnumerator LoadSaveFile()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");
        SaveState savestate = new SaveState();
        savestate.loadGameState();

        try
        {
            GameObject.FindGameObjectsWithTag("LoadGameSuccess");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }
        
        yield return null;
    }

    // ==== Pause and UnPause Games ====
    //74 This tests when a user pauses and unpauses during a battle
    [UnityTest]
    public IEnumerator PauseAndUnpauseGame()
    {
        SetupCoreScene("Assets/Scenes/main.unity");
        PausingLogic pauseGameLogic = new PausingLogic();

        pauseGameLogic.PauseGame();


        try
        {
            GameObject.FindGameObjectsWithTag("GamePaused");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.That(pauseGameLogic.paused == true);

        pauseGameLogic.UnPauseGame();

        Assert.That(pauseGameLogic.paused == false);


        yield return null;
    }

    [UnityTest]
    //75 This tests when a user exits a level when in the pause screen
    public IEnumerator ExitLevel()
    {
        SetupCoreScene("Assets/Scenes/main.unity");
        PausingLogic pauseGameLogic = new PausingLogic();

        pauseGameLogic.PauseGame();


        try
        {
            GameObject.FindGameObjectsWithTag("GamePaused");
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        Assert.That(pauseGameLogic.paused == true);

        pauseGameLogic.exitLevel();


        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "Menu");

        yield return null;
    }


    void SetupCoreScene(string s)
    {
        EditorSceneManager.OpenScene(s);
    }
}
