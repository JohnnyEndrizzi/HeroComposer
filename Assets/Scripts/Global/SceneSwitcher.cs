using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    //Controls scene switching buttons - saves the last scene unless noted

    private string origin; //temporary scene location

    public void sceneSwitch(string sceneNew) //Swich scene
    {
        if(sceneNew.Equals("Back")){
            origin = LastScene.instance.prevScene;            
        }
        else
        {
            origin = sceneNew;
        }

        LastScene.instance.prevScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(origin, LoadSceneMode.Single);
    }

    public void sceneSwitchHidden(string sceneNew) //switch scene w/o updating lastScene
    {
       SceneManager.LoadScene(sceneNew, LoadSceneMode.Single);
    }

    public void sceneSwitchCurtains(string sceneNew) //switch scene with curtains
    {
        if(sceneNew.Equals("Back")){        
            origin = LastScene.instance.prevScene;
        }
        else
        {
            origin = sceneNew;
        }
        LastScene.instance.prevScene = SceneManager.GetActiveScene().name;
        GameObject.Find("CurtainsOpenTransition").GetComponent<CurtainMovementQuick>().CloseCurtains(origin);   
    }
    public void sceneSwitchHiddenCurtains(string sceneNew) //switch scene with curtains w/o updating lastScene
    {        
        GameObject.Find("CurtainsOpenTransition").GetComponent<CurtainMovementQuick>().CloseCurtains(sceneNew);   
    }
}