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

    /* This test checks that the note bar exists within the scene. */
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

    /* This test checks that the note bar is instantiated in the proper position. */
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

    /* This test checks that the note bar will light up as a response to the user holding down an action button (U, I, O or P). */
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

    /* This test checks that the accuracy text for inputs is caclualted correctly. */
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

    /* This test checks that the character(s) exist within the scene. */
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

    /* This test checks that the character(s) is/are instantiated in the proper position. */
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

    // ==== Menu ====

    /* This test checks that the game menu exists within the scene. */
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

    /* This test checks that the game menu is instantiated in the proper position. */
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

    /* This test checks that a menu's state change will appropriately change the menu's sprite. */
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
    
    /* This test checks that the boss exists within the scene. */
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

    /* This test checks that the boss is instantiated in the proper position. */
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

    /* This test checks that the boss' health bar exists within the scene. */
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

    /* This test checks that the boss's health bar is instantiated in the proper position. */
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

    // ==== Scene ====

    /* This test checks that the curtains exist within the scene. */
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

    /* This test checks that the curtains are instantiated in the proper position. */
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

    /* This test checks that the stage exists within the scene. */
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

    /* This test checks that the stage is instantiated in the proper position. */
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

    /* This test checks that the spotlights exist within the scene. */
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

    /* This test checks that the spotlights are instantiated in the proper position. */
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

    /* This test checks that the background exists within the scene. */
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

    /* This test checks that the background is instantiated in the proper position. */
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

    // Yifu code



    // Pavle code

}
