using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CharacterListener : MonoBehaviour
{
    /* Used for attack sound effects */
    public AudioClip ATK_sfx;
    public AudioClip Boss_barrier_sfx;
    public AudioClip DEF_low_sfx;
    public AudioClip DEF_high_sfx;
    public AudioClip fireball_sfx;
    private AudioClip chosenATK_sfx;

    /* Used for keeping score */
    public float songScore;
    public GameObject songScoreText;
    public int perfectCount = 0;
    public int greatCount = 0;
    public int goodCount = 0;
    public int missCount = 0;

    public GameObject boss;
    public SpriteRenderer shield;
    public bool showDamage = false;

    private bool inSliderHitRange = false;
    private bool keyUp;
    private bool keyHold;
    private Color color;
    private bool initHoldInterval = false;

    private float startTime;

    /* Used for locking coroutines */
    private int currentSprite = 0;
    private int[] lockCoroutine = { 0, 0, 0, 0 };
    int noteLock = 0;

    /* This function changes the kill point on the note bar to appear as white while a note is being pressed (better feedback) */
    public void ChangeNoteBarHighlight(Color c)
    {
        GameObject.Find("Note_Bar_Circle").GetComponent<SpriteRenderer>().color = c;
    }

    /* Functional Requirement
     * ID: 8.1.1-3
     * Description: The system must be able to calculate the player’s score by the end of the song.
     * 
     * This updates the user's runnign score for the current level */
    public void UpdateScore(float value)
    {
        /* The logic for determining how scores will be caclulated will eb revisted, and will appear 
         * in a form similar to the following:
         * 
         * Score += Note Value + (Note Value * (Combo Multiplier * Difficulty Multiplier) / 25) 
         *
         * For now, the following tentative version will be used. 
         */
        songScore += (value + (value * ((0 * 1) / 25)));
        songScoreText.GetComponent<UnityEngine.UI.Text>().text = "Score : " + songScore;
    }

    /* Functional Requirement
     * ID: 8.1.1-1
     * Description: The system must be able to calculate the accuracy of the player’s inputted command.
     * 
     * This function will determine the accuracy of the entered note */
    public SpriteRenderer GetNoteAccuracySprite(decimal songStartTime, decimal currentHit, decimal nextHit, int currentCharacter, bool isAttacking)
    {
        decimal hitTime = currentHit * 1000;
        decimal nextTime = nextHit + (1000 * songStartTime);

        /* Similar to the caclulation of the score, the logic for the error window calculations will be revisted
         * and further optimized to work for any BPM song. */
        decimal errorDifference = nextTime - hitTime;

        /* Perfect hit = 300 points
         * Great hit   = 100 points
         * Good hit    = 50 points
         * Miss hit    = 0 points
         * 
         * These values will be used in our TBD optimized scoring formula. 
         */
        if (errorDifference <= 25)
        {
            perfectCount++;
            GameObject.Find("Menu").GetComponent<GameLogic>().hitIndex++;

            UpdateScore(300);
            GetComponent<BossLogic>().takeDamage((int)(GameObject.Find("character_" + currentCharacter).GetComponent<CharacterLogic>().atk * 1.5));
            return Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Perfect");
        }
        else if (errorDifference <= 100)
        {
            greatCount++;
            GameObject.Find("Menu").GetComponent<GameLogic>().hitIndex++;

            UpdateScore(100);
            GetComponent<BossLogic>().takeDamage((int)(GameObject.Find("character_" + currentCharacter).GetComponent<CharacterLogic>().atk * 1.2));
            return Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Great");
        }
        else if (errorDifference <= 200)
        {
            goodCount++;
            GameObject.Find("Menu").GetComponent<GameLogic>().hitIndex++;

            UpdateScore(50);
            GetComponent<BossLogic>().takeDamage((int)(GameObject.Find("character_" + currentCharacter).GetComponent<CharacterLogic>().atk * 1.0));
            return Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Good");
        }
        else
        {
            if (isAttacking)
            {
                chosenATK_sfx = Boss_barrier_sfx;
                GameObject.Find("Barrier").GetComponent<Renderer>().enabled = true;
                showDamage = true;
                CameraShake.Shake(0.25f, 0.1f);

                float maxHealth = GameObject.Find("character_" + currentSprite).GetComponent<CharacterLogic>().hp;

                if (GameObject.Find("character_" + currentSprite).GetComponent<CharacterLogic>().currentHp - (int)(GameObject.Find("character_" + currentSprite).GetComponent<CharacterLogic>().hp * 0.1) > 0)
                {
                    GameObject.Find("character_" + currentSprite).GetComponent<CharacterLogic>().currentHp -= (int)(GameObject.Find("character_" + currentSprite).GetComponent<CharacterLogic>().hp * 0.1);
                }
                else
                {
                    GameObject.Find("character_" + currentSprite).GetComponent<CharacterLogic>().currentHp = 0;
                    this.GetComponent<BossLogic>().killCharacter(currentSprite);
                }

                GameObject.Find("character_health_" + currentSprite).transform.Find("Health").transform.localScale = new Vector3(((GameObject.Find("character_" + currentSprite).GetComponent<CharacterLogic>().currentHp) / maxHealth), 1, 1);
            }

            return Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Miss");
        }
    }

    /* This function spawns, lerps, and kills the provided note accuracy text */
    public void moveNoteScore(float counter, float duration, SpriteRenderer score, Vector3 spawnPoint, Vector3 toPosition)
    {
        score.transform.position = Vector3.Lerp(spawnPoint, toPosition, counter / duration);
    }

    /* This function is used for our unit tests, testing if the provided audio clip is playing */
    public bool isPlaying(AudioClip clip)
    {
        if ((Time.time - startTime) >= clip.length)
        {
            return false;
        }

        return true;
    }

    /* This is called by the attack coroutine to play the corresponding sound effect. 
     * This will be altered in future if we intend to add more effects to a character's 
     * attack animations. */
    public void characterAttackMovement()
    {
        GetComponent<AudioSource>().PlayOneShot(chosenATK_sfx, 0.5F);
        startTime = Time.time;
    }

    /* This is called by the defend coroutine to play the corresponding sound effect and
     * instantiate the corresponding sprite. */
    public SpriteRenderer createShield(Vector3 spawnPoint)
    {
        GetComponent<AudioSource>().PlayOneShot(DEF_low_sfx, 0.7F);
        startTime = Time.time;

        return Instantiate(shield, spawnPoint, Quaternion.identity);
    }

    /* Functional Requirement
     * ID: 8.1.1-1
     * Description: The system must be able to calculate the accuracy of the player’s inputted command.
     * 
     * This coroutine is called on user input, instantiating the corresponding accuracy text in the scene */
    public IEnumerator spawnNoteScore(Vector3 spawnPoint, float duration, SpriteRenderer noteSprite)
    {
        /* To avoid spam leading to lag, this coroutine can only be run once at any one time */
        if (noteLock != 0)
        {
            yield break;
        }

        noteLock = 1;

        /* Instantiate the provided accuracy text */
        SpriteRenderer score;
        score = Instantiate(noteSprite, spawnPoint, Quaternion.identity);

        Vector3 toPosition = new Vector3(2.02f, 1.5f, -7.77f);

        /* This calls the moveNoteScore function using varying values to lerp the text */
        float counter = 0;
        while (counter < duration)
        {
            moveNoteScore(counter, duration, score, spawnPoint, toPosition);

            counter += Time.deltaTime;
            yield return null;
        }

        Destroy(score.gameObject);
        GameObject.Find("Barrier").GetComponent<Renderer>().enabled = false;
        showDamage = false;

        /* Unlock the coroutine */
        noteLock = 0;
    }

    /* Functional Requirement
     * ID: 8.1-2
     * Description: The player’s battle commands must invoke the proper attack animations as a response.
     * 
     * This coroutine is called on a DEF input, instantiating the shield sprite on the corresponding character */
    IEnumerator spawnShield(Vector3 spawnPoint, float duration, int spriteLock)
    {
        /* To avoid spam leading to lag, this coroutine can only be run once at any one time for each character */
        if (lockCoroutine[spriteLock - 1] != 0)
        {
            yield break;
        }

        lockCoroutine[spriteLock - 1] = 1;

        /* Instantiates the shield GameObject */
        SpriteRenderer shieldSprite = createShield(spawnPoint);

        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        Destroy(shieldSprite.gameObject);
        
        /* Unlock the coroutine */
        lockCoroutine[spriteLock - 1] = 0;
    }

    /* Functional Requirement
     * ID: 8.1-2
     * Description: The player’s battle commands must invoke the proper attack animations as a response.
     * 
     * This coroutine is called on an ATK input, lerping the corresponding character for the "attack" animation */
    IEnumerator attackMovement(Transform fromPosition, Vector3 toPosition, float duration, int spriteLock)
    {
        /* To avoid spam leading to lag, this coroutine can only be run once at any one time for each character */
        if (lockCoroutine[spriteLock - 1] != 0)
        {
            yield break;
        }
        
        lockCoroutine[spriteLock - 1] = 1;
        Vector3 startPos = fromPosition.position;

        /* Lerp the character for their attack animation and play the correpsinding sound effect */
        characterAttackMovement();

        /* Lerp from the starting position to the boss */
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.position = Vector3.Lerp(startPos, toPosition, counter / duration);
            yield return null;
        }

        /* Lerp from the boss back to the starting position */
        counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            fromPosition.position = Vector3.Lerp(toPosition, startPos, counter / duration);
            yield return null;
        }

        GameObject check = GameObject.Find("character_" + spriteLock);
        if (check.GetComponent<SpriteRenderer>().enabled == false)
        {
            Destroy(check);
        }

        /* Unlock the coroutine */
        lockCoroutine[spriteLock - 1] = 0;
    }

    /* Functional Requirement
     * ID: 8.1-2
     * Description: The player’s battle commands must invoke the proper attack animations as a response.
     * 
     * This coroutine is called on a MGC input, playing the corresponding magic animation set to the corresponding character */
    IEnumerator magicAnimation(GameObject toUseGO, float duration, int spriteLock)
    {
        /* To avoid spam leading to lag, this coroutine can only be run once at any one time for each character */
        if (lockCoroutine[spriteLock - 1] != 0)
        {
            yield break;
        }

        lockCoroutine[spriteLock - 1] = 1;

        /* Plays the corresponding animation for the selected character */
        toUseGO.GetComponent<AttackAnimator>().ATTACK(toUseGO.GetComponent<CharacterLogic>().attack, spriteLock, 5);
        GetComponent<AudioSource>().PlayOneShot(fireball_sfx, 0.5F);

        /* Unlock the coroutine */
        lockCoroutine[spriteLock - 1] = 0;
    }

    void Start()
    {
        /* Sets the colour of the note bar's highlight as "off" (white is for "on")*/
        color = Color.red;

    }
    
    void Update()
    {
        GameObject toUseGO = null;

        /* Checks if the current state of the song is within the range of a hold note or not */
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

        /* The highlight for the note bar will be white while either i, j, k or l is being pressed, 
         * but wille be red upon releasing those keys. */
        if ((Input.GetKeyDown("i") && GameObject.Find("character_1")) || (Input.GetKeyDown("j") && GameObject.Find("character_2")) || 
            (Input.GetKeyDown("k") && GameObject.Find("character_3")) || (Input.GetKeyDown("l") && GameObject.Find("character_3")))
        {
            color = Color.white;
        }
        else if (Input.GetKeyUp("i") || Input.GetKeyUp("j") || Input.GetKeyUp("k") || Input.GetKeyUp("l"))
        {
            color = Color.red;
        }

        /* Change the color of the note bar's region is necessary */
        ChangeNoteBarHighlight(color);

        /* Functional Requirement
         * ID: 8.1-3
         * Description: The player must be able to input battle commands.
         * 
         * Functional Requirement
         * ID: 8.1-4
         * Description: The player must be able to choose what character performs the selected battle command.
         * 
         * Functional Requirement
         * ID: 8.1-5
         * Description: The player must be able to choose to either hold or tap the note.
         * 
         * The current keyboard layout for choosing the character is IJKL. We believed this will provide a much more
         * intuitive mapping from keyboard to game space. This will reduce the amount of time the player will look at 
         * the keyboard while they're playing, allowing them to focus on the gameplay instead.
         * 
         * I - character_1
         * J - character_2
         * K - character_3
         * L - character_4 
         */
        if (initHoldInterval)
        {
            if (Input.GetKeyUp("i"))
            {
                currentSprite = 1;
                initHoldInterval = false;
            }
            else if (Input.GetKeyUp("j"))
            {
                currentSprite = 2;
                initHoldInterval = false;
            }
            else if (Input.GetKeyUp("k"))
            {
                currentSprite = 3;
                initHoldInterval = false;
            }
            else if (Input.GetKeyUp("l"))
            {
                currentSprite = 4;
                initHoldInterval = false;
            }
        }
        else
        {
            bool holdInterval = Input.GetKeyDown("j") && GameObject.Find("Menu").GetComponent<GameLogic>().holdNoteInterval;
            if (Input.GetKeyDown("i") && GameObject.Find("character_1"))
            {
                if (!holdInterval)
                {
                    currentSprite = 1;
                }
                else
                {
                    initHoldInterval = true;
                }
            }
            else if (Input.GetKeyDown("j") && GameObject.Find("character_2"))
            {
                if (!holdInterval)
                {
                    currentSprite = 2;
                }
                else
                {
                    initHoldInterval = true;
                }
            }
            else if (Input.GetKeyDown("k") && GameObject.Find("character_3"))
            {
                if (!holdInterval)
                {
                    currentSprite = 3;
                }
                else
                {
                    initHoldInterval = true;
                }
            }
            else if (Input.GetKeyDown("l") && GameObject.Find("character_4"))
            {
                if (!holdInterval)
                {
                    currentSprite = 4;
                }
                else
                {
                    initHoldInterval = true;
                }
            }
            else
            {
                currentSprite = 0;
            }
        }

        /* If i, j, k or l are inputted, fetch the character GameObject corresponding to them and calculate the accuracy of the input */
        if (currentSprite > 0)
        {
            chosenATK_sfx = ATK_sfx;

            SpriteRenderer noteScoreSprite = GetNoteAccuracySprite(GameLogic.songStartTime, ((decimal)AudioSettings.dspTime + 0.150m), (decimal)GameLogic.getNextHit(), currentSprite, ClickListener.menu_state == ClickListener.state.ATK ? true : false);
            StartCoroutine(spawnNoteScore(new Vector3(2.02f, 1.87f, -7.77f), 0.3f, noteScoreSprite));

            toUseGO = GameObject.Find("character_" + currentSprite);
        }

        /* Functional Requirement
         * ID: 8.1-2
         * Description: The player’s battle commands must invoke the proper attack animations as a response.
         * 
         * If a character GameObject was fetched, the animation corresponding the current state of the menu will be started for that character */
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
                /* Since MGC commands are much stronger than ATK commands, we want to give the user an incentive to still use the ATK
                 * command. Our way around that was reqguiring MGC commands 2 stages for input: 1) queuing the command; 2) performing
                 * the command. This means these stronger commands may not be smart to go for due to taking too long to issue. */
                //if (toUseGO.GetComponent<CharacterLogic>().magicQueue == 1)
                //{
                    StartCoroutine(magicAnimation(toUseGO, 0.3f, currentSprite));
                //    toUseGO.GetComponent<CharacterLogic>().magicQueue = 0;
                //}
                //else
                //{
                //    toUseGO.GetComponent<CharacterLogic>().magicQueue = 1;
                //}
            }
        }
    }
}
