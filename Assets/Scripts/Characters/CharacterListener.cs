﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterListener : MonoBehaviour
{
    [SerializeField] public AudioClip ATK_sfx;
    [SerializeField] public AudioClip DEF_low_sfx;
    [SerializeField] public AudioClip DEF_high_sfx;
    [SerializeField] public AudioClip fireball_sfx;

    public float songScore;
    public GameObject songScoreText;

    public GameObject boss;
    
    public SpriteRenderer shield;

    private bool inSliderHitRange = false;
    private bool keyUp;
    private bool keyHold;
    private Color color;

    private float startTime;

    private int currentSprite = 0;
    private int[] lockCoroutine = { 0, 0, 0, 0 };
    int noteLock = 0;

    public void ChangeNoteBarHighlight(Color c)
    {
        GameObject.Find("Note_Bar_Circle").GetComponent<SpriteRenderer>().color = c;
    }

    public void UpdateScore(float value)
    {
        // Score += Note Value + (Note Value * (Combo Multiplier * Difficulty Multiplier) / 25)
        songScore += (value + (value * ((0 * 1) / 25)));
        songScoreText.GetComponent<UnityEngine.UI.Text>().text = "Score : " + songScore;
    }

    public SpriteRenderer GetNoteAccuracySprite(decimal songStartTime, decimal currentHit, decimal nextHit)
    {
        decimal hitTime = currentHit * 1000;
        decimal nextTime = nextHit + (1000 * songStartTime);

        decimal errorDifference = nextTime - hitTime;
        if (errorDifference <= 25)
        {
            //Debug.Log("PERFECT");
            //GameLogic.hitIndex++;
            UpdateScore(300);
            return Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Perfect");
        }
        else if (errorDifference <= 100)
        {
            //Debug.Log("GREAT");
            //GameLogic.hitIndex++;
            UpdateScore(100);
            return Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Great");
        }
        else if (errorDifference <= 200)
        {
            //Debug.Log("GOOD");
            //GameLogic.hitIndex++;
            UpdateScore(50);
            return Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Good");
        }
        else
        {
            /* It should say 'Miss' only when a note passes, not on invalid clicks */
            //Debug.Log("MISS");
            return Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Miss");
        }
    }

    public void moveNoteScore(float counter, float duration, SpriteRenderer score, Vector3 spawnPoint, Vector3 toPosition)
    {
        score.transform.position = Vector3.Lerp(spawnPoint, toPosition, counter / duration);
    }

    public bool isPlaying(AudioClip clip)
    {
        if ((Time.time - startTime) >= clip.length)
        {
            return false;
        }

        return true;
    }

    public void characterAttackMovement()
    {
        /* This is where we can further animations later on */
        GetComponent<AudioSource>().PlayOneShot(ATK_sfx, 0.5F);
        startTime = Time.time;
    }

    public SpriteRenderer createShield(Vector3 spawnPoint)
    {
        GetComponent<AudioSource>().PlayOneShot(DEF_low_sfx, 0.7F);
        startTime = Time.time;

        return Instantiate(shield, spawnPoint, Quaternion.identity);
    }

    public IEnumerator spawnNoteScore(Vector3 spawnPoint, float duration, SpriteRenderer noteSprite)
    {
        if (noteLock != 0)
        {
            yield break;
        }

        noteLock = 1;

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

        noteLock = 0;
    }

    IEnumerator spawnShield(Vector3 spawnPoint, float duration, int spriteLock)
    {
        if (lockCoroutine[spriteLock - 1] != 0)
        {
            yield break;
        }

        lockCoroutine[spriteLock - 1] = 1;

        SpriteRenderer shieldSprite = createShield(spawnPoint);

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

        characterAttackMovement();

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

    IEnumerator magicAnimation(GameObject toUseGO, float duration, int spriteLock)
    {
        if (lockCoroutine[spriteLock - 1] != 0)
        {
            yield break;
        }

        lockCoroutine[spriteLock - 1] = 1;

        toUseGO.GetComponent<AttackAnimator>().ATTACK(Assets.Scripts.MainMenu.ApplicationModel.characters[spriteLock - 1].mag_Eqp, spriteLock, 5);
        GetComponent<AudioSource>().PlayOneShot(fireball_sfx, 0.5F);

        lockCoroutine[spriteLock - 1] = 0;
    }

    void Start()
    {
        color = Color.red;
    }
    
    void Update()
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

        if (Input.GetKeyDown("i") || Input.GetKeyDown("j") || Input.GetKeyDown("k") || Input.GetKeyDown("l"))
        {
            color = Color.white;
        }
        else if (Input.GetKeyUp("i") || Input.GetKeyUp("j") || Input.GetKeyUp("k") || Input.GetKeyUp("l"))
        {
            color = Color.red;
        }

        ChangeNoteBarHighlight(color);

        if (Input.GetKeyUp("i"))
        {
            currentSprite = 1;
        }
        else if (Input.GetKeyUp("j"))
        {
            currentSprite = 2;
        }
        else if (Input.GetKeyUp("k"))
        {
            currentSprite = 3;
        }
        else if (Input.GetKeyUp("l"))
        {
            currentSprite = 4;
        }
        else
        {
            currentSprite = 0;
        }

        if (currentSprite > 0)
        {
            toUseGO = GameObject.Find("character_" + currentSprite);
            SpriteRenderer noteScoreSprite = GetNoteAccuracySprite(GameLogic.songStartTime, ((decimal)AudioSettings.dspTime + 0.150m), (decimal)GameLogic.nextHit);
            StartCoroutine(spawnNoteScore(new Vector3(2.45f, 1.87f, -7.77f), 0.3f, noteScoreSprite));
        }

        if (toUseGO != null)
        {
            if (ClickListener.menu_state == ClickListener.state.ATK)
            {
                StartCoroutine(attackMovement(toUseGO.transform, boss.transform.position, 0.075f, currentSprite));
            }
            else if (ClickListener.menu_state == ClickListener.state.DEF)
            {
                StartCoroutine(spawnShield(toUseGO.transform.position, 0.3f, currentSprite));
            }
            else if (ClickListener.menu_state == ClickListener.state.MGC)
            {
                Debug.Log(toUseGO.GetComponent<CharacterLogic>().magicQueue);
                if (toUseGO.GetComponent<CharacterLogic>().magicQueue == 1)
                {
                    StartCoroutine(magicAnimation(toUseGO, 0.3f, currentSprite));
                    toUseGO.GetComponent<CharacterLogic>().magicQueue = 0;
                }
                else
                {
                    toUseGO.GetComponent<CharacterLogic>().magicQueue = 1;
                }
            }
        }
    }
}
