using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNightBorneFight : MonoBehaviour
{
    [SerializeField] GameObject hpCanvas;
    [SerializeField] PathFinder pathFinder;
    [SerializeField] NightBorneBattleSystem nightBorneBattleSystem;
    [SerializeField] GameObject backgroundMusic;
    [SerializeField] GameObject nightborneBattleMusic;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        if (gameManager.nightBorneDefeated)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameManager.nightBorneDefeated) { backgroundMusic.SetActive(true); nightborneBattleMusic.SetActive(false); return; }
        backgroundMusic.SetActive(false);
        nightborneBattleMusic.SetActive(true);
        pathFinder.enabled = true;
        hpCanvas.SetActive(true);
    }
}
