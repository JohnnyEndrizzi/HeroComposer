using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightBehavior : MonoBehaviour {

    public bool right_side;
    private bool isMoving = false;
    private SpriteRenderer spotlightSprite;

    IEnumerator spotlightLerp()
    {
        if (isMoving)
        {
            yield break;
        }

        isMoving = true;

        Vector3 start = spotlightSprite.transform.position;
        
        Vector3 firstPhase = start;
        firstPhase.x += right_side ? -1.3f : 1.3f;
        firstPhase.y += 0.5f;

        Vector3 secondPhase = firstPhase;
        secondPhase.x += right_side ? 1.3f : -1.3f;

        Vector3 thirdPhase = secondPhase;
        thirdPhase.x +=  right_side ? -1.3f : 1.3f;
        thirdPhase.y -= 0.5f;

        float counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(start, firstPhase, counter / 1.0f);
            yield return null;
        }

        counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(firstPhase, secondPhase, counter / 1.0f);
            yield return null;
        }

        counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(secondPhase, thirdPhase, counter / 1.0f);
            yield return null;
        }

        counter = 0;
        while (counter < 1.0f)
        {
            counter += Time.deltaTime;
            spotlightSprite.transform.position = Vector3.Lerp(thirdPhase, start, counter / 1.0f);
            yield return null;
        }

        isMoving = false;
    }

    // Use this for initialization
    void Start ()
    {
        spotlightSprite = GetComponent<SpriteRenderer>();
        if (right_side)
        {
            spotlightSprite.transform.position = new Vector3(0.6f, 0.77f, -9.0f);
        }
        else
        {
            spotlightSprite.transform.position = new Vector3(-0.6f, 0.77f, -9.0f);
        }

        Color spotlightColor = spotlightSprite.color;
        spotlightColor.a = 0.5f;
        spotlightSprite.color = spotlightColor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        StartCoroutine(spotlightLerp());
    }
}
