using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlayerStatusScreen : MonoBehaviour
{
    [SerializeField] GameObject achivements;
    [SerializeField] GameObject currency;
    [SerializeField] GameObject quitGameButton;
    [SerializeField] GameObject currencyButton;
    [SerializeField] GameObject achivementImage;
    Battle battle;
    DefenderBattleSystem defenderBattleSystem;
    ReaperBattleSystem reaperBattleSystem;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        battle = FindObjectOfType<Battle>();
        defenderBattleSystem = FindObjectOfType<DefenderBattleSystem>();
        reaperBattleSystem = FindObjectOfType<ReaperBattleSystem>();
    }
    private void Update()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }



    bool isOn;


    public void TriggerStatusScreen()
    {
        audioPlayer.PlayerInventoryClip();
        if (isOn)
        {
            achivements.SetActive(false);
            currency.SetActive(false);
            currencyButton.SetActive(false);
            quitGameButton.SetActive(false);
            achivementImage.SetActive(false);
            isOn = false;
        }
        else
        {
            achivements.SetActive(true);
            currency.SetActive(true);
            currencyButton.SetActive(true);
            quitGameButton.SetActive(true);
            achivementImage.SetActive(true);
            isOn = true;
            
        }
    }
    public void TriggerStatusButton()
    {
        if(battle.StartBattle() || defenderBattleSystem.defenderBattleStarted || reaperBattleSystem.reaperStartBattle)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
