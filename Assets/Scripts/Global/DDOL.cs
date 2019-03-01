using UnityEngine;

public class DDOL : MonoBehaviour {

	// Use this for initialization
	public void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);	
	}
}
