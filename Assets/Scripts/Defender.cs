using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Defender : MonoBehaviour
{
    PlayerMovement player;
    DefenderBattleSystem defenderBattleSystem;
    Animator animator;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    Failsafe failsafe;
    AudioPlayer audioPlayer;
    [HideInInspector] public bool isAlive = true;
    [SerializeField] GameObject defenderAboveHead;
    [SerializeField] GameObject defenderExplosion;
    [SerializeField] GameObject defenderLightningStrike;
    [SerializeField] GameObject reflectShield;
    [SerializeField] GameObject protect;
    [SerializeField] GameObject oneHitShield;
    [SerializeField] GameObject darkShield;
    [SerializeField] GameObject charge;
    [SerializeField] GameObject dispel;
    [SerializeField] GameObject poisonPlayer;
    [SerializeField] GameObject slowPlayer;
    [SerializeField] GameObject breakPlayerDamage;
    [SerializeField] GameObject defenderDeath;
    [SerializeField] Color white;
    [SerializeField] Color regular;
    [SerializeField] TextMeshProUGUI defenderNegativeStatusesText;
    [SerializeField] TextMeshProUGUI defenderPositiveStatusesText;

    //Negative statuses
    [HideInInspector] public bool damageBreak;
    [HideInInspector] public bool hasSlow;
    [HideInInspector] public bool hasPoison;
    [HideInInspector] public bool defenceBreak;

    //Positive statuses

    [HideInInspector] public bool hasProtect;
    [HideInInspector] public bool hasOneHitShield;
    [HideInInspector] public bool hasDarkShield;
    [HideInInspector] public bool hasReflectShield;
    List<string> defenderNegativeStatuses = new List<string>();
    List<string> defenderPositiveStatuses = new List<string>();


    //Variables
    [HideInInspector] public bool didDemolition;
    [HideInInspector] public int defenderChargeCounter = 0;
    int tempPlayerMagicDamage;
    int tempPlayerDamage;
    int tempPlayerDefence;




    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        defenderBattleSystem = FindObjectOfType<DefenderBattleSystem>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        failsafe = FindObjectOfType<Failsafe>();
    }

    private void Start()
    {
        if (gameManager.defenderDefeated)
        {
            gameObject.SetActive(false);
            return;
        }

    }
    private void Update()
    {
        DisplayDefenderNegativeEffects();
        DisplayDefenderPositiveEffects();
    }



    public void DefenderWalk()
    {
        animator.SetBool("isWalking", true);
    }
    public void DefenderIdle(float delay,string animation)
    {
        StartCoroutine(WaitAndReturnToIdle(delay, animation));
    }

    public void DefenderAttack()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
        DefenderExplosion();
        StartCoroutine(WaitAndReturnToIdle(1.8f, "isAttacking"));
    }
    public void DefenderExplosion()
    {
        StartCoroutine(WaitAndBlowShitUp(1));
    }
    public void DefenderReflectShield()
    {
        hasReflectShield = true;
        GameObject relfectShieldEffect = Instantiate(reflectShield, transform.position, Quaternion.identity);
        CheckForDefenderPositiveEffects("- Reflect Shield");
        audioPlayer.PlayDoubleHpClip();
    }
    public void DefenderDarkShield()
    {
        hasDarkShield = true;
        GameObject darkShieldEffect = Instantiate(darkShield, transform.position, Quaternion.identity);
        CheckForDefenderPositiveEffects("- Dark Shield");
    }
    public void DefenderProtect()
    {
        hasProtect = true;
        GameObject protectEffect = Instantiate(protect, transform.position, Quaternion.identity);
        CheckForDefenderPositiveEffects("- Protect");
        audioPlayer.PlayProtectClip();
    }
    public void DefenderOneHitShield()
    {
        hasOneHitShield = true;
        GameObject oneHitShieldEffect = Instantiate(oneHitShield, transform.position, Quaternion.identity);
        CheckForDefenderPositiveEffects("- One Hit Shield");
        audioPlayer.PlayOneHitShieldClip();
    }
    public void DefenderCharge()
    {
        defenderChargeCounter++;
        GameObject chargeEffect = Instantiate(charge, transform.position + new Vector3(0,1f,0), Quaternion.identity);
        Destroy(chargeEffect, 0.4f);
        audioPlayer.PlayThunderHawkImpact();
    }
    public void DefenderDispel()
    {
        GameObject dispelEffect = Instantiate(dispel, defenderAboveHead.transform.position, Quaternion.identity);
        DispelDefender();
        Destroy(dispelEffect, 0.5f);
        audioPlayer.PlayDispelClip();
    }
    public void DispelPlayer()
    {
        GameObject dispelEffect = Instantiate(dispel, player.transform.position + new Vector3(0,0.2f,0), Quaternion.identity);
        RemovePlayerBreak();
        player.DispelPlayer();
        player.ClearStatusesInDefenderBattle();
        Destroy(dispelEffect, 0.5f);
        audioPlayer.PlayDispelClip();
    }
    public void PoisonPlayer()
    {
       
        GameObject poisonEffect = Instantiate(poisonPlayer, player.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        Destroy(poisonEffect, 0.8f);
        if (player.HasPoison()) { defenderBattleSystem.Immune(); return; }
        player.CheckForNegativeStatuses("Poison");
        player.PoisonPlayer();
        audioPlayer.PlayPoisonClip();
    }
    
    public void BreakPlayerDamage()
    {
        
        GameObject breakPlayerDamageEffect = Instantiate(breakPlayerDamage, player.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        Destroy(breakPlayerDamageEffect, 0.5f);
        if (player.damageBreak) { defenderBattleSystem.Immune(); return; }
        player.CheckForNegativeStatuses("Damage Break");
        BreakPlayerDamageSpell();
        audioPlayer.PlaySlashClip();
    }
    public void BreakPlayerDefence()
    {
       
        GameObject breakPlayerDefenceEffect = Instantiate(breakPlayerDamage, player.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        Destroy(breakPlayerDefenceEffect, 0.5f);
        if (player.defenceBreak) { defenderBattleSystem.Immune(); return; }
        player.CheckForNegativeStatuses("Defence Break");
        BreakPlayerDefenceSpell();
        audioPlayer.PlaySlashClip();
    }
    public void BreakPlayerMagic()
    {
        GameObject breakPlayerMagicEffect = Instantiate(breakPlayerDamage, player.transform.position + new Vector3(0, 0.2f, 0), Quaternion.identity);
        Destroy(breakPlayerMagicEffect, 0.5f);
        player.CheckForNegativeStatuses("Magic Break");
        BreakPlayerMagicDamageSpell();
        audioPlayer.PlaySlashClip();
    }
    public void SlowPlayer()
    {
        
        GameObject slowPlayerEffect = Instantiate(slowPlayer, player.transform.position + new Vector3(0,0.2f,0), Quaternion.identity);
        Destroy(slowPlayerEffect, 1);
        if (player.HasSlow()) { defenderBattleSystem.Immune(); return; }
        player.CheckForNegativeStatuses("Slow");
        player.SlowPlayer();
        audioPlayer.PlaySlowClip();
    }
    public void HurtDefender()
    {
        spriteRenderer.color = white;
        StartCoroutine(WaitAndUnhitBoss(0.2f));
    }
    public void DefenderDeath()
    {
        failsafe.defenderDestroyed = true;
        gameManager.defenderDefeated = true;
        GameObject defenderDeathEffect = Instantiate(defenderDeath, transform.position + new Vector3(0,1.1f,0), Quaternion.identity);
        Destroy(defenderDeathEffect, 3);
        gameObject.SetActive(false);
        audioPlayer.PlayFireExplosionClip();
        RemovePlayerBreak();
        failsafe.FixAllPlayerStatuses();
    }

    public void CheckForDefenderNegativeEffects(string effect)
    {
        if (effect == "- Damage Break")
        {
            defenderNegativeStatuses.Add(effect);
        }
        if (effect == "- Defence Break")
        {
            defenderNegativeStatuses.Add(effect);
        }
        if (effect == "- Poison")
        {
            defenderNegativeStatuses.Add(effect);
        }
        if (effect == "- Slow")
        {
            defenderNegativeStatuses.Add(effect);
        }
    }
    public void CheckForDefenderPositiveEffects(string effect)
    {
        if (effect == "- Protect")
        {
            defenderPositiveStatuses.Add(effect);
        }
        if (effect == "- One Hit Shield")
        {
            defenderPositiveStatuses.Add(effect);
        }
        if (effect == "- Dark Shield")
        {
            defenderPositiveStatuses.Add(effect);
        }
        if (effect == "- Reflect Shield")
        {
            defenderPositiveStatuses.Add(effect);
        }
    }
    void DisplayDefenderNegativeEffects()
    {
        if (defenderNegativeStatuses.Count == 1)
        {
            defenderNegativeStatusesText.text = defenderNegativeStatuses[0];
        }
        if (defenderNegativeStatuses.Count == 2)
        {
            defenderNegativeStatusesText.text = defenderNegativeStatuses[0] + "\n" + defenderNegativeStatuses[1];
        }
        if (defenderNegativeStatuses.Count == 3)
        {
            defenderNegativeStatusesText.text = defenderNegativeStatuses[0] + "\n" + defenderNegativeStatuses[1] + "\n" + defenderNegativeStatuses[2];
        }
        if (defenderNegativeStatuses.Count == 4)
        {
            defenderNegativeStatusesText.text = defenderNegativeStatuses[0] + "\n" + defenderNegativeStatuses[1] + "\n" + defenderNegativeStatuses[2] + "\n" + defenderNegativeStatuses[3];
            defenderBattleSystem.selfDispel = true;
        }
    }
    void DisplayDefenderPositiveEffects()
    {
        if (defenderPositiveStatuses.Count == 1)
        {
            defenderPositiveStatusesText.text = defenderPositiveStatuses[0];
        }
        if (defenderPositiveStatuses.Count == 2)
        {
            defenderPositiveStatusesText.text = defenderPositiveStatuses[0] + "\n" + defenderPositiveStatuses[1];
        }
        if (defenderPositiveStatuses.Count == 3)
        {
            defenderPositiveStatusesText.text = defenderPositiveStatuses[0] + "\n" + defenderPositiveStatuses[1] + "\n" + defenderPositiveStatuses[2];
        }
        if (defenderPositiveStatuses.Count == 4)
        {
            defenderPositiveStatusesText.text = defenderPositiveStatuses[0] + "\n" + defenderPositiveStatuses[1] + "\n" + defenderPositiveStatuses[2] + "\n" + defenderPositiveStatuses[3];
        }
    }
    public void RemoveStatusEffect(string effect)
    {
        for (int i = 0; i < defenderNegativeStatuses.Count; i++)
        {
            if (defenderNegativeStatuses[i] == effect)
            {
                defenderNegativeStatuses.Remove(effect);
            }
        }
        for (int i = 0; i < defenderPositiveStatuses.Count; i++)
        {
            if (defenderPositiveStatuses[i] == effect)
            {
                defenderPositiveStatuses.Remove(effect);
            }
        }
    }
    public void DispelDefender()
    {
        hasDarkShield = false;
        hasOneHitShield = false;
        hasProtect = false;
        hasReflectShield = false;
        hasSlow = false;
        hasPoison = false;
        damageBreak = false;
        defenceBreak = false;
        defenderPositiveStatuses.Clear();
        defenderNegativeStatuses.Clear();
        defenderBattleSystem.generatedNumbers.Remove(7);
        defenderBattleSystem.generatedNumbers.Remove(8);
        defenderBattleSystem.generatedNumbers.Remove(9);
        defenderPositiveStatusesText.text = "";
        defenderNegativeStatusesText.text = "";
    }

    void BreakPlayerMagicDamageSpell()
    {
        failsafe.SetFailSafePlayerMagicDamageLevel(gameManager.playerMagicDamage);
        player.magicBreak = true;
        tempPlayerMagicDamage = gameManager.playerMagicDamage;
        gameManager.playerMagicDamage -= 10;
        failsafe.SetFailSafePlayerMagicDamageLevel(gameManager.playerMagicDamage);
        if (gameManager.playerMagicDamage < 0)
        {
            gameManager.playerMagicDamage = 0;
        }
    }
    void BreakPlayerDamageSpell()
    {
        failsafe.SetFailSafePlayerDamageLevel(gameManager.playerPhysicalDamage);
        player.damageBreak = true;
        tempPlayerDamage = gameManager.playerPhysicalDamage;
        gameManager.playerPhysicalDamage -= 10;
        if (gameManager.playerPhysicalDamage < 0)
        {
            gameManager.playerPhysicalDamage = 0;
        }
    }
    void BreakPlayerDefenceSpell()
    {
        failsafe.SetFailSafePlayerDefenceLevel(gameManager.playerPhysicalDefence);
        player.defenceBreak = true;
        tempPlayerDefence = gameManager.playerPhysicalDefence;
        gameManager.playerPhysicalDefence -= 10;
        if (gameManager.playerPhysicalDefence < 0)
        {
            gameManager.playerPhysicalDefence = 0;
        }
    }

    public void RemovePlayerDamageBreak()
    {
        if(player.damageBreak == false) { return; }
        player.damageBreak = false;
        gameManager.playerPhysicalDamage = tempPlayerDamage;
    }
    public void RemovePlayerDefenceBreak()
    {
        if(player.defenceBreak == false) { return; }
        player.defenceBreak = false;
        gameManager.playerPhysicalDefence = tempPlayerDefence;
    }
    public void RemovePlayerMagicBreak()
    {
        if(player.magicBreak == false) { return; }
        player.magicBreak = false;
        gameManager.playerMagicDamage = tempPlayerMagicDamage;
    }
    public void RemovePlayerBreak()
    {
        RemovePlayerDamageBreak();
        RemovePlayerDefenceBreak();
        RemovePlayerMagicBreak();
        defenderBattleSystem.HalvePlayerHp();
    }


    IEnumerator WaitAndBlowShitUp(float delay)
    {
        Vector3 firstHit = new Vector3(-0.4f, 0.5f, 0);
        Vector3 secondHit = new Vector3(-0.35f, 0.55f, 0);
        Vector3 thirdHit = new Vector3(-0.45f, 0.45f, 0);
        yield return new WaitForSeconds(delay);
        Vector3 swordTip = new Vector3(-0.4f, 0.1f, 0);
        GameObject defenderExplosionEffect1 = Instantiate(defenderExplosion, transform.position + firstHit, Quaternion.identity);
        audioPlayer.PlayNukeExplosionClip();
        Destroy(defenderExplosionEffect1, 1.5f);
        yield return new WaitForSeconds(0.2f);
        GameObject defenderExplosionEffect2 = Instantiate(defenderExplosion, transform.position + secondHit, Quaternion.identity);
        audioPlayer.PlayNukeExplosionClip();
        Destroy(defenderExplosionEffect2, 1.5f);
        yield return new WaitForSeconds(0.2f);
        GameObject defenderExplosionEffect3 = Instantiate(defenderExplosion, transform.position + thirdHit, Quaternion.identity);
        audioPlayer.PlayNukeExplosionClip();
        Destroy(defenderExplosionEffect3, 1.5f);
        defenderBattleSystem.IsHit("Player", ReduceDamage(99999));
        didDemolition = true;
        defenderBattleSystem.CheckIfSurvivedDemolition();
    }

    IEnumerator WaitAndReturnToIdle(float delay, string animation)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(animation, false);
    }

    IEnumerator WaitAndUnhitBoss(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.color = regular;
    }
    public int ReduceDamage(int damage)
    {
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
        damage = CheckPlayerDefence(damage);
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
}
