using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class AssetController : MonoBehaviour {

    /// <summary>
    /// Unfinished, unused code, DO NOT USE
    /// </summary>

    public Sprite[] img;
    public string[] titles;
    public string[] descriptions;

    public Transform showSprite;

    GameObject canvas;
    GameObject textTitle;
    GameObject imageMusic;

    Canvas myCanvas;
    Text text;
    RectTransform rectTransform;
    Image image;
    Font font;

	// Use this for initialization
	void Start () {
        //Create Canvas
        canvas = new GameObject();
        canvas.transform.parent = this.transform;
        canvas.name = "canvas";
        canvas.AddComponent<Canvas>();

        myCanvas = canvas.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        textTitle = new GameObject();


        writer();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void writer(){
        // Text Title       
        textTitle.transform.parent = canvas.transform;
        textTitle.name = "Title";

        text = textTitle.AddComponent<Text>();
        text.font = font;
        text.text = "Art";
        text.fontSize = 100;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 150, 1);
        rectTransform.sizeDelta = new Vector2(800, 200);

        ////
        // Image 
        imageMusic = new GameObject();
        imageMusic.transform.parent = canvas.transform;
        imageMusic.name = "img1";

        image = imageMusic.AddComponent<Image>();
        image.sprite = img[0];
        rectTransform = image.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(200, 200);
    }
}
