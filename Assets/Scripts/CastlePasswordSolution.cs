using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CastlePasswordSolution : MonoBehaviour
{
    [SerializeField] GameObject inputTextObject;
    [SerializeField] Text inputFieldText;
    [SerializeField] TextMeshProUGUI responceText;
    [SerializeField] GameObject responceTextObject;
    [SerializeField] GameObject levelWarp;

    string solution;
    string userInput;
    bool didInputValue;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        solution = "Masculinity";
    }



    private void Update()
    {
        ChechIfPasswordIsCorrect();
    }



    void ChechIfPasswordIsCorrect()
    {
        if (gameManager.castlePuzzleSolved) { levelWarp.SetActive(true); return; }
        if (userInput == solution && didInputValue)
        {
            ResponceText("Correct!");
            didInputValue = false;
            inputTextObject.SetActive(false);
            gameManager.castlePuzzleSolved = true;
        }
        if (userInput != solution && didInputValue)
        {
            ResponceText("Incorrect!");
            ClearInput();
            didInputValue = false;
        }
    }

    public void UserInput()
    {
        userInput = inputFieldText.text.ToString();
        didInputValue = true;
    }
    public void ClearInput()
    {
        userInput = "";
        inputFieldText.text = null;
    }



    void ResponceText(string text)
    {
        responceTextObject.SetActive(true);
        responceText.text = text;
        StartCoroutine(WaitAndTurnOffResponceText(2));
    }








    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && gameManager.castlePuzzleSolved == false)
        {
            inputTextObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inputTextObject.SetActive(false);
        }
    }



    IEnumerator WaitAndTurnOffResponceText(float delay)
    {
        yield return new WaitForSeconds(delay);
        responceTextObject.SetActive(false);
        responceText.text = "";
    }
}
