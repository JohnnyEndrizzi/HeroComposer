using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoreGameplayPlayMode : MonoBehaviour
{
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
            //Menu.GetComponent<CharacterListener>().moveNoteScore(counter, duration, noteScore, spawnPoint, new Vector3(2.45f, 1.5f, -7.77f));

            if (GameObject.Find("Perfect(Clone)") != null)
            {
                Debug.Log("Test");
                Assert.Pass();
            }

            yield return null;
        }

        Assert.Fail();
        yield return null;
    }

    void SetupCoreScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
