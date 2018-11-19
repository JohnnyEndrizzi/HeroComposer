using DummyFiles;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Threading;

public class CoreGameplayTest : MonoBehaviour
{
    void SetupCoreScene(string s)
    {
        EditorSceneManager.OpenScene(s);
    }

    // ==== Shop Room ==== (UI has not been created so therefore placeholder positions are currently set)

    // Test 1: This tests if the first instrument ui is present in the store and in correct location (item is a placeholder)
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

    // Test 2: This tests if the second instrument ui is present in the store and in correct location (item is a placeholder)
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

    // Test 3: This tests if the first defence item ui is present in the store and in correct location (item is a placeholder)
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

    // Test 4: This tests if the second defence item ui is present in the store and in correct location (item is a placeholder)
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

    // Test 5: This tests if the first powerup item ui is present in the store and in correct location (item is a placeholder)
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

    // Test 6: This tests if the second powerup item ui is present in the store and in correct location (item is a placeholder)
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

    // Test 7: This tests that the current cash amount UI to the user is present and in correct position
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

    // Test 8: This tests that the current inventory of items UI is present and in correct location
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

    // Test 9: This tests that the background image for the shop exits
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

    // Test 10: This tests that correct background song for the shop is being played
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

    // Test 11: This tests that when a user selects an item the correct info is outputed
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

    // Test 12: This tests that when a user successfully purchases an item that it gets added to the user
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

    // Test 13: This tests that a failure scenario where the user doesn't have enough money to buy an item
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
        catch (Exception e)
        {
            Assert.Fail();
        }

        List<GameItems> test = user.GetItemList();
        Assert.That(test.Count == 0);

