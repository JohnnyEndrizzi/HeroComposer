using DummyFiles;
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
        target.currentHealth -= bossPower * 0.75f * (target.defenseValue / 100);
    }

    public void BossSpecial(Characters target)
    {
        target.currentHealth -= bossPower * 2.0f * (target.defenseValue / 100);
    }
}
