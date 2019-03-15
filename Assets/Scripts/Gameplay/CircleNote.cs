using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Functional Requirement
 * ID: 8.1-1
 * Description: The player must be able to view incoming notes. */
public class CircleNote : MonoBehaviour
{
    /* Variables used for spawning and killing the circle note */
    public Vector2 startPos;
    public Vector2 endPos;

    /* Variables used for lerping the circle note */
    public float approachRate;
    public float startTimeInBeats;
    public float songPosInBeats;
    public float beatNumber;
    public int defendTarget;

    /* Variables used for identifying the note */
    public float id;
    public bool firstNoteOfSlider;
    public bool secondNoteOfSlider;
    public float endTimeOfSlider;

    int noteLock = 0;

    /* This function spawns, lerps, and kills the provided note accuracy text */
    public void moveNoteScore(float counter, float duration, SpriteRenderer score, Vector3 spawnPoint, Vector3 toPosition)
    {
        score.transform.position = Vector3.Lerp(spawnPoint, toPosition, counter / duration);
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

        GameObject menu = GameObject.Find("Menu");
        menu.GetComponent<BossLogic>().bossFrequency--;
        menu.GetComponent<CharacterListener>().missCount++;
        menu.GetComponent<GameLogic>().hitIndex++;

        /* If this was a note where the boss was queued to attack, the attack coroutine is called */

        if (this.name.Split('_')[0] == "defendNoteEnd")
        {
            GameObject.Find("Menu").GetComponent<BossLogic>().BossAttack(defendTarget);
        }

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

        /* Unlock the coroutine */
        noteLock = 0;

        GameObject.Find("Menu").GetComponent<CharacterListener>().showDamage = false;
        Destroy(transform.gameObject);
    }

    /* Update is called once per frame */
    void Update ()
    {
        /* A standard circle note will spawn at startPos and lerp to endPos before dying */
        if (transform.GetComponent<RectTransform>().anchoredPosition != endPos)
        {
            transform.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(startPos, endPos, (approachRate - (startTimeInBeats - songPosInBeats)) / approachRate);
        }
        else
        {
            /* If the circle note is the leading note of a hold note pair, it will wait for the duration of the hold note 
             * to end (when the end of the hold note arrives at the kill point) before dying. */
            if (firstNoteOfSlider)
            {
                if (songPosInBeats >= endTimeOfSlider)
                {
                    Destroy(transform.gameObject);
                }
            }
            /* If the circle note is independant or is the ending note of a hold note pair it will delete immediately */
            else
            {
                this.gameObject.GetComponent<Image>().enabled = false;
                if (GameObject.Find("Menu").GetComponent<GameLogic>().hitIndex == float.Parse(this.name.Split('_')[1]))
                {
                    StartCoroutine(spawnNoteScore(new Vector3(2.02f, 1.87f, -7.77f), 0.3f, Resources.Load<SpriteRenderer>("Prefab/NoteMessage/Miss")));
                }
            }
        }
    }

    /* This is the function that is subscribed to Metronome's publishing */
    public void UpdateSongPosition(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
