using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

public class CoreGameplayPlayMode
{ 
    void SetupCoreScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
