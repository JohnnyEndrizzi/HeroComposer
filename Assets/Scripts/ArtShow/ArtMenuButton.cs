using UnityEngine;

public class ArtMenuButton : MonoBehaviour {

    public GameObject BG;
    public GameObject Back;
    public GameObject R2Menu;

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){toggle();} 
	}

    void toggle()
    {
        if (R2Menu.activeInHierarchy == true) {
            R2Menu.SetActive(false);
            Back.SetActive(false);
            BG.SetActive(false);
        }
        else {
            R2Menu.SetActive(true);
            Back.SetActive(true);
            BG.SetActive(true);
        }
    }
}