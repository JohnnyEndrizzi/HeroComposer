using System.Collections;
using UnityEngine;

public class ShopEntrance : MonoBehaviour {

    [SerializeField]
    GameObject LeftShelf;
    [SerializeField]
    GameObject RightShelf;
    [SerializeField]
    GameObject CenterShelf;
    
    //Lerp positions
    Vector3 LeftShelfStart;
    Vector3 LeftShelfEnd;
    Vector3 RightShelfStart;           
    Vector3 RightShelfEnd;
    Vector3 CenterShelfStart;
    Vector3 CenterShelfEnd;

    private void Start()
    {
        LeftShelfEnd = LeftShelf.transform.position;        
        LeftShelfStart = LeftShelfEnd;
        LeftShelfStart.x -= 500f;
        LeftShelfStart.y += 30f;

        RightShelfEnd = RightShelf.transform.position;
        RightShelfStart = RightShelfEnd;
        RightShelfStart.x += 500f;
        RightShelfStart.y += 30f;

        CenterShelfEnd = CenterShelf.transform.position;
        CenterShelfStart = CenterShelfEnd;
        CenterShelfStart.y += 500f;

        StartCoroutine(Lerper());
    }

    private IEnumerator Lerper()
    {
        //Set Start positions
        LeftShelf.transform.position = LeftShelfStart;
        RightShelf.transform.position = RightShelfStart;
        CenterShelf.transform.position = CenterShelfStart;

        //wait for curtain
        yield return new WaitForSeconds(2f);

        //Lerp to overshoot
        StartCoroutine(Lerp(LeftShelf, LeftShelfStart, LeftShelfEnd, 2f));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Lerp(RightShelf, RightShelfStart, RightShelfEnd, 2f));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Lerp(CenterShelf, CenterShelfStart, CenterShelfEnd, 2f));
    }

    private IEnumerator Lerp(GameObject LerpObject, Vector3 StartPos, Vector3 EndPos, float time)
    {
        float elapsedTime = 0;
        LerpObject.transform.position = StartPos;

        while (elapsedTime < time)
        {
            LerpObject.transform.position = Vector3.Lerp(LerpObject.transform.position, EndPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }    
}