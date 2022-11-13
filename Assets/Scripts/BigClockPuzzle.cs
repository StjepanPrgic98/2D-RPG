using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BigClockPuzzle : MonoBehaviour
{
    GameManager gameManager;
    AudioPlayer audioPlayer;
    [HideInInspector] public List<int> hours;
    [SerializeField] bool bigClock;
    [SerializeField] bool smallClock;
    [SerializeField] TextMeshProUGUI completionText;
    [SerializeField] GameObject chest1;
    [SerializeField] GameObject chest2;
    [SerializeField] GameObject chest3;
    [SerializeField] GameObject chest4;
    [SerializeField] GameObject darkBolt;
    [SerializeField] List<GameObject> positionsForEffect;


    

    bool cleared;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        if(smallClock && gameManager.smallClockSolved)
        {
            SpawnChests();
        }
        else
        {
            chest1.SetActive(false);
            chest2.SetActive(false);
            chest3.SetActive(false);
            chest4.SetActive(false);
        }
        if(bigClock && gameManager.bigClockSolved)
        {
            SpawnChests();
        }
        else
        {
            chest1.SetActive(false);
            chest2.SetActive(false);
            chest3.SetActive(false);
            chest4.SetActive(false);
        }
    }



    private void Update()
    {
        if(cleared == false)
        {
            StartCoroutine(WaitAndClearList(2));
        }
        if(bigClock && gameManager.bigClockSolved == false)
        {
            CheckIfClockIsSolved();
        }
        if(smallClock && gameManager.smallClockSolved == false)
        {
            CheckIfClockIsSolved();
        }
        
    }


    void CheckIfClockIsSolved()
    {
        if (bigClock)
        {
            if (hours.Count == 4)
            {
                if (hours[0] == 3 && hours[1] == 6 && hours[2] == 9 && hours[3] == 12)
                {
                    gameManager.bigClockSolved = true;
                    CompletionText(2, "Something happened!");
                    audioPlayer.PlaySidequestDoneClip();
                    hours.Clear();
                    StartEffect();
                }
                else
                {
                    hours.Clear();
                }
            }
        }
        if (smallClock)
        {
            if(hours.Count == 4)
            {
                if (hours[0] == 3 && hours[1] == 12 && hours[2] == 9 && hours[3] == 6)
                {
                    gameManager.smallClockSolved = true;
                    CompletionText(2, "Something happened!");
                    audioPlayer.PlaySidequestDoneClip();
                    hours.Clear();
                    StartEffect();
                }
                else
                {
                    hours.Clear();
                }
            }
        }
        
    }
    void StartEffect()
    {
        for (int i = 0; i < positionsForEffect.Count; i++)
        {
            GameObject darkBoltEffect = Instantiate(darkBolt, positionsForEffect[i].transform.position, Quaternion.identity);
            audioPlayer.PlayOblivionClip();
            Destroy(darkBoltEffect, 1);
        }
        StartCoroutine(WaitAndSpawnChests());
    }
    IEnumerator WaitAndSpawnChests()
    {
        yield return new WaitForSeconds(0.3f);
        SpawnChests();
        
    }
    void SpawnChests()
    {
        chest1.SetActive(true);
        chest2.SetActive(true);
        chest3.SetActive(true);
        chest4.SetActive(true);
    }

    void CompletionText(float delay, string text)
    {
        completionText.text = text;
        StartCoroutine(WaitAndTurnOffCompletionText(delay));
    }
    IEnumerator WaitAndTurnOffCompletionText(float delay)
    {
        yield return new WaitForSeconds(delay);
        completionText.text = "";
    }

    IEnumerator WaitAndClearList(float delay)
    {
        cleared = true;
        yield return new WaitForSeconds(delay);
        hours.Clear();
    }
}
