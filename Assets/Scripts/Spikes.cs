using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    [SerializeField] Sprite closedSpike;
    [SerializeField] Sprite openSpike;
    [SerializeField] Button button;
    [SerializeField] NightBorne nightBorne;
    [SerializeField] PathFinder pathFinder;
    [SerializeField] NightBorneBattleSystem nightBorneBattleSystem;
    [HideInInspector] public bool ableToHurtBoss = true;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    AudioPlayer audioPlayer;

    bool playAudio = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }





    private void Update()
    {
        if (button.isClicked)
        {
            spriteRenderer.sprite = openSpike;
            boxCollider.enabled = true;
            if (playAudio) { audioPlayer.PlaySpikesClip();playAudio = false; }
        }
        else
        {
            spriteRenderer.sprite = closedSpike;
            boxCollider.enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss" && ableToHurtBoss)
        {
            boxCollider.enabled = false;
            ableToHurtBoss = false;           
            pathFinder.enabled = false;
            nightBorne.BossIdle("isRunning");
            nightBorne.BossHurt();
            nightBorneBattleSystem.DamageBoss(5000);
            if(nightBorneBattleSystem.isAlive == false) { return; }
            StartCoroutine(WaitAndReEnablePathfinder(1));
            StartCoroutine(WaitAndSetSpikesToHurtBoss(15));           
        }
    }


    IEnumerator WaitAndReEnablePathfinder(float delay)
    {
        if(nightBorneBattleSystem.isAlive == false) { yield break; }
        yield return new WaitForSeconds(delay);
        pathFinder.enabled = true;
        
    }

    IEnumerator WaitAndSetSpikesToHurtBoss(float delay)
    {
        yield return new WaitForSeconds(delay);
        ableToHurtBoss = true;
    }






}
