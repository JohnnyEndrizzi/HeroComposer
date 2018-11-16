using UnityEngine;
using System.Linq;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;

public class CoreGameplayTest : MonoBehaviour
{

    // ==== Note Bar ====

    [UnityTest]
    public IEnumerator NoteBarExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

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
        SetupCoreScene("Assets/Scenes/main.unity");

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
        SetupCoreScene("Assets/Scenes/main.unity");

        CharacterListener test = new CharacterListener();
        test.ChangeNoteBarHighlight(Color.white);

        if (GameObject.Find("Note_Bar_Circle").GetComponent<SpriteRenderer>().color == Color.white)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    // ==== Characters ====

    [UnityTest]
    public IEnumerator CharactersExist()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");

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

    [UnityTest]
    public IEnumerator CharacterPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        Vector3[] vectors = new [] {new Vector3(3.38f, 1.56f, -4.5f),
                                    new Vector3(1.43f, 1.19f, -4.8f),
                                    new Vector3(1.43f, -0.35f, -5.1f),
                                    new Vector3(2.82f, -0.67f, -5.5f)};

        int correct = 0;
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

        if (correct == characters.Length)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator NoteAccuracyTextExists() // Doesn't work yet
    {
        SetupCoreScene("Assets/Scenes/main.unity");
    
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

    // ==== Menu ====

    [UnityTest]
    public IEnumerator MenuExist()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectsWithTag("Menu") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator MenuPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectWithTag("Menu").transform.position == new Vector3(5.4f, 0.52f, -5f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator MenuHasCorrectStateAndSpriteUponChange()
    {
        SetupCoreScene("Assets/Scenes/TestScene.unity");

        GameObject Menu = GameObject.FindGameObjectWithTag("Menu");
        if (Menu == null)
        {
            Assert.Fail();
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Menu/TempMenu");
        if (sprites == null)
        {
            Assert.Fail();
        }

        string[] allStates = {"ATK", "DEF", "MGC", "ULT"};
        for (int i = 0; i < allStates.Length; i++)
        {
            Sprite TextSprite = null;
            for (int j = 0; j < sprites.Length; j++)
            {
                if (sprites[j].name == allStates[i])
                {
                    TextSprite = sprites[j];
                }
            }

            if (TextSprite == null)
            {
                Assert.Fail();
            }

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

            Menu.GetComponent<ClickListener>().ChangeMenuState(TextSprite, testState);

            if ((Menu.GetComponent<SpriteRenderer>().sprite != TextSprite) || (testState != Menu.GetComponent<ClickListener>().GetMenuState()))
            {
                Assert.Fail();
            }
        }
        Assert.Pass();
        yield return null;
    }

    // ==== Boss ====

    // ==== Scene ====

    // Yifu code



    // Pavle code

    void SetupCoreScene(string s)
    {
        EditorSceneManager.OpenScene(s);
    }
}
