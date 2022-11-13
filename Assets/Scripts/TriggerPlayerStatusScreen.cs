using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerPlayerStatusScreen : MonoBehaviour
{
    [SerializeField] GameObject achivements;
    [SerializeField] GameObject currency;
    [SerializeField] GameObject quitGameButton;
    [SerializeField] GameObject currencyButton;
    [SerializeField] GameObject achivementImage;
    [SerializeField] GameObject playerStatsObject;
    [SerializeField] TextMeshProUGUI playerStatsText;
    [SerializeField] TextMeshProUGUI playerKeysText;
    [SerializeField] GameObject playerKeysObject;
    Battle battle;
    DefenderBattleSystem defenderBattleSystem;
    ReaperBattleSystem reaperBattleSystem;
    GameManager gameManager;

    AudioPlayer audioPlayer;

    List<string> playerKeys = new List<string>();
    const string hasSmallKey = "Small Key";
    const string hasLargeKey = "Large Key";
    const string hasWornKey = "Worn down Key";
    const string hasDungeonKey = "Dungeon Key";
    const string hasRuneKey = "Rune Key";
    string thunderCrystals = "Thunder crystals: ";
    const string keyItems = "Key Items: ";
    bool thunderCrystalAdded;

    const string playerPhysicalDamage = "Player physical damage: ";
    const string playerDefence = "Player defence: ";
    const string playerMagicDamage = "Player magic damage: ";
    const string playerMaxHp = "Player max Hp level: ";
    string lineBreak = "\n";

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        battle = FindObjectOfType<Battle>();
        defenderBattleSystem = FindObjectOfType<DefenderBattleSystem>();
        reaperBattleSystem = FindObjectOfType<ReaperBattleSystem>();
        gameManager = FindObjectOfType<GameManager>();
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
            playerStatsObject.SetActive(false);
            playerKeysObject.SetActive(false);
            isOn = false;
        }
        else
        {
            achivements.SetActive(true);
            currency.SetActive(true);
            currencyButton.SetActive(true);
            quitGameButton.SetActive(true);
            achivementImage.SetActive(true);
            playerStatsObject.SetActive(true);
            playerKeysObject.SetActive(true);
            UpdatePlayerStats();
            UpdatePlayerKeys();
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

    void UpdatePlayerStats()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerStatsText.text = playerPhysicalDamage + gameManager.playerPhysicalDamage + lineBreak + lineBreak + playerDefence + gameManager.playerPhysicalDefence + lineBreak + lineBreak
            + playerMagicDamage + gameManager.playerMagicDamage + lineBreak + lineBreak + playerMaxHp + gameManager.playerMaxHpLevel;
    }
    void UpdatePlayerKeys()
    {
        playerKeys.Clear();
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.playerHasSmallKey) { playerKeys.Add(hasSmallKey); }
        if (gameManager.playerHasLargeKey) { playerKeys.Add(hasLargeKey); }
        if (gameManager.playerHasWornDownKey) { playerKeys.Add(hasWornKey); }
        if (gameManager.playerHasDungeonKey) { playerKeys.Add(hasDungeonKey); }
        if (gameManager.playerHasRuneKey) { playerKeys.Add(hasRuneKey); }
        if (gameManager.thunderCrystals > 0) { playerKeys.Add(thunderCrystals + gameManager.thunderCrystals); }


        if (playerKeys.Count == 0) { playerKeysText.text = keyItems; }
        if(playerKeys.Count == 1) { playerKeysText.text = keyItems + lineBreak + lineBreak + playerKeys[0]; }
        if(playerKeys.Count == 2) { playerKeysText.text = keyItems + lineBreak + lineBreak + playerKeys[0] + lineBreak + playerKeys[1]; }
        if(playerKeys.Count == 3) { playerKeysText.text = keyItems + lineBreak + lineBreak + playerKeys[0] + lineBreak + playerKeys[1] + lineBreak + playerKeys[2]; }
        if(playerKeys.Count == 4) { playerKeysText.text = keyItems + lineBreak + lineBreak + playerKeys[0] + lineBreak + playerKeys[1] + lineBreak + playerKeys[2] + lineBreak + playerKeys[3]; }
        if(playerKeys.Count == 5) { playerKeysText.text = keyItems + lineBreak + lineBreak + playerKeys[0] + lineBreak + playerKeys[1] + lineBreak + playerKeys[2] + lineBreak + playerKeys[3] + lineBreak + playerKeys[4]; }
        if(playerKeys.Count == 6) { playerKeysText.text = keyItems + lineBreak + lineBreak + playerKeys[0] + lineBreak + playerKeys[1] + lineBreak + playerKeys[2] + lineBreak + playerKeys[3] + lineBreak + playerKeys[4] + lineBreak + playerKeys[5]; }
    }
   
    public void QuitGame()
    {
        Application.Quit();
    }
}
