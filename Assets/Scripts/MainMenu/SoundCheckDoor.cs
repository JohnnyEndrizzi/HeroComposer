using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundCheckDoor : MonoBehaviour {

    public Animator soundcheckclose;

    void OnClick()
    {
        string tempName = this.name.Replace("DoorClose", "");

        if (!tempName.Equals("Play"))
        {
            //TODO close curtains here?
            //MenuSceneSwitch(this.name.Replace("DoorClose",""));
            MenuSceneSwitch(tempName);
        }
    }

    void OnMouseOver(){
        soundcheckclose.Play("SoundCheckOpen");
    }

    void OnMouseExit(){
        soundcheckclose.Play("SoundCheckClose");
    }

    public void MenuSceneSwitch(string sceneNew){
        LastScene.instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneNew, LoadSceneMode.Single);
    }
}
