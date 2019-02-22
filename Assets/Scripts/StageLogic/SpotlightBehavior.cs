using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotlightBehavior : MonoBehaviour {

    public bool right_side;
    private bool isMoving = false;

    private SpriteRenderer spotlightSprite;
    private Image spotlightImage;
    
    //Modifiers
    private float slideDistX = 200; //1.3
    private float slideDistY = 100; //0.5
    private Vector3 startCoord = new Vector3(0.6f, 1f, -9.0f); //(0.6f, 0.77f, -9.0f)
    private Color spotlightColor = new Color(1f, 1f, 0.4f, 0.5f);


    /* This coroutine controls the spotlight movement during the opening animation. There are
     * 4 phases for a full spotlight movement. */
    IEnumerator spotlightLerp()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        Vector3 start = this.transform.localPosition;

        /* Parameters for phase 0 */
        Vector3 intro = converter(new Vector3(1f,-1f,0f));
        intro.x += right_side ? intro.x : -intro.x;       

        /* Parameters for phase 1 and 4 */
        Vector3 firstPhase = start;
        firstPhase.x += right_side ? -slideDistX : slideDistX;
        firstPhase.y += slideDistY;

        /* Parameters for phase 2 */
        Vector3 secondPhase = firstPhase;
        secondPhase.x += right_side ? slideDistX : -slideDistX;

        /* Parameters for phase 3 */
        Vector3 thirdPhase = secondPhase;
        thirdPhase.x += right_side ? -slideDistX : slideDistX;
        thirdPhase.y -= slideDistY;

        /* Phase 0 (bring light in) */
        float counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(intro, start, counter / 1.0f);
            yield return null;
        }

        /* Phase 1 (upward diagonal movement) */
        counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(start, firstPhase, counter / 1.0f);
            yield return null;
        }

        /* Phase 2 (upper horizontal movement) */
        counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(firstPhase, secondPhase, counter / 1.0f);
            yield return null;
        }

        /* Phase 3 (downward diagonal movement)*/
        counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(secondPhase, thirdPhase, counter / 1.0f);
            yield return null;
        }

        /* Phase 4 (lower horizontal movement)*/
        counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(thirdPhase, start, counter / 1.0f);
            yield return null;
        }

        //isMoving = false;        
    }       

    /* Use this for initialization */
    void Start ()
    {
        /* Depending on whether the spotlight is right/left will determine its movement pattern */
        spotlightSprite = GetComponent<SpriteRenderer>();
        spotlightImage = GetComponent<Image>();
        if (right_side)
        {
            spotlightSprite.transform.localPosition = converter(startCoord);
        }
        else
        {
            startCoord.x *= -1f;
            spotlightSprite.transform.localPosition = converter(startCoord);
        }

        /* Changes the spotlight colour and opacity */
        spotlightImage.color = spotlightColor;               
    }
	
	/* Update is called once per frame */
	void Update ()
    {
        /* Upon spawning the spotlight GameObject it will call this movement coroutine */
        StartCoroutine(spotlightLerp());

        spotlightImage.sprite = spotlightSprite.sprite;
    }

    Vector3 converter(Vector3 point) //Convert coordinates from world space to screen points
    {    
        return Camera.main.WorldToScreenPoint(point);
    }

    void ToolDebugVector3(Vector3 v)
    {
        Debug.Log("X= " + v.x.ToString() + "  Y= " + v.y.ToString() + "  Z= " + v.z.ToString());
    }
}