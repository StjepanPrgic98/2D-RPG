using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] Boss bossScript;
    [SerializeField] PathFinder pathfinderScript;
    [SerializeField] bool makeBossAppear;
    [SerializeField] GameObject dungeonMusic;
    [SerializeField] GameObject bringerChaseMusic;
    bool isSpawned = false;
    GameManager gameManager;
    
    Vector3 bossPosition = new Vector3(0.826f, 0.335f, 0);

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && isSpawned == false && makeBossAppear == false && gameManager.bringerChaseStarted == false)
        {
            pathfinderScript.enabled = true;
            isSpawned = true;
            bossScript.SetBossSpawned();
            gameManager.bringerChaseStarted = true;
            bringerChaseMusic.SetActive(true);
            StartCoroutine(WaitAndTurnOffDungeonMusic(2));
        }
        else if(collision.tag == "Player" && isSpawned == false && makeBossAppear && bossScript.IsBossSpawned() == false)
        {
            bossScript.BossAppear(bossPosition);
            isSpawned = true;
            bossScript.SetBossSpawned();
        }

    }

    IEnumerator WaitAndTurnOffDungeonMusic(float delay)
    {
        yield return new WaitForSeconds(delay);
        dungeonMusic.SetActive(false);
    }
}
