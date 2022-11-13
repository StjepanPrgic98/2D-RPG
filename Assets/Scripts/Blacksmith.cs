using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Blacksmith : MonoBehaviour
{
    [SerializeField] GameObject blacksmithCanvas;
    [SerializeField] TextMeshProUGUI upgradeWeaponText;
    [SerializeField] TextMeshProUGUI upgradeDefenceText;
    [SerializeField] TextMeshProUGUI upgradeMagicPowerText;
    [SerializeField] TextMeshProUGUI upgradeMaxHp;
    [SerializeField] TextMeshProUGUI blacksmithText;
    [SerializeField] TextMeshProUGUI playerCurrency;
    [SerializeField] GameObject blacksmithTextObject;
    [SerializeField] TextMeshProUGUI newAbilityText;
    AudioPlayer audioPlayer;

    int upgradeWeaponCost = 500;
    int upgradeDefenceCost = 500;
    int upgradeMagicPowerCost = 500;
    bool weaponMaxUpgrade;
    bool defenceMaxUpgrade;
    bool magicPowerMaxUpgrade;
    bool maxHpMaxUpgrade;
    int damageSharpGemCost = 0;
    int damageSpecialGemCost = 0;
    int defenceSharpGemCost = 0;
    int defenceSpecialGemCost = 0;
    int magicGemCost = 0;
    int magicSpecialGemCost = 0;
    int hpSpecialGemCost = 0;
    int hpUpgradeCost = 1000;

   

    string upgradeWeaponBasicText = "Upgrade weapon to level ";  
    string upgradeDefenceBasicText = "Upgrade defence to level ";   
    string upgradeMagicPowerBasicText = "Upgrade magic power to level ";
    string upgradeMaxHpBasicText = "Upgrade Max Hp to level ";
    string noMoney = "Not enough cash, stranger!";
    string success = "Upgraded to level ";
    string maxedOut = "Maxed Out!";

   



    PlayerMovement player;
    GameManager gameManager;
    Failsafe failsafe;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        failsafe = FindObjectOfType<Failsafe>();
    }
    private void Start()
    {
        UpdateBlacksmithText();
    }

    private void Update()
    {
        UpdateBlacksmithText();
        UpdatePlayerCurrency();
    }









    public void UpgradeWeapon()
    {
        if (weaponMaxUpgrade) { BuyingText(1, maxedOut); return; }
        if (gameManager.MoneyOwned() < upgradeWeaponCost || gameManager.SharpGemsOwned() < damageSharpGemCost || gameManager.SpecialGemsOwned() < damageSpecialGemCost) { BuyingText(1, noMoney); audioPlayer.PlayCantBuyClip(); return; }
        gameManager.playerPhysicalDamage++;
        CheckForNewAbility();
        gameManager.SpentMoney(upgradeWeaponCost);
        gameManager.SpentSharpGem(damageSharpGemCost);
        gameManager.SpentSpecialGem(damageSpecialGemCost);
        upgradeWeaponCost += (upgradeWeaponCost / 100) * 20;
        BuyingText(1, success + (gameManager.playerPhysicalDamage));
        audioPlayer.PlayUpgradeClip();
    }
    public void UpgradeDefence()
    {
        if (defenceMaxUpgrade) { BuyingText(1, maxedOut); return; }
        if (gameManager.MoneyOwned() < upgradeDefenceCost || gameManager.SharpGemsOwned() < defenceSharpGemCost || gameManager.SpecialGemsOwned() < defenceSpecialGemCost) { BuyingText(1, noMoney); audioPlayer.PlayCantBuyClip(); return; }
        gameManager.playerPhysicalDefence++;
        gameManager.SpentMoney(upgradeDefenceCost);
        gameManager.SpentSharpGem(defenceSharpGemCost);
        gameManager.SpentSpecialGem(defenceSpecialGemCost);
        upgradeDefenceCost += (upgradeDefenceCost / 100) * 20;
        BuyingText(1, success + (gameManager.playerPhysicalDefence));
        audioPlayer.PlayUpgradeClip();
    }
    public void UpgradeMagicDefence()
    {
        if (magicPowerMaxUpgrade) { BuyingText(1, maxedOut); return; }
        if (gameManager.MoneyOwned() < upgradeMagicPowerCost || gameManager.MagicGemsOwned() < magicGemCost || gameManager.SpecialGemsOwned() < magicSpecialGemCost) { BuyingText(1, noMoney); audioPlayer.PlayCantBuyClip(); return; }
        gameManager.playerMagicDamage++;
        gameManager.SpentMoney(upgradeMagicPowerCost);
        gameManager.SpentMagicGem(magicGemCost);
        gameManager.SpentSpecialGem(magicSpecialGemCost);
        upgradeMagicPowerCost += (upgradeMagicPowerCost / 100) * 20;
        BuyingText(1, success + (gameManager.playerMagicDamage));
        audioPlayer.PlayUpgradeClip();
    }
    public void UpgradeMaxHp()
    {
        if (maxHpMaxUpgrade) { BuyingText(1, maxedOut); return; }
        if (gameManager.MoneyOwned() < hpUpgradeCost || gameManager.SpecialGemsOwned() < hpSpecialGemCost) { BuyingText(1, noMoney);audioPlayer.PlayCantBuyClip(); return; }
        gameManager.playerMaxHpLevel++;
        gameManager.SpentMoney(hpUpgradeCost);
        gameManager.SpentSpecialGem(hpSpecialGemCost);
        hpUpgradeCost += (hpUpgradeCost / 100) * 20;
        BuyingText(1, success + (gameManager.playerMaxHpLevel));
        audioPlayer.PlayUpgradeClip();

    }


    void UpdateBlacksmithText()
    {
        UpdateWeaponUpgrade();
        UpdateDefenceUpgrade();
        UpdateMagicDefenceUpgrade();
        UpdateMaxPlayerHp();
    }

    void BuyingText(float delay, string text)
    {
        blacksmithTextObject.SetActive(true);
        blacksmithText.text = text;
        StartCoroutine(WaitAndTurnOffBlacksmithText(delay));
    }
    IEnumerator WaitAndTurnOffBlacksmithText(float delay)
    {
        yield return new WaitForSeconds(delay);
        blacksmithText.text = "";
        blacksmithTextObject.SetActive(false);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            blacksmithCanvas.SetActive(true);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            blacksmithCanvas.SetActive(false);
        }
    }



    void UpdateWeaponUpgrade()
    {
        if (gameManager.playerPhysicalDamage == 1)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 2 + "\n" + "   Price: " + upgradeWeaponCost;
        }
        if (gameManager.playerPhysicalDamage == 2)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 3 + "\n" + "   Price: " + upgradeWeaponCost;
        }
        if (gameManager.playerPhysicalDamage == 3)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 4 + "\n" + "   Price: " + upgradeWeaponCost;
        }
        if (gameManager.playerPhysicalDamage == 4)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 5 + "\n" + "   Price: " + upgradeWeaponCost;
        }
        if (gameManager.playerPhysicalDamage == 5)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 6 + "\n" + "   Price: " + upgradeWeaponCost + "\n" + "Sharp gems required: 1!";
            damageSharpGemCost = 1;
        }
        if (gameManager.playerPhysicalDamage == 6)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 7 + "\n" + "   Price: " + upgradeWeaponCost + "\n" + "Sharp gems required: 1!";
        }
        if (gameManager.playerPhysicalDamage == 7)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 8 + "\n" + "   Price: " + upgradeWeaponCost + "\n" + "Sharp gems required: 1!";
        }
        if (gameManager.playerPhysicalDamage == 8)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 9 + "\n" + "   Price: " + upgradeWeaponCost + "\n" + "Sharp gems required: 2!";
            damageSharpGemCost = 2;
        }
        if (gameManager.playerPhysicalDamage == 9)
        {
            upgradeWeaponText.text = upgradeWeaponBasicText + 10 + "\n" + "   Price: " + upgradeWeaponCost + "\n" + "Sharp gems required: 3!" + "\n" + "Special gems required: 1";
            damageSharpGemCost = 3;
            damageSpecialGemCost = 1;
        }
        if (gameManager.playerPhysicalDamage == 10)
        {
            weaponMaxUpgrade = true;
            upgradeWeaponText.text = "Weapon Maxed Out!";
        }
    }

    void UpdateDefenceUpgrade()
    {
        if(gameManager.playerPhysicalDefence == 1)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 2 + "\n" + "   Price: " + upgradeDefenceCost ;
        }
        if (gameManager.playerPhysicalDefence == 2)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 3 + "\n" + "   Price: " + upgradeDefenceCost;
        }
        if (gameManager.playerPhysicalDefence == 3)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 4 + "\n" + "   Price: " + upgradeDefenceCost;
        }
        if (gameManager.playerPhysicalDefence == 4)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 5 + "\n" + "   Price: " + upgradeDefenceCost;
        }
        if (gameManager.playerPhysicalDefence == 5)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 6 + "\n" + "   Price: " + upgradeDefenceCost + "\n" + "Sharp gems required: 1!";
            defenceSharpGemCost = 1;
        }
        if (gameManager.playerPhysicalDefence == 6)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 7 + "\n" + "   Price: " + upgradeDefenceCost + "\n" + "Sharp gems required: 1!";
        }
        if (gameManager.playerPhysicalDefence == 7)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 8 + "\n" + "   Price: " + upgradeDefenceCost + "\n" + "Sharp gems required: 1!";
        }
        if (gameManager.playerPhysicalDefence == 8)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 9 + "\n" + "   Price: " + upgradeDefenceCost + "\n" + "Sharp gems required: 2!";
            defenceSharpGemCost = 2;
        }
        if (gameManager.playerPhysicalDefence == 9)
        {
            upgradeDefenceText.text = upgradeDefenceBasicText + 10 + "\n" + "   Price: " + upgradeDefenceCost + "\n" + "Sharp gems required: 3!" + "\n" + "Special gems required: 1";
            defenceSharpGemCost = 3;
            defenceSpecialGemCost = 1;
        }
        if (gameManager.playerPhysicalDefence == 10)
        {
            defenceMaxUpgrade = true;
            upgradeDefenceText.text = "Defence Maxed Out!";
        }
    }
    void UpdateMagicDefenceUpgrade()
    {
        if(gameManager.playerMagicDamage == 1)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 2 + "\n" + "   Price: " + upgradeMagicPowerCost;
        }
        if (gameManager.playerMagicDamage == 2)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 3 + "\n" + "   Price: " + upgradeMagicPowerCost;
        }
        if (gameManager.playerMagicDamage == 3)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 4 + "\n" + "   Price: " + upgradeMagicPowerCost;
        }
        if (gameManager.playerMagicDamage == 4)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 5 + "\n" + "   Price: " + upgradeMagicPowerCost;
        }
        if (gameManager.playerMagicDamage == 5)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 6 + "\n" + "   Price: " + upgradeMagicPowerCost + "\n" + "Magic gems required: 1";
            magicGemCost = 1;
        }
        if (gameManager.playerMagicDamage == 6)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 7 + "\n" + "   Price: " + upgradeMagicPowerCost + "\n" + "Magic gems required: 1";
        }
        if (gameManager.playerMagicDamage == 7)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 8 + "\n" + "   Price: " + upgradeMagicPowerCost + "\n" + "Magic gems required: 1";
        }
        if (gameManager.playerMagicDamage == 8)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 9 + "\n" + "   Price: " + upgradeMagicPowerCost + "\n" + "Magic gems required: 2";
            magicGemCost = 2;
        }
        if (gameManager.playerMagicDamage == 9)
        {
            upgradeMagicPowerText.text = upgradeMagicPowerBasicText + 10 + "\n" + "   Price: " + upgradeMagicPowerCost + "\n" + "Magic gems required: 3" + "\n" + "Special gems required: 1";
            magicGemCost = 3;
            magicSpecialGemCost = 1;
        }
        if (gameManager.playerMagicDamage == 10)
        {
            magicPowerMaxUpgrade = true;
            upgradeMagicPowerText.text = "Magic power Maxed Out!";
        }
    }
    void UpdateMaxPlayerHp()
    {
        if (gameManager.playerMaxHpLevel == 1)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 2 + "\n" + "   Price: " + hpUpgradeCost;
        }
        if (gameManager.playerMaxHpLevel == 2)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 3 + "\n" + "   Price: " + hpUpgradeCost;
        }
        if (gameManager.playerMaxHpLevel == 3)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 4 + "\n" + "   Price: " + hpUpgradeCost;
        }
        if (gameManager.playerMaxHpLevel == 4)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 5 + "\n" + "   Price: " + hpUpgradeCost;
        }
        if (gameManager.playerMaxHpLevel == 5)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 6 + "\n" + "   Price: " + hpUpgradeCost + "\n" + "Special gems required: 1";
            hpSpecialGemCost = 1;
        }
        if (gameManager.playerMaxHpLevel == 6)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 7 + "\n" + "   Price: " + hpUpgradeCost + "\n" + "Special gems required: 1";
        }
        if (gameManager.playerMaxHpLevel == 7)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 8 + "\n" + "   Price: " + hpUpgradeCost + "\n" + "Special gems required: 1";
        }
        if (gameManager.playerMaxHpLevel == 8)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 9 + "\n" + "   Price: " + hpUpgradeCost + "\n" + "Special gems required: 1";
            hpSpecialGemCost = 2;
        }
        if (gameManager.playerMaxHpLevel == 9)
        {
            upgradeMaxHp.text = upgradeMaxHpBasicText + 10 + "\n" + "   Price: " + hpUpgradeCost + "\n" + "Special gems required: 1";
            hpSpecialGemCost = 3;
        }
        if (gameManager.playerMaxHpLevel == 10)
        {
            maxHpMaxUpgrade = true;
            upgradeMaxHp.text = "Max Hp Maxed Out!";
        }
    }
    void UpdatePlayerCurrency()
    {
        playerCurrency.text = "Gold: " + gameManager.MoneyOwned() + "\n" + "Magic gems: " + gameManager.MagicGemsOwned() + "\n" + "Sharp gems: " + gameManager.SharpGemsOwned() + "\n" + "Special gems: " + gameManager.SpecialGemsOwned(); ;
    }

    void CheckForNewAbility()
    {
        if(gameManager.playerPhysicalDamage == 5)
        {
            NewAbilityText("Jump Smash Attack Unlocked!");
            gameManager.jumpSmashAttackUnlocked = true;
        }
        if(gameManager.playerPhysicalDamage == 10)
        {
            NewAbilityText("Combo Attack Unlocked");
            gameManager.comboAttasckUnlocked = true;
        }
    }

    void NewAbilityText(string text)
    {
        newAbilityText.text = text;
        Invoke(nameof(ResetNewAbilityText), 2);
    }
    void ResetNewAbilityText()
    {
        newAbilityText.text = "";
    }
}
