using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainCanvas : MonoBehaviour {
    //Singleton instance
    public static CurtainCanvas Instance { get; private set; }

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
        }else{
            Destroy(this.gameObject);
        }
    }
}
