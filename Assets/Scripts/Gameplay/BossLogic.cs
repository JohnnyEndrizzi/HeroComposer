using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OsuParser;

public class BossLogic : MonoBehaviour
{
    /* Variables used for the stats of the boss */
    public string bossID;
    public float bossPower;
    public float bossHP;
    public int target;

    /* Variables used for caclulating attacks and healthbars */
    private float songPosInBeats;
    private float maxNoteCount;
    public float bossFrequency;

    /* This list is used for randomly choosing the target for an attack */
    private List<int> weigthedValues = new List<int>();

    private Beatmap beatmap;

    /* Functional Requirement
     * ID: 8.1.1-4
     * Description: The system must allow the enemies to randomly attack the players throughout a song.
     * 
     * This function is called by a circle note that has a boss attack queued to it */
    public void BossAttack(int chosen)
    {
        /* Plays the boss attack animation towards the target character */
        GameObject.Find("Boss").GetComponent<AttackAnimator>().ATTACK("arrowHail", 0, chosen + 1);

        /* These variables and calculations are tentative as we develop a better logic */
        int bossPower = 200;
        float maxHealth = GameObject.Find("character_" + (chosen + 1)).GetComponent<CharacterLogic>().hp;

        /* Reduce the target character's current healt hand have it show in their health bar */
        GameObject.Find("character_" + (chosen + 1)).GetComponent<CharacterLogic>().currentHp -= (int)(bossPower * 0.75f * (GameObject.Find("character_" + (chosen + 1)).GetComponent<CharacterLogic>().def / 100.0f));
        GameObject.Find("character_health_" + (chosen + 1)).transform.Find("Health").transform.localScale = new Vector3(((GameObject.Find("character_" + (chosen + 1)).GetComponent<CharacterLogic>().currentHp) / maxHealth), 1, 1);
    }

    /* Used to generate a new Random seed for random number generation */
    public System.Random newRandomSeed()
    {
        return new System.Random();
    }

    /* Functional Requirement
     * ID: 8.1.1-4
     * Description: The system must allow the enemies to randomly attack the players throughout a song.
     * 
     * This function randomly chooses a target from the weigthed array of target */
    public int chooseAttackTarget()
    {
        System.Random random = newRandomSeed();
        return weigthedValues[random.Next(0, weigthedValues.Count)];
    }

    public void setupBoss()
    {
        /* Randomly generate the timing of the boss' next queued attack (3 - 7 beats apart) */
        System.Random random = newRandomSeed();
        bossFrequency = 5 + random.Next(-2, 3);

        beatmap = GetComponent<GameLogic>().beatmap;
        maxNoteCount = GetComponent<GameLogic>().beatmap.HitObjects.Count;

        /* Character placement on your team will depict how much damage their attacks due, as well as 
         * how often the boss will attack them. For example, front row hits harder but has a higher 
         * chance of getting attacked, and the back row hits weake but has less of a chance of getting
         * attacked.
         * 
         * Back Row Character:       12.5% chance 
         * Middle Row Character #1:  25.0% chance
         * Middle Row Character #2:  25.0% chance
         * Front Row Character:      37.5% chance 
         * 
         * If the player chooses to enter a team with less than 4 player, these rates will alter accordingly. */

        Dictionary<int, Character> charactersInParty = GameManager.Instance.gameDataManager.GetCharactersInParty();
        if (charactersInParty.ContainsKey((int)CharacterPosition.FrontRow))
        {
            weigthedValues.Add(0);
            weigthedValues.Add(0);
        }
        if(charactersInParty.ContainsKey((int)CharacterPosition.CentreLeft))
        {
            weigthedValues.Add(1);
            weigthedValues.Add(1);
            weigthedValues.Add(1);
        }
        if(charactersInParty.ContainsKey((int)CharacterPosition.CentreRight))
        {
            weigthedValues.Add(2);
            weigthedValues.Add(2);
        }
        if(charactersInParty.ContainsKey((int)CharacterPosition.BackRow))
        {
            weigthedValues.Add(3);
        }

        /* Randomly choose the target for the next queued attack */
        target = chooseAttackTarget();
    }

    void Update()
    {
        /* If the frequency count is zero, queue an attack and spawn the corresponding headshot of
         * the target character on the note bar */
        if (bossFrequency == 0)
        {
            /* Spawns the headshot note */
            GetComponent<GameLogic>().setDefendNote(target);

            /* Randomly choose how many beats for the next queued attack, as well as the target */
            System.Random random = newRandomSeed();
            bossFrequency = 5 + random.Next(-2, 3);
            target = chooseAttackTarget();
        }
    }

    /* The function that is subscribed to the Metronome's publishing  */
    public void Test(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
