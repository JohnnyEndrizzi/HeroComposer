using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    private string origin;

    public void sceneSwitch(string sceneNew){
        if(sceneNew.Equals("Back")){
            origin = LastScene.instance.prevScene;
            LastScene.instance.prevScene = SceneManager.GetActiveScene().name;


            SceneManager.LoadScene(origin,LoadSceneMode.Single);
        }else{
            LastScene.instance.prevScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneNew, LoadSceneMode.Single);
        }
    }
    public void sceneSwitchHidden(string sceneNew){
       SceneManager.LoadScene(sceneNew, LoadSceneMode.Single);
    }

    public void sceneSwitchCurtains(string sceneNew){
        if(sceneNew.Equals("Back")){
            origin = LastScene.instance.prevScene;
        }
            LastScene.instance.prevScene = SceneManager.GetActiveScene().name;
            GameObject.Find("CurtainsOpenTransition").GetComponent<CurtainMovementQuick>().closeCurtains(origin);   
    }
    public void sceneSwitchHiddenCurtains(string sceneNew){        
        GameObject.Find("CurtainsOpenTransition").GetComponent<CurtainMovementQuick>().closeCurtains(sceneNew);   
    }
}