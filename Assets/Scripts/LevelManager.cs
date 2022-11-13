using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Slider loadScreenSlider;
    [SerializeField] GameObject loadScreen;
    [SerializeField] PlayerMovement player;
    GameManager gameManager;



    bool loadingOver;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        LoadScreen();
    }
    public bool IsLoadingOver()
    {
        return loadingOver;
    }

    public void LoadSceneDungeon()
    {
        SceneManager.LoadScene("Dungeon");
    }
    public void LoadSceneOutside()
    {
        SceneManager.LoadScene("Outside");
    }
    public void LoadSceneUnderCastle()
    {
        SceneManager.LoadScene("UnderCastle");
    }

    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    void LoadScreen()
    {
        loadScreen.SetActive(true);
        StartCoroutine(WaitAndIncreaseLoadSlider(0.2f));
    }
    IEnumerator WaitAndIncreaseLoadSlider(float delay)
    {
        yield return new WaitForSeconds(delay);
        loadScreenSlider.value += 0.25f;
        yield return new WaitForSeconds(delay);
        loadScreenSlider.value += 0.25f;
        yield return new WaitForSeconds(delay);
        loadScreenSlider.value += 0.25f;
        yield return new WaitForSeconds(delay);
        loadScreenSlider.value += 0.25f;
        loadScreen.SetActive(false);
        loadingOver = true;

    }
    public void RevivePlayer()
    {
        gameManager.RevivePlayer();
    }

    IEnumerator WaitAndTurnLoadScreenOff(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

   
}
