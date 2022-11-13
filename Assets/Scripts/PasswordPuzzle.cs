using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PasswordPuzzle : MonoBehaviour
{
    [SerializeField] GameObject inputTextObject;
    [SerializeField] Text inputFieldText;
    [SerializeField] TextMeshProUGUI responceText;
    [SerializeField] GameObject responceTextObject;
    [SerializeField] List<GameObject> listOfEffectPositions;
    [SerializeField] GameObject waterTornado;
    [SerializeField] GameObject chest1;
    [SerializeField] GameObject chest2;
    [SerializeField] GameObject chest3;
    [SerializeField] GameObject chest4;


    string myName = "STJEPAN";
    string userInput;
    bool passwordCorrect;
    bool didInputValue;

    GameManager gameManager;
    AudioPlayer audioPlayer;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        if (gameManager.passwordSolved)
        {
            chest1.SetActive(true);
            chest2.SetActive(true);
            chest3.SetActive(true);
            chest4.SetActive(true);
            return;
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
        ChechIfPasswordIsCorrect();       
    }



    void ChechIfPasswordIsCorrect()
    {
        if(passwordCorrect) { return; }
        if (gameManager.passwordSolved) { return; }
        if(userInput == myName && didInputValue)
        {
            DoEffects();
            ResponceText("Correct!");
            passwordCorrect = true;
            didInputValue = false;
            inputTextObject.SetActive(false);
            gameManager.passwordSolved = true;
            audioPlayer.PlaySidequestDoneClip();
            
        }
        if(userInput != myName && didInputValue)
        {
            ResponceText("Incorrect!");
            ClearInput();
            didInputValue = false;
        }
    }

    public void UserInput()
    {
        userInput = inputFieldText.text;
        didInputValue = true;
    }
    public void ClearInput()
    {
        userInput = "";
        inputFieldText.text = null;
    }



    void ResponceText(string text)
    {
        responceText.text = text;
        StartCoroutine(WaitAndTurnOffResponceText(2));
    }








    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && gameManager.passwordSolved == false)
        {
            inputTextObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            inputTextObject.SetActive(false);
        }
    }
    void DoEffects()
    {
        StartCoroutine(WaitAndSpawnEffects(0.1f));
    }
    IEnumerator WaitAndSpawnEffects(float delay)
    {
        for (int i = 0; i < listOfEffectPositions.Count; i++)
        {
            GameObject waterTornadoEffect = Instantiate(waterTornado, listOfEffectPositions[i].transform.position, Quaternion.identity);
            audioPlayer.PlayWaterTornadoClip();          
            StartCoroutine(WaitAndSpawnChest());           
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator WaitAndSpawnChest()
    {
        yield return new WaitForSeconds(0.2f);
        chest1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        chest2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        chest3.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        chest4.SetActive(true);
    }



    IEnumerator WaitAndTurnOffResponceText(float delay)
    {
        yield return new WaitForSeconds(delay);
        responceTextObject.SetActive(false);
        responceText.text = "";
    }
} 
