using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListener : MonoBehaviour
{ 
    public AudioClip ATK_sfx;
    public AudioClip DEF_low_sfx;
    public AudioClip DEF_high_sfx;

    public GameObject character_1;
    public GameObject character_2;
    public GameObject character_3;
    public GameObject character_4;
    public GameObject boss;
    
    public SpriteRenderer shield;

    private bool inSliderHitRange = false;
    private bool keyUp;
    private bool keyHold;
    private Color color;

    public void ChangeNoteBarHighlight(Color c)
    {
        GameObject.Find("Note_Bar_Circle").GetComponent<SpriteRenderer>().color = c;
    }

    public SpriteRenderer CalculateInputAccuracy(decimal nextTime, decimal hitTime)
    {
        SpriteRenderer noteScoreSprite;

        decimal errorDifference = nextTime - hitTime;
        if (errorDifference <= 25)
        {
            //Debug.Log("PERFECT");
            noteScoreSprite = Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Perfect");
        }
        else if (errorDifference <= 100)
        {
            //Debug.Log("GREAT");
            noteScoreSprite = Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Great");
        }
        else if (errorDifference <= 200)
        {
            //Debug.Log("GOOD");
            noteScoreSprite = Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Good");
        }
        else
        {
            /* It should say 'Miss' only when a note passes, not on invalid clicks */
            //Debug.Log("MISS");
            noteScoreSprite = Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Miss");
        }

        return noteScoreSprite;
    }

    void CheckInputBeat(int character_num, decimal time)
    {
        decimal hitTime = time * 1000;
        decimal nextTime = (decimal)GameLogic.nextHit + (1000 * GameLogic.songStartTime);

        SpriteRenderer noteScoreSprite = CalculateInputAccuracy(nextTime, hitTime);
        StartCoroutine(spawnNoteScore(new Vector3(2.45f, 1.87f, -7.77f), 0.3f, noteScoreSprite));
    }

    public void moveNoteScore(float counter, float duration, SpriteRenderer score, Vector3 spawnPoint, Vector3 toPosition)
    {
        score.transform.position = Vector3.Lerp(spawnPoint, toPosition, counter / duration);
    }

    private int currentSprite = 0;
    private int[] lockCoroutine = { 0, 0, 0, 0 };

    public IEnumerator spawnNoteScore(Vector3 spawnPoint, float duration, SpriteRenderer noteSprite)
    {
        SpriteRenderer score;
        score = Instantiate(noteSprite, spawnPoint, Quaternion.identity);

        Vector3 toPosition = new Vector3(2.45f, 1.5f, -7.77f);

        float counter = 0;
        while (counter < duration)
        {
            moveNoteScore(counter, duration, score, spawnPoint, toPosition);

            counter += Time.deltaTime;
            yield return null;
        }

        Destroy(score.gameObject);
    }

    IEnumerator spawnShield(Vector3 spawnPoint, float duration, int spriteLock)
    {
        if (lockCoroutine[spriteLock - 1] != 0)
        {
            yield break;
        }

        lockCoroutine[spriteLock - 1] = 1;

        SpriteRenderer shieldSprite;
        shieldSprite = Instantiate(shield, spawnPoint, Quaternion.identity);

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        Destroy(shieldSprite.gameObject);

        lockCoroutine[spriteLock - 1] = 0;
    }

    IEnumerator attackMovement(Transform fromPosition, Vector3 toPosition, float duration, int spriteLock)
    {
        if (lockCoroutine[spriteLock - 1] != 0)
        {
            yield break;
        }
        
        lockCoroutine[spriteLock - 1] = 1;
        Vector3 startPos = fromPosition.position;

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
            yield return null;
        }

        counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.position = Vector3.Lerp(toPosition, startPos, counter / duration);
            yield return null;
        }

        lockCoroutine[spriteLock - 1] = 0;
    }

    void Start()
    {
        color = Color.red;
    }

    void OnGUI()
    {
        GameObject toUseGO = null;

        decimal time = ((decimal)AudioSettings.dspTime + 0.150m) * 1000;
        if (time > ((decimal)GameLogic.nextHit + (1000 * GameLogic.songStartTime)) && 
            time < ((decimal)GameLogic.nextHitEnd + (1000 * GameLogic.songStartTime)))
        {
            inSliderHitRange = true;
        }
        else
        {
            inSliderHitRange = false;
        }

        if (Input.GetKeyDown("u") || Input.GetKeyDown("i") || Input.GetKeyDown("o") || Input.GetKeyDown("p"))
        {
            color = Color.white;
        }
        else if (Input.GetKeyUp("u") || Input.GetKeyUp("i") || Input.GetKeyUp("o") || Input.GetKeyUp("p"))
        {
            color = Color.red;
        }

        ChangeNoteBarHighlight(color);

        if ((Event.current.Equals(Event.KeyboardEvent("u")) && !Input.GetKey("u")) || Input.GetKeyUp("u"))//Event.current.Equals(Event.KeyboardEvent("u")))
        {
            CheckInputBeat(1, ((decimal)AudioSettings.dspTime + 0.150m));
            toUseGO = character_1;
            currentSprite = 1;
        }
        else if ((Event.current.Equals(Event.KeyboardEvent("i")) && !Input.GetKey("i")) || Input.GetKeyUp("i"))
        {
            CheckInputBeat(2, ((decimal)AudioSettings.dspTime + 0.150m));
            toUseGO = character_2;
            currentSprite = 2;
        } 
        else if ((Event.current.Equals(Event.KeyboardEvent("o")) && !Input.GetKey("o")) || Input.GetKeyUp("o"))
        {
            CheckInputBeat(3, ((decimal)AudioSettings.dspTime + 0.150m));
            toUseGO = character_3;
            currentSprite = 3;
        }
        else if ((Event.current.Equals(Event.KeyboardEvent("p")) && !Input.GetKey("p")) || Input.GetKeyUp("p"))
        {
            CheckInputBeat(4, ((decimal)AudioSettings.dspTime + 0.150m));
            toUseGO = character_4;
            currentSprite = 4;
        }

        if (toUseGO != null)
        {
            if (ClickListener.menu_state == ClickListener.state.ATK)
            {
                GetComponent<AudioSource>().PlayOneShot(ATK_sfx, 0.5F);
                StartCoroutine(attackMovement(toUseGO.transform, boss.transform.position, 0.075f, currentSprite));
            }
            else if (ClickListener.menu_state == ClickListener.state.DEF)
            {
                GetComponent<AudioSource>().PlayOneShot(DEF_low_sfx, 0.7F);
                StartCoroutine(spawnShield(toUseGO.transform.position, 0.3f, currentSprite));
            }
        }
    }
}
