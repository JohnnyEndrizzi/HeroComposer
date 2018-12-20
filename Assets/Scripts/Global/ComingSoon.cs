using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComingSoon : MonoBehaviour {
    [SerializeField]
    Text comingSoonTxt;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void clicked () {
        comingSoonTxt.enabled = true;
        StartCoroutine(FadePulse(comingSoonTxt));
    }

    IEnumerator FadePulse(Text txt)
    {
        StartCoroutine(Grow(1.2f));
        yield return StartCoroutine(TextFader(1f, txt, 0, 1));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Grow(0.8f));
        yield return StartCoroutine(TextFader(1f, txt, 1, 0));
        yield return new WaitForSeconds(0.5f);
        comingSoonTxt.enabled = false;

        yield return null;
    }

    private IEnumerator Grow(float scale)
    {
        float LerpTime = 1f;
        float currentLerpTime = 0;
        Vector3 local = comingSoonTxt.transform.localScale;

        while (currentLerpTime < LerpTime)
        {
            comingSoonTxt.transform.localScale = Vector3.Lerp(comingSoonTxt.transform.localScale, local * scale, (currentLerpTime / LerpTime));
            currentLerpTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator TextFader(float t, Text i, float fade1, float fade2) //Controls the text fading in or out for text 
    {
        //0 to invisible, 1 to visible - fader2 is opposite of fader
        //fade1 = start alpha, fade2 = end alpha

        i.color = new Color(i.color.r, i.color.g, i.color.b, fade1);

        while (i.color.a < fade1 + 0.1 && i.color.a > fade2 - 0.1 || i.color.a < fade2 + 0.1 && i.color.a > fade1 - 0.1)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t) * fade1 + (Time.deltaTime / t) * fade2);
            try
            {
                Outline O1 = i.transform.GetComponents<Outline>()[0];
                O1.effectColor = new Color(O1.effectColor.r, O1.effectColor.g, O1.effectColor.b, i.color.a);
                try
                {
                    Outline O2 = i.transform.GetComponents<Outline>()[1];
                    O2.effectColor = new Color(O2.effectColor.r, O2.effectColor.g, O2.effectColor.b, i.color.a);
                }
                catch { }
            }
            catch { }
            yield return null;
        }
    }
}
