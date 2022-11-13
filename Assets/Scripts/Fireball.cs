using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] string spellName;
    [SerializeField] string type;
    [SerializeField] int damage;
    

    Rigidbody2D myRigidBody;
    Boss boss;
    Animator animator;
    DefenderBattleSystem defenderBattleSystem;
    ReaperBattleSystem reaperBattleSystem;
    Defender defender;
    Reaper reaper;
    PlayerMovement player;
    Battle battle;
    AudioPlayer audioPlayer;
    Failsafe failsafe;

    private void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        boss = FindObjectOfType<Boss>();
        animator = GetComponent<Animator>();
        defenderBattleSystem = FindObjectOfType<DefenderBattleSystem>();
        reaperBattleSystem = FindObjectOfType<ReaperBattleSystem>();
        defender = FindObjectOfType<Defender>();
        reaper = FindObjectOfType<Reaper>();
        player = FindObjectOfType<PlayerMovement>();
        battle = FindObjectOfType<Battle>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        failsafe = FindObjectOfType<Failsafe>();
    }

    private void Update()
    {
        MoveFireBall();
    }

    void MoveFireBall()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0) * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjects();
        if(spellName != null)
        {
            CheckWhatBossToHit();
            animator.SetBool("didHit", true);
            Destroy(gameObject, 1);
        }
        else
        {
            CheckWhatBossToHit();
            Destroy(gameObject);
        }
        
    }

    void CheckWhatBossToHit()
    {
        CheckForSFX();
        if(failsafe.defenderDestroyed == false)
        {
            if (defenderBattleSystem.defenderBattleStarted)
            {
                defender.HurtDefender();
                player.CheckWhatBossToHitWithSpell(0, player.IncreaseMagicDamage(damage), type);
            }
        }
        if(failsafe.reaperDestroyed == false)
        {
            if (reaperBattleSystem.reaperStartBattle)
            {
                reaper.HurtReaper();
                player.CheckWhatBossToHitWithSpell(0, player.IncreaseMagicDamage(damage), type);
            }
        }
        if(failsafe.bringerDestroyed == false)
        {
            if (battle.bringerFightStart)
            {
                boss.BossIsHurt();
                player.CheckWhatBossToHitWithSpell(0, player.IncreaseMagicDamage(damage), type);
            }
        }      
    }

    void CheckForSFX()
    {
        if(spellName == "ThunderHawk")
        {
            audioPlayer.PlayThunderHawkImpact();
        }
        if (spellName == "ThunderProjectile")
        {
            audioPlayer.PlayThunderHawkImpact();
        }
        if(spellName == "WaterProjectile")
        {
            audioPlayer.PlayWaterProjectileImpactClip();
        }
        if(spellName == "WindProjectile")
        {
            audioPlayer.PlayWindProjectileImpactClip();
        }
        if(spellName == "HolyProjectile")
        {
            audioPlayer.PlayHolyProjectileImpact();
        }
        if(spellName == "IceProjectile")
        {
            audioPlayer.PlayIceProjectieClip();
        }

    }

    void FindObjects()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        boss = FindObjectOfType<Boss>();
        animator = GetComponent<Animator>();
        defenderBattleSystem = FindObjectOfType<DefenderBattleSystem>();
        reaperBattleSystem = FindObjectOfType<ReaperBattleSystem>();
        defender = FindObjectOfType<Defender>();
        reaper = FindObjectOfType<Reaper>();
        player = FindObjectOfType<PlayerMovement>();
        battle = FindObjectOfType<Battle>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        failsafe = FindObjectOfType<Failsafe>();
    }
}
