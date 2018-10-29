using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;
public class NewBehaviourScript : MonoBehaviour
{

    //public GameObject PlaySprite;  ///set this in the inspector
	private string spriteNames = "play_door";
	private int spriteVersion = 0;
	//private SpriteRenderer spriteR;
	private Sprite[] sprites;
	public Animator closeddoor;
	private int doorstatus;
	public Canvas selectcanvas;
	public Button play;


    void Start()
    {
		//spriteR = gameObject.GetComponent<SpriteRenderer>();
		//sprites = Resources.LoadAll<Sprite>(spriteNames);
		//PlaySprite.GetComponents<PolygonCollider2D> ();
		selectcanvas.enabled = false;

    }


    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown (0) && doorstatus == 1 ) {
			
			if(!selectcanvas.enabled){
				Debug.Log ("Goes to selection menu");
				//SceneManager.LoadScene ("selection", LoadSceneMode.Single);
				selectcanvas.enabled = true;
                play.onClick.AddListener(PlaybuttonOnClick);
			}
			else {
				Debug.Log("canvas is enabled");
			}
		}


			
		//if (Input.GetMouseButtonDown(1))
		//	Debug.Log("Pressed secondary button.");

		//if (Input.GetMouseButtonDown(2))
		//	Debug.Log("Pressed middle click.");
    }

	void OnMouseOver()
	{	
		if (selectcanvas.enabled == false) {
			doorstatus = 1;
			closeddoor.Play ("door_open");

		}
		//If your mouse hovers over the GameObject with the script attached, output this message
		//Debug.Log("Mouse is over door. status: " + doorstatus);
	}

	void OnMouseExit()
	{	doorstatus = 0;
		closeddoor.Play ("door_animation");
		//The mouse is no longer hovering over the GameObject so output this message each frame
		//Debug.Log("Mouse is no longer on door. status: " + doorstatus);
	}

	void PlaybuttonOnClick()
	{
		//Output this to console when Button1 or Button3 is clicked
		Debug.Log("You have clicked the button!");
        SceneManager.LoadScene("Scenes/main", LoadSceneMode.Single);
    }
}

