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

    public SpriteRenderer miss_note_sprite;
    public SpriteRenderer good_note_sprite;
    public SpriteRenderer great_note_sprite;
    public SpriteRenderer perfect_note_sprite;

    public SpriteRenderer shield;

    void CheckInputBeat(int character_num, decimal time)
    {
        decimal hitTime = time * 1000;
        decimal nextTime = (decimal)GameLogic.nextHit + (1000 * GameLogic.songStartTime);

        Debug.Log(string.Format("{0} Command with Character {1}: {2}", ClickListener.menu_state, character_num, time));
        Debug.Log(string.Format("Next Hit = {0}, Input Hit = {1}", nextTime, hitTime));

        SpriteRenderer noteScoreSprite;
        SpriteRenderer note;

        decimal errorDifference = nextTime - hitTime;
        if (errorDifference <= 25)
        {
            Debug.Log("PERFECT");
            noteScoreSprite = perfect_note_sprite;
        }
        else if (errorDifference <= 100)
        {
            Debug.Log("GREAT");
            noteScoreSprite = great_note_sprite;
        }
        else if (errorDifference <=  200)
        {
            Debug.Log("GOOD");
            noteScoreSprite = good_note_sprite;
        }
        else
        {
            /* It should say 'Miss' only when a note passes, not on invalid clicks */
            Debug.Log("MISS");
            noteScoreSprite = miss_note_sprite;
        }

        StartCoroutine(spawnNoteScore(new Vector3(2.53f, 1.87f, -7.77f), 0.3f, noteScoreSprite));
    }

    private int currentSprite = 0;
    private int[] lockCoroutine = {0, 0, 0, 0};

    IEnumerator spawnNoteScore(Vector3 spawnPoint, float duration, SpriteRenderer noteSprite)
    {
        SpriteRenderer score;
        score = Instantiate(noteSprite, spawnPoint, Quaternion.identity);

        Vector3 toPosition = new Vector3(2.53f, 1.5f, -7.77f);

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            score.transform.position = Vector3.Lerp(spawnPoint, toPosition, counter / duration);
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

    void OnGUI()
    {
        GameObject toUseGO = null;

        if (Event.current.Equals(Event.KeyboardEvent("u")))
        {
            CheckInputBeat(1, ((decimal)AudioSettings.dspTime + 0.150m));
            toUseGO = character_1;
            currentSprite = 1;
        }
        else if (Event.current.Equals(Event.KeyboardEvent("i")))
        {
            CheckInputBeat(2, ((decimal)AudioSettings.dspTime + 0.150m));
            toUseGO = character_2;
            currentSprite = 2;
        }
        else if (Event.current.Equals(Event.KeyboardEvent("o")))
        {
            CheckInputBeat(3, ((decimal)AudioSettings.dspTime + 0.150m));
            toUseGO = character_3;
            currentSprite = 3;
        }
        else if (Event.current.Equals(Event.KeyboardEvent("p")))
        {
            CheckInputBeat(4, ((decimal)AudioSettings.dspTime + 0.150m));
            toUseGO = character_4;
            currentSprite = 4;
        }

        if (toUseGO != null)
        {
            if (ClickListener.menu_state == ClickListener.state.ATK)
            {
                GetComponent<AudioSource>().PlayOneShot(ATK_sfx, 0.7F);
                StartCoroutine(attackMovement(toUseGO.transform, boss.transform.position, 0.1f, currentSprite));
            }
            else if (ClickListener.menu_state == ClickListener.state.DEF)
            {
                GetComponent<AudioSource>().PlayOneShot(DEF_low_sfx, 0.7F);
                StartCoroutine(spawnShield(toUseGO.transform.position, 0.3f, currentSprite));
            }
        }
    }
}
