using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour {
    //Singleton instance
    public static UIContainer Instance { get; private set; }

    //Called before start
    void Awake()
    {
        // Check if instance already exists
        if (Instance == null)
        {
            //Save singleton instance
            Instance = this;
            //Dont destroy between scenes
            DontDestroyOnLoad(gameObject);
            //Destroy this instance
        }
        //Add this UI's elements to already existing UI container
        else
        {
            Transform newParent = Instance.transform;
            Transform oldParent = this.transform;
            while (oldParent.childCount > 0)
            {
                oldParent.GetChild(oldParent.childCount - 1).SetParent(newParent, false);
            }
            Destroy(this.gameObject);
        }
    }
}
