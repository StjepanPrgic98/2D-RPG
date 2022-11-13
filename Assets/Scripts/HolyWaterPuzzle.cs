using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HolyWaterPuzzle : MonoBehaviour
{
    int counter;

    GameManager gameManager;
    AudioPlayer audioPlayer;
    [SerializeField] GameObject pushObject1;
    [SerializeField] GameObject pushObject2;
    [SerializeField] GameObject pushObject3;
    [SerializeField] GameObject textObject;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject chest1;
    [SerializeField] GameObject chest2;
    [SerializeField] GameObject chest3;
    [SerializeField] GameObject chest4;
    [SerializeField] GameObject holyEffect;



    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        if (gameManager.holyWaterSolved)
        {
            pushObject1.SetActive(false);
            pushObject2.SetActive(false);
            pushObject3.SetActive(false);
            gameObject.SetActive(false);
            SpawnChests();
            return;
        }
        else
        {
            chest1.SetActive(false);
            chest2.SetActive(false);
            chest3.SetActive(false);
            chest4.SetActive(false);
            holyEffect.SetActive(false);
        }
    }

    private void Update()
    {
        if (gameManager.holyWaterSolved)
        {
            pushObject1.SetActive(false);
            pushObject2.SetActive(false);
            pushObject3.SetActive(false);
            SpawnChests();
            return;
        }
        if(counter == 3)
        {
            audioPlayer.PlaySidequestDoneClip();
            gameManager.holyWaterSolved = true;
            textObject.SetActive(true);
            text.text = "Something happened!";
            SpawnChests();
            StartCoroutine(WaitAndTurnOffText());
        }
    }
    void SpawnChests()
    {
        chest1.SetActive(true);
        chest2.SetActive(true);
        chest3.SetActive(true);
        chest4.SetActive(true);
        holyEffect.SetActive(true);
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PushObject1")
        {
            counter++;
            pushObject1.SetActive(false);
            audioPlayer.PlayHolyWaterBarrerClip();
        }
        else if(collision.tag == "PushObject2")
        {
            counter++;
            pushObject2.SetActive(false);
            audioPlayer.PlayHolyWaterBarrerClip();
        }
        else if (collision.tag == "PushObject3")
        {
            counter++;
            pushObject3.SetActive(false);
            audioPlayer.PlayHolyWaterBarrerClip();
        }

    }

    IEnumerator WaitAndTurnOffText()
    {
        yield return new WaitForSeconds(2);
        textObject.SetActive(false);
        text.text = "";
    }
}
