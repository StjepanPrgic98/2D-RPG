using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    Vector2 moveInput;
    Vector2 originalPlayerPosition;
    Vector3 currentPosition;
    Vector3 emptyVector = new Vector3(0, 0, 0);
    Vector3 inputVector;

    string lineBreak = "\n";
    [HideInInspector] public int playerPhysicalDamage = 1;
    [HideInInspector] public int playerPhysicalDefence = 1;
    [HideInInspector] public int playerMagicDefence = 1;
    [HideInInspector] public List<string> playerNegativeStatusList = new List<string>();
    [HideInInspector] public List<string> playerPositiveStatusList = new List<string>();

    //Bools
    bool isAlive = true;
    bool originalPositionIsSet;
    bool actionIsTaken;
    bool restartButtonIsOn;
    bool swordIdle;
    bool playSheatClip = true;

    //Bools for sword attacks... dont bother trying to understand...
    bool isAttackingWithSword;
    bool attackedWithSword;
    bool jumpSmashAttack;
    bool comboAttack;
    bool clearToMove;
    bool callCorutine = true;
    bool wasAtFeetPosition;
    bool wasAtHeadPosition;
    bool airAttack1End;
    bool runBack;
    bool bossIsHit;
    bool movingToBoss;
    bool attackIsOver;
    [HideInInspector] public bool retreatToFirstPosition;
    [HideInInspector] public bool retreatToSecondPosition;

    //Statuses - buffs
    bool hasProtect;
    bool hasOneHitShield;
    bool hasHolyBarrier;
    bool hasHaste;
    [HideInInspector] public bool hasDoubleHp;
    [HideInInspector] public bool hasRegen;

    //Statuses - debuffs
    [HideInInspector] public bool playerBroken;
    bool hasSlow;
    bool hasPoison;
    bool hasBleed;
    [HideInInspector] public bool damageBreak;
    [HideInInspector] public bool defenceBreak;
    [HideInInspector] public bool magicBreak;
    [HideInInspector] public bool hpHalved;
    [HideInInspector] public bool healBreaK;
    
    

    //Components
    Rigidbody2D myRigidbody2D;
    Animator animator;
    GameManager gameManager;
    CantCollectYet cantCollectYet;
    NightBorneBattleSystem nightBorneBattleSystem;
    DefenderBattleSystem defenderBattleSystem;
    Defender defender;
    Reaper reaper;
    ReaperBattleSystem reaperBattleSystem;
    AudioPlayer audioPlayer;

    //SerializeFields
    [Header("Player controls")]
    [SerializeField] float moveSpeed = 10f;

    [Header("Object references")]
    [SerializeField] Boss boss;
    [SerializeField] GameObject bossBody;
    [SerializeField] GameObject bossLegs;
    [SerializeField] GameObject bossPositionToHit;
    [SerializeField] GameObject bossAboveHead;
    [SerializeField] GameObject bossInfront;
    [SerializeField] GameObject bossHead;
    [SerializeField] GameObject BringerOfDeathBody;
    [SerializeField] GameObject BringerOfDeathLegs;
    [SerializeField] GameObject BringerOfDeathAboveHead;
    [SerializeField] GameObject BringerOfDeathInfront;
    [SerializeField] GameObject BringerOfDeathHead;
    [SerializeField] GameObject BringerOfDeathPositionToHit;
    [SerializeField] GameObject BringerOfDeathPositionToHitJumpAttack;
    [SerializeField] GameObject defenderPositionToHit;
    [SerializeField] GameObject defenderPositionToHitJumpAttack;
    [SerializeField] GameObject defenderAboveHead;
    [SerializeField] GameObject defenderBody;
    [SerializeField] GameObject defenderInfront;
    [SerializeField] GameObject defenderLegs;
    [SerializeField] GameObject defenderHead;
    [SerializeField] GameObject reaperPositionToHitJumpAttack;
    [SerializeField] GameObject reaperPositionToHit;
    [SerializeField] GameObject reaperAboveHead;
    [SerializeField] GameObject reaperBody;
    [SerializeField] GameObject reaperInfront;
    [SerializeField] GameObject reaperLegs;
    [SerializeField] GameObject reaperHead;
    [SerializeField] GameObject footsteps;
    [SerializeField] GameObject statusButton;

    [SerializeField] GameObject spell;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject slashEffect;
    [SerializeField] GameObject healEffect;
    [SerializeField] GameObject windSpell;
    [SerializeField] GameObject windBreath;
    [SerializeField] GameObject waterTornado;
    [SerializeField] GameObject waterProjectile;
    [SerializeField] GameObject thunderHawk;
    [SerializeField] GameObject thunderProjectile;
    [SerializeField] GameObject thunderStrike;
    [SerializeField] GameObject thunderSplash;
    [SerializeField] GameObject iceSplash;
    [SerializeField] GameObject iceGround;
    [SerializeField] GameObject iceProjectile;
    [SerializeField] GameObject holyProjectile;
    [SerializeField] GameObject holyGround;
    [SerializeField] GameObject fireExplosion;
    [SerializeField] GameObject nukeExplosion;  
    [SerializeField] GameObject holyShield;
    [SerializeField] GameObject hasteSpell;
    [SerializeField] GameObject slowSpell;    
    [SerializeField] GameObject poisonSpell;    
    [SerializeField] GameObject protectShield;    
    [SerializeField] GameObject oneHitShield;    
    [SerializeField] GameObject doubleHpSpell;    
    [SerializeField] GameObject regenSpell;    
    [SerializeField] GameObject dispel;    
    [SerializeField] GameObject defenderFightFirstPosition;    
    [SerializeField] GameObject defenderFightSecondPosition;    
    [SerializeField] Battle battle;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject deathCanvas;
    [SerializeField] LevelManager levelManager;
    [SerializeField] TextMeshProUGUI battleText;
    [SerializeField] TextMeshProUGUI playerStatus;
    [SerializeField] TextMeshProUGUI playerNegativeStatus;
    [SerializeField] GameObject retreat;
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Awake(), Start(), Update() methods
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        cantCollectYet = FindObjectOfType<CantCollectYet>();
        nightBorneBattleSystem = FindObjectOfType<NightBorneBattleSystem>();
        defenderBattleSystem = FindObjectOfType<DefenderBattleSystem>();
        reaper = FindObjectOfType<Reaper>();
        reaperBattleSystem = FindObjectOfType<ReaperBattleSystem>();
        defender = FindObjectOfType<Defender>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        
    }
    private void Update()
    {
        Run();
        FlipSprite();
        RestartButton();
        MovePlayerForSwordAttack();
        MovePlayerForJumpSmashAttack();
        MovePlayerForComboAttack();
        MovePlayerBackDefenderFight();
        DisplayPositiveStatusEffects();
        ShowNegativeStatuses();
    }
 //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Getter methods
    public bool IsActionTaken()
    {
        return actionIsTaken;
    }
    public bool IsAlive()
    {
        return isAlive;
    }
    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
    public bool HasProtect()
    {
        return hasProtect;
    }
    public bool HasOneHitShield()
    {
        return hasOneHitShield;
    }
    public bool HasHolyBarrier()
    {
        return hasHolyBarrier;
    }
    public bool HasHaste()
    {
        return hasHaste;
    }
    public bool HasPoison()
    {
        return hasPoison;
    }
    public bool HasBleed()
    {
        return hasBleed;
    }
    public bool HasSlow()
    {
        return hasSlow;
    }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Setter methods
    public void SetActionTaken(bool argument)
    {
        actionIsTaken = argument;   
    }
    public void SlowPlayer()
    {
        hasSlow = true;
    }
    public void PoisonPlayer()
    {
        hasPoison = true;
    }
    public void BleedPlayer()
    {
        hasBleed = true;
    }
    public void RemoveProtect()
    {
        hasProtect = false;
    }
    public void RemoveOneHitBarrier()
    {
        hasOneHitShield = false;
    }
    public void RemoveHolyBarrier()
    {
        hasHolyBarrier = false;
    }
    public void RemoveHaste()
    {
        hasHaste = false;
    }
    public void RemovePosion()
    {
        hasPoison = false;
    }
    public void RemoveBleed()
    {
        hasBleed = false;
    }
    public void DispelPlayer()
    {
        if (battle.StartBattle())
        {
            DispelFullBreak();
        }
        if (defenderBattleSystem.defenderBattleStarted)
        {
            defender.RemovePlayerBreak();
            ClearStatusesInDefenderBattle();
        }
        hasProtect = false;
        hasOneHitShield = false;
        hasHolyBarrier = false;
        hasHaste = false;
        hasBleed = false;
        hasPoison = false;
        hasSlow = false;
        damageBreak = false;
        hpHalved = false;
        defenceBreak = false;
        magicBreak = false;
        healBreaK = false;
        playerPositiveStatusList.Clear();
        playerNegativeStatusList.Clear();
        playerStatus.text = "";
        playerNegativeStatus.text = "";
        
    }
