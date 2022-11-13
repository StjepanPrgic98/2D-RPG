using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reaper : MonoBehaviour
{


    [HideInInspector] public bool isAlive = true;
    [SerializeField] GameObject summonSpirit;
    [SerializeField] Color white;
    [SerializeField] Color regular;
    [SerializeField] GameObject reaperDeathAnimation;
    [SerializeField] TextMeshProUGUI reaperNegativeStatusesText;
    [HideInInspector] public int numberOfSummons = 0;
    List<string> reaperNegativeStatuses = new List<string>();
    Vector3 firstSummonPosition;
    Vector3 secondSummonPosition;
    Vector3 thirdSummonPosition;
    Vector3 fourthSummonPosition;
    Vector3 fifthSummonPosition;
    Vector3 sixthSummonPosition;
    Vector3 originalPosition;
    Vector3 getCloserToPlayer;

    [HideInInspector] public bool reaperAttack;
    [HideInInspector] public bool reaperSpikeAttack;

    [HideInInspector] public bool hpHalved;
    bool brokenDefence;
    bool brokenDamage;
    bool brokenMagic;
    bool brokenHeal;

    bool awayFromPlayer;
    bool atPlayerPosition;
    bool goBack;
    
   

    PlayerMovement player;
    Animator animator;
    ReaperBattleSystem reaperBattleSystem;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    AudioPlayer audioPlayer;
    Failsafe failsafe;


    //Status Effects
    [HideInInspector] public bool damageBreak;
    [HideInInspector] public bool hasSlow;
    [HideInInspector] public bool hasPoison;


    //Values during battle
    int tempPlayerDefence;
    int tempPlayerDamage;
    int tempPlayerMagicDamage;
    int firstNumGenerated = 0;
    int secondNumGenerated = 0;
    int thirdNumGenerated = 0;
    int fourthNumGenerated = 0;
    int fifthNumGenerated = 0;
    int sixthNumGenerated = 0;
    

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        reaperBattleSystem = FindObjectOfType<ReaperBattleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        failsafe = FindObjectOfType<Failsafe>();
    }
    private void Start()
    {
        if (gameManager.reaperDefeated) { gameObject.SetActive(false); return; }
        firstSummonPosition = transform.position + new Vector3(0.3f, 0.3f, 0);
        secondSummonPosition = transform.position + new Vector3(-0.2f, 0.3f, 0);
        thirdSummonPosition = transform.position + new Vector3(0.3f, 0, 0);
        fourthSummonPosition = transform.position + new Vector3(-0.2f, 0, 0);
        fifthSummonPosition = transform.position + new Vector3(0.3f, -0.3f, 0);
        sixthSummonPosition = transform.position + new Vector3(-0.2f, -0.3f, 0);
        originalPosition = transform.position;
    }


    private void Update()
    {
        DisplayReaperNegativeEffects(); 
        if(player.IsAlive() == false) { return; }
        if (reaperAttack)
        {
            if (reaperAttack)
            {
                getCloserToPlayer = new Vector3(0.3f, 0.1f, 0);
            }
            if (reaperSpikeAttack)
            {
                getCloserToPlayer = new Vector3(0.1f, 0.1f, 0);
            }
            if(transform.position != player.transform.position + getCloserToPlayer && awayFromPlayer == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position + getCloserToPlayer, 0.02f);
            }
            else if(transform.position == player.transform.position + getCloserToPlayer && awayFromPlayer == false)
            {
                awayFromPlayer = true;
                if (reaperSpikeAttack)
                {
                    ReaperSpikeAttack();
                    player.CheckForNegativeStatuses("Bleed");
                    player.BleedPlayer();
                    StartCoroutine(WaitAndHurtPlayer(0.2f, ReduceDamage(1000)));
                    StartCoroutine(WaitAndDoSpikeSound(0.2f));
                }
                else
                {
                    ReaperAttack();
                    player.CheckForNegativeStatuses("Poison");
                    player.PoisonPlayer();
                    StartCoroutine(WaitAndHurtPlayer(0.1f, ReduceDamage(500)));
                    StartCoroutine(WaitAndHurtPlayer(1f, ReduceDamage(500)));
                    StartCoroutine(WaitAndDoSlashSound(0, 0.5f));
                }              
                StartCoroutine(WaitAndReturnBossAfterAttack(2));
            }
            if (goBack)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, 0.01f);
                if(transform.position == originalPosition)
                {
                    goBack = false;
                    reaperAttack = false;
                    reaperSpikeAttack = false;
                    awayFromPlayer = false;
                }
            }
            
            
            
        }
    }
    public void ReaperAttack()
    {
        animator.SetBool("isAttacking", true);
        StartCoroutine(WaitAndReturnToIdle(1, "isAttacking"));
    }

    public void ReaperSpikeAttack()
    {
        animator.SetBool("isSpikeing", true);
        StartCoroutine(WaitAndReturnToIdle(1, "isSpikeing"));
    }
    public void HurtReaper()
    {
        spriteRenderer.color = white;
        StartCoroutine(WaitAndUnhitBoss(0.2f));
    }
    public void ReaperDeath()
    {
        isAlive = false;
        StartCoroutine(WaitAndKillReaper(0.1f));
        audioPlayer.PlayScreamSound();
        failsafe.reaperDestroyed = true;
    }

    public void ReaperSummon()
    {
        numberOfSummons++;
        animator.SetBool("isSummoning", true);
        audioPlayer.PlaySummonClip();
        if(numberOfSummons == 1)
        {
            Instantiate(summonSpirit, firstSummonPosition, Quaternion.identity);
            GenerateRandomNegativeEffect();

        }
        else if(numberOfSummons == 2)
        {
            Instantiate(summonSpirit, secondSummonPosition, Quaternion.identity);
            GenerateRandomNegativeEffect();


        }
        else if(numberOfSummons == 3)
        {
            Instantiate(summonSpirit, thirdSummonPosition, Quaternion.identity);
            GenerateRandomNegativeEffect();

        }
        else if (numberOfSummons == 4)
        {
            Instantiate(summonSpirit, fourthSummonPosition, Quaternion.identity);
            GenerateRandomNegativeEffect();
        }
        else if (numberOfSummons == 5)
        {
            Instantiate(summonSpirit, fifthSummonPosition, Quaternion.identity);
            GenerateRandomNegativeEffect();

        }
        else if (numberOfSummons == 6)
        {
            Instantiate(summonSpirit, sixthSummonPosition, Quaternion.identity);
        }


        StartCoroutine(WaitAndReturnToIdle(1, "isSummoning"));
    }

    void GenerateRandomNegativeEffect()
    {
        FreeSummonSlot();
        int randomNum = Random.Range(1, 6);
        randomNum = CheckIfHasStatus(randomNum);
        SaveGeneratedNumbers(randomNum);
        if(randomNum == 1)
        {
            hpHalved = true;
            player.CheckForNegativeStatuses("Hp Halved");
            reaperBattleSystem.HalvePlayerHp();
        }
        else if(randomNum == 2)
        {
            brokenDefence = true;
            player.CheckForNegativeStatuses("Defence Break");
            BreakPlayerDefence();
        }
        else if (randomNum == 3)
        {
            brokenMagic = true;
            player.CheckForNegativeStatuses("Magic Break");
            BreakPlayerMagicDamage();
        }
        else if (randomNum == 4)
        {
            brokenDamage = true;
            player.CheckForNegativeStatuses("Damage Break");
            BreakPlayerDamage();
        }
        else if (randomNum == 5)
        {
            brokenHeal = true;
            player.CheckForNegativeStatuses("Heal Break");
            BreakPlayerHeal();
        }
    }

    void SaveGeneratedNumbers(int randomNum)
    {
        if(numberOfSummons == 1)
        {
            firstNumGenerated = randomNum;
        }
        if (numberOfSummons == 2)
        {
            secondNumGenerated = randomNum;
        }
        if (numberOfSummons == 3)
        {
            thirdNumGenerated = randomNum;
        }
        if (numberOfSummons == 4)
        {
            fourthNumGenerated = randomNum;
        }
        if (numberOfSummons == 5)
        {
            fifthNumGenerated = randomNum;
        }
        if (numberOfSummons == 6)
        {
            sixthNumGenerated = randomNum;
        }
    }
    void FreeSummonSlot()
    {
        if (numberOfSummons == 1)
        {
            firstNumGenerated = 0;
        }
        if (numberOfSummons == 2)
        {
            secondNumGenerated = 0;
        }
        if (numberOfSummons == 3)
        {
            thirdNumGenerated = 0;
        }
        if (numberOfSummons == 4)
        {
            fourthNumGenerated = 0;
        }
        if (numberOfSummons == 5)
        {
            fifthNumGenerated = 0;
        }
        if (numberOfSummons == 6)
        {
            sixthNumGenerated = 0;
        }
    }
    int CheckIfHasStatus(int randomNum)
    {
        while(randomNum == firstNumGenerated || randomNum == secondNumGenerated || randomNum == thirdNumGenerated || randomNum == fourthNumGenerated || randomNum == fifthNumGenerated || randomNum == sixthNumGenerated)
        {
            randomNum = Random.Range(1, 6);
        }
        return randomNum;
    }

    public void CheckForReaperNegativeEffects(string effect)
    {
        if(effect == "- Damage Break")
        {
            reaperNegativeStatuses.Add(effect);
        }
        if (effect == "- Poison")
        {
            reaperNegativeStatuses.Add(effect);
        }
        if (effect == "- Slow")
        {
            reaperNegativeStatuses.Add(effect);
        }
        if (effect == "Break Defence")
        {
            reaperNegativeStatuses.Add(effect);
        }
    }
    void DisplayReaperNegativeEffects()
    {
        if(reaperNegativeStatuses.Count == 1)
        {
            reaperNegativeStatusesText.text = reaperNegativeStatuses[0];
        }
        if (reaperNegativeStatuses.Count == 2)
        {
            reaperNegativeStatusesText.text = reaperNegativeStatuses[0] + "\n" + reaperNegativeStatuses[1];
        }
        if (reaperNegativeStatuses.Count == 3)
        {
            reaperNegativeStatusesText.text = reaperNegativeStatuses[0] + "\n" + reaperNegativeStatuses[1] + "\n" + reaperNegativeStatuses[2];
        }
        if (reaperNegativeStatuses.Count == 4)
        {
            reaperNegativeStatusesText.text = reaperNegativeStatuses[0] + "\n" + reaperNegativeStatuses[1] + "\n" + reaperNegativeStatuses[2] + "\n" + reaperNegativeStatuses[3];
        }
        if (reaperNegativeStatuses.Count == 5)
        {
            reaperNegativeStatusesText.text = reaperNegativeStatuses[0] + "\n" + reaperNegativeStatuses[1] + "\n" + reaperNegativeStatuses[2] + "\n" + reaperNegativeStatuses[3] + "\n" + reaperNegativeStatuses[4];
        }
    }
    public void DispelReaper()
    {
        hasSlow = false;
        hasPoison = false;
        damageBreak = false;
    }

    


    public int ReduceDamage(int damage)
    {
        damage = CheckPlayerDefence(damage);
        if (player.HasHolyBarrier())
        {
            damage /= 3;
        }
        if (player.HasOneHitShield())
        {
            damage /= 4;
        }
        if (player.HasProtect())
        {
            damage /= 2;
        }
        if (damageBreak)
        {
            damage /= 2;
        }
        return damage;
    }


    int CheckPlayerDefence(int damage)
    {
        if (gameManager.playerPhysicalDefence == 0)
        {
            damage *= 1;
        }
        if (gameManager.playerPhysicalDefence == 1)
        {
            damage -= (damage * 3) / 100;
        }
        if (gameManager.playerPhysicalDefence == 2)
        {
            damage -= (damage * 5) / 100;
        }
        if (gameManager.playerPhysicalDefence == 3)
        {
            damage -= (damage * 7) / 100;
        }
        if (gameManager.playerPhysicalDefence == 4)
        {
            damage -= (damage * 9) / 100;
        }
        if (gameManager.playerPhysicalDefence == 5)
        {
            damage -= (damage * 11) / 100;
        }
        if (gameManager.playerPhysicalDefence == 6)
        {
            damage -= (damage * 13) / 100;
        }
        if (gameManager.playerPhysicalDefence == 7)
        {
            damage -= (damage * 15) / 100;
        }
        if (gameManager.playerPhysicalDefence == 8)
        {
            damage -= (damage * 17) / 100;
        }
        if (gameManager.playerPhysicalDefence == 9)
        {
            damage -= (damage * 19) / 100;
        }
        if (gameManager.playerPhysicalDefence == 10)
        {
            damage -= (damage * 25) / 100;
        }
        return damage;
    }

    void BreakPlayerDefence()
    {
        player.defenceBreak = true;
        tempPlayerDefence = gameManager.playerPhysicalDefence;
        gameManager.playerPhysicalDefence -= 10;
        if(gameManager.playerPhysicalDefence < 0)
        {
            gameManager.playerPhysicalDefence = 0;
        }
    }
    public void RemoveDefenceBreak()
    {
        brokenDefence = false;
        player.defenceBreak = false;
        gameManager.playerPhysicalDefence = tempPlayerDefence;
    }
    void BreakPlayerMagicDamage()
    {
        player.magicBreak = true;
        tempPlayerMagicDamage = gameManager.playerMagicDamage;
        gameManager.playerMagicDamage -= 10;
        if(gameManager.playerMagicDamage < 0)
        {
            gameManager.playerMagicDamage = 0;
        }
    }
    public void RemoveMagicBreak()
    {
        brokenMagic = false;
        player.magicBreak = false;
        gameManager.playerMagicDamage = tempPlayerMagicDamage;
    }
    void BreakPlayerDamage()
    {
        player.damageBreak = true;
        tempPlayerDamage = gameManager.playerPhysicalDamage;
        gameManager.playerPhysicalDamage -= 10;
        if(gameManager.playerPhysicalDamage < 0)
        {
            gameManager.playerPhysicalDamage = 0;
        }
    }
    public void RemoveDamageBreak()
    {
        brokenDamage = false;
        player.damageBreak = false;
        gameManager.playerPhysicalDamage = tempPlayerDamage;
    }
    void BreakPlayerHeal()
    {
        player.healBreaK = true;
    }
    public void RemoveHealBreak()
    {
        brokenHeal = false;
        player.healBreaK = false;
    }
    public void ClearNegativeStatusEffects()
    {
        reaperNegativeStatuses.Clear();
        reaperNegativeStatusesText.text = "";
    }
    IEnumerator WaitAndReturnBossAfterAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        goBack = true;
    }
    IEnumerator WaitAndHurtPlayer(float delay, int damage)
    {
        yield return new WaitForSeconds(delay);
        reaperBattleSystem.IsHit("Player", damage, true);
    }

    


    IEnumerator WaitAndReturnToIdle(float delay, string animation)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(animation, false);
    }
    IEnumerator WaitAndDoSpikeSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioPlayer.PlaySpikesClip();
    }

    IEnumerator WaitAndDoSlashSound(float delay, float delay2)
    {
        yield return new WaitForSeconds(delay);
        audioPlayer.PlaySwing1Clip();
        yield return new WaitForSeconds(delay2);
        audioPlayer.PlaySwing2Clip();
    }

    IEnumerator WaitAndUnhitBoss(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.color = regular;
    }
    IEnumerator WaitAndKillReaper(float delay)
    {
        GameObject reaperDeathEffect1 = Instantiate(reaperDeathAnimation, transform.position, Quaternion.identity);
        Destroy(reaperDeathEffect1, 1);
        audioPlayer.PlayWindBreathClip();
        yield return new WaitForSeconds(0.1f);
        GameObject reaperDeathEffect2 = Instantiate(reaperDeathAnimation, transform.position, Quaternion.identity);
        Destroy(reaperDeathEffect2, 1);
        audioPlayer.PlayWindBreathClip();
        Destroy(gameObject, 0.5f);
        yield return new WaitForSeconds(0.1f);
        GameObject reaperDeathEffect3 = Instantiate(reaperDeathAnimation, transform.position, Quaternion.identity);
        Destroy(reaperDeathEffect3, 1);
        audioPlayer.PlayWindBreathClip();
        yield return new WaitForSeconds(0.1f);
        GameObject reaperDeathEffect4 = Instantiate(reaperDeathAnimation, transform.position, Quaternion.identity);
        Destroy(reaperDeathEffect4, 1);
        audioPlayer.PlayWindBreathClip();
        yield return new WaitForSeconds(0.1f);
        GameObject reaperDeathEffect5 = Instantiate(reaperDeathAnimation, transform.position, Quaternion.identity);
        Destroy(reaperDeathEffect5, 1);
        audioPlayer.PlayWindBreathClip();
        yield return new WaitForSeconds(0.1f);
        GameObject reaperDeathEffect6 = Instantiate(reaperDeathAnimation, transform.position, Quaternion.identity);
        Destroy(reaperDeathEffect6, 1);
        audioPlayer.PlayWindBreathClip();
        yield return new WaitForSeconds(0.1f);
        this.enabled = false;
    }


    void DoesNothing()
    {
        //Turns off warnings in console!
        if (brokenDamage) { return; }
        if (brokenDefence) { return; }
        if (brokenMagic) { return; }
        if (brokenHeal) { return; }
        brokenDefence = true;
        brokenDamage = true;
        brokenMagic = true;
        brokenHeal = true;
    }


}
