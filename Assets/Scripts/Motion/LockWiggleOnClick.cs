using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LockWiggleOnClick : MonoBehaviour {

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(delegate { OnClick(); });
    }

    void OnClick() //Gets clicked
    {
        StopAllCoroutines();
        FindObjectOfType<InvController>().LockedChar();
        StartCoroutine(QuickWiggle());
    }

    IEnumerator QuickWiggle()
    {
        float lerpTime = 0.05f;
        float elapsedTime = 0;
        int loops = 10;

        Vector3 startPos = transform.localPosition;
        Vector3 nextPos, endPos;
        
        for (int i = 0; i<loops; i++)
        {
            elapsedTime = 0;
            nextPos = MoveNext(startPos, i, loops);

            while (elapsedTime < lerpTime)
            {
                transform.localPosition = Vector3.Lerp(startPos, nextPos, (elapsedTime / lerpTime));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            elapsedTime = 0;
            endPos = Reverse(startPos, nextPos);

            while (elapsedTime < lerpTime)
            {
                transform.localPosition = Vector3.Lerp(nextPos, endPos, (elapsedTime / lerpTime));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        yield return null;
    }

    Vector3 MoveNext(Vector3 startingPos, int i, int loops)
    {
        float rad = 2f;
        return new Vector3
        {
            x = startingPos.x + Mathf.Cos(i * rad * 2f * Mathf.PI / loops),
            y = startingPos.y + Mathf.Sin(i * rad * 2f * Mathf.PI / loops),            
            z = startingPos.z
        };
    }

    Vector3 Reverse(Vector3 startingPos, Vector3 endPos)
    {
        return new Vector3
        {
            x = startingPos.x - endPos.x,
            y = startingPos.y - endPos.y,
            z = startingPos.z
        };
    }
}
