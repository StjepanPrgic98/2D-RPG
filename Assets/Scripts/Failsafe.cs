using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Failsafe : MonoBehaviour
{
    GameManager gameManager;


    [HideInInspector] public bool defenderDestroyed;
    [HideInInspector] public bool reaperDestroyed;
    [HideInInspector] public bool bringerDestroyed;

    [HideInInspector] public  int tempPlayerDamage;
    [HideInInspector] public int tempPlayerMagicDamage;
    [HideInInspector] public int tempPlayerDefence;
    [HideInInspector] public int tempPlayerHp;
    bool tempDamageSet;
    bool tempDefenceSet;
    bool tempMagicDamageSet;
    bool tempHpSet;

    private void Awake()
    {
        ManageSingleton();
        gameManager = FindObjectOfType<GameManager>();
    }


    public void SetAllStatuses()
    {
        tempPlayerDamage = gameManager.playerPhysicalDamage;
        tempPlayerDefence = gameManager.playerPhysicalDefence;
        tempPlayerMagicDamage = gameManager.playerMagicDamage;
        tempPlayerHp = gameManager.playerMaxHpLevel;
    }
    public void SetBossesDestroyed()
    {
        defenderDestroyed = gameManager.defenderDefeated;
        reaperDestroyed = gameManager.reaperDefeated;
        bringerDestroyed = gameManager.bringerDefeated;
    }
    public void SetFailSafePlayerDamageLevel(int level)
    {
        if (tempDamageSet) { return; }
        tempPlayerDamage = level;
        tempDamageSet = true;
    }
    public void SetFailSafePlayerDefenceLevel(int level)
    {
        if (tempDefenceSet) { return; }
        tempPlayerDefence = level;
        tempDefenceSet = true;
    }
    public void SetFailSafePlayerMagicDamageLevel(int level)
    {
        if (tempMagicDamageSet) { return; }
        tempPlayerMagicDamage = level;
        tempMagicDamageSet = true;
    }
    public void SetFailSafePlayerHpLevel(int level)
    {
        if (tempHpSet) { return; }
        tempPlayerHp = level;
        tempHpSet = true;
    }
    public void FixAllPlayerStatuses()
    {
        gameManager = FindObjectOfType<GameManager>();
        FixFailSafePlayerDamageLevel();
        FixFailSafePlayerDefenceLevel();
        FixFailSafePlayerHpLevel();
        FixFailSafePlayerMagicDamageLevel();
    }


    public void FixFailSafePlayerDamageLevel()
    {
        gameManager.playerPhysicalDamage = tempPlayerDamage;
    }
    public void FixFailSafePlayerDefenceLevel()
    {
        gameManager.playerPhysicalDefence = tempPlayerDefence;
    }
    public void FixFailSafePlayerMagicDamageLevel()
    {
        gameManager.playerMagicDamage = tempPlayerMagicDamage;
    }
    public void FixFailSafePlayerHpLevel()
    {
        gameManager.playerMaxHpLevel = tempPlayerHp;
    }


    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
