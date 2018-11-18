using UnityEngine;
using System.Linq;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEditor.SceneManagement;

public class CoreGameplayTest : MonoBehaviour
{
    void SetupCoreScene(string s)
    {
        EditorSceneManager.OpenScene(s);
    }

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
        
        if (listener.GetNoteAccuracySprite(100, 100, 25) != Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Perfect"))
        {
            Assert.Fail();
        }

        if (listener.GetNoteAccuracySprite(100, 100, 50) != Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Great"))
        {
            Assert.Fail();
        }

        if (listener.GetNoteAccuracySprite(100, 100, 150) != Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Good"))
        {
            Assert.Fail();
        }

        if (listener.GetNoteAccuracySprite(100, 100, 250) != Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Miss"))
        {
            Assert.Fail();
        }

        Assert.Pass();
        yield return null;
    }

    // ==== Characters ====

    [UnityTest]
    public IEnumerator CharactersExist()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        int numberOfCharacters = 4;

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
        SetupCoreScene("Assets/Scenes/main.unity");

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

    [UnityTest]
    public IEnumerator BossExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectsWithTag("Boss") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BossPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectWithTag("Boss").transform.position == new Vector3(-3.5f, 1.16f, -5.5f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }
    
    // ==== Scene ====

    [UnityTest]
    public IEnumerator SceneCurtainExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectsWithTag("Curtains") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator SceneCurtainPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectWithTag("Curtains").transform.position == new Vector3(0.0f, 1.43f, -7.95f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator SceneStageExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectsWithTag("Stage") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator SceneStagePosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectWithTag("Stage").transform.position == new Vector3(0.0f, 7.9f, -2.0f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator SceneSpotlightExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectsWithTag("Spotlight").Length == 2)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator SceneSpotlightPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        GameObject[] spotlights = GameObject.FindGameObjectsWithTag("Spotlight");

        for (int i = 0; i < spotlights.Length; i++)
        {
            if (spotlights[i].transform.localPosition != new Vector3(-0.72f, 1.25f, -9.0f))
            {
                Assert.Fail();
            }
        }

        Assert.Pass();
        yield return null;
    }

    [UnityTest]
    public IEnumerator SceneBackgroundExists()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectsWithTag("Background") != null)
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator SceneBackgroundPosition()
    {
        SetupCoreScene("Assets/Scenes/main.unity");

        if (GameObject.FindGameObjectWithTag("Background").transform.localPosition == new Vector3(21.75f, 4.03f, 6.0f))
        {
            Assert.Pass();
        }

        Assert.Fail();
        yield return null;
    }

    // Yifu code



    // Pavle code

}
