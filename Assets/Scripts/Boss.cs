using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Boss : MonoBehaviour
{
    //Variables
    Vector2 currentPosition;

    bool darkSpell;
    bool darkShield;
    bool oblivion;
    bool curse;
    bool darkBoulderSpell;

    bool dispelSpell;

    //Bools
    bool actionTaken;
    bool isAlive = true;
    bool isSpawned;

    [HideInInspector] public bool didOblivion;



    //Statuses - buffs
    bool hasDarkShield;
    bool hasReflectShield;

    //Statuses - debuffs
    bool damageBreak;
    bool hasSlow;
    bool hasPoison;
    [HideInInspector] public bool defenceBreak;
    List<string> bringerNegativeStatuses = new List<string>();
    List<string> bringerPositiveStatuses = new List<string>();
    int tempPlayerDamage = 0;
    int tempPlayerDefence = 0;
    int tempPlayerMagicDamage = 0;
    int tempPlayerMaxHp = 0;


    //Components
    Animator animator;

    //SerializeFields and objects to find
    PlayerMovement player;
    CircleCollider2D circleCollider;
    GameManager gameManager;
    AudioPlayer audioPlayer;
    Failsafe failsafe;

    [Header("Object references")]
    [SerializeField] GameObject enemySpell;
    [SerializeField] GameObject bossCurseSpell;
    [SerializeField] GameObject bossProjectile;
    [SerializeField] GameObject bossShield;
    [SerializeField] GameObject darkBoulder;
    [SerializeField] GameObject darkBolt;
    [SerializeField] GameObject reflectShield;
    [SerializeField] GameObject dispel;
    [SerializeField] Battle battle;
    [SerializeField] TextMeshProUGUI bringerPositiveStatusesText;
    [SerializeField] TextMeshProUGUI bringerNegativeStatusesText;
    [SerializeField] Chests chest1;
    [SerializeField] Chests chest2;
    [SerializeField] Chests chest3;
//--------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Awake(), Start(), Update() methods
    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        circleCollider = FindObjectOfType<CircleCollider2D>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        failsafe = FindObjectOfType<Failsafe>();
    }
    private void Start()
    {
        if (gameManager.bringerDefeated)
        {
            chest1.MakeChestGood();
            chest2.MakeChestGood();
            chest3.MakeChestGood();
        }
    }
    private void Update()
    {
        DisplayBringerNegativeEffects();
        DisplayBringerPositiveEffects();
        currentPosition = transform.position;
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Getter methods

    public Vector2 GetCurrentBossPosition()
    {
        return currentPosition;
    }
    public bool IsActionTaken()
    {
        return actionTaken;
    }
    public bool IsBossAlive()
    {
        return isAlive;
    }
    public bool IsBossSpawned()
    {
        return isSpawned;
    }
    public bool BossHasSlow()
    {
        return hasSlow;
    }
    public bool BossHasPoison()
    {
        return hasPoison;
    }
    public bool BossHasReflectShield()
    {
        return hasReflectShield;
    }
    public bool BossHasShield()
    {
        return hasDarkShield;
    }
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Setter methods
    public void SetActionTaken(bool argument)
    {
        actionTaken = argument;
    }
    public void SetBossSpawned()
    {
        isSpawned = true;
    }
    public void BreakBossDamage()
    {
        damageBreak = true;
    }
    public void SlowBoss()
    {
        hasSlow = true;
    }
    public void PoisonBoss()
    {
        hasPoison = true;
    }
    public void RemoveBossSlow()
    {
        hasSlow = false;
    }
    public void RemoveBossPoison()
    {
        hasPoison = false;
    }
    public void DispelBoss()
    {
        hasReflectShield = false;
        hasPoison = false;
        hasDarkShield = false;
        hasSlow = false;
        damageBreak = false;
        defenceBreak = false;
        ClearNegativeEffects();
        ClearPositiveEffects();
    }
//------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Boss movement methods
    public void BossDeath()
    {
        failsafe.bringerDestroyed = true;
        isAlive = false;
        StartCoroutine(WaitForBossDeath(1));
        Destroy(gameObject, 2);
    }
    public void BossIsHurt()
    {
        animator.SetBool("isHurt", true);
        StartCoroutine(WaitAndReturnToIdle("isHurt", 0.5f, 0));
    }
    public void BossWalk()
    {
        animator.SetBool("isWalking", true);

    }
    public void BossAppear(Vector3 whereToSpawn)
    {
        if (isSpawned) { return; }
        animator.SetBool("doesAppear", true);
        transform.position = whereToSpawn;
        StartCoroutine(WaitAndReturnToIdle("doesAppear", 0.8f, 0));

    }
    public void BossDissappear()
    {
        isSpawned = false;
        animator.SetBool("isDead", true);
        circleCollider.enabled = false;
        StartCoroutine(WaitForBossDissappear());
    }
    public void StopMovement()
    {
        animator.SetBool("isWalking", false);
    }

//-------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Boss Attacks, Spells, and Effects

    public void BossAttack()                                                //Called in Battle script during BossTurn() method
    {
        animator.SetBool("regularAttack", true);
        player.PlayerIsHurt();
        StartCoroutine(WaitAndReturnToIdle("regularAttack", 0.5f, ReduceDamage(1500)));
        audioPlayer.PlaySwing1Clip();
    }
    public void BossMagic(string type)                                      //Called in Battle script during BossTurn() method, arguments are "regular" or "mega"
    {
        
        darkSpell = true;
        animator.SetBool("magicAttack", true);
        audioPlayer.PlayDarkCastClip();
        BossSpellEffect(type);
        if(type == "regular")
        {
            StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, ReduceDamage(1800)));
        }
        else if (type == "mega")
        {
            StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, ReduceDamage(2500)));
        }

    }
    public void BossSpellEffect(string type)                                //Called in BossMagic() method, checks what spell to instantiate
    {
        if(type == "regular")
        {
            Vector3 getAbovePlayer = new Vector2(0.05f, 0.3f);
            StartCoroutine(InstantiateSpellEffect(1f, enemySpell, getAbovePlayer, true, true, true));
        }
        else if(type == "mega")
        {
            StartCoroutine(InstantiateMegaSpellEffect(1f));
        }     
    }
    public void BossCurseSpell()
    {
        darkShield = false;
        animator.SetBool("magicAttack", true);
        Vector3 abovePlayer = new Vector3(0, 0.35f, 0);
        curse = true;
        StartCoroutine(InstantiateSpellEffect(1f, bossCurseSpell, abovePlayer, false, false, true));
        player.DispelPlayer();
        StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, 0));

    }
    public void BossProjectile()
    {
        animator.SetBool("magicAttack", true);
        Vector3 bossHand = new Vector3(0.1f, 0.3f, 0);
        Vector3 secondSoul = new Vector3(0.1f, 0.1f, 0);
        Vector3 thirdSoul = new Vector3(-0.15f, -0.15f, 0);
        Vector3 fourthSoul = new Vector3(-0.25f, -0.25f, 0);
        Vector3 fifthSoul = new Vector3(0.2f, 0.05f, 0);
        StartCoroutine(InstantiateSpellEffect(1f, bossProjectile, bossHand, false, false, false));
        StartCoroutine(InstantiateSpellEffect(1f, bossProjectile, bossHand + secondSoul, false, false, false));
        StartCoroutine(InstantiateSpellEffect(1f, bossProjectile, bossHand + thirdSoul, false, false, false));
        StartCoroutine(InstantiateSpellEffect(1f, bossProjectile, bossHand + fourthSoul, false, false, false));
        StartCoroutine(InstantiateSpellEffect(1f, bossProjectile, bossHand + fifthSoul, false, false, false));
        StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, 0));
    }
    public void BossShield()
    {
        hasDarkShield = true;
        darkShield = true;
        animator.SetBool("magicAttack", true);
        Vector2 bossPosition = new Vector3(0.38f, 0.25f, 0);
        StartCoroutine(InstantiateSpellEffect(1f, bossShield, bossPosition, false, false, false));
        StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, 0));
        CheckForBringerPositiveEffects("- Dark Shield");

    }
    public void DarkBoulder()
    {
        Vector3 abovePlayer = new Vector3(0.05f, 0, 0);
        animator.SetBool("magicAttack", true);
        darkBoulderSpell = true;
        StartCoroutine(InstantiateSpellEffect(1f, darkBoulder, abovePlayer, true, true, true));
        StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, ReduceDamage(2100)));

    }
    public void DarkBolt()
    {
        Vector3 abovePlayer = new Vector3(0.05f, 0.6f, 0);
        animator.SetBool("magicAttack", true);
        oblivion = true;
        StartCoroutine(InstantiateSpellEffect(1f, darkBolt, abovePlayer, true, true, true));
        StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, ReduceDamage(2500)));

    }
    public void ReflectShield()
    {
        hasReflectShield = true;
        Vector2 bossPosition = new Vector3(0.35f, -0.1f, 0);
        animator.SetBool("magicAttack", true);
        StartCoroutine(InstantiateSpellEffect(1f, reflectShield, bossPosition, false, false, false));
        StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, 0));
        CheckForBringerPositiveEffects("- Reflect Shield");
        audioPlayer.PlayOneHitShieldClip();
    }
    public void Dispel()
    {
        dispelSpell = true;
        Vector3 aboveBoss = new Vector3(0.35f, 0.55f, 0);
        animator.SetBool("magicAttack", true);
        StartCoroutine(InstantiateSpellEffect(1f, dispel, aboveBoss, false, false, false));
        StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, 0));
        DispelBoss();
    }
    public void Oblivion()
    {
        Vector3 abovePlayer = new Vector3(0.05f, 0.55f, 0);
        animator.SetBool("magicAttack", true);
        StartCoroutine(InstantiateOblivion());
        StartCoroutine(WaitAndReturnToIdle("magicAttack", 0.8f, ReduceDamage(99999)));
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
        failsafe.SetFailSafePlayerDefenceLevel(gameManager.playerPhysicalDefence);
        player.defenceBreak = true;
        tempPlayerDefence = gameManager.playerPhysicalDefence;
        gameManager.playerPhysicalDefence -= 10;
        if (gameManager.playerPhysicalDefence < 0)
        {
            gameManager.playerPhysicalDefence = 0;
        }
    }
    void BreakPlayerMagicDamage()
    {
        failsafe.SetFailSafePlayerMagicDamageLevel(gameManager.playerMagicDamage);
        player.magicBreak = true;
        tempPlayerMagicDamage = gameManager.playerMagicDamage;
        gameManager.playerMagicDamage -= 10;
        if (gameManager.playerMagicDamage < 0)
        {
            gameManager.playerMagicDamage = 0;
        }
    }
    void BreakPlayerDamage()
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
    void BreakPlayerHeal()
    {
        player.healBreaK = true;
    }
    void BreakPlayerMaxHp()
    {
        failsafe.SetFailSafePlayerHpLevel(gameManager.playerMaxHpLevel);
        tempPlayerMaxHp = gameManager.playerMaxHpLevel;
        gameManager.playerMaxHpLevel -= 10;
        if (gameManager.playerMaxHpLevel < 0)
        {
            gameManager.playerMaxHpLevel = 0;
        }
        battle.ReducePlayerMaxHp();
    }
    void BreakPlayer()
    {
        player.playerBroken = true;
        BreakPlayerDamage();
        BreakPlayerDefence();
        BreakPlayerMagicDamage();
        BreakPlayerHeal();
        BreakPlayerMaxHp();
        player.CheckForNegativeStatuses("Damage Break");
        player.CheckForNegativeStatuses("Defence Break");
        player.CheckForNegativeStatuses("Magic Break");
        player.CheckForNegativeStatuses("Heal Break");
        player.CheckForNegativeStatuses("Max Hp Reduced");
    }
    public void RemovePlayerBreaks()
    {
        player.playerBroken = false;
        player.damageBreak = false;
        player.defenceBreak = false;
        player.magicBreak = false;
        player.healBreaK = false;
        gameManager.playerPhysicalDamage = tempPlayerDamage;
        gameManager.playerMagicDamage = tempPlayerMagicDamage;
        gameManager.playerPhysicalDefence = tempPlayerDefence;
        gameManager.playerMaxHpLevel = tempPlayerMaxHp;
        battle.SetPlayerMaxHpToNormal();
    }
    //-------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Instantiating boss spell effects methods

    IEnumerator InstantiateSpellEffect(float delay, GameObject spell, Vector3 whereToSpawn, bool doDestroy, bool staggerPlayer, bool abovePlayer)                         //Instantiates regular spell, one hit, above player
    {
        yield return new WaitForSeconds(delay);
        if (abovePlayer)
        {
            GameObject darkSpell = Instantiate(spell, player.GetPlayerPosition() + whereToSpawn, Quaternion.identity);
            CheckForAudio();
            if (staggerPlayer)
            {
                player.PlayerIsHurt();
            }
            if (doDestroy)
            {
                Destroy(darkSpell, 1);
            }
        }
        else
        {
            CheckForAudio();
            GameObject darkSpell = Instantiate(spell, transform.position + whereToSpawn, Quaternion.identity);
            if (staggerPlayer)
            {
                player.PlayerIsHurt();
            }
            if (doDestroy)
            {
                Destroy(darkSpell, 1);
            }
        }
        
        
       
        
    }
  

    IEnumerator InstantiateMegaSpellEffect(float delay)                     //Instantiates multiple spells, on different locations above player
    {                                                                       //Logic is work in progress, works for now
        yield return new WaitForSeconds(delay);
        Vector3 getAbovePlayer = new Vector2(0.05f, 0.3f);
        Vector3 firstBlast = new Vector3(0.05f, 0.1f);
        Vector3 secondBlast = new Vector3(-0.3f, -0.15f);
        Vector3 thirdBlast = new Vector3(0.3f, 0.2f);
        Vector3 fourthBlast = new Vector3(-0.2f, 0.1f);
        Vector3 fifthBlast = new Vector3(0.3f, 0);
        Vector3 sixthBlast = new Vector3(0.3f, 0.2f);
        Vector3 seventhBlast = new Vector3(0.5f, 0.1f);
        Vector3 eightBlast = new Vector3(-0.1f, 0.25f);
        Vector3 nineBlast = new Vector3(-0.25f, 0.25f);
        GameObject darkSpell1 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer, Quaternion.identity);
        player.PlayerIsHurt();
        Destroy(darkSpell1, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell2 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + firstBlast, Quaternion.identity);
        Destroy(darkSpell2, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell3 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + secondBlast, Quaternion.identity);
        Destroy(darkSpell3, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell4 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + thirdBlast, Quaternion.identity);
        Destroy(darkSpell4, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell5 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + fourthBlast, Quaternion.identity);
        Destroy(darkSpell5, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell6 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + fifthBlast, Quaternion.identity);
        Destroy(darkSpell6, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell7 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + sixthBlast, Quaternion.identity);
        Destroy(darkSpell7, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell8 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + seventhBlast, Quaternion.identity);
        Destroy(darkSpell8, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell9 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + eightBlast, Quaternion.identity);
        Destroy(darkSpell9, 1);
        audioPlayer.PlayDarkSpellClip();
        yield return new WaitForSeconds(0.1f);
        //GameObject darkSpell10 = Instantiate(enemySpell, player.GetPlayerPosition() + getAbovePlayer + nineBlast, Quaternion.identity);
        //Destroy(darkSpell10, 1);
    }
    IEnumerator InstantiateOblivion()
    {
        yield return new WaitForSeconds(1);
        Vector3 getAbovePlayer = new Vector2(0.05f, 0.4f);
        Vector3 firstBlast = new Vector3(0.05f, 0.1f);
        Vector3 secondBlast = new Vector3(-0.3f, -0.15f);
        Vector3 thirdBlast = new Vector3(0.3f, 0.2f);
        Vector3 fourthBlast = new Vector3(-0.2f, 0.1f);
        Vector3 fifthBlast = new Vector3(0.3f, 0);
        Vector3 sixthBlast = new Vector3(0.3f, 0.2f);
        Vector3 seventhBlast = new Vector3(0.5f, 0.1f);
        Vector3 eightBlast = new Vector3(-0.1f, 0.25f);
        Vector3 nineBlast = new Vector3(-0.25f, 0.25f);
        GameObject darkSpell1 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer, Quaternion.identity);
        player.PlayerIsHurt();
        Destroy(darkSpell1, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell2 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + firstBlast, Quaternion.identity);
        Destroy(darkSpell2, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell3 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + secondBlast, Quaternion.identity);
        Destroy(darkSpell3, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell4 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + thirdBlast, Quaternion.identity);
        Destroy(darkSpell4, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell5 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + fourthBlast, Quaternion.identity);
        Destroy(darkSpell5, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell6 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + fifthBlast, Quaternion.identity);
        Destroy(darkSpell6, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell7 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + sixthBlast, Quaternion.identity);
        Destroy(darkSpell7, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell8 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + seventhBlast, Quaternion.identity);
        Destroy(darkSpell8, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell9 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + eightBlast, Quaternion.identity);
        Destroy(darkSpell9, 1);
        audioPlayer.PlayOblivionClip();
        yield return new WaitForSeconds(0.1f);
        GameObject darkSpell10 = Instantiate(darkBolt, player.GetPlayerPosition() + getAbovePlayer + nineBlast, Quaternion.identity);
        Destroy(darkSpell10, 1);
        audioPlayer.PlayOblivionClip();
        didOblivion = true;
        battle.CheckIfOblivionIsSurvived();
    }
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Wait for time methods

    IEnumerator WaitAndReturnToIdle(string bossAction, float delay, int damageAmmount)             //Called in every Attack and Spell method, sets animation back to idle after some time
    {                       
        yield return new WaitForSeconds(delay);
        animator.SetBool(bossAction, false);
        if(bossAction == "isHurt" || bossAction == "doesAppear" || bossAction == "isDead") { yield break; }
        actionTaken = true;
        if (battle.StartBattle())
        {
            if(bossAction == "regularAttack")
            {
                battle.IsHit("Player", damageAmmount);
            }
            else if(bossAction == "magicAttack")
            {
                battle.IsHit("Player", damageAmmount);
            }
                     
        }             
    }
    IEnumerator WaitForBossDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isDead", true);
    }
    IEnumerator WaitForBossDissappear()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isDead", false);
        Vector3 dissapearPosition = new Vector3(-1.23f, -3.3f, 0);
        transform.position = dissapearPosition;
    }
    IEnumerator WaitAndSetActionTaken(float delay)
    {
        yield return new WaitForSeconds(delay);
        actionTaken = true;
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
    

    public void CheckForBringerNegativeEffects(string effect)
    {
        if (effect == "- Damage Break")
        {
            bringerNegativeStatuses.Add(effect);
        }
        if (effect == "- Poison")
        {
            bringerNegativeStatuses.Add(effect);
        }
        if (effect == "- Slow")
        {
            bringerNegativeStatuses.Add(effect);
        }
        if(effect == "- Defence Break")
        {
            bringerNegativeStatuses.Add(effect);
        }
    }
    public void CheckForBringerPositiveEffects(string effect)
    {
        if (effect == "- Protect")
        {
            bringerPositiveStatuses.Add(effect);
        }
        if (effect == "- One Hit Shield")
        {
            bringerPositiveStatuses.Add(effect);
        }
        if (effect == "- Dark Shield")
        {
            bringerPositiveStatuses.Add(effect);
        }
        if (effect == "- Reflect Shield")
        {
            bringerPositiveStatuses.Add(effect);
        }
    }
    void DisplayBringerNegativeEffects()
    {
        if (bringerNegativeStatuses.Count == 1)
        {
            bringerNegativeStatusesText.text = bringerNegativeStatuses[0];
        }
        if (bringerNegativeStatuses.Count == 2)
        {
            bringerNegativeStatusesText.text = bringerNegativeStatuses[0] + "\n" + bringerNegativeStatuses[1];
        }
        if (bringerNegativeStatuses.Count == 3)
        {
            bringerNegativeStatusesText.text = bringerNegativeStatuses[0] + "\n" + bringerNegativeStatuses[1] + "\n" + bringerNegativeStatuses[2];
        }
        if (bringerNegativeStatuses.Count == 4)
        {
            bringerNegativeStatusesText.text = bringerNegativeStatuses[0] + "\n" + bringerNegativeStatuses[1] + "\n" + bringerNegativeStatuses[2] + "\n" + bringerNegativeStatuses[3];
        }
        if (bringerNegativeStatuses.Count == 5)
        {
            bringerNegativeStatusesText.text = bringerNegativeStatuses[0] + "\n" + bringerNegativeStatuses[1] + "\n" + bringerNegativeStatuses[2] + "\n" + bringerNegativeStatuses[3] + "\n" + bringerNegativeStatuses[4];
        }
    }
    void DisplayBringerPositiveEffects()
    {
        if (bringerPositiveStatuses.Count == 1)
        {
            bringerPositiveStatusesText.text = bringerPositiveStatuses[0];
        }
        if (bringerPositiveStatuses.Count == 2)
        {
            bringerPositiveStatusesText.text = bringerPositiveStatuses[0] + "\n" + bringerPositiveStatuses[1];
        }
        if (bringerPositiveStatuses.Count == 3)
        {
            bringerPositiveStatusesText.text = bringerPositiveStatuses[0] + "\n" + bringerPositiveStatuses[1] + "\n" + bringerPositiveStatuses[2];
        }
        if (bringerPositiveStatuses.Count == 4)
        {
            bringerPositiveStatusesText.text = bringerPositiveStatuses[0] + "\n" + bringerPositiveStatuses[1] + "\n" + bringerPositiveStatuses[2] + "\n" + bringerPositiveStatuses[3];
        }
    }
    public void ClearPositiveEffects()
    {
        bringerPositiveStatuses.Clear();
        bringerPositiveStatusesText.text = "";
    }
    public void ClearNegativeEffects()
    {
        bringerNegativeStatuses.Clear();
        bringerNegativeStatusesText.text = "";
    }

    void CheckForAudio()
    {
        if (darkSpell)
        {
            audioPlayer.PlayDarkSpellClip();
            darkSpell = false;
        }
        if (darkBoulderSpell)
        {
            audioPlayer.PlayDarkBoulderClip();
            darkBoulderSpell = false;
        }
        if (oblivion)
        {
            audioPlayer.PlayOblivionClip();
            oblivion = false;
        }
        if (curse)
        {
            audioPlayer.PlayCurseClip();
            BreakPlayer();
            curse = false;
        }
        if (darkShield)
        {
            audioPlayer.PlayProtectClip();
            darkShield = false;
        }
        if (dispelSpell)
        {
            audioPlayer.PlayDispelClip();
        }
        dispelSpell = false;
        darkSpell = false;
        darkBoulderSpell = false;
        oblivion = false;
        curse = false;
        darkShield = false;
    }

}
