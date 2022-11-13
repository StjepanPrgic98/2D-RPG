using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopList;
    [SerializeField] GameObject detailList;
    
    [SerializeField] List<GameObject> fireSpells;
    [SerializeField] List<GameObject> waterSpells;
    [SerializeField] List<GameObject> thunderSpells;
    [SerializeField] List<GameObject> iceSpells;
    [SerializeField] List<GameObject> holySpells;
    [SerializeField] List<GameObject> debuffs;
    [SerializeField] List<GameObject> buffs;
    [SerializeField] List<GameObject> heals;
    [SerializeField] GameObject textObject;
    [SerializeField] TextMeshProUGUI shopText;
    [SerializeField] TextMeshProUGUI playerCurrency;
    [SerializeField] GameObject statusButton;
    [SerializeField] GameObject playerCurrencyImage;


    string notEnoughMoney = "Not enough gold, stranger!";
    string enoughMoney = "Bought ";

    AudioPlayer audioPlayer;
    ShopInformation shopInformation;


    //Bools


    


    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        shopInformation = FindObjectOfType<ShopInformation>();
    }

    private void Update()
    {
        UpdatePlayerCurrency();
    }


    void DisplayDetailList()
    {
        detailList.SetActive(true);
    }
    

    public void DisplayFireSpells()
    {
        ClearDetailList();
        DisplayDetailList();
        for (int i = 0; i < fireSpells.Count; i++)
        {
            fireSpells[i].SetActive(true);
            if (fireSpells[i].tag == "FireExplosion" && gameManager.playerHasFireExplosion && shopInformation.fireExplosionBought) { fireSpells[i].SetActive(false); }
            if (fireSpells[i].tag == "NukeExplosion" && gameManager.playerHasNukeExplosion && shopInformation.nukeExplosionBought) { fireSpells[i].SetActive(false); }
            
        }
    }
    public void DisplayWaterSpells()
    {
        ClearDetailList();
        DisplayDetailList();
        for (int i = 0; i < waterSpells.Count; i++)
        {
            waterSpells[i].SetActive(true);
            if (waterSpells[i].tag == "WaterProjectile" && gameManager.playerHasWaterProjectile && shopInformation.waterProjectileBought) { waterSpells[i].SetActive(false); }
            if (waterSpells[i].tag == "WaterTornado" && gameManager.playerHasWaterTornado && shopInformation.waterTornadoBought) { waterSpells[i].SetActive(false); }
            
        }
    }
    public void DisplayThunderSpells()
    {
        ClearDetailList();
        DisplayDetailList();
        for (int i = 0; i < thunderSpells.Count; i++)
        {
            thunderSpells[i].SetActive(true);
            if (thunderSpells[i].tag == "ThunderProjectile" && gameManager.playerHasThunderProjectile && shopInformation.thunderProjectileBought) { thunderSpells[i].SetActive(false); }
            if (thunderSpells[i].tag == "ThunderHawk" && gameManager.playerHasThunderHawk && shopInformation.thunderHawkBought) { thunderSpells[i].SetActive(false); }
            if (thunderSpells[i].tag == "ThunderSplash" && gameManager.playerHasThunderSplash && shopInformation.thunderSplashBought) { thunderSpells[i].SetActive(false); }
            if (thunderSpells[i].tag == "ThunderStrike" && gameManager.playerHasThunderStrike && shopInformation.thunderStrikeBought) { thunderSpells[i].SetActive(false); }

        }
    }
    public void DisplayIceSpells()
    {
        ClearDetailList();
        DisplayDetailList();
        for (int i = 0; i < iceSpells.Count; i++)
        {
            iceSpells[i].SetActive(true);
            if (iceSpells[i].tag == "IceProjectile" && gameManager.playerHasIceProjectile && shopInformation.iceProjectileBought) { iceSpells[i].SetActive(false); }
            if (iceSpells[i].tag == "IceSplash" && gameManager.playerHasIceSplash && shopInformation.iceSplashBought) { iceSpells[i].SetActive(false); }
            if (iceSpells[i].tag == "IceGround" && gameManager.playerHasIceGround && shopInformation.iceGroundBought) { iceSpells[i].SetActive(false); }
            
        }
    }
    public void DisplayHolySpells()
    {
        ClearDetailList();
        DisplayDetailList();
        for (int i = 0; i < holySpells.Count; i++)
        {
            holySpells[i].SetActive(true);
            if (holySpells[i].tag == "HolyProjectile" && gameManager.playerHasHolyProjectile && shopInformation.holyProjectileBought) { holySpells[i].SetActive(false); }
            if (holySpells[i].tag == "HolyGround" && gameManager.playerHasHolyGround && shopInformation.holyGroundBought) { holySpells[i].SetActive(false); }
            
        }
    }
    public void DisplayDebuffs()
    {
        ClearDetailList();
        DisplayDetailList();
        for (int i = 0; i < debuffs.Count; i++)
        {
            debuffs[i].SetActive(true);
            if (debuffs[i].tag == "Break" && gameManager.playerHasDamageBreak && shopInformation.damageBreakBought) { debuffs[i].SetActive(false); }
            if (debuffs[i].tag == "DefenceBreak" && gameManager.playerHasDefenceBreak && shopInformation.defenceBreakBought) { debuffs[i].SetActive(false); }
            if (debuffs[i].tag == "Poison" && gameManager.playerHasPoison && shopInformation.poisonBought) { debuffs[i].SetActive(false); }
            if (debuffs[i].tag == "Slow" && gameManager.playerHasSlow && shopInformation.slowBought) { debuffs[i].SetActive(false); }
            if (debuffs[i].tag == "Dispel" && gameManager.playerHasDispel && shopInformation.dispelBought) { debuffs[i].SetActive(false); }

        }
    }
    public void DisplayBuffs()
    {
        ClearDetailList();
        DisplayDetailList();
        for (int i = 0; i < buffs.Count; i++)
        {
            buffs[i].SetActive(true);
            if (buffs[i].tag == "Protect" && gameManager.playerHasProtect && shopInformation.protectBought) { buffs[i].SetActive(false); }
            if (buffs[i].tag == "HolyShield" && gameManager.playerHasHolyBarrier && shopInformation.holyBarrierBought) { buffs[i].SetActive(false); }
            if (buffs[i].tag == "OneHitShield" && gameManager.playerHasOneHitShield && shopInformation.oneHitShieldBought) { buffs[i].SetActive(false); }
            if (buffs[i].tag == "DoubleHp" && gameManager.playerHasDoubleHp && shopInformation.doubleHpBought) { buffs[i].SetActive(false); }
            if (buffs[i].tag == "Haste" && gameManager.playerHasHaste && shopInformation.hasteBought) { buffs[i].SetActive(false); }
            
        }
    }
    public void DisplayHeals()
    {
        ClearDetailList();
        DisplayDetailList();
        for (int i = 0; i < heals.Count; i++)
        {
            heals[i].SetActive(true);
            if (heals[i].tag == "Heal" && gameManager.playerHasHeal && shopInformation.healBought) { heals[i].SetActive(false); }
            if (heals[i].tag == "LargeHeal" && gameManager.playerHasLargeHeal && shopInformation.largeHealBought) { heals[i].SetActive(false); }
            if (heals[i].tag == "Regen" && gameManager.playerHasRegen && shopInformation.regenBought) { heals[i].SetActive(false); }
            
        }
    }

    public void BuyFireExplosion()
    {
        if(gameManager.MoneyOwned() < 2600 || gameManager.MagicGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < fireSpells.Count; i++)
        {
            if (fireSpells[i].tag == "FireExplosion")
            {
                BuyingItems(enoughMoney, "Fire Explosion!");
                gameManager.SpentMoney(2600);
                gameManager.SpentMagicGem(1);
                gameManager.playerHasFireExplosion = true;
                gameManager.fireExplosionCount += 3;
                fireSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.fireExplosionBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyNukeExplosion()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.MagicGemsOwned() < 2 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < fireSpells.Count; i++)
        {
            if (fireSpells[i].tag == "NukeExplosion")
            {
                BuyingItems(enoughMoney, "Nuke Explosion!");
                gameManager.SpentMoney(3500);
                gameManager.SpentMagicGem(2);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasNukeExplosion = true;
                gameManager.nukeExplosionCount += 3;
                fireSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.nukeExplosionBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyWaterProjectile()
    {
        if (gameManager.MoneyOwned() < 1500 || gameManager.MagicGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < waterSpells.Count; i++)
        {
            if (waterSpells[i].tag == "WaterProjectile")
            {
                BuyingItems(enoughMoney, "Water Projectile!");
                gameManager.SpentMoney(1500);
                gameManager.SpentMagicGem(1);
                gameManager.playerHasWaterProjectile = true;
                gameManager.waterProjectileCount += 3;
                waterSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.waterProjectileBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyWaterTornado()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.MagicGemsOwned() < 2 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < waterSpells.Count; i++)
        {
            if (waterSpells[i].tag == "WaterTornado")
            {
                BuyingItems(enoughMoney, "Water Tornado!");
                gameManager.SpentMoney(3500);
                gameManager.SpentMagicGem(2);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasWaterTornado = true;
                gameManager.waterTornadoCount += 3;
                waterSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.waterTornadoBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyThunderProjectile()
    {
        if (gameManager.MoneyOwned() < 1500 || gameManager.MagicGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < thunderSpells.Count; i++)
        {
            if (thunderSpells[i].tag == "ThunderProjectile")
            {
                BuyingItems(enoughMoney, "Thunder Projectile!");
                gameManager.SpentMoney(1500);
                gameManager.SpentMagicGem(1);
                gameManager.playerHasThunderProjectile = true;
                gameManager.thunderProjectileCount += 3;
                thunderSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.thunderProjectileBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyThunderSplash()
    {
        if (gameManager.MoneyOwned() < 1800 || gameManager.MagicGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < thunderSpells.Count; i++)
        {
            if (thunderSpells[i].tag == "ThunderSplash")
            {
                BuyingItems(enoughMoney, "Thunder Splash!");
                gameManager.SpentMoney(1800);
                gameManager.SpentMagicGem(1);
                gameManager.playerHasThunderSplash = true;
                gameManager.thunderSplashCount += 3;
                thunderSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.thunderSplashBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyThunderHawk()
    {
        if (gameManager.MoneyOwned() < 2200 || gameManager.MagicGemsOwned() < 2)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < thunderSpells.Count; i++)
        {
            if (thunderSpells[i].tag == "ThunderHawk")
            {
                BuyingItems(enoughMoney, "Thunder Hawk!");
                gameManager.SpentMoney(2200);
                gameManager.SpentMagicGem(2);
                gameManager.playerHasThunderHawk = true;
                gameManager.thunderHawkCount += 3;
                thunderSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.thunderHawkBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyThunderStrike()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.MagicGemsOwned() < 2 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < thunderSpells.Count; i++)
        {
            if (thunderSpells[i].tag == "ThunderStrike")
            {
                BuyingItems(enoughMoney, "Thunder Strike!");
                gameManager.SpentMoney(3500);
                gameManager.SpentMagicGem(2);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasThunderStrike = true;
                gameManager.thunderStrikeCount += 3;
                thunderSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.thunderStrikeBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyIceProjectile()
    {
        if (gameManager.MoneyOwned() < 1500 || gameManager.MagicGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < iceSpells.Count; i++)
        {
            if (iceSpells[i].tag == "IceProjectile")
            {
                BuyingItems(enoughMoney, "Ice Projectile!");
                gameManager.SpentMoney(1500);
                gameManager.SpentMagicGem(1);
                gameManager.playerHasIceProjectile = true;
                gameManager.iceProjectileCount += 3;
                iceSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.iceProjectileBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyIceSplash()
    {
        if (gameManager.MoneyOwned() < 1900 || gameManager.MagicGemsOwned() < 2)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < iceSpells.Count; i++)
        {
            if (iceSpells[i].tag == "IceSplash")
            {
                BuyingItems(enoughMoney, "Ice Splash!");
                gameManager.SpentMoney(1900);
                gameManager.SpentMagicGem(2);
                gameManager.playerHasIceSplash = true;
                gameManager.iceSplashCount += 3;
                iceSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.iceSplashBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyIceGround()
    {
        if (gameManager.MoneyOwned() < 3200 || gameManager.MagicGemsOwned() < 2 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < iceSpells.Count; i++)
        {
            if (iceSpells[i].tag == "IceGround")
            {
                BuyingItems(enoughMoney, "Ice Ground!");
                gameManager.SpentMoney(3200);
                gameManager.SpentMagicGem(2);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasIceGround = true;
                gameManager.iceGroundCount += 3;
                iceSpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.iceGroundBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyHolyProjectile()
    {
        if (gameManager.MoneyOwned() < 2100 || gameManager.MagicGemsOwned() < 3)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < holySpells.Count; i++)
        {
            if (holySpells[i].tag == "HolyProjectile")
            {
                BuyingItems(enoughMoney, "Holy Projectile!");
                gameManager.SpentMoney(2100);
                gameManager.SpentMagicGem(3);
                gameManager.playerHasHolyProjectile = true;
                gameManager.holyProjectileCount += 3;
                holySpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.holyProjectileBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyHolyGround()
    {
        if (gameManager.MoneyOwned() < 4000 || gameManager.MagicGemsOwned() < 3 || gameManager.SpecialGemsOwned() < 2)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < holySpells.Count; i++)
        {
            if (holySpells[i].tag == "HolyGround")
            {
                BuyingItems(enoughMoney, "Holy Ground!");
                gameManager.SpentMoney(4000);
                gameManager.SpentMagicGem(3);
                gameManager.SpentSpecialGem(2);
                gameManager.playerHasHolyGround = true;
                gameManager.holyGroundCount += 3;
                holySpells[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.holyGroundBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyDamageBreak()
    {
        if (gameManager.MoneyOwned() < 4000 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < debuffs.Count; i++)
        {
            if (debuffs[i].tag == "Break")
            {
                BuyingItems(enoughMoney, "Damage Break!");
                gameManager.SpentMoney(4000);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasDamageBreak = true;
                gameManager.damageBreakCount += 3;
                debuffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.damageBreakBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyDefenceBreak()
    {
        if (gameManager.MoneyOwned() < 4000 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < debuffs.Count; i++)
        {
            if (debuffs[i].tag == "DefenceBreak")
            {
                BuyingItems(enoughMoney, "Defence Break!");
                gameManager.SpentMoney(4000);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasDefenceBreak = true;
                gameManager.defenceBreakCount += 3;
                debuffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.defenceBreakBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyPoison()
    {
        if (gameManager.MoneyOwned() < 3200 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < debuffs.Count; i++)
        {
            if (debuffs[i].tag == "Poison")
            {
                BuyingItems(enoughMoney, "Posion!");
                gameManager.SpentMoney(3200);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasPoison = true;
                gameManager.poisonCount += 3;
                debuffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.poisonBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyHaste()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].tag == "Haste")
            {
                BuyingItems(enoughMoney, "Haste");
                gameManager.SpentMoney(3500);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasHaste = true;
                gameManager.hasteCount += 3;
                buffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.hasteBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuySlow()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < debuffs.Count; i++)
        {
            if (debuffs[i].tag == "Slow")
            {
                BuyingItems(enoughMoney, "Slow");
                gameManager.SpentMoney(3500);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasSlow = true;
                gameManager.slowCount += 3;
                debuffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.slowBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyDispel()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < debuffs.Count; i++)
        {
            if (debuffs[i].tag == "Dispel")
            {
                BuyingItems(enoughMoney, "Dispel");
                gameManager.SpentMoney(3500);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasDispel = true;
                gameManager.dispelCount += 3;
                debuffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.dispelBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyRegen()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < heals.Count; i++)
        {
            if (heals[i].tag == "Regen")
            {
                BuyingItems(enoughMoney, "Regen");
                gameManager.SpentMoney(3500);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasRegen = true;
                gameManager.regenCount += 3;
                heals[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.regenBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyProtect()
    {
        if (gameManager.MoneyOwned() < 5000 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].tag == "Protect")
            {
                BuyingItems(enoughMoney, "Protect!");
                gameManager.SpentMoney(5000);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasProtect = true;
                gameManager.protectCount += 3;
                buffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.protectBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyHolyBarrier()
    {
        if (gameManager.MoneyOwned() < 4000 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].tag == "HolyShield")
            {
                BuyingItems(enoughMoney, "Holy Barrier!");
                gameManager.SpentMoney(4000);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasHolyBarrier = true;
                gameManager.holyBarrierCount += 3;
                buffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.holyBarrierBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyOneHitShield()
    {
        if (gameManager.MoneyOwned() < 3900 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].tag == "OneHitShield")
            {
                BuyingItems(enoughMoney, "One Hit Shield!");
                gameManager.SpentMoney(3900);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasOneHitShield = true;
                gameManager.oneHitShieldCount += 3;
                buffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.oneHitShieldBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyDoubleHp()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < buffs.Count; i++)
        {
            if (buffs[i].tag == "DoubleHp")
            {
                BuyingItems(enoughMoney, "Double HP!");
                gameManager.SpentMoney(3500);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasDoubleHp = true;
                gameManager.doubleHpCount += 3;
                buffs[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.doubleHpBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyHeal()
    {
        if (gameManager.MoneyOwned() < 1500 || gameManager.MagicGemsOwned() < 2)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < heals.Count; i++)
        {
            if (heals[i].tag == "Heal")
            {
                BuyingItems(enoughMoney, "Heal!");
                gameManager.SpentMoney(1500);
                gameManager.SpentMagicGem(2);
                gameManager.playerHasHeal = true;
                gameManager.healCount += 3;
                heals[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.healBought = true;
                shopInformation.startTimer = true;
            }
        }
    }
    public void BuyLargeHeal()
    {
        if (gameManager.MoneyOwned() < 3500 || gameManager.MagicGemsOwned() < 3 || gameManager.SpecialGemsOwned() < 1)
        {
            BuyingItems(notEnoughMoney, "");
            audioPlayer.PlayCantBuyClip();
            return;
        }
        for (int i = 0; i < heals.Count; i++)
        {
            if (heals[i].tag == "LargeHeal")
            {
                BuyingItems(enoughMoney, "Large Heal!");
                gameManager.SpentMoney(3500);
                gameManager.SpentMagicGem(3);
                gameManager.SpentSpecialGem(1);
                gameManager.playerHasLargeHeal = true;
                gameManager.largeHealCount += 3;
                heals[i].SetActive(false);
                audioPlayer.PlayShopBuyClip();
                shopInformation.largeHealBought = true;
                shopInformation.startTimer = true;
            }
        }
    }





    void ClearDetailList()
    {
        for (int i = 0; i < fireSpells.Count; i++)
        {
            fireSpells[i].SetActive(false);
        }
        for (int i = 0; i < waterSpells.Count; i++)
        {
            waterSpells[i].SetActive(false);
        }
        for (int i = 0; i < thunderSpells.Count; i++)
        {
            thunderSpells[i].SetActive(false);
        }
        for (int i = 0; i < iceSpells.Count; i++)
        {
            iceSpells[i].SetActive(false);
        }
        for (int i = 0; i < holySpells.Count; i++)
        {
            holySpells[i].SetActive(false);
        }
        for (int i = 0; i < debuffs.Count; i++)
        {
            debuffs[i].SetActive(false);
        }
        for (int i = 0; i < buffs.Count; i++)
        {
            buffs[i].SetActive(false);
        }
        for (int i = 0; i < heals.Count; i++)
        {
            heals[i].SetActive(false);
        }

    }

    void UpdatePlayerCurrency()
    {
        playerCurrency.text = "Gold: " + gameManager.MoneyOwned() + "\n" + "Magic gems: " + gameManager.MagicGemsOwned() + "\n" + "Sharp gems: " + gameManager.SharpGemsOwned() + "\n" + "Special gems: " + gameManager.SpecialGemsOwned();
    }
    










    private void OnCollisionEnter2D(Collision2D collision)
    {
        shopList.SetActive(true);
        shopInformation.TurnTimerOnorOff(true);
        statusButton.SetActive(false);
        playerCurrencyImage.SetActive(true);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        shopList.SetActive(false);
        detailList.SetActive(false);
        shopInformation.TurnTimerOnorOff(false);
        statusButton.SetActive(true);
        playerCurrencyImage.SetActive(true);
    }

    void BuyingItems(string didntBuy, string text)
    {
        textObject.SetActive(true);
        shopText.text = didntBuy + text;
        StartCoroutine(WaitAndTurnShopTextOff(1));
    }

    IEnumerator WaitAndTurnShopTextOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        textObject.SetActive(false);
        shopText.text = "";
    }
}
