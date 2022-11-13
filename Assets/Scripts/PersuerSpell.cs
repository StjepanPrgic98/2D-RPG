using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersuerSpell : MonoBehaviour
{
    Boss boss;
    PlayerMovement player;
    Animator animator;
    Battle battle;
    AudioPlayer audioPlayer;
    GameManager gameManager;

    float speed = 0.01f;
    bool didHit;

    private void Awake()
    {
        boss = FindObjectOfType<Boss>();
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        battle = FindObjectOfType<Battle>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (didHit) { return; }
        FollowTarget();
    }

    void FollowTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.PlayerIsHurt();
        battle.IsHit("Player", boss.ReduceDamage(700));
        didHit = true;
        animator.SetBool("didHit", true);
        Destroy(gameObject, 0.5f);
        audioPlayer.PlaySlapClip();
        ReduceSpellAmmout();
    }

    void ReduceSpellAmmout()
    {
        int randomNumber = Random.Range(1, 20);
        if(randomNumber == 1)
        {
            gameManager.holyBarrierCount--;
        }
        if (randomNumber == 2)
        {
            gameManager.damageBreakCount--;
        }
        if (randomNumber == 3)
        {
            gameManager.doubleHpCount--;
        }
        if (randomNumber == 4)
        {
            gameManager.healCount--;
        }
        if (randomNumber == 5)
        {
            gameManager.regenCount--;
        }
        if (randomNumber == 6)
        {
            gameManager.oneHitShieldCount--;
        }
        if (randomNumber == 7)
        {
            gameManager.protectCount--;
        }
        if (randomNumber == 8)
        {
            gameManager.fireExplosionCount--;
        }
        if (randomNumber == 9)
        {
            gameManager.nukeExplosionCount--;
        }
        if (randomNumber == 10)
        {
            gameManager.waterProjectileCount--;
        }
        if (randomNumber == 11)
        {
            gameManager.waterTornadoCount--;
        }
        if (randomNumber == 12)
        {
            gameManager.thunderProjectileCount--;
        }
        if (randomNumber == 13)
        {
            gameManager.thunderHawkCount--;
        }
        if (randomNumber == 14)
        {
            gameManager.thunderSplashCount--;
        }
        if (randomNumber == 15)
        {
            gameManager.thunderStrikeCount--;
        }
        if (randomNumber == 16)
        {
            gameManager.iceProjectileCount--;
        }
        if (randomNumber == 17)
        {
            gameManager.iceSplashCount--;
        }
        if (randomNumber == 18)
        {
            gameManager.iceGroundCount--;
        }
        if (randomNumber == 19)
        {
            gameManager.holyProjectileCount--;
        }
        if (randomNumber == 20)
        {
            gameManager.holyGroundCount--;
        }
        battle.ReduceSpellAmmountText();
    }
}
