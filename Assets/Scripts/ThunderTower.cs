using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ThunderTower : MonoBehaviour
{
    GameManager gameManager;
    SpriteRenderer spriteRenderer;
    AudioPlayer audioPlayer;

    [SerializeField] string Id;
    bool towerSolved;

    [SerializeField] GameObject thunderStrike;
    [SerializeField] TextMeshProUGUI towerText;



    Vector3 addHeight = new Vector3(0, 0.8f, 0);

    public string GetTowerId()
    {
        return Id;
    }
    public void SolveTower()
    {
        towerSolved = true;
    }

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spriteRenderer.sortingOrder = -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameManager.thunderTowersSolved) { return; }
        if (towerSolved) { return; }
        if(gameManager.thunderCrystals <= 0)
        {
            TowerText("Thunder crystal required!");
        }
        else
        {
            TowerText("Activated!");
            gameManager.activatedTowers.Add(Id);
            gameManager.SpendThunderCrystals();
            StartCoroutine(WaitAndSpawnThunder(0.2f));
            towerSolved = true;
            if(gameManager.activatedTowers.Count == 10)
            {
                gameManager.thunderTowersSolved = true;
                audioPlayer.PlaySidequestDoneClip();
                TowerText("Rune Key Found!");
                gameManager.playerHasRuneKey = true;
            }
        }
        
    }

    IEnumerator WaitAndSpawnThunder(float delay)
    {
        GameObject thunderStrikeEffect = Instantiate(thunderStrike, transform.position + addHeight, Quaternion.identity);
        audioPlayer.PlayThunderStrikeClip();
        Destroy(thunderStrikeEffect, 1);
        yield return new WaitForSeconds(delay);
        GameObject thunderStrikeEffect2 = Instantiate(thunderStrike, transform.position + addHeight, Quaternion.identity);
        audioPlayer.PlayThunderStrikeClip();
        Destroy(thunderStrikeEffect2, 1);
        yield return new WaitForSeconds(delay);
        GameObject thunderStrikeEffect3 = Instantiate(thunderStrike, transform.position + addHeight, Quaternion.identity);
        audioPlayer.PlayThunderStrikeClip();
        Destroy(thunderStrikeEffect3, 1);
        yield return new WaitForSeconds(delay);
        GameObject thunderStrikeEffect4 = Instantiate(thunderStrike, transform.position + addHeight, Quaternion.identity);
        audioPlayer.PlayThunderStrikeClip();
        Destroy(thunderStrikeEffect4, 1);
        yield return new WaitForSeconds(delay);
        GameObject thunderStrikeEffect5 = Instantiate(thunderStrike, transform.position + addHeight, Quaternion.identity);
        audioPlayer.PlayThunderStrikeClip();
        Destroy(thunderStrikeEffect5, 1);
        yield return new WaitForSeconds(delay);
    }

    void TowerText(string text)
    {
        towerText.text = text;
        Invoke("DeleteTowerText", 2);
    }
    void DeleteTowerText()
    {
        towerText.text = "";
    }
}
