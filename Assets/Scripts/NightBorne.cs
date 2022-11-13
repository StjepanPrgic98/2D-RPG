using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBorne : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    AudioPlayer audioPlayer;
    [SerializeField] GameObject nightBorneRunAudio;
    [SerializeField] GameObject nightBorneStaticAudio;



    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        if (gameManager.nightBorneDefeated)
        {
            gameObject.SetActive(false);
        }
    }


    public void BossRun()
    {
        animator.SetBool("isRunning", true);
        nightBorneRunAudio.SetActive(true);
        nightBorneStaticAudio.SetActive(false);
    }
    
    public void BossIdle(string animation)
    {
        if(animation == "isRunning")
        {
            animator.SetBool("isRunning", false);
            nightBorneRunAudio.SetActive(false);
            nightBorneStaticAudio.SetActive(true);
        }
    }
    public void BossDeath()
    {
        gameManager.nightBorneDefeated = true;
        animator.SetBool("isDead", true);
        audioPlayer.PlayNightBorneDeathClip();
        StartCoroutine(WaitAndExplodeBoss(0.3f));
        Destroy(gameObject,2);
    }
    public void BossHurt()
    {
        animator.SetBool("isHit", true);
        StartCoroutine(WaitAndReturnToIdle(0.5f, "isHit"));
    }
    public void BossAttack()
    {
        animator.SetBool("isAttacking", true);
        NightBorneAttackAudio();
        StartCoroutine(WaitAndReturnToIdle(0.8f, "isAttacking"));
    }

    public void FlipNightBorneSprite(string direction)
    {
        if(direction == "left")
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    void NightBorneAttackAudio()
    {
        StartCoroutine(WaitAndDoSwingSound(0.1f));
    }
    IEnumerator WaitAndDoSwingSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioPlayer.PlaySwing1Clip();
    }
    IEnumerator WaitAndExplodeBoss(float delay)
    {
        yield return new WaitForSeconds(delay);
        audioPlayer.PlayOblivionClip();
    }



    IEnumerator WaitAndReturnToIdle(float delay, string animation)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool(animation, false);
    }
}
