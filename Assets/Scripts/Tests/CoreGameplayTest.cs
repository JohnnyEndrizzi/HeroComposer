using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;

public class CoreGameplayTest : MonoBehaviour
{
    [UnityTest]
    public IEnumerator NoteBarExists()
    {
        SetupCoreScene();

        if (GameObject.FindGameObjectsWithTag("NoteBar") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator NoteBarPosition()
    {
        SetupCoreScene();

        if (GameObject.FindGameObjectWithTag("NoteBar").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator NoteBarHighlight()
    {
        SetupCoreScene();

        CharacterListener test = new CharacterListener();
        test.ChangeNoteBarHighlight(Color.white);

        if (GameObject.Find("Note_Bar_Circle").GetComponent<SpriteRenderer>().color == Color.white)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator NoteAccuracyTextExists() // Doesn't work yet
    {
        SetupCoreScene();

        CharacterListener test = new CharacterListener();

        SpriteRenderer noteScoreSprite = Resources.Load("Assets/Prefabs/NoteMessage/Perfect.prefab") as SpriteRenderer;

        StartCoroutine(test.spawnNoteScore(new Vector3(2.45f, 1.87f, -7.77f), 5.3f, noteScoreSprite));

        if (GameObject.Find("Perfect") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    // Yifu code



    // Pavle code



    void SetupCoreScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/main.unity");
    }
}