//----------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Player movement methods

    void OnMove(InputValue value)               //Unity input system, called by pressing WASD or arrow keys
    {     
        moveInput = value.Get<Vector2>();
    }

    void Run()                                  //Called in Update(), if player is alive and the battle has not started he can move, if not the player stops                                               
    {                                           //and controls are taken away from the player
        if (isAlive)
        {
            if(battle.StartBattle() == false && levelManager.IsLoadingOver() && nightBorneBattleSystem.seenByNightBorne == false && defenderBattleSystem.defenderBattleStarted == false && reaperBattleSystem.reaperStartBattle == false)
            {
                MovePlayer();
            }
            else
            {
                StopPlayerMovement();
                footsteps.SetActive(false);
            }         
        }
    }
    void MovePlayer()                           //Called in Run() method
    {
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed * Time.deltaTime, moveInput.y * moveSpeed * Time.deltaTime);
        myRigidbody2D.velocity = playerVelocity;
        if (playerVelocity.x == 0 && playerVelocity.y == 0)
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("returnToIdle", true);
            footsteps.SetActive(false);
        }
        else if (playerVelocity.x > 0 || playerVelocity.x < 0 || playerVelocity.y > 0 || playerVelocity.y < 0)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("returnToIdle", false);
            footsteps.SetActive(true);
        }
    }
    void StopPlayerMovement()                   //Called in Run() method and in Level manager
    {
        Vector2 stopPlayer = new Vector2(0, 0);
        myRigidbody2D.velocity = stopPlayer;
        animator.SetBool("isWalking", false);
        animator.SetBool("returnToIdle", true);
    }
    void FlipSprite()                           //Called in Update(), flips sprite depending on positive or negative x.axis movement
    {
        bool playerIsMoving = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerIsMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }
    public void PlayerDeath()                   //Called in Battle script, CheckForPlayerDeath() method 
    {
        statusButton.SetActive(false);
        isAlive = false;
        StopPlayerMovement();
        StartCoroutine(WaitForPlayerDeath(1));
        Destroy(gameObject, 2);
    }
    public void PlayerIsHurt()
    {
        if (swordIdle)
        {
            animator.SetBool("returnToIdle", false);
            animator.SetBool("swordIdle", false);
            animator.SetBool("isHurt", true);
            StartCoroutine(WaitAndReturnToSwordIdle(0.5f, "isHurt"));
        }
        else if(!swordIdle)
        {
            animator.SetBool("swordIdle", false);
            animator.SetBool("isHurt", true);
            StartCoroutine(WaitAndReturnToIdle("isHurt", 0.5f));
        }
        
    }
    void RestartButton()
    {
        if (IsAlive() == false && restartButtonIsOn == false)
        {
            restartButtonIsOn = true;
            StartCoroutine(WaitForDeathCanvas());
        }
    }
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    //OnCollision and OnTrigger methods

    private void OnTriggerEnter2D(Collider2D collision) //Activates the battle system canvas, and saves player position where the fight was triggered
    {
        if (collision.tag == "BattleSystem" && battle.battleCanvasIsOn == false)
        {
            SetOriginalPosition();
            canvas.SetActive(true);
            battle.battleCanvasIsOn = true;
        }      
    }
    
    //-----------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Player Attacks, Spells and Effects methods


    public void DrawSword()
    {
        swordIdle = true;
        gameManager.playerDrewSword = true;
        animator.SetBool("drawSword", true);
        StartCoroutine(WaitAndStartNewIdle(0.5f, "drawSword"));
        audioPlayer.PlayDrawSwordClip();
    }
    public void SheatSword()
    {
        swordIdle = false;
        gameManager.playerDrewSword = false;
        animator.SetBool("swordIdle", false);
        animator.SetBool("sheatSword", true);
        StartCoroutine(WaitAndStartNewIdle(0.4f, "sheatSword"));
        audioPlayer.PlaySheatSwordClip();
    }
    public void PlayerRegularAttack()                   //Called by button press on BattleUI
    {
        if (cantCollectYet.TriedToCollect())
        {
            battleText.text = "Too far!";
            StartCoroutine(WaitAndTurnOffBattleText(2));
        }
        else
        {
            currentPosition = transform.position;
            isAttackingWithSword = true;
            TimesHit("regular");
        }
           
    }        
    public void PlayerJumpSmashAttack()
    {
        if (cantCollectYet.TriedToCollect())
        {
            battleText.text = "Too far!";
            StartCoroutine(WaitAndTurnOffBattleText(2));
        }
        else
        {
            currentPosition = transform.position;
            runBack = false;
            jumpSmashAttack = true;
            TimesHit("regular");

        }
    }
    public void PlayerComboAttack()
    {
        if (cantCollectYet.TriedToCollect())
        {
            battleText.text = "Too far!";
            StartCoroutine(WaitAndTurnOffBattleText(2));
        }
        else
        {
            currentPosition = transform.position;
            comboAttack = true;
            movingToBoss = true;
            TimesHit("regular");

        }
    }
    public void PlayerDefend()
    {
        animator.SetBool("isDefending", true);
        animator.SetBool("swordIdle", false);
        StartCoroutine(WaitAndSetActionTaken(0.5f));
    }
    public void ResetDefenceState()
    {
        animator.SetBool("swordIdle", true);
        animator.SetBool("isDefending", false);
        
    }
    public void PlayerCastMagic()                       //Called by button press on BattleUI
    {
        animator.SetBool("isCasting", true);
        ExplosionSpell();       
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
    }
    public void PlayerCastProjectile()
    {
        animator.SetBool("isCasting", true);
        ThunderProjectile();       
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
    }
    public void ExplosionSpell()                        //Called in PlayerCastMagic() method, instantiates explosion spell object prefab
    {
        animator.SetBool("isCasting", true);
        Vector2 getCloser = new Vector2(0.35f, 0);
        GameObject explosion = Instantiate(spell, boss.GetCurrentBossPosition() + getCloser, Quaternion.identity);
        StartCoroutine(WaitAndHurtBoss(0.2f, 100));
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        Destroy(explosion, 1);
    }
    public void FireBallSpell()
    {
        animator.SetBool("isCasting", true);
        Vector3 moveAwayFromPlayer = new Vector3(0.1f, 0,0);
        GameObject fireBall = Instantiate(fireball, transform.position + moveAwayFromPlayer, Quaternion.identity);
        StartCoroutine(WaitAndHurtBoss(0.2f, 100));
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
    }
    public void SlashEffectSpell()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject slashEffectSpell = Instantiate(slashEffect, bossAboveHead.transform.position, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        if (defender.damageBreak) { defenderBattleSystem.BossImmune();return; }
        BreakBossDamage();
        audioPlayer.PlaySlashClip();
        gameManager.damageBreakCount--;
    }
    public void BreakBossDefence()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject slashEffectSpell = Instantiate(slashEffect, bossAboveHead.transform.position, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        if (defender.defenceBreak) { defenderBattleSystem.BossImmune(); return; }
        BreakBossDefenceSpell();
        gameManager.defenceBreakCount--;
        audioPlayer.PlaySlashClip();
        
    }
    public void HealEffect()
    {
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject healEffectSpell = Instantiate(healEffect, transform.position + abovePlayer, Quaternion.identity);
        HealPlayerHealth(1500);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        audioPlayer.PlayHealClip();
        gameManager.healCount--;
    }
    public void LargeHeal()
    {
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject healEffectSpell = Instantiate(healEffect, transform.position + abovePlayer, Quaternion.identity);
        HealPlayerHealth(2200);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        audioPlayer.PlayHealClip();
        gameManager.largeHealCount--;
    }
    public void WindSpell()
    {
        animator.SetBool("isCasting", true);
        Vector3 moveAwayFromPlayer = new Vector3(0.1f, 0, 0);
        GameObject windSpellEffect = Instantiate(windSpell, transform.position + moveAwayFromPlayer, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayWindProjectileClip();
    }
    public void WindBreath()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        Vector2 getCloser = new Vector2(0.2f, 0);
        GameObject windBreathEffect = Instantiate(windBreath, bossBody.transform.position, Quaternion.identity);
        //InflictDamageToBoss("wind", 3200);
        CheckWhatBossToHitWithSpell(0.5f, IncreaseMagicDamage(3200), "wind");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayWindBreathClip();
    }
    public void WaterTornado()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        Vector2 getCloser = new Vector2(0.35f, 0);
        GameObject waterTornadoEffect = Instantiate(waterTornado, bossBody.transform.position, Quaternion.identity);
        //InflictDamageToBoss("water", 2400, 0.5f);
        CheckWhatBossToHitWithSpell(0.5f, IncreaseMagicDamage(2400), "water");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayWaterTornadoClip();
        gameManager.waterTornadoCount--;
    }
    public void WaterProjectile()
    {
        animator.SetBool("isCasting", true);
        Vector3 moveAwayFromPlayer = new Vector3(0.05f, 0, 0);
        GameObject waterProjectileEffect = Instantiate(waterProjectile, transform.position + moveAwayFromPlayer, Quaternion.identity);
        //InflictDamageToBoss("water", 1600, 0.5f);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayWaterProjectileClip();
        gameManager.waterProjectileCount--;
    }
    public void ThunderProjectile()
    {
        animator.SetBool("isCasting", true);
        Vector3 moveAwayFromPlayer = new Vector3(0.05f, 0, 0);
        GameObject thunderProjectileEffect = Instantiate(thunderProjectile, transform.position + moveAwayFromPlayer, Quaternion.identity);
        //InflictDamageToBoss("thunder", 1700, 0.5f);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayThunderProjectileStartClip();
        gameManager.thunderProjectileCount--;
    }
    public void ThunderHawk()
    {
        animator.SetBool("isCasting", true);
        Vector3 moveAwayFromPlayer = new Vector3(0.05f, 0, 0);
        GameObject thunderHawkEffect = Instantiate(thunderHawk, transform.position + moveAwayFromPlayer, Quaternion.identity);
        //InflictDamageToBoss("thunder", 2100, 0.5f);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayThunderHawkEagle();
        gameManager.thunderHawkCount--;
    }
    public void ThunderStrike()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        GameObject thunderStrikeEffect = Instantiate(thunderStrike, bossBody.transform.position, Quaternion.identity);
        //InflictDamageToBoss("thunder", 2600, 0.5f);
        CheckWhatBossToHitWithSpell(0.5f, IncreaseMagicDamage(2600), "thunder");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayThunderStrikeClip();
        gameManager.thunderStrikeCount--;
    }
    public void ThunderSplash()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        GameObject thunderSplashEffect = Instantiate(thunderSplash, bossInfront.transform.position, Quaternion.identity);
        //InflictDamageToBoss("thunder", 1400,0.5f);
        CheckWhatBossToHitWithSpell(0.5f, IncreaseMagicDamage(1400), "thunder");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayThunderSplashClip();
        gameManager.thunderSplashCount--;
    }
    public void IceGround()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        GameObject iceGroundEffect = Instantiate(iceGround, bossLegs.transform.position, Quaternion.identity);
        //InflictDamageToBoss("ice", 2300, 0.5f);
        CheckWhatBossToHitWithSpell(0.5f, IncreaseMagicDamage(2300), "ice");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayIceGroundClip();
        gameManager.iceGroundCount--;
    }
    public void IceSplash()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        GameObject iceSplashEffect = Instantiate(iceSplash, bossHead.transform.position, Quaternion.identity); ;
        //InflictDamageToBoss("ice", 1800, 0.5f);
        CheckWhatBossToHitWithSpell(0.5f, IncreaseMagicDamage(1800), "ice");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayIceSplashClip();
        gameManager.iceSplashCount--;
    }
    public void IceProjectile()
    {
        animator.SetBool("isCasting", true);
        Vector3 moveAwayFromPlayer = new Vector3(0.1f, 0, 0);
        GameObject iceProjectileEffect = Instantiate(iceProjectile, transform.position + moveAwayFromPlayer, Quaternion.identity);
        //InflictDamageToBoss("ice", 1700, 0.5f);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayIceProjectileStartClip();
        gameManager.iceProjectileCount--;
    }
    public void HolyProjectile()
    {
        animator.SetBool("isCasting", true);
        Vector3 moveAwayFromPlayer = new Vector3(0.1f, 0, 0);
        GameObject holyProjectileEffect = Instantiate(holyProjectile, transform.position + moveAwayFromPlayer, Quaternion.identity);
        //InflictDamageToBoss("dark", 2600, 0.5f);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayHolyProjectile();
        gameManager.holyProjectileCount--;
    }
    public void HolyGround()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        GameObject holyGroundEffect = Instantiate(holyGround, bossBody.transform.position, Quaternion.identity);
        //InflictDamageToBoss("dark", 3500, 0.5f);
        CheckWhatBossToHitWithSpell(0.5f, IncreaseMagicDamage(3500), "dark");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayHolyGroundClip();
        gameManager.holyGroundCount--;
    }
    public void FireExplosion()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        GameObject fireExplosionEffect = Instantiate(fireExplosion, bossHead.transform.position, Quaternion.identity);
        //InflictDamageToBoss("fire", 2300, 0.5f);
        CheckWhatBossToHitWithSpell(0.1f, IncreaseMagicDamage(2300), "fire");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        audioPlayer.PlayFireExplosionClip();
        gameManager.fireExplosionCount--;
    }
    public void NukeExplosion()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        GameObject nukeExplosionEffect = Instantiate(nukeExplosion, bossBody.transform.position, Quaternion.identity);
        //InflictDamageToBoss("fire", 3000, 0.5f);
        CheckWhatBossToHitWithSpell(0.5f, IncreaseMagicDamage(3000), "fire");
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        TimesHit("spell");
        Destroy(nukeExplosionEffect, 1.5f);
        NukeExplosionClip();
        gameManager.nukeExplosionCount--;
    }
    public void HolyShield()
    {      
        CheckForPositiveEffects("- Holy Barrier");
        animator.SetBool("isCasting", true);
        hasHolyBarrier = true;
        ResetHolyBarrierTurns();
        Vector3 correctPosition = new Vector3(0, -0.5f, 0);
        GameObject holyShieldEffect = Instantiate(holyShield, transform.position + correctPosition, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        gameManager.holyBarrierCount--;
    }
    public void HasteSpell()
    {
        CheckForPositiveEffects("- Haste");
        hasHaste = true;
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject hasteSpellEffect = Instantiate(hasteSpell, transform.position + abovePlayer, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        Destroy(hasteSpellEffect, 1);
        gameManager.hasteCount--;
        audioPlayer.PlayHasteClip();
    }
    
    public void SlowSpell()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject slowSpellEffect = Instantiate(slowSpell, bossAboveHead.transform.position, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        Destroy(slowSpellEffect, 2);
        if (defender.hasSlow) { defenderBattleSystem.BossImmune(); return; }
        SlowBoss();
        gameManager.slowCount--;
        audioPlayer.PlaySlowClip();
    }
    public void PoisonSpell()
    {
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject poisonSpellEffect = Instantiate(poisonSpell, bossAboveHead.transform.position, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        Destroy(poisonSpellEffect, 2);
        if (defender.hasPoison) { defenderBattleSystem.BossImmune(); return; }
        PoisonBoss();
        audioPlayer.PlayPoisonClip();
        gameManager.poisonCount--;
    }
    public void OneHitShield()
    {
        CheckForPositiveEffects("- One Hit Shield");
        animator.SetBool("isCasting", true);
        hasOneHitShield = true;
        GameObject oneHitShieldEffect = Instantiate(oneHitShield, transform.position, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        audioPlayer.PlayOneHitShieldClip();
        gameManager.oneHitShieldCount--;
    }
    public void ProtectShield()
    {
        CheckForPositiveEffects("- Protect");
        animator.SetBool("isCasting", true);
        hasProtect = true;
        GameObject protectShieldEffect = Instantiate(protectShield, transform.position, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        audioPlayer.PlayProtectClip();
        gameManager.protectCount--;
    }
    public void DoubleHpSpell()
    {
        if (hasDoubleHp ||hpHalved) 
        {
            battleText.text = "Cant use!";
            StartCoroutine(WaitAndTurnOffBattleText(2));
            return;
        }
        hasDoubleHp = true;
        CheckForPositiveEffects("- Double Hp");
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject doubleHpSpellEffect = Instantiate(doubleHpSpell, transform.position + abovePlayer, Quaternion.identity);
        DoublePlayerHealth();
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        Destroy(doubleHpSpellEffect, 2);
        audioPlayer.PlayDoubleHpClip();
        gameManager.doubleHpCount--;
    }
    public void RegenSpell()
    {
        CheckForPositiveEffects("- Regen");
        hasRegen = true;
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject regenSpellEffect = Instantiate(regenSpell, transform.position + abovePlayer, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        Destroy(regenSpellEffect, 2);
        audioPlayer.PlayHealClip();
        gameManager.regenCount--;
    }
    public void DispelBoss()
    {
        if (reaperBattleSystem.reaperStartBattle)
        {
            battleText.text = "Cant use!";
            StartCoroutine(WaitAndTurnOffBattleText(2));
            return;
        }
        SetSpellPositionToHit();
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject dispelSpellEffect = Instantiate(dispel, bossAboveHead.transform.position, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        DispelBosses();
        Destroy(dispelSpellEffect, 2);
        gameManager.dispelCount--;
        audioPlayer.PlayDispelClip();
    }
    public void SelfDispel()
    {
        if (reaperBattleSystem.reaperStartBattle) 
        {
            battleText.text = "Cant use!";
            StartCoroutine(WaitAndTurnOffBattleText(2));
            return;
        }
        animator.SetBool("isCasting", true);
        Vector3 abovePlayer = new Vector3(0, 0.2f, 0);
        GameObject dispelSpellEffect = Instantiate(dispel, transform.position + abovePlayer, Quaternion.identity);
        StartCoroutine(WaitAndReturnToIdle("isCasting", 0.4f));
        DispelPlayer();
        Destroy(dispelSpellEffect, 2);
        gameManager.dispelCount--;
        audioPlayer.PlayDispelClip();
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Methods called by Enemies that effect player

    public void PullPlayerForSpellHit()                 //Not called for now, would be used by enemies to move player to certial location to get hit by a spell
    {
        Vector2 difference = new Vector2(0.1f, 0.3f);
        Vector2 positionToMovePlayerTo = boss.GetCurrentBossPosition() - difference;
        transform.position = positionToMovePlayerTo;
    }
    public void PushPlayerAway()                        //Not called for now, would be used by enemies who have powerfull attacks, and would push player back
    {
        Vector3 difference = new Vector3(0.1f, 0, 0);
        transform.position -= difference;
        animator.SetBool("isHurt", true);
        StartCoroutine(WaitAndReturnToIdle("isHurt", 0.5f));
    }
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Wait for time methods
    IEnumerator WaitAndReturnToIdle(string currentAnimation, float delayAmmount)        //Called as Corutine in every Attack or Spell method, takes animation name, 
    {                                                                                   //and delay time to end the animation
        yield return new WaitForSeconds(delayAmmount);      
        CheckPlayerPosition(currentAnimation);
        animator.SetBool(currentAnimation, false);
        animator.SetBool("returnToIdle", true);
        if(currentAnimation == "isHurt") { yield break; }
        actionIsTaken = true;                                                           //actionIsTaken is set to true here, because it is used in every Attack and Spell method
        
    }
    IEnumerator WaitForPlayerDeath(float delay)                                         //Waits certian time before starting player death animation
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("swordIdle", false);
        animator.SetBool("isDefending", false);
        animator.SetBool("isDead", true);
        gameManager.playerIsAlive = false;
    }
    IEnumerator WaitForDeathCanvas()
    {
        yield return new WaitForSeconds(1.9f);
        deathCanvas.SetActive(true);
    }
    IEnumerator WaitAndTurnOffBattleText(float delay)
    {
        yield return new WaitForSeconds(delay);
        actionIsTaken = true;
        battleText.text = "";
    }
    IEnumerator WaitAndHurtBoss(float delay, int damage)
    {
        yield return new WaitForSeconds(delay);
        boss.BossIsHurt();
        battle.IsHit("Boss", damage);
    }
    IEnumerator WaitAndHurtDefender(float delay, int damage)
    {
        yield return new WaitForSeconds(delay);
        defender.HurtDefender();
        defenderBattleSystem.IsHit("Boss", damage);
    }
    IEnumerator WaitAndHurtReaper(float delay, int damage, string type)
    {
        if(type == "dark") { damage *= 2; }
        yield return new WaitForSeconds(delay);
        reaper.HurtReaper();
        reaperBattleSystem.IsHit("Boss", damage, true);
    }

    IEnumerator WaitAndStartNewIdle(float delay, string animation)
    {
        yield return new WaitForSeconds(delay);
        if(animation == "drawSword")
        {
            animator.SetBool("drawSword", false);
            animator.SetBool("swordIdle", true);
        }
        else if(animation == "sheatSword")
        {
            animator.SetBool("sheatSword", false);
        }       
    }
   
    IEnumerator WaitAndMoveForAttack(float delay, bool didAttack)
    {
        callCorutine = false;
        yield return new WaitForSeconds(delay);
        clearToMove = true;
        if (didAttack)
        {
            attackedWithSword = true;
            clearToMove = false;
            callCorutine = true;
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Methods used less frequently
    void SetOriginalPosition()                                                          //Saves position where battle was triggered
    {
        if(originalPositionIsSet == false)
        {
            Vector3 addToPosition = new Vector3(0.2f, 0,0);
            originalPlayerPosition = transform.position + addToPosition;
            originalPositionIsSet = true;
        }
    }
    void CheckPlayerPosition(string currentAnimation)                                   //Called in WaitAndReturnToIdle() Corutine, if player did regular attack,
    {                                                                                   //he was moved closer to boss, and this methods puts him back, close to
        if (currentAnimation == "isAttacking")                                          //where he was before the attack
        {
            transform.position = originalPlayerPosition;         
        }
    }

    void DisplayPositiveStatusEffects()
    {
        if (battle.StartBattle() == false && defenderBattleSystem.defenderBattleStarted == false && reaperBattleSystem.reaperStartBattle == false) { return; }
        if (playerPositiveStatusList.Count == 0)
        {
            playerStatus.text = "";           
        }
        if(playerPositiveStatusList.Count == 1)
        {
            playerStatus.text = playerPositiveStatusList[0];
        }
        if (playerPositiveStatusList.Count == 2)
        {
            playerStatus.text = playerPositiveStatusList[0] + lineBreak + playerPositiveStatusList[1];
        }
        if (playerPositiveStatusList.Count == 3)
        {
            playerStatus.text = playerPositiveStatusList[0] + lineBreak + playerPositiveStatusList[1] + lineBreak + playerPositiveStatusList[2];
        }
        if (playerPositiveStatusList.Count == 4)
        {
            playerStatus.text = playerPositiveStatusList[0] + lineBreak + playerPositiveStatusList[1] + lineBreak + playerPositiveStatusList[2] + lineBreak
                 + playerPositiveStatusList[3];
        }
        if (playerPositiveStatusList.Count == 5)
        {
            playerStatus.text = playerPositiveStatusList[0] + lineBreak + playerPositiveStatusList[1] + lineBreak + playerPositiveStatusList[2] + lineBreak
                 + playerPositiveStatusList[3] + lineBreak + playerPositiveStatusList[4];
        }

    }
    void InflictDamageToBoss(string phase, int damage, float delay)
    {
        if (battle.GetPhase() == phase)
        {
            StartCoroutine(WaitAndHurtBoss(delay, damage * 2));
            battle.timesHitWithWeakToElement++;
        }
        else
        {
            StartCoroutine(WaitAndHurtBoss(delay, damage));
        }
    }

    void MovePlayerForSwordAttack()
    {
        
        if (isAttackingWithSword)
        {
            SetPositionToHit(defenderPositionToHit, reaperPositionToHit, BringerOfDeathPositionToHit);
            animator.SetBool("swordIdle", false);
            animator.SetBool("sheatSword", true);
            if (playSheatClip) { playSheatClip = false; audioPlayer.PlaySheatSwordClip(); }
            StartCoroutine(WaitAndStartNewIdle(0.4f, "sheatSword"));
            if (callCorutine)
            {
                StartCoroutine(WaitAndMoveForAttack(0.5f, false));
            }            
            if (clearToMove)
            {               
                transform.position = Vector3.MoveTowards(transform.position, bossPositionToHit.transform.position, 0.015f);
                animator.SetBool("isWalking", true);
                if (transform.position == bossPositionToHit.transform.position)
                {
                    animator.SetBool("isAttacking", true);
                    //StartCoroutine(WaitAndHurtBoss(0.1f, 1700));
                    CheckWhatBossToHit(0.1f, IncreasePhysicalDamage(999999));
                    audioPlayer.PlaySwing1Clip();
                    StartCoroutine(WaitAndStopAttackAnimation(0.45f, "isAttacking"));
                    isAttackingWithSword = false;
                    clearToMove = false;
                    callCorutine = true;                  
                    StartCoroutine(WaitAndMoveForAttack(0.5f, true));
                    
                }
            }           
        }
        else if (attackedWithSword)
        {
            animator.SetBool("swordIdle", false);
            animator.SetBool("sheatSword", true);
            StartCoroutine(WaitAndStartNewIdle(0.4f, "sheatSword"));
            if (callCorutine)
            {
                StartCoroutine(WaitAndMoveForAttack(0.5f, false));
            }
            
            if (clearToMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPosition, 0.015f);
                animator.SetBool("isWalking", true);
                transform.localScale = new Vector3(-1, 1, 1);
                if (transform.position == currentPosition && clearToMove)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    animator.SetBool("drawSword", true);
                    StartCoroutine(WaitAndStartNewIdle(0.5f, "drawSword"));
                    attackedWithSword = false;
                    clearToMove = false;
                    isAttackingWithSword = false;
                    actionIsTaken = true;
                    callCorutine = true;
                    playSheatClip = true;
                }
            }
        }
    }

    void MovePlayerForJumpSmashAttack()
    {
        if (jumpSmashAttack)
        {
            SetPositionToHit(defenderPositionToHitJumpAttack, reaperPositionToHitJumpAttack, BringerOfDeathPositionToHitJumpAttack);
            Vector3 aboveBoss = new Vector3(0.25f, 0.5f, 0);
            if (wasAtFeetPosition == false)
            {
                animator.SetBool("swordIdle", false);
                animator.SetBool("sheatSword", true);
                if (playSheatClip) { playSheatClip = false; audioPlayer.PlaySheatSwordClip(); }
                StartCoroutine(WaitAndStartNewIdle(0.4f, "sheatSword"));
                if (callCorutine)
                {
                    StartCoroutine(WaitAndMoveForAttack(0.5f, false));
                }
                if (clearToMove)
                {                    
                    transform.position = Vector3.MoveTowards(transform.position, bossPositionToHit.transform.position, 0.015f);
                    animator.SetBool("isWalking", true);
                }                     
            }
            if (transform.position == bossPositionToHit.transform.position || wasAtFeetPosition && wasAtHeadPosition == false)
            {               
                wasAtFeetPosition = true;
                transform.position = Vector3.MoveTowards(transform.position, bossPositionToHit.transform.position + aboveBoss, 0.035f);
                animator.SetBool("isWalking", false);
                animator.SetBool("returnToIdle", false);
                animator.SetBool("isJumping", true);
            }
            if(transform.position == bossPositionToHit.transform.position + aboveBoss || wasAtHeadPosition && airAttack1End == false)
            {
                if (!bossIsHit)
                {
                    //StartCoroutine(WaitAndHurtBoss(0.1f, 1650));
                    CheckWhatBossToHit(0.1f, IncreasePhysicalDamage(1650));
                    audioPlayer.PlaySwing1Clip();
                    bossIsHit = true;
                }               
                wasAtHeadPosition = true;
                transform.position = Vector3.MoveTowards(transform.position, bossPositionToHit.transform.position + new Vector3(0.2f, -0.05f, 0), 0.05f);
                animator.SetBool("isJumping", false);
                animator.SetBool("airAttack1", true);
            }
            if(transform.position == bossPositionToHit.transform.position + new Vector3(0.2f, -0.05f, 0) && airAttack1End == false)
            {               
                airAttack1End = true;
                animator.SetBool("airAttack1", false);
                animator.SetBool("airAttack1End", true);
                //StartCoroutine(WaitAndHurtBoss(0.1f, 890));
                CheckWhatBossToHit(0.1f, IncreasePhysicalDamage(890));
                audioPlayer.PlaySwing2Clip();
                StartCoroutine(WaitAndStopAttackAnimation(0.5f, "airAttack1End"));              
            }
            if (runBack)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPosition, 0.015f);
                animator.SetBool("isWalking", true);
                transform.localScale = new Vector3(-1, 1, 1);
                if (transform.position == currentPosition && clearToMove)
                {
                    runBack = false;
                    transform.localScale = new Vector3(1, 1, 1);
                    animator.SetBool("drawSword", true);
                    StartCoroutine(WaitAndStartNewIdle(0.5f, "drawSword"));
                    clearToMove = false;
                    jumpSmashAttack = false;
                    airAttack1End = false;
                    wasAtFeetPosition = false;
                    wasAtHeadPosition = false;
                    callCorutine = true;
                    playSheatClip = true;
                    bossIsHit = false;
                    actionIsTaken = true;
                }
            }
        }
    }

    void MovePlayerForComboAttack()
    {
        if (comboAttack)
        {
            SetPositionToHit(defenderPositionToHit, reaperPositionToHit, BringerOfDeathPositionToHit);
            if (movingToBoss)
            {
                movingToBoss = false;
                animator.SetBool("swordIdle", false);
                animator.SetBool("sheatSword", true);
                if (playSheatClip) { playSheatClip = false; audioPlayer.PlaySheatSwordClip(); }
                StartCoroutine(WaitAndStartNewIdle(0.4f, "sheatSword"));

            }
            
            if (callCorutine)
            {
                StartCoroutine(WaitAndMoveForAttack(0.5f, false));
            }
            if (clearToMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, bossPositionToHit.transform.position, 0.015f);
                animator.SetBool("isWalking", true);
            }
            if(transform.position == bossPositionToHit.transform.position && clearToMove)
            {
                clearToMove = false;
                animator.SetBool("returnToIdle", false);
                animator.SetBool("comboHit1", true);
                animator.SetBool("isWalking", false);
                //StartCoroutine(WaitAndHurtBoss(0.1f, 1000));
                CheckWhatBossToHit(0.1f, IncreasePhysicalDamage(1000));
                audioPlayer.PlaySwing1Clip();
                StartCoroutine(WaitAndDoComboAttack(0.5f));
            }
            if (attackIsOver)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentPosition, 0.015f);
                animator.SetBool("isWalking", true);
                transform.localScale = new Vector3(-1, 1, 1);
                if (transform.position == currentPosition && attackIsOver)
                {
                    attackIsOver = false;
                    transform.localScale = new Vector3(1, 1, 1);
                    animator.SetBool("drawSword", true);
                    StartCoroutine(WaitAndStartNewIdle(0.5f, "drawSword"));
                    comboAttack = false;
                    clearToMove = false;
                    callCorutine = true;
                    actionIsTaken = true;
                    playSheatClip = true;
                }
            }
        }
    }

    public void MovePlayerBackDefenderFight()
    {
        if (retreatToFirstPosition)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = Vector3.MoveTowards(transform.position, defenderFightFirstPosition.transform.position, 0.015f);
        }
        if(transform.position == defenderFightFirstPosition.transform.position && retreatToFirstPosition)
        {
            retreatToFirstPosition = false;
            transform.localScale = new Vector3(1, 1, 1);
            actionIsTaken = true;
            defenderBattleSystem.playerfirstPosition = true;
        }
        if (retreatToSecondPosition)
        {
            animator.SetBool("isWalking", true);
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = Vector3.MoveTowards(transform.position, defenderFightSecondPosition.transform.position, 0.015f);
        }
        if(transform.position == defenderFightSecondPosition.transform.position && retreatToSecondPosition)
        {
            retreatToSecondPosition = false;
            transform.localScale = new Vector3(1, 1, 1);
            actionIsTaken = true;
            defenderBattleSystem.playersecondPosition = true;
            retreat.SetActive(false);
        }
       
    }

    void SetPositionToHit(GameObject positionToHitDefender, GameObject positionToHitReaper, GameObject positionToHitBringer)
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            bossPositionToHit = positionToHitDefender;
            bossAboveHead = defenderAboveHead;
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            bossPositionToHit = positionToHitReaper;
            bossAboveHead = reaperAboveHead;
        }
        else if (battle.bringerFightStart)
        {
            bossPositionToHit = positionToHitBringer;
            bossAboveHead = BringerOfDeathAboveHead;
        }
    }
    void SetSpellPositionToHit()
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            bossAboveHead = defenderAboveHead;
            bossBody = defenderBody;
            bossInfront = defenderInfront;
            bossLegs = defenderLegs;
            bossHead = defenderHead;
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            bossAboveHead = reaperAboveHead;
            bossBody = reaperBody;
            bossInfront = reaperInfront;
            bossLegs = reaperLegs;
            bossHead = reaperHead;
        }
        else if (battle.bringerFightStart)
        {
            bossAboveHead = BringerOfDeathAboveHead;
            bossBody = BringerOfDeathBody;
            bossInfront = BringerOfDeathInfront;
            bossLegs = BringerOfDeathLegs;
            bossHead = BringerOfDeathHead;
        }
    }
    void HurtDefender(float delay, int damage)
    {
        StartCoroutine(WaitAndHurtDefender(delay, damage));
    }
    void HurtReaper(float delay, int damage, string type)
    {
        StartCoroutine(WaitAndHurtReaper(delay, damage, type));
    }

    void CheckWhatBossToHit(float delay, int damage)
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            HurtDefender(delay, damage);
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            HurtReaper(delay, damage, "regular");
        }
        else if(battle.bringerFightStart)
        {
            StartCoroutine(WaitAndHurtBoss(delay, damage));
        }
    }
    public void CheckWhatBossToHitWithSpell(float delay, int damage, string type)
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            HurtDefender(delay, damage);
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            HurtReaper(delay, damage, type);
        }
        else
        {
            InflictDamageToBoss(type, damage, delay);
        }
    }

    public void CheckForNegativeStatuses(string effect)
    {
        if(effect == "Bleed" && hasBleed == false)
        {
            playerNegativeStatusList.Add(effect);
        }
        else if(effect == "Poison" && hasPoison == false)
        {
            playerNegativeStatusList.Add(effect);
        }
        else if (effect == "Slow" && hasSlow == false)
        {
            playerNegativeStatusList.Add(effect);
        }
        else if(effect == "Damage Break")
        {
            playerNegativeStatusList.Add(effect);
        }
        else if(effect == "Defence Break")
        {
            playerNegativeStatusList.Add(effect);
        }
        else if (effect == "Magic Break")
        {
            playerNegativeStatusList.Add(effect);
        }
        else if (effect == "Hp Halved")
        {
            playerNegativeStatusList.Add(effect);
        }
        else if (effect == "Heal Break")
        {
            playerNegativeStatusList.Add(effect);
        }
        else if (effect == "Max Hp Reduced")
        {
            playerNegativeStatusList.Add(effect);
        }
    }
    public void CheckForPositiveEffects(string effect)
    {
        if(effect == "- Protect")
        {
            playerPositiveStatusList.Add(effect);
        }
        if (effect == "- One Hit Shield")
        {
            playerPositiveStatusList.Add(effect);
        }
        if (effect == "- Double Hp")
        {
            playerPositiveStatusList.Add(effect);
        }
        if (effect == "- Regen")
        {
            playerPositiveStatusList.Add(effect);
        }
        if (effect == "- Holy Barrier")
        {
            playerPositiveStatusList.Add(effect);
        }
        if (effect == "- Haste")
        {
            playerPositiveStatusList.Add(effect);
        }
    }

    void ShowNegativeStatuses()
    {
        if(battle.StartBattle() == false && defenderBattleSystem.defenderBattleStarted == false && reaperBattleSystem.reaperStartBattle == false) { return; }
        if(playerNegativeStatusList.Count == 0)
        {
            playerNegativeStatus.text = "";
        }
        if(playerNegativeStatusList.Count == 1)
        {
            playerNegativeStatus.text = playerNegativeStatusList[0];
        }
        else if(playerNegativeStatusList.Count == 2)
        {
            playerNegativeStatus.text = playerNegativeStatusList[0] + lineBreak + playerNegativeStatusList[1];
        }
        else if(playerNegativeStatusList.Count == 3)
        {
            playerNegativeStatus.text = playerNegativeStatusList[0] + lineBreak + playerNegativeStatusList[1] + lineBreak + playerNegativeStatusList[2];
        }
        else if (playerNegativeStatusList.Count == 4)
        {
            playerNegativeStatus.text = playerNegativeStatusList[0] + lineBreak + playerNegativeStatusList[1] + lineBreak + playerNegativeStatusList[2] + lineBreak + playerNegativeStatusList[3];
        }
        else if (playerNegativeStatusList.Count == 5)
        {
            playerNegativeStatus.text = playerNegativeStatusList[0] + lineBreak + playerNegativeStatusList[1] + lineBreak + 
                playerNegativeStatusList[2] + lineBreak + playerNegativeStatusList[3] + lineBreak + playerNegativeStatusList[4];
        }
        else if (playerNegativeStatusList.Count == 6)
        {
            playerNegativeStatus.text = playerNegativeStatusList[0] + lineBreak + playerNegativeStatusList[1] + lineBreak +
                playerNegativeStatusList[2] + lineBreak + playerNegativeStatusList[3] + lineBreak + playerNegativeStatusList[4] + lineBreak + playerNegativeStatusList[5];
        }
        else if (playerNegativeStatusList.Count == 7)
        {
            playerNegativeStatus.text = playerNegativeStatusList[0] + lineBreak + playerNegativeStatusList[1] + lineBreak +
                playerNegativeStatusList[2] + lineBreak + playerNegativeStatusList[3] + lineBreak + playerNegativeStatusList[4] + lineBreak + playerNegativeStatusList[5] + lineBreak + playerNegativeStatusList[6];
        }
    }
    public void RemoveNegativeStatus(string status)
    {
        for (int i = 0; i < playerNegativeStatusList.Count; i++)
        {
            if (playerNegativeStatusList[i] == status)
            {
                playerNegativeStatusList.Remove(status);
            }
        }
    }
    public void RemovePositiveStatus(string status)
    {
        for (int i = 0; i < playerPositiveStatusList.Count; i++)
        {
            if (playerPositiveStatusList[i] == status)
            {
                playerPositiveStatusList.Remove(status);
            }
        }
    }

    void BreakBossDamage()
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            defender.damageBreak = true;
            defender.CheckForDefenderNegativeEffects("- Damage Break");
            defenderBattleSystem.CheckForCounterAction(true, "Break Damage");
            defenderBattleSystem.generatedNumbers.Add(3);
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            reaper.damageBreak = true;
            reaper.CheckForReaperNegativeEffects("- Damage Break");
        }
        else if (battle.bringerFightStart)
        {
            boss.BreakBossDamage();
            boss.CheckForBringerNegativeEffects("- Damage Break");
        }
    }
    void BreakBossDefenceSpell()
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            defender.defenceBreak = true;
            defender.CheckForDefenderNegativeEffects("- Defence Break");
            defenderBattleSystem.CheckForCounterAction(true, "Break Defence");
            defenderBattleSystem.generatedNumbers.Add(4);
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            reaperBattleSystem.reaperDefenceBreak = true;
            reaper.CheckForReaperNegativeEffects("Break Defence");
        }
        else if (battle.bringerFightStart)
        {
            boss.defenceBreak = true;
            boss.CheckForBringerNegativeEffects("- Defence Break");
        }
    }
    void SlowBoss()
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            defender.hasSlow = true;
            defender.CheckForDefenderNegativeEffects("- Slow");
            defenderBattleSystem.CheckForCounterAction(true, "Slow");
            defenderBattleSystem.generatedNumbers.Add(2);
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            reaper.hasSlow = true;
            reaper.CheckForReaperNegativeEffects("- Slow");
        }
        else if (battle.bringerFightStart)
        {
            boss.SlowBoss();
            boss.CheckForBringerNegativeEffects("- Slow");
        }
    }
    void PoisonBoss()
    {

        if (defenderBattleSystem.defenderBattleStarted)
        {
            defender.hasPoison = true;
            defender.CheckForDefenderNegativeEffects("- Poison");
            defenderBattleSystem.CheckForCounterAction(true, "Poison");
            defenderBattleSystem.generatedNumbers.Add(1);
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            reaper.hasPoison = true;
            reaper.CheckForReaperNegativeEffects("- Poison");
        }
        else if (battle.bringerFightStart)
        {
            boss.PoisonBoss();
            boss.CheckForBringerNegativeEffects("- Poison");
        }
    }
    void DoublePlayerHealth()
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            defenderBattleSystem.DoubleHp();
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            reaperBattleSystem.DoubleHp();
        }
        else if (battle.bringerFightStart)
        {
            battle.DoubleHp();
        }
    }
    void HealPlayerHealth(int healAmmount)
    {
        if (healBreaK) { healAmmount /= 2; }
        if (defenderBattleSystem.defenderBattleStarted)
        {
            defenderBattleSystem.HealPlayer(IncreaseMagicDamage(healAmmount));
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            reaperBattleSystem.HealPlayer(IncreaseMagicDamage(healAmmount));
        }
        else if (battle.bringerFightStart)
        {
            battle.HealPlayer(IncreaseMagicDamage(healAmmount));
        }
    }
    void DispelBosses()
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            defender.DispelDefender();
            defenderBattleSystem.CheckForCounterAction(true, "Dispel");
        }
        else if (reaperBattleSystem.reaperStartBattle)
        {
            reaper.DispelReaper();
        }
        else if (battle.bringerFightStart)
        {
            boss.DispelBoss();
        }
    }

    void TimesHit(string type)
    {
        if (battle.bringerFightStart && type == "spell")
        {
            battle.timesHitWithMagic++;
        }
        if(battle.bringerFightStart && type == "regular")
        {
            battle.timesHitWithSword++;
        }
        if (reaperBattleSystem.reaperStartBattle)
        {
            reaperBattleSystem.reaperTimesHit++;
        }
    }

    int IncreasePhysicalDamage(int damage)
    {
        if (defenderBattleSystem.defenderBattleStarted)
        {
            damage += (damage * 35) / 100;
        }
        if (gameManager.playerPhysicalDamage == 0)
        {
            damage *= 1;
        }
        if (gameManager.playerPhysicalDamage == 1)
        {
            damage += (damage * 6) / 100;
        }
        if (gameManager.playerPhysicalDamage == 2)
        {
            damage += (damage * 12) / 100;
        }
        if (gameManager.playerPhysicalDamage == 3)
        {
            damage += (damage * 18) / 100;
        }
        if (gameManager.playerPhysicalDamage == 4)
        {
            damage += (damage * 24) / 100;
        }
        if (gameManager.playerPhysicalDamage == 5)
        {
            damage += (damage * 30) / 100;
        }
        if (gameManager.playerPhysicalDamage == 6)
        {
            damage += (damage * 35) / 100;
        }
        if (gameManager.playerPhysicalDamage == 7)
        {
            damage += (damage * 40) / 100;
        }
        if (gameManager.playerPhysicalDamage == 8)
        {
            damage += (damage * 43) / 100;
        }
        if (gameManager.playerPhysicalDamage == 9)
        {
            damage += (damage * 47) / 100;
        }
        if (gameManager.playerPhysicalDamage == 10)
        {
            damage += (damage * 50) / 100;
        }
        return damage;
    }
    public int IncreaseMagicDamage(int damage)
    {
        if (gameManager.playerMagicDamage == 0)
        {
            damage *= 1;
        }
        if (gameManager.playerMagicDamage == 1)
        {
            damage += (damage * 6) / 100;
        }
        if (gameManager.playerMagicDamage == 2)
        {
            damage += (damage * 12) / 100;
        }
        if (gameManager.playerMagicDamage == 3)
        {
            damage += (damage * 18) / 100;
        }
        if (gameManager.playerMagicDamage == 4)
        {
            damage += (damage * 24) / 100;
        }
        if (gameManager.playerMagicDamage == 5)
        {
            damage += (damage * 30) / 100;
        }
        if (gameManager.playerMagicDamage == 6)
        {
            damage += (damage * 35) / 100;
        }
        if (gameManager.playerMagicDamage == 7)
        {
            damage += (damage * 40) / 100;
        }
        if (gameManager.playerMagicDamage == 8)
        {
            damage += (damage * 43) / 100;
        }
        if (gameManager.playerMagicDamage == 9)
        {
            damage += (damage * 47) / 100;
        }
        if (gameManager.playerMagicDamage == 10)
        {
            damage += (damage * 50) / 100;
        }
        return damage;
    }
    void DispelFullBreak()
    {
        if (playerBroken)
        {
            boss.RemovePlayerBreaks();
        }
    }

    public void ClearNegativeEffects()
    {
        playerNegativeStatusList.Clear();
        playerNegativeStatus.text = "";
    }
    public void ClearPositiveEffects()
    {
        playerPositiveStatusList.Clear();
        playerStatus.text = "";
    }
    public void ClearStatusesInDefenderBattle()
    {
        defenderBattleSystem.generatedNumbers.Remove(1);
        defenderBattleSystem.generatedNumbers.Remove(2);
        defenderBattleSystem.generatedNumbers.Remove(3);
        defenderBattleSystem.generatedNumbers.Remove(4);
        defenderBattleSystem.generatedNumbers.Remove(5);
        defenderBattleSystem.generatedNumbers.Remove(6);
    }
    void NukeExplosionClip()
    {
        audioPlayer.PlayNukeShineClip();
        StartCoroutine(WaitAndDoNukeExplosion(0.2f));
    }
    void ResetHolyBarrierTurns()
    {
        battle.holyBarrierTurns = 4;
        defenderBattleSystem.holyBarrierTurns = 4;
        reaperBattleSystem.holyBarrierTurns = 4;
    }
    IEnumerator WaitAndDoNukeExplosion(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioPlayer.PlayNukeExplosionClip();
    }
    IEnumerator WaitAndReturnToSwordIdle(float delay, string animation)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("returnToIdle", false);
        animator.SetBool("swordIdle", true);
        animator.SetBool(animation, false);       
    }

    IEnumerator WaitAndStopAttackAnimation(float delay, string animation)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(animation, false);
        animator.SetBool("swordIdle", true);
        runBack = true;
    }

    IEnumerator WaitAndDoComboAttack(float delay)
    {
        yield return new WaitForSeconds(delay);       
        animator.SetBool("comboHit2", true);
        animator.SetBool("comboHit1", false);
        //StartCoroutine(WaitAndHurtBoss(0.3f, 1000));
        CheckWhatBossToHit(0.3f, IncreasePhysicalDamage(1000));
        audioPlayer.PlaySwing2Clip();
        yield return new WaitForSeconds(delay + 0.2f);
        animator.SetBool("comboHit3", true);
        animator.SetBool("comboHit2", false);
        //StartCoroutine(WaitAndHurtBoss(0.4f, 1000));
        CheckWhatBossToHit(0.4f, IncreasePhysicalDamage(1000));
        audioPlayer.PlaySwing3Clip();
        StartCoroutine(WaitAndSheatSword(0.5f, "comboHit3"));
        StartCoroutine(WaitAndStartNewIdle(0.9f, "sheatSword"));
    }

    IEnumerator WaitAndSheatSword(float delay, string animation)
    {
        yield return new WaitForSeconds(delay);       
        animator.SetBool(animation, false);
        animator.SetBool("sheatSword", true);
        yield return new WaitForSeconds(0.4f);
        attackIsOver = true;
    }
    public void SheatSwordFromOtherScript()
    {
        StartCoroutine(WaitAndSheatSword(0, "swordIdle"));
    }

    IEnumerator WaitAndSetActionTaken(float delay)
    {
        yield return new WaitForSeconds(delay);
        actionIsTaken = true;
    }


    void DoesNothing()
    {
        //Removes console warnings
        if (hasHaste) { return; }
        if (hasProtect) { return; }
        if (hasOneHitShield) { return; }
        if (hasHolyBarrier) { return; }
        if (hasDoubleHp) { return; }
    }
    


}