        yield return null;
    }

    // Test 14: This tests that the user can access the inventory of items they have bought
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

    // Test 15: This tests if the first recruitable character ui is present in the store and in correct location (item is a placeholder)
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

    // Test 16: This tests if the second recruitable character ui is present in the store and in correct location (item is a placeholder)
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

    // Test 17: This tests if the third recruitable character ui is present in the store and in correct location (item is a placeholder)
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

    // Test 18: This tests if the fourth recruitable character ui is present in the store and in correct location (item is a placeholder)
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

    // Test 19: This tests that the current cash amount UI to the user is present and in correct position on the recruiting scene
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

    // Test 20: This tests that the background image for the recruiting exits
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

    // Test 21: This tests that correct background song for the recruit scene is being played
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

    // Test 22: This tests that the character inventory UI exits
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

    // Test 23: This test ensures that an info screen pops up when selecting a recruitable character
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

    // Test 24: This tests when a user buys a character successfully
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

    // Test 25: This tests when a user fails to buy a character
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

    // Test 26: This tests that the user can access the inventory of characters they have bought
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

    // Test 27: This tests if the first position in the lineup UI exists and in correct position 
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

    // Test 28: This tests if the second position in the lineup UI exists and in correct position 
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

    // Test 29: This tests if the third position in the lineup UI exists and in correct position 
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

    // Test 30: This tests if the fourth position in the lineup UI exists and in correct position 
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

    // Test 31: This tests if the edit button UI exists and in correct position
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

    // Test 32: This tests that the background image for the rehearsal exists
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

    // Test 33: This tests that correct background song for the rehearsal scene is being played
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

    // Test 34: This tests that a user is able to change the lineup of characters they want to go to battle with
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

    // Test 35: This tests that a user is able to select a character and get information on them
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

    // Test 36: This tests that a user can equip a character with a piece of equipment
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

    // Test 37: This tests that a user can select an upgrade on a characters upgrade path and get info on it
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

    // Test 38: This tests if a user successfully made an upgrade for a character
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

    // Test 39: This tests the scenario when a user doesn't have enough xp to upgrade a character
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

    // Test 40: This tests that Shop selection on the menu goes to the correct scene
    [UnityTest]
    public IEnumerator SelectShopOnMainMenu()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.chooseShopScene();
        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "shop");

        yield return null;
    }

    // Test 41: This tests that Recruit selection on the menu goes to the correct scene
    [UnityTest]
    public IEnumerator SelectRecruitOnMainMenu()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");

        GlobalLogic.chooseShopScene();
        Scene currentScene = SceneManager.GetActiveScene();

        Assert.That(currentScene.name == "Recruit");

        yield return null;
    }

    // Test 43: This tests that Rehearsal selection on the menu goes to the correct scene
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

    // Test 44: This tests that ui for music volume ajustment exists and is located in the proper place
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

    // Test 45: This tests that a user is able to adjust the volume of the music
    [UnityTest]
    public IEnumerator AdjustMusicVolume()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustMusic(90);

        Assert.That(globalLogic.musicVolume == 90);

        yield return null;
    }

    // Test 46: This tests that ui for sound volume ajustment exists and is located in the proper place
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

    // Test 47: This tests that a user is able to adjust the volume of the sound
    [UnityTest]
    public IEnumerator AdjustSoundVolume()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustSound(90);

        Assert.That(globalLogic.soundVolume == 90);

        yield return null;
    }

    // Test 48: This tests that ui for diplay settings ajustment exists and is located in the proper place
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

    // Test 49: This tests that a user is able to adjust the display settings 
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

    // Test 50: This tests that ui for key bind adjustment exists and is located in the proper place
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

    // Test 51: This tests that a user is able to successfully customize their keybind
    [UnityTest]
    public IEnumerator AdjustKeyBindsSuccess()
    {
        KeyBinds keyBinds = new KeyBinds();

        keyBinds.rebindKey("r", "k");

        bool keyFound = false;
        foreach (string key in keyBinds.keybinds)
        {
            Assert.That(key != "r");

            if (key == "k")
            {
                keyFound = true;
            }
        }

        if (!keyFound)
        {
            Assert.Fail();
        }

        yield return null;
    }

    // Test 52: This tests a failure scenario where a user tries to rebind two keys to the same key
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

    // Test 53: This tests that ui for audio latency adjustment exists and is located in the proper place
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

    // Test 76: This tests that a user is able to adjust the audio latency
    [UnityTest]
    public IEnumerator AdjustAudioLatency()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustAudioLatency(90);

        Assert.That(globalLogic.audioLatency == 90);

        yield return null;
    }

    // Test 54: This tests that ui for video latency adjustment exists and is located in the proper place
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

    // Test 55: This tests that a user is able to adjust the video latency
    [UnityTest]
    public IEnumerator AdjustVideoLatency()
    {
        GlobalLogic globalLogic = new GlobalLogic();

        globalLogic.adjustVideoLatency(90);

        Assert.That(globalLogic.videoLatency == 90);

        yield return null;
    }

    // Test 56: This tests that the background image for the settings exists
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

    // Test 57: This tests that correct background song for the settings scene is being played
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

    // Test 58: This tests if the world map ui exists
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

    // Test 59: This tests if the user can choose a level on the world map
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

    // Test 60: This tests if the first cutscene is exists and in correct location
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

    // Test 61: This tests if the second cutscene is exists and in correct location
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

    // Test 62: This tests if the third cutscene is exists and in correct location
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

    // Test 63: This tests if the third cutscene is exists and in correct location
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

    // Test 64: This tests if the fourth cutscene is exists and in correct location
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

    // Test 65: This tests if the skip button during a cutscene exists and in correct location
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

    // Test 66: This tests that a cutscene will be played for the user
    [UnityTest]
    public IEnumerator PlayCutscene()
    {
        CutsceneLogic cutScene = new CutsceneLogic();

        cutScene.playCutscene(1);

        Assert.That(cutScene.currentCutscene == 1);
        Assert.That(cutScene.isCutScenePlaying == true);


        yield return null;
    }

    // Test 67: This tests if a user can pause and unpause a cutscene
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

    // Test 68: This tests if a user can skip a cutscene
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

    // Test 69: This tests if a user can save the game
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

    // Test 70: This tests if the system can automatically save for the user
    [UnityTest]
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

    // Test 71: This tests if a user selects an empty save slot
    [UnityTest]
    public IEnumerator SelectEmptySavePath()
    {
        SetupCoreScene("Assets/Scenes/Menu.unity");
        SaveState savestate = new SaveState();
        savestate.selectSavePath("test");
        Assert.That(savestate.savePath == "test");
        yield return null;
    }

    // Test 72: This tests if a user selects an occupied save slot
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

        Assert.That(savestate.savePath == "test");
        yield return null;
    }

    // Test 73: This tests when a user loads a saved game
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

    // Test 74: This tests when a user pauses and unpauses during a battle
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

    // Test 75: This tests when a user exits a level when in the pause screen
    [UnityTest]
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

    // ==== Note Bar ====

    /* Test 77: This test checks that the note bar exists within the scene. */
    [UnityTest]
    public IEnumerator NoteBarExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Checks if the gameobject exists upon opening the scene. */
        if (GameObject.FindGameObjectsWithTag("NoteBar") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 78: This test checks that the note bar is instantiated in the proper position. */
    [UnityTest]
    public IEnumerator NoteBarPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Compares the gameobject's position to the proper position. */
        if (GameObject.FindGameObjectWithTag("NoteBar").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 79: This test checks that the note bar will light up as a response to the user holding down an action button (U, I, O or P). */
    [UnityTest]
    public IEnumerator NoteBarHighlight()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Creates a CharacterListener instance, which is used to call the function that an action button would normally call. */
        CharacterListener test = new CharacterListener();
        test.ChangeNoteBarHighlight(Color.white);

        /* Check that the note bar changes colour as a response to ChangeNoteBarHighlight being called. */
        if (GameObject.Find("Note_Bar_Circle").GetComponent<SpriteRenderer>().color == Color.white)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 80: This test checks that the accuracy text for inputs is caclualted correctly. */
    [UnityTest]
    public IEnumerator NoteBarAccuracyCalculation()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Error Bounds
         * (25 >= x >= 0):   Perfect 
         * (100 >= x > 25):  Great
         * (200 >= x > 100): Good
         * (200 < x):        Miss */
        CharacterListener listener = new CharacterListener();
        
        /* Hit time = 100, Note time = 125, Expected Output = Perfect */
        if (listener.GetNoteAccuracySprite(100, 100, 25) != Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Perfect"))
        {
            Assert.Fail();
        }

        /* Hit time = 100, Note time = 150, Expected Output = Great */
        if (listener.GetNoteAccuracySprite(100, 100, 50) != Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Great"))
        {
            Assert.Fail();
        }

        /* Hit time = 100, Note time = 250, Expected Output = Good */
        if (listener.GetNoteAccuracySprite(100, 100, 150) != Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Good"))
        {
            Assert.Fail();
        }

        /* Hit time = 100, Note time = 350, Expected Output = Miss */
        if (listener.GetNoteAccuracySprite(100, 100, 250) != Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Miss"))
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    // ==== Characters ====

    /* Test 81: This test checks that the character(s) exist within the scene. */
    [UnityTest]
    public IEnumerator CharactersExist()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        int numberOfCharacters = 4;

        /* Gets all the gameobjects existing upon opening the scene. */
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        /* There can only be 1-4 characters (this will eventually be tested againt a global character count variable). */
        if (characters.Length > 0 && characters.Length <= 4)
        {
            Assert.Pass();
        }
        else
        {
            Assert.Fail();
        }
        yield return null;
    }

    /* Test 82: This test checks that the character(s) is/are instantiated in the proper position. */
    [UnityTest]
    public IEnumerator CharacterPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

        /* Valid character positions. */
        Vector3[] vectors = new [] {new Vector3(3.38f, 1.56f, -4.5f),
                                    new Vector3(1.43f, 1.19f, -4.8f),
                                    new Vector3(1.43f, -0.35f, -5.1f),
                                    new Vector3(2.82f, -0.67f, -5.5f)};

        int correct = 0;
        /* Compares the each of gameobject's position to an array of proper positions. */
        for (int i = 0; i < characters.Length; i++)
        {
            for (int j = 0; j < characters.Length; j++)
            {
                if (characters[i].transform.position == vectors[j])
                {
                    correct++;
                    break;
                }
            }
        }

        /* Each gameobject's position must be valid. */
        if (correct == characters.Length)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 83: */
    [UnityTest]
    public IEnumerator CharacterLevelUp()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");

        Characters character = new Characters();

        character.currentLevel = 10;
        character.calculateLevelUpXP();

        character.currentXP = character.currentBoundedXP - 1;
        character.currentXP++;

        yield return null;

        if (character.currentLevel == 11)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    /* Test 84: */
    [UnityTest]
    public IEnumerator CharacterSpecialGaugeIncrease()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");

        Characters character = new Characters();

        float currentGaugeValue = character.specialGaugeValue;
        Menu.GetComponent<CharacterListener>().GetNoteAccuracySprite(100, 100, 25);

        if (character.specialGaugeValue > currentGaugeValue)
        {
            Assert.Pass();
        }

        Assert.Fail();

        yield return null;
    }

    // ==== Menu ====

    /* Test 85: This test checks that the game menu exists within the scene. */
    [UnityTest]
    public IEnumerator MenuExist()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Checks if the gameobject exists upon opening the scene. */
        if (GameObject.FindGameObjectsWithTag("Menu") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 86: This test checks that the game menu is instantiated in the proper position. */
    [UnityTest]
    public IEnumerator MenuPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Compares the gameobject's position to the proper position. */
        if (GameObject.FindGameObjectWithTag("Menu").transform.position == new Vector3(5.4f, 0.52f, -5f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 87: This test checks that a menu's state change will appropriately change the menu's sprite. */
    [UnityTest]
    public IEnumerator MenuHasCorrectStateAndSpriteUponChange()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Fetches the menu gameobject, so long it exists upon opening the scene. */
        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");
        if (Menu == null)
        {
            Assert.Fail();
        }

        /* Loads in the sprite map for the menu. */
        Sprite[] sprites = Resources.LoadAll<Sprite>("Menu/TempMenu");
        if (sprites == null)
        {
            Assert.Fail();
        }

        /* Eahc state of the menu. */
        string[] allStates = {"ATK", "DEF", "MGC", "ULT"};
        
        /* For each state of the menu, fetch the corresponding menu slice-sprite */
        for (int i = 0; i < allStates.Length; i++)
        {
            Sprite TextSprite = null;
            for (int j = 0; j < sprites.Length; j++)
            {
                if (sprites[j].name == allStates[i])
                {
                    /* Fetches the corresponding slice-sprite. */
                    TextSprite = sprites[j];
                }
            }

            /* Ensures a slice-sprite was found. */
            if (TextSprite == null)
            {
                Assert.Fail();
            }

            /* Sets each of the menu states through each loop iteration. */
            ClickListener.state testState;
            if (allStates[i] == "ATK")
            {
                testState = ClickListener.state.ATK;
            }
            else if (allStates[i] == "DEF")
            {
                testState = ClickListener.state.DEF;
            }
            else if (allStates[i] == "MGC")
            {
                testState = ClickListener.state.MGC;
            }
            else
            {
                testState = ClickListener.state.ULT;
            }

            /* Calls the function to reload the menu, providing the new sprite and menu state.  */
            Menu.GetComponent<ClickListener>().ChangeMenuState(TextSprite, testState);

            /* Check that the state matches the sprite, for each state. */
            if ((Menu.GetComponent<SpriteRenderer>().sprite != TextSprite) || (testState != Menu.GetComponent<ClickListener>().GetMenuState()))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
        yield return null;
    }

    // ==== Boss ====

    /* Test 88: This test checks that the boss exists within the scene. */
    [UnityTest]
    public IEnumerator BossExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Checks if the gameobject exists upon opening the scene. */
        if (GameObject.FindGameObjectsWithTag("Boss") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 89: This test checks that the boss is instantiated in the proper position. */
    [UnityTest]
    public IEnumerator BossPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Compares the gameobject's position to the proper position. */
        if (GameObject.FindGameObjectWithTag("Boss").transform.position == new Vector3(-3.5f, 1.16f, -5.5f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 90: This test checks that the boss' health bar exists within the scene. */
    [UnityTest]
    public IEnumerator BossHealthBarExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Checks if the gameobject exists upon opening the scene. */
        if (GameObject.Find("Healthbar") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 91: This test checks that the boss's health bar is instantiated in the proper position. */
    [UnityTest]
    public IEnumerator BossHealthBarPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Compares the gameobject's position to the proper position. */
        if (GameObject.Find("Healthbar").GetComponent<RectTransform>().anchoredPosition == new Vector2(399.0f, 175.0f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BossAttacksCharacter()
    {
        BossLogic boss = new BossLogic();
        boss.bossPower = 100;

        Characters character = new Characters();
        character.currentHealth = 1000;
        character.defenseValue = 20;

        boss.BossAttack(character);

        if (character.currentHealth != 985)
        {
            Assert.Fail();
        }

        character.currentHealth = 1000;
        boss.BossSpecial(character);

        if (character.currentHealth != 960)
        {
            Assert.Fail();
        }

        Assert.Pass();

        yield return null;
    }

    // ==== Scene ====

    /* Test 92: This test checks that the curtains exist within the scene. */
    [UnityTest]
    public IEnumerator SceneCurtainExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Checks if the gameobject exists upon opening the scene. */
        if (GameObject.FindGameObjectsWithTag("Curtains") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 93: This test checks that the curtains are instantiated in the proper position. */
    [UnityTest]
    public IEnumerator SceneCurtainPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Compares the gameobject's position to the proper position. */
        if (GameObject.FindGameObjectWithTag("Curtains").transform.position == new Vector3(0.0f, 1.43f, -7.95f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 94: This test checks that the stage exists within the scene. */
    [UnityTest]
    public IEnumerator SceneStageExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Checks if the gameobject exists upon opening the scene. */
        if (GameObject.FindGameObjectsWithTag("Stage") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 95: This test checks that the stage is instantiated in the proper position. */
    [UnityTest]
    public IEnumerator SceneStagePosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Compares the gameobject's position to the proper position. */
        if (GameObject.FindGameObjectWithTag("Stage").transform.position == new Vector3(0.0f, 7.9f, -2.0f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 96: This test checks that the spotlights exist within the scene. */
    [UnityTest]
    public IEnumerator SceneSpotlightExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Checks if the gameobject exists upon opening the scene. */
        if (GameObject.FindGameObjectsWithTag("Spotlight").Length == 2)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 97: This test checks that the spotlights are instantiated in the proper position. */
    [UnityTest]
    public IEnumerator SceneSpotlightPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        GameObject[] spotlights = GameObject.FindGameObjectsWithTag("Spotlight");

        /* Compares the each gameobject's position to the proper position. */
        for (int i = 0; i < spotlights.Length; i++)
        {
            /* Both spotlights originally instantiate ot the same position, before CurtainMovement sets them up. */
            if (spotlights[i].transform.localPosition != new Vector3(-0.72f, 1.25f, -9.0f))
            {
                Assert.Fail();
            }
        }

        Assert.Pass();
        yield return null;
    }

    /* Test 98: This test checks that the background exists within the scene. */
    [UnityTest]
    public IEnumerator SceneBackgroundExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Checks if the gameobject exists upon opening the scene. */
        if (GameObject.FindGameObjectsWithTag("Background") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 99: This test checks that the background is instantiated in the proper position. */
    [UnityTest]
    public IEnumerator SceneBackgroundPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        /* Compares the gameobject's position to the proper position. */
        if (GameObject.FindGameObjectWithTag("Background").transform.localPosition == new Vector3(21.75f, 4.03f, 6.0f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 100: */
    [UnityTest]
    public IEnumerator SceneStageScore()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");

        LevelLogic level = new LevelLogic();

        level.maxNumericalStageScore = 100000;

        level.currentGrade = level.calculateStageGrade(90000);
        if (level.currentGrade != LevelLogic.stageGrade.A)
        {
            Assert.Fail();
        }

        level.currentGrade = level.calculateStageGrade(75000);
        if (level.currentGrade != LevelLogic.stageGrade.B)
        {
            Assert.Fail();
        }

        level.currentGrade = level.calculateStageGrade(65000);
        if (level.currentGrade != LevelLogic.stageGrade.C)
        {
            Assert.Fail();
        }

        level.currentGrade = level.calculateStageGrade(50000);
        if (level.currentGrade != LevelLogic.stageGrade.D)
        {
            Assert.Fail();
        }

        level.currentGrade = level.calculateStageGrade(45000);
        if (level.currentGrade != LevelLogic.stageGrade.F)
        {
            Assert.Fail();
        }

        Assert.Pass();

        yield return null;
    }

    /* Test 101: */
    [UnityTest]
    public IEnumerator SceneStageCompletion()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");

        LevelLogic level = new LevelLogic();

        level.maxNumericalStageScore = 100000;

        level.currentGrade = level.calculateStageGrade(90000);
        if (level.levelPassed == false)
        {
            Assert.Fail();
        }

        level.currentGrade = level.calculateStageGrade(75000);
        if (level.levelPassed == false)
        {
            Assert.Fail();
        }

        level.currentGrade = level.calculateStageGrade(65000);
        if (level.levelPassed == false)
        {
            Assert.Fail();
        }

        level.currentGrade = level.calculateStageGrade(50000);
        if (level.levelPassed == false)
        {
            Assert.Fail();
        }

        level.currentGrade = level.calculateStageGrade(45000);
        if (level.levelPassed == true)
        {
            Assert.Fail();
        }

        Assert.Pass();

        yield return null;
    }
    
    // Yifu code



    // Pavle code

}
