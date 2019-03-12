using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoreGameplayPlayMode : MonoBehaviour
{
    void SetupCoreScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    // ==== Note Bar ====

    /* Test 115: This test checks that the accuracy text exists within the scene upon the user inputting an action. */
    [UnityTest]
    public IEnumerator NoteBarAccuracyTextExists()
    {
        SetupCoreScene("Assets/Scenes/TestScene.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");
        if (Menu == null)
        {
            Assert.Fail();
        }

        SpriteRenderer noteScoreSprite = Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Perfect");
        if (noteScoreSprite == null)
        {
            Assert.Fail();
        }

        yield return new WaitForSeconds(2);

        Vector3 spawnPoint = new Vector3(2.45f, 1.87f, -7.77f);
        SpriteRenderer noteScore = Instantiate(noteScoreSprite, spawnPoint, Quaternion.identity);

        float counter = 0;
        float duration = 0.3f;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            Menu.GetComponent<CharacterListener>().moveNoteScore(counter, duration, noteScore, spawnPoint, new Vector3(2.45f, 1.5f, -7.77f));

            if (GameObject.Find("Perfect(Clone)") != null)
            {
                Assert.Pass();
            }

            yield return null;
        }

        Assert.Fail();
        yield return null;
    }

    /* Test 116: This test checks that the song's physical note objects are instantiated in the proper position. */
    [UnityTest]
    public IEnumerator NoteBarNoteSpawnPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject curtains = GameObject.FindGameObjectWithTag("Curtains");
        while (curtains.GetComponent<SpriteRenderer>().enabled != false)
        {
            yield return null;
        }


        int count = 0;
        while (count <= 120)
        {
            if ((GameObject.Find("CircleNote(Clone)") != null) && (GameObject.Find("CircleNote(Clone)").GetComponent<RectTransform>().anchoredPosition == new Vector2(-372f, 134.2F)))
            {
                Assert.Pass();
            }


            count++;
            yield return null;
        }

        Assert.Fail();
    }

    /* Test 117: This test checks the calculation for deciding whether a note was Perfect, Great, Good, or Miss. 
    [UnityTest]
    public IEnumerator NoteBarNoteSpawnTiming()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");

        GameObject curtains = GameObject.FindGameObjectWithTag("Curtains");
        while (curtains.GetComponent<SpriteRenderer>().enabled != false)
        {
            yield return null;
        }

        GameObject note = null;

        int count = 0;
        while (count <= 120 && note == null)
        {
            if (GameObject.Find("CircleNote(Clone)") != null)
            {
                note = GameObject.Find("CircleNote(Clone)");
                break;
            }

            count++;
            yield return null;
        }

        if (note == null)
        {
            Assert.Fail();
        }

        while (note.GetComponent<RectTransform>().anchoredPosition != new Vector2(322.37F, 134.2F))
        {
            yield return null;
        }

        decimal hitTime = ((decimal)AudioSettings.dspTime) * 1000;
        decimal nextTime = (decimal)Menu.GetComponent<GameLogic>().getNextHit() + (1000 * Menu.GetComponent<GameLogic>().getSongStartTime());

        if (hitTime - nextTime <= 25)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }*/

    /* Test 118: This test checks that the song's physical note objects are destroyed at the proper position. */
    [UnityTest]
    public IEnumerator NoteBarNoteDestroyPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject curtains = GameObject.FindGameObjectWithTag("Curtains");
        while (curtains.GetComponent<SpriteRenderer>().enabled != false)
        {
            yield return null;
        }

        GameObject note = null;

        int count = 0;
        while (count <= 360 && note == null)
        {
            if ((GameObject.Find("CircleNote(Clone)") != null) && !GameObject.Find("CircleNote(Clone)").GetComponent<CircleNote>().firstNoteOfSlider)
            {
                note = GameObject.Find("CircleNote(Clone)");
            }

            count++;
            yield return null;
        }

        while (note.GetComponent<RectTransform>().anchoredPosition != new Vector2(322.37F, 134.2F))
        {
            yield return null;
        }

        yield return null;
        yield return null;

        if (note == null)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    // ==== Characters ====

    /* Test 119: This test checks that the character's shield spawns as a result of the user selecting 'DEF' from the menu. */
    [UnityTest]
    public IEnumerator CharacterSpawnShield()
    {
        SetupCoreScene("Assets/Scenes/TestScene.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");
        if (Menu == null)
        {
            Assert.Fail();
        }

        SpriteRenderer shield = Resources.Load<SpriteRenderer>("Prefab/BattleSprites/Shield");
        if (shield == null)
        {
            Assert.Fail();
        }

        yield return null;

        Vector3 spawnPoint = new Vector3(3.38f, 1.56f, -4.5f);

        Menu.GetComponent<CharacterListener>().createShield(spawnPoint);

        yield return null;

        if (GameObject.Find("Shield(Clone)") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    /* Test 120: This test checks that the character's shield plays the correct SFX. */
    [UnityTest]
    public IEnumerator CharacterShieldSFX()
    {
        SetupCoreScene("Assets/Scenes/TestScene.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");
        if (Menu == null)
        {
            Assert.Fail();
        }

        SpriteRenderer shield = Resources.Load<SpriteRenderer>("Prefab/BattleSprites/Shield");
        if (shield == null)
        {
            Assert.Fail();
        }

        yield return null;

        Vector3 spawnPoint = new Vector3(3.38f, 1.56f, -4.5f);
        Menu.GetComponent<CharacterListener>().createShield(spawnPoint);

        if (Menu.GetComponent<CharacterListener>().isPlaying(Resources.Load<AudioClip>("Songs/SFX/shield_low")))
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    /* Test 121: This test checks that the character's attack animation plays the correct SFX. */
    [UnityTest]
    public IEnumerator CharacterAttackSFX()
    {
        SetupCoreScene("Assets/Scenes/TestScene.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");
        if (Menu == null)
        {
            Assert.Fail();
        }

        yield return null;

        Menu.GetComponent<CharacterListener>().characterAttackMovement();

        if (Menu.GetComponent<CharacterListener>().isPlaying(Resources.Load<AudioClip>("Songs/SFX/atk_sfx")))
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    // ==== Menu ====

        // N/A

    // ==== Boss ====

        // N/A

    // ==== Scene ====

    /* Test 122: This test checks that the curtain's opening animation plays correctly with proper timing. */
    [UnityTest]
    public IEnumerator SceneCurtainOpeningAnimation()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject curtains = GameObject.FindGameObjectWithTag("Curtains");
        while (curtains != null)
        {
            if (curtains.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "CurtainsOpen")
            {
                Assert.Pass();
            }

            yield return null;
        }

        Assert.Fail();
    }

    /* Test 123: This test checks that the spotlights are destroyed upon the curtain's opening animation playing. */
    [UnityTest]
    public IEnumerator SceneSpotlightDestroy()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject curtains = GameObject.FindGameObjectWithTag("Curtains");
        GameObject[] spotlights = GameObject.FindGameObjectsWithTag("Spotlight");

        while (curtains != null)
        {
            if (spotlights[0] == null && spotlights[1] == null)
            {
                Assert.Pass();
            }

            yield return null;
        }

        Assert.Fail();
    }

    /* Test 124: This test checks that the curtains renderer is disabled upon the curtain's opening animation finishing. */
    [UnityTest]
    public IEnumerator SceneCurtainDisableAfterAnimation()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject curtains = GameObject.FindGameObjectWithTag("Curtains");
        while (curtains.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name == "CurtainsIdle")
        {
            yield return null;
        }

        while (curtains.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.length > 
                curtains.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            yield return null;
        }

        /* This wait value corresponds to the 'yield return WaitForSeconds()' command in CurtainMovement. */
        yield return new WaitForSeconds(3);

        if (curtains.GetComponent<SpriteRenderer>().enabled == false)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    /* Test 125: This test checks that the scene plays the correct applause SFX upon loading in. */
    [UnityTest]
    public IEnumerator SceneCurtainApplauseSFX()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject curtains = GameObject.FindGameObjectWithTag("Curtains");
        if (curtains.GetComponent<AudioSource>().clip == Resources.Load("Songs/SFX/Applause"))
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    /* Test 126: This test checks that the scene plays the correct stage music upon the curtains disabling. */
    [UnityTest]
    public IEnumerator SceneStageSongPlays()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");
        GameObject curtains = GameObject.FindGameObjectWithTag("Curtains");

        while (curtains.GetComponent<SpriteRenderer>().enabled != false)
        {
            yield return null;
        }

        yield return null;

        if (Menu.GetComponent<AudioSource>().clip == Resources.Load("Songs/ALiVE"))
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    /* Test 127: This test checks that the user's currency increases upon completing a level. */ 
    [UnityTest]
    public IEnumerator SceneGainCurrency()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");

        GlobalLogic logic = new GlobalLogic();
        GlobalUser user = logic.getGlobalUser();

        int currentCash = user.Cash;

        int count = 0; /* Set the song as finished after 5 seconds, to shorten wait time for the test. */
        while (Menu.GetComponent<GameLogic>().isSongDone() == false && count < 300)
        {
            count++;
            yield return null;
        }

        yield return null;

        if (user.Cash > currentCash)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }

    /* Test 128: This test checks that the character's XP value increases upon completing a level. */
    [UnityTest]
    public IEnumerator SceneEXPGain()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        yield return null;

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");

        GlobalLogic logic = new GlobalLogic();
        GlobalUser user = logic.getGlobalUser();

        int currentXP = user.XP;

        int count = 0; /* Set the song as finished after 5 seconds, to shorten wait time for the test. */
        while (Menu.GetComponent<GameLogic>().isSongDone() == false && count < 300)
        {
            count++;
            yield return null;
        }

        yield return null;

        if (user.XP > currentXP)
        {
            Assert.Pass();
        }

        Assert.Fail();
    }
}
