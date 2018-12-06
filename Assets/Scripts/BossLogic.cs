using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OsuParser;

public class BossLogic : MonoBehaviour
{
    public string bossID;
    public float bossPower;
    public float bossHP;
    public int target;

    private float songPosInBeats;
    private float maxNoteCount;
    private int currentNoteCount = 0;
    private float bossFrequency;

    private List<int> weigthedValues = new List<int>();

    private Beatmap beatmap;
    
    public void BossAttack(int chosen)
    {
        GameObject.Find("Boss").GetComponent<AttackAnimator>().ATTACK("arrowHail", 0, chosen + 1);
        GameObject.Find("Boss").GetComponent<AttackAnimator>().ATTACK("arrowHail", 0, chosen + 1);
        GameObject.Find("Boss").GetComponent<AttackAnimator>().ATTACK("arrowHail", 0, chosen + 1);

        int bossPower = 200;
        float maxHealth = GameObject.Find("character_" + (chosen + 1)).GetComponent<CharacterLogic>().hp;

        GameObject.Find("character_" + (chosen + 1)).GetComponent<CharacterLogic>().currentHp -= (int)(bossPower * 0.75f * (GameObject.Find("character_" + (chosen + 1)).GetComponent<CharacterLogic>().def / 100.0f));
        GameObject.Find("character_health_" + (chosen + 1)).transform.Find("Health").transform.localScale = new Vector3(((GameObject.Find("character_" + (chosen + 1)).GetComponent<CharacterLogic>().currentHp) / maxHealth), 1, 1);
    }

    /*
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

        target = chooseAttackTarget();
    }

    void Update()
    {
        if (bossFrequency == 0)
        {
            GetComponent<GameLogic>().setDefendNote(target);

            System.Random random = newRandomSeed();
            bossFrequency = 5 + random.Next(-2, 3);
            target = chooseAttackTarget();
        }

        if (songPosInBeats > GetComponent<GameLogic>().getNextBeat())
        {
            currentNoteCount++;
            bossFrequency--;
        }
    }

    public void Test(object sender, Metronome.TickEventArgs e)
    {
        songPosInBeats = e.positionInBeats;
    }
}
