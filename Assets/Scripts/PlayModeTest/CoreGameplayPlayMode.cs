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

    // ==== Characters ====

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

    // ==== Boss ====

    // ==== Scene ====

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

}
