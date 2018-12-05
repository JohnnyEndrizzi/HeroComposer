using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OsuParser;

public class BossLogic : MonoBehaviour
{
    public string bossID;
    public float bossPower;
    public float bossHP;

    private float songPosInBeats;
    private float maxNoteCount;
    private int currentNoteCount = 0;
    private float bossFrequency;

    private List<int> weigthedValues = new List<int>();

    private Beatmap beatmap;

    /*
    public void BossAttack(Characters target)
    {
        //target.currentHealth -= (int)(bossPower * 0.75f * (target.def / 100));
    }

    public void BossSpecial(Characters target)
    {
        //target.currentHealth -= (int)(bossPower * 2 * (target.def / 100));
    }
    */
    public System.Random newRandomSeed()
    {
        return new System.Random();
    }

    public int chooseAttackTarget()
    {
        System.Random random = newRandomSeed();
        return weigthedValues[random.Next(0, weigthedValues.Count)];
    }

    void Start()
    {
        System.Random random = newRandomSeed();
        bossFrequency = 5 + random.Next(-2, 3);
        beatmap = GetComponent<GameLogic>().beatmap;
        maxNoteCount = GetComponent<GameLogic>().beatmap.HitObjects.Count;

        if (Assets.Scripts.MainMenu.ApplicationModel.characters[0] != null)
        {
            weigthedValues.Add(0);
            weigthedValues.Add(0);
        }

        if (Assets.Scripts.MainMenu.ApplicationModel.characters[1] != null)
        {
            weigthedValues.Add(1);
            weigthedValues.Add(1);
            weigthedValues.Add(1);
            weigthedValues.Add(1);
        }

        if (Assets.Scripts.MainMenu.ApplicationModel.characters[2] != null)
        {
            weigthedValues.Add(2);
            weigthedValues.Add(2);
        }

        if (Assets.Scripts.MainMenu.ApplicationModel.characters[3] != null)
        {
            weigthedValues.Add(3);
        }
    }

    void Update()
    {
        if (bossFrequency == 0)
        {
            int target = chooseAttackTarget() + 1;
            Debug.Log("ATTACKED " + target);

            GameObject.Find("Boss").GetComponent<AttackAnimator>().ATTACK("arrowHail", 0, target);
            GameObject.Find("Boss").GetComponent<AttackAnimator>().ATTACK("arrowHail", 0, target);
            GameObject.Find("Boss").GetComponent<AttackAnimator>().ATTACK("arrowHail", 0, target);

            System.Random random = newRandomSeed();
            bossFrequency = 5 + random.Next(-2, 3);
        }

        if (songPosInBeats > GetComponent<GameLogic>().getNextBeat())
        {
            currentNoteCount++;
            bossFrequency--;
        }

        //Debug.Log("Now: " + currentNoteCount + ", Current: " + songPosInBeats + ", Next: " + GetComponent<GameLogic>().getNextBeat());
        //Debug.Log("Now: " + currentNoteCount + ", Boss: " + bossFrequency);
    }

    public void Test(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
