using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotlightBehavior : MonoBehaviour {

    public bool right_side;
    private bool isMoving = false;

    private SpriteRenderer spotlightSprite;
    private Image spotlightImage;

    public GameObject temp;

    private float slideDistX = 100; //1.3
    private float slideDistY = 100; //0.5

    private Vector3 startCoord = new Vector3(0.6f, 1f, -9.0f); //(0.6f, 0.77f, -9.0f)

    /* This coroutine controls the spotlight movement during the opening animation. There are
     * 4 phases for a full spotlight movement. */

    void coordTester()
    {
        Vector3 start2 = spotlightSprite.transform.position;
        Vector3 start = this.transform.localPosition;

        ToolDebugVector3(start);
        ToolDebugVector3(start2);

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

        


        createPoint(start, 0);
        createPoint(firstPhase, 1);
        createPoint(secondPhase, 2);
        createPoint(thirdPhase, 3);
    }

    void createPoint(Vector3 vec3, int i)
    {
        GameObject light = Instantiate(temp) as GameObject;
        light.SetActive(true);
        light.transform.position = vec3;
        light.name = ("light #" + i.ToString());
        light.transform.SetParent(this.transform, false);
    }

    IEnumerator spotlightLerp2()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        Vector3 start = this.transform.localPosition;

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

        /* Phase 1 (upward diagonal movement) */
        float counter = 0;
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

        isMoving = false;
    }

    IEnumerator spotlightLerp()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        Vector3 start = spotlightSprite.transform.position;
                
        /* Parameters for phase 1 and 4 */
        Vector3 firstPhase = start;
        firstPhase.x += right_side ? -1.3f : 1.3f;
        firstPhase.y += 0.5f;

        /* Parameters for phase 2 */
        Vector3 secondPhase = firstPhase;
        secondPhase.x += right_side ? 1.3f : -1.3f;

        /* Parameters for phase 3 */
        Vector3 thirdPhase = secondPhase;
        thirdPhase.x +=  right_side ? -1.3f : 1.3f;
        thirdPhase.y -= 0.5f;


        start = converter(start);
        firstPhase = converter(firstPhase);
        secondPhase = converter(secondPhase);
        thirdPhase = converter(thirdPhase);

        /* Phase 1 (upward diagonal movement) */
        float counter = 0;
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

        isMoving = false;
    }

    /* Use this for initialization */
    void Start ()
    {
        /* Depending on whether the spotlight is right/left will determine its movement pattern */
        spotlightSprite = GetComponent<SpriteRenderer>();
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
        Color spotlightColor = spotlightSprite.color;
        spotlightColor.a = 0.5f;
        spotlightSprite.color = spotlightColor;               
    }
	
	/* Update is called once per frame */
	void Update ()
    {
        /* Upon spawning the spotlight GameObject it will call this movement coroutine */
        StartCoroutine(spotlightLerp2());

        spotlightImage.sprite = spotlightSprite.sprite;



        //spotlightSprite.transform.position
    }

    void test()
    {
        converter(new Vector3(0,0,0));
        converter(new Vector3(100, 0, 0));
        converter(new Vector3(0, 100, 0));
        converter(new Vector3(0, 0, 100));
        converter(new Vector3(200, 200, 0));
    }


    Vector3 converter(Vector3 point)
    {
        //ToolDebugVector3(Camera.main.WorldToScreenPoint(point));
        return Camera.main.WorldToScreenPoint(point);
    }

    void ToolDebugVector3(Vector3 v)
    {
        Debug.Log("X= " + v.x.ToString() + "  Y= " + v.y.ToString() + "  Z= " + v.z.ToString());
    }

}
