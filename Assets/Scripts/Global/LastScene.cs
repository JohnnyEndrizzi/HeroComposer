using UnityEngine;

public class LastScene : MonoBehaviour {
    //Persistant script to save the last loaded scene for use in back buttons

    public static LastScene instance = null;
    public string prevScene = null;

    //Persist and prevent multiple instances
    void Awake ()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
	}
}