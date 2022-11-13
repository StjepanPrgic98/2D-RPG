using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lever : MonoBehaviour
{
    [SerializeField] Button button1;
    [SerializeField] Button button2;
    [SerializeField] GameObject textObject;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite pulledSprite;
    [SerializeField] bool leverForPower;
    [SerializeField] bool leverForCastleGate;
    bool leverPulled;

    SpriteRenderer spriteRenderer;
    GameManager gameManager;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    



    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioPlayer.PlayLeverClip();
        gameManager = FindObjectOfType<GameManager>();
        if(collision.tag == "Player" && leverPulled == false && leverForPower)
        {
            leverPulled = true;
            spriteRenderer.sprite = pulledSprite;
            textObject.SetActive(true);
            text.text = "Power activated!";
            button1.levelPulled = true;
            button2.levelPulled = true;
            StartCoroutine(WaitAndTurnTextOff(2));
        }
        else if(collision.tag == "Player" && leverPulled == false && leverForCastleGate)
        {
            leverPulled = true;
            spriteRenderer.sprite = pulledSprite;
            textObject.SetActive(true);
            text.text = "Gate lowered!";
            gameManager.unlockCastleGate = true;
            gameManager.CloseCastleGate();
            StartCoroutine(WaitAndTurnTextOff(2));
            //StartCoroutine(WaitAndResetLever(30));
        }
    }
    



    IEnumerator WaitAndTurnTextOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        text.text = "";
        textObject.SetActive(false);
    }

    IEnumerator WaitAndResetLever(float delay)
    {
        yield return new WaitForSeconds(delay);
        leverPulled = false;
        spriteRenderer.sprite = normalSprite;
    }
}
