using DummyFiles;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TestTools;

public class PavleTest : MonoBehaviour {

    [UnityTest]
    public IEnumerator Instrument1Exists()
    {
        SetupCoreScene();
        if (GameObject.FindGameObjectsWithTag("Instrument1") != null)
        {
            Assert.Pass();
        }
        if (GameObject.FindGameObjectWithTag("Instrument1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Instrument2Exists()
    {
        SetupCoreScene();
        if (GameObject.FindGameObjectsWithTag("Instrument2") != null)
        {
            Assert.Pass();
        }
        if (GameObject.FindGameObjectWithTag("Instrument2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Defence1Exists()
    {
        SetupCoreScene();
        if (GameObject.FindGameObjectsWithTag("Defence1") != null)
        {
            Assert.Pass();
        }
        if (GameObject.FindGameObjectWithTag("Defence1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator Defence2Exists()
    {
        SetupCoreScene();
        if (GameObject.FindGameObjectsWithTag("Defence2") != null)
        {
            Assert.Pass();
        }
        if (GameObject.FindGameObjectWithTag("Defence2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator PowerUp1Exists()
    {
        SetupCoreScene();
        if (GameObject.FindGameObjectsWithTag("PowerUp1") != null)
        {
            Assert.Pass();
        }
        if (GameObject.FindGameObjectWithTag("PowerUp1").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }
    [UnityTest]
    public IEnumerator PowerUp2Exists()
    {
        SetupCoreScene();
        if (GameObject.FindGameObjectsWithTag("PowerUp2") != null)
        {
            Assert.Pass();
        }
        if (GameObject.FindGameObjectWithTag("PowerUp2").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator CurrentCashExists()
    {
        SetupCoreScene();
        if (GameObject.FindGameObjectsWithTag("CurrentCash") != null)
        {
            Assert.Pass();
        }
        if (GameObject.FindGameObjectWithTag("CurrentCash").transform.position == new Vector3(-0.47f, 2.7f, -5.97f))
        {
            Assert.Pass();
        }
        Assert.Fail();
        yield return null;
    }

    [UnityTest]
    public IEnumerator BuyItemSuccess()
    { 
        SetupCoreScene();

        GlobalUser user = new GlobalUser();
        StoreLogic.BuyItem(GameItems.Instrument1);

        if (GameObject.FindGameObjectsWithTag("ItemBought") == null)
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
        SetupCoreScene();

        GlobalUser user = new GlobalUser();
        StoreLogic.BuyItem(GameItems.Instrument1);

        if (GameObject.FindGameObjectsWithTag("FailedToBuy") == null)
        {
            Assert.Fail();
        }

        List<GameItems> test = user.GetItemList();
        Assert.That(test.Count == 0);
        
        yield return null;
    }
    void SetupCoreScene()
    {
        EditorSceneManager.OpenScene("Assets/Scenes/shop.unity");
    }
}
