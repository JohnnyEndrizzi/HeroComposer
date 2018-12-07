using UnityEngine;

public class fake : MonoBehaviour {
    //Quality of life script to disable object on load (for objects not intended to be seen in game but used for positioning other objects in editor)
    void Start ()
    {
        this.gameObject.SetActive(false);
	}
}