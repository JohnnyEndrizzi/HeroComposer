using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DDOL : MonoBehaviour {

	// Use this for initialization
	public void Awake ()
    {
        DontDestroyOnLoad(gameObject);	
	}

    public void Start()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
