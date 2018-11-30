﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class ArtController2 : MonoBehaviour
{

    //Images
    public Sprite[] img;
    public string[] titles;
    public string[] descriptions;
    public AudioClip[] music;

    public Sprite BG;
    public bool anims = false;

    //Image Configs
    public int dist = 50; //Radius of circle
    public int viewDist = 20; //Dist b/n edge of circle and camera

    public Transform showSprite; //Empty SpriteRender object
    private int numImages;
    Vector3 centrePoint;
    private Vector3[] posSave = new Vector3[4];

    //Movement
    public float artLerpTime = 0.5f;
    float currentLerpTime = 0;

    private int moveOrder = 0;
    private int moveCmd = 0;
    private int currLoc = 0;

    GameObject attacker;
    private int upCount = 0;
    private int dbCount = 0;

    //Text
    GameObject canvas;
    GameObject textTitle;
    GameObject textDesc;
    GameObject titleBG;
    GameObject descBG;

    int titleFontSize = 80;
    int descFontSize = 20;

    Canvas myCanvas;
    Text text;
    RectTransform rectTransform;
    Font myFont;
    Image image;

    void Start()
    {
        numImages = img.Length;
        //titles = new string[numImages];
        //descriptions = new string[numImages];

        attacker = GameObject.Find("AttackAnimController");
        centrePoint = new Vector3(0, 16, dist + viewDist);

        //Create all art
        for (int i = 0; i < numImages; i++)
        {
            Transform artPiece = Instantiate(showSprite, Degree(i), Quaternion.identity);
            artPiece.transform.parent = this.transform;
            artPiece.name = "artPiece " + i.ToString();
            artPiece.GetComponent<SpriteRenderer>().sprite = img[i];
            artPiece.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }


        for (int i = 0; i < numImages; i++)
        {
            Transform artPiece = Instantiate(showSprite, Degree(i), Quaternion.identity);
            artPiece.transform.parent = this.transform;
            artPiece.name = "artPieceBG " + i.ToString();
            artPiece.GetComponent<SpriteRenderer>().sprite = BG;
            artPiece.GetComponent<SpriteRenderer>().sortingOrder = 1;
            artPiece.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.9f, 0.7f);
        }


        //Create Canvas
        canvas = new GameObject();
        canvas.transform.parent = this.transform;
        canvas.name = "canvas";
        canvas.AddComponent<Canvas>();

        myCanvas = canvas.GetComponent<Canvas>();
        myCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.AddComponent<CanvasScaler>();
        canvas.AddComponent<GraphicRaycaster>();

        myFont = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        //Create writing
        textTitle = new GameObject();
        textDesc = new GameObject();

        writer();
        reWriter(titles[0], descriptions[0]);

        //For text Lerping - can modify vector3 values
        posSave[0] = textTitle.transform.localPosition;
        posSave[1] = textDesc.transform.localPosition;
        posSave[2] = posSave[0] + new Vector3(0, 1200, 0);
        posSave[3] = posSave[1] + new Vector3(0, -1200, 0);
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) { moveOrder += 1; }
        if (Input.GetKeyDown(KeyCode.RightArrow)) { moveOrder += -1; }

        if (Sign(moveOrder) != 0) { moveCmd = moveOrder; } //to prevent move getting stuck mid lerp

        //movement
        if (moveCmd != 0)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > artLerpTime)
            {
                currentLerpTime = artLerpTime;
            }

            float perc = currentLerpTime / artLerpTime;

            for (int i = 0; i < numImages; i++)
            { //Lerp Sprites
                GameObject.Find("artPiece " + i).transform.position = Vector3.Lerp(Degree(spin(i + currLoc)), Degree(spin(i + currLoc + Sign(moveCmd))), perc);
                GameObject.Find("artPieceBG " + i).transform.position = Vector3.Lerp(Degree(spin(i + currLoc)), Degree(spin(i + currLoc + Sign(moveCmd))), perc);
            }

            if (perc <= 0.5)
            { //Lerp words
                textTitle.transform.localPosition = Vector3.Lerp(posSave[0], posSave[2], perc);
                textDesc.transform.localPosition = Vector3.Lerp(posSave[1], posSave[3], perc);

                titleBG.transform.localPosition = Vector3.Lerp(posSave[0], posSave[2], perc);
                descBG.transform.localPosition = Vector3.Lerp(posSave[1], posSave[3], perc);
            }
            else
            {
                textTitle.transform.localPosition = Vector3.Lerp(posSave[2], posSave[0], perc);
                textDesc.transform.localPosition = Vector3.Lerp(posSave[3], posSave[1], perc);

                titleBG.transform.localPosition = Vector3.Lerp(posSave[2], posSave[0], perc);
                descBG.transform.localPosition = Vector3.Lerp(posSave[3], posSave[1], perc);
            }

            if (perc >= 1)
            {
                currLoc += Sign(moveCmd);
                moveOrder += -Sign(moveCmd);
                moveCmd += -Sign(moveCmd);
                currentLerpTime = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            { //Audio
                AudioSource audio = GameObject.Find("Canvas").GetComponent<AudioSource>();
                if (audio.isPlaying && audio.clip == music[spin(currLoc * -1)])
                {
                    audio.Stop();
                }
                else
                {
                    audio.clip = music[spin(currLoc * -1)];
                    audio.Play();
                }
            }

            if (anims == true && upCount >= 20 && upCount <= 40)
            {
                if (spin(currLoc * -1) == 8) attacker.GetComponent<AttackAnimator>().ATTACKdemo("blizzard", 1, 1);
            }
            if (anims == true && upCount >= 40 && upCount <= 60)
            {
                if (spin(currLoc * -1) == 8) attacker.GetComponent<AttackAnimator>().ATTACKdemo("blizzard", 1, 1);
            }
            if (anims == true && upCount >= 60 && upCount <= 80)
            {
                if (spin(currLoc * -1) == 8) attacker.GetComponent<AttackAnimator>().ATTACKdemo("blizzard", 1, 1);
            }
            if (anims == true && upCount >= 80)
            {
                upCount = 0;

                switch (spin(currLoc * -1))
                {
                    case 0:
                        attacker.GetComponent<AttackAnimator>().ATTACKdemo("heal", 1, 1);
                        break;
                    case 1:
                        if (dbCount == 0)
                        {
                            attacker.GetComponent<AttackAnimator>().ATTACKdemo("slash1", 1, 1);
                            dbCount = 1;
                        }
                        else
                        {
                            attacker.GetComponent<AttackAnimator>().ATTACKdemo("slash2", 1, 1);
                            dbCount = 0;
                        }
                        break;
                    case 2:
                        attacker.GetComponent<AttackAnimator>().ATTACKdemo("fireball", 1, 1);
                        break;
                    case 3:
                        attacker.GetComponent<AttackAnimator>().ATTACKdemo("arrow", 1, 1);
                        break;
                    case 4:
                        attacker.GetComponent<AttackAnimator>().ATTACKdemo("healTeam", 1, 1);
                        break;
                    case 5:
                        attacker.GetComponent<AttackAnimator>().ATTACKdemo("meteor", 1, 1);
                        break;
                    case 6:
                        attacker.GetComponent<AttackAnimator>().ATTACKdemo("arrowHail", 1, 1);
                        break;
                    case 7:
                        attacker.GetComponent<AttackAnimator>().ATTACKdemo("fire3", 1, 1);
                        break;
                    case 8:
                        attacker.GetComponent<AttackAnimator>().ATTACKdemo("blizzard", 1, 1);
                        break;
                    default:
                        break;
                }
            }
        }

        reWriter(titles[spin(currLoc * -1)], descriptions[spin(currLoc * -1)]); //TODO move between text Lerps to change at top
        upCount++;
    }

    void fillWords()
    { //Load text here
        for (int i = 0; i < numImages; i++)
        {
            titles[i] = "Art Piece #" + i.ToString();
            descriptions[i] = "this amazing art piece is art number " + i.ToString() + " and is about stuff.";
        }
    }

    void writer()
    { //Initial Write / create text locations

        // Image 1 
        titleBG = new GameObject();
        titleBG.transform.parent = canvas.transform;
        titleBG.name = "titleBG";

        image = titleBG.AddComponent<Image>();
        image.color = new Color(0.8f, 0.8f, 0.9f, 0.7f);
        rectTransform = image.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 150, 1);
        rectTransform.sizeDelta = new Vector2(420, 100);

        // Image 2
        descBG = new GameObject();
        descBG.transform.parent = canvas.transform;
        descBG.name = "titleBG";

        image = descBG.AddComponent<Image>();
        image.color = new Color(0.8f, 0.8f, 0.9f, 0.7f);
        rectTransform = image.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, -175, 0);
        rectTransform.sizeDelta = new Vector2(300, 100);

        // Text Title       
        textTitle.transform.parent = canvas.transform;
        textTitle.name = "Title";

        text = textTitle.AddComponent<Text>();
        text.font = myFont;
        text.text = "Art";
        text.fontSize = titleFontSize;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;

        // Title text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, 150, 1);
        rectTransform.sizeDelta = new Vector2(1000, 200);

        ////
        // Text Description
        textDesc.transform.parent = canvas.transform;
        textDesc.name = "Desc";

        text = textDesc.AddComponent<Text>();
        text.font = myFont;
        //text.text = "This is art of a character blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah";
        text.fontSize = descFontSize;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;

        // Description text position
        rectTransform = text.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(0, -175, 0);
        rectTransform.sizeDelta = new Vector2(400, 200);
    }

    void reWriter(string title, string desc)
    { //Update Text
        textTitle.GetComponent<Text>().text = title;
        textDesc.GetComponent<Text>().text = desc;

        image = titleBG.GetComponent<Image>();
        rectTransform = image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(stringLength(title, titleFontSize) + 10, 100);

        image = descBG.GetComponent<Image>();
        rectTransform = image.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(stringLength(desc, descFontSize) + 10, 100);
    }

    int stringLength(string s, int size)
    {
        int totalLength = 0;
        CharacterInfo characterInfo = new CharacterInfo();

        char[] chars = s.ToCharArray();

        foreach (char c in chars)
        {
            myFont.GetCharacterInfo(c, out characterInfo, size);

            totalLength += characterInfo.advance;
        }
        return totalLength;
    }

    int spin(int i)
    { //loops numbers to correct values
        if (i < 0) { i = spin(i + numImages); }
        if (i >= numImages) { i = spin(i - numImages); }
        return i;
    }

    Vector3 Degree(int i)
    { //Calculate object location around circle
        Vector3 spot = new Vector3(0, 0, 0);
        float angleD = 360 / numImages * i;
        float angle = angleD * Mathf.PI / 180;

        spot.x = centrePoint.x + (int)Mathf.Round(dist * Mathf.Sin(angle));
        spot.y = centrePoint.y - (int)Mathf.Round(dist * Mathf.Cos(angle)) / 3;
        spot.z = centrePoint.z - (int)Mathf.Round(dist * Mathf.Cos(angle));

        return spot;
    }

    public static int Sign(int i)
    {//Math.Signf but int and with 0 (why does this not exist?)        
        if (i < 0) { return -1; }
        if (i > 0) { return 1; }
        return 0;
    }
}