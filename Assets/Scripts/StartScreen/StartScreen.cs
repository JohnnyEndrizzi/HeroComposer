using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    //Studio logo and press to continue text
    public Image studioLogo;
    public Text continueText;

    //Curtain
    public Curtain curtain;

    //Background audio
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        HideGraphic(studioLogo);
        HideGraphic(continueText);
        StartCoroutine(SplashScreen());
	}

    //Splash screen loop
    IEnumerator SplashScreen()
    {
        //Fade in logo
        yield return new WaitForSeconds(1.0f);
        yield return StartCoroutine(FadeGraphicIn(studioLogo));
        yield return new WaitForSeconds(1.0f);
        //Fade out logo
        yield return StartCoroutine(FadeGraphicOut(studioLogo));
        yield return new WaitForSeconds(0.1f);
        //Open curtain
        curtain.Open();
        yield return new WaitForSeconds(1.0f);
        //Pulse "Press to Continue" text until button is pressed       
        Coroutine pulseCoroutine = StartCoroutine(Pulse(continueText));
        yield return new WaitUntil(() => Input.anyKeyDown == true);
        StopCoroutine(pulseCoroutine);
        HideGraphic(continueText);
        //Close curtain
        curtain.Close();
        //Fade audio
        StartCoroutine(FadeAudio());
        yield return new WaitForSeconds(2.0f);
        //Switch to main menu
        GameObject.Find("SceneManagerWrapper").GetComponent<SceneManagerWrapper>().SwitchScene("MainMenu");
    }

    //Pulse (fade in/out) graphic
    IEnumerator Pulse(Graphic pulseObject)
    {
        while (true)
        {
            yield return StartCoroutine(FadeGraphicIn(pulseObject));
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(FadeGraphicOut(pulseObject));
            yield return new WaitForSeconds(0.5f);
        }
    }

    //Set alpha channel to 0
    private void HideGraphic(Graphic g)
    {
        Color c = g.material.color;
        c.a = 0;
        g.material.color = c;
    }

    //Fade graphic in
    IEnumerator FadeGraphicIn(Graphic fadeObject)
    {
        for (float f = 0f; f <= 1; f += 0.1f)
        {
            Color c = fadeObject.material.color;
            c.a = (f+0.1f > 1) ? 1 : f;
            fadeObject.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    //Fade graphic out
    IEnumerator FadeGraphicOut(Graphic fadeObject)
    {
        for (float f = 1f; f >= 0; f -= 0.1f)
        {
            Color c = fadeObject.material.color;
            c.a = (f - 0.1f < 0) ? 0 : f;
            fadeObject.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    //Fade volume
    IEnumerator FadeAudio()
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / 1;
            yield return null;
        }
    }
}
