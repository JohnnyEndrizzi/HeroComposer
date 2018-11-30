using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DeveloperCommentary : MonoBehaviour {

    GameObject player;
    VideoPlayer vp;

    public GameObject go1;
    public GameObject go2;
    public GameObject go3;
    public GameObject go4;
    public GameObject go5;
    public GameObject go6;

	void Start () {
        vp = this.GetComponent<VideoPlayer>();
	}
	

	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            vp.Pause();
            vp.enabled = false;
            go1.SetActive(true);
            go2.SetActive(true);
            go3.SetActive(true);
            go4.SetActive(true);
            go5.SetActive(true);
            go6.SetActive(true);
        }
	}

    public void PlayPause(){
        if(vp.isPlaying){
            vp.Pause();
            vp.enabled = false;
            go1.SetActive(true);
            go2.SetActive(true);
            go3.SetActive(true);
            go4.SetActive(true);
            go5.SetActive(true);
            go6.SetActive(true);
        }
        else{
            vp.enabled = true; 
            vp.Play();
            go1.SetActive(false);
            go2.SetActive(false);
            go3.SetActive(false);
            go4.SetActive(false);
            go5.SetActive(false);
        } 
    }
}