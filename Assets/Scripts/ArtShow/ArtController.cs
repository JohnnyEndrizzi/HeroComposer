using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ArtController : MonoBehaviour {

    /// <summary>
    /// Old code, DO NOT USE
    /// </summary>


    //public Texture2D[] img;
    public Sprite[] img;
    public Transform showSprite;

    float travelTime = 1;
    float currentTime = 0;
    float perc;
    RectTransform rectTransform;

	// Use this for initialization
	void Start () {
        GameObject canvas;
        GameObject textTitle;
        GameObject textDesc;
        GameObject image1;
  
        Font font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        Canvas myCanvas;
        Text text;
        Image image;







        // Canvas
        canvas = new GameObject();
        canvas.name = "canvas";
        canvas.AddComponent<Canvas>();

        myCanvas = canvas.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        // Text Title
        textTitle = new GameObject();
        textTitle.transform.parent = canvas.transform;
        textTitle.name = "Title";

        text = textTitle.AddComponent<Text>();
        text.font = font;
        text.text = "Art";
        text.fontSize = 100;
        text.alignment = TextAnchor.MiddleCenter;
            
        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 150, 1);
        rectTransform.sizeDelta = new Vector2(400, 200);

        ////
        // Text Description 
        textDesc = new GameObject();
        textDesc.transform.parent = canvas.transform;
        textDesc.name = "Desc";

        text = textDesc.AddComponent<Text>();
        text.font = font;
        text.text = "This is art of a character blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah";
        text.fontSize = 20;
        text.alignment = TextAnchor.MiddleCenter;

        // Text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, -175, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);

        ////
        // Image 1 
        image1 = new GameObject();
        image1.transform.parent = canvas.transform;
        image1.name = "img1";

        image = image1.AddComponent<Image>();
        //image.sprite = Sprite.Create(img[0], new Rect(5, 5, 128, 128), new Vector2());
        image.sprite = img[0];
        rectTransform = image.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 0, 0);
        rectTransform.sizeDelta = new Vector2(200, 200);
            
      
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.LeftArrow)){ShiftLeft();}
        if(Input.GetKeyDown(KeyCode.RightArrow)){ShiftRight();}
	}

    void ShiftLeft(){
    
    }

    void ShiftRight(){


    }

    IEnumerator LerpObject(){

        while (currentTime <= travelTime) {
            currentTime += Time.deltaTime;
            perc = currentTime / travelTime;

            //rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, perc);
            yield return null;
        }
    }

}
