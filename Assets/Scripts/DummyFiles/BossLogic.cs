using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    public string bossID;
    public float bossPower;
    public float bossHP;

    public void BossAttack(Characters target)
    {
        //target.currentHealth -= (int)(bossPower * 0.75f * (target.def / 100));
    }

    public void BossSpecial(Characters target)
    {
        //target.currentHealth -= (int)(bossPower * 2 * (target.def / 100));
    }

    void Start()
    {
        //GetComponent<GameLogic>().metronome.Tick += Test;
        //metronome = new Metronome(songStartTime, beatmap.TimingPoints[0].TimePerBeat);
    }

    //public void Test(object sender, Metronome.TickEventArgs e)
    //{

    //}
}
