using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


public class StartScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject yesButton;
    [SerializeField] GameObject noButton;
    [HideInInspector] public bool started;

    SaveSystem saveSystem;
    GameManager gameManager;
    private void Awake()
    {
        saveSystem = FindObjectOfType<SaveSystem>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        
    }

    public void StartGame()
    {
        text.text = "Loading!";
        started = true;
        StartCoroutine(WaitAndStartLoading(1));
        saveSystem.DeleteSave();
        saveSystem.DeleteSave();
        saveSystem.LoadSave();
        gameManager.LoadSave();
    }
    public void DontStartNewGame()
    {
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }


    public void StartNewGame()
    {
        if (started) { return; }
        if (saveSystem.saveFileExists)
        {
            text.text = "Save File exists!";
            yesButton.SetActive(true);
            noButton.SetActive(true);
            StartCoroutine(WaitAndDisableText(2));
            return;
        }
        text.text = "Loading!";
        started = true;
        StartCoroutine(WaitAndStartLoading(1));
        saveSystem.DeleteSave();
        saveSystem.DeleteSave();
        saveSystem.LoadSave();
        gameManager.LoadSave();
    }
    public void CheckForLoadGame()
    {
        if(saveSystem.saveFileExists == false)
        {
            text.text = "Doesnt exist!";
            return;
        }
        else
        {
            if (started) { return; }
            text.text = "Loading!";
            started = true;
            StartCoroutine(WaitAndStartLoading(1));
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndStartLoading(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Outside");
    }
    IEnumerator WaitAndDisableText(float delay)
    {
        yield return new WaitForSeconds(delay);
        text.text = "";
    }



}
