using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    //Variables
    string locationOfWarp = "";
    [HideInInspector] public List<int> collectedChestsID = new List<int>();
    [HideInInspector] public List<string> activatedTowers = new List<string>();
    [HideInInspector] public List<string> activatedTotems = new List<string>();
    [HideInInspector] public List<string> visitedSigns = new List<string>();
    [HideInInspector] public List<string> visitedAreas = new List<string>();
    List<GameObject> listOfAbilities;
    List<TextMeshProUGUI> abilitiesCount;


    [HideInInspector] public int playerHp = 1500;

    //Currency
    [HideInInspector] public int moneyOwned = 0;
    [HideInInspector] public int magicGemsOwned = 0;
    [HideInInspector] public int sharpGemsOwned = 0;
    [HideInInspector] public int specialGemsOwned = 0;

    
    
    

    //Warp Locations
    Vector3 warpOutside = new Vector3(0.2f, 0, 0);
    Vector3 warpDungeon = new Vector3(-6.46f, -1.14f, 0);
    Vector3 warpDungeonLeftLadder = new Vector3(-13.4f, 1.5f, 0);
    Vector3 warpOutsideLeftLadder = new Vector3(-6.627f, -0.883f, 0);
    Vector3 warpDungeonMiddleLadder = new Vector3(-7.04f, 3.14f, 0);
    Vector3 warpUnderCastleBottomLadder = new Vector3(-7.103f, -8.905f, 0);
    Vector3 warpInsideShop = new Vector3(0.225f, -1.348f, 0);
    Vector3 warpOutsideShop = new Vector3(-4.79f, -0.35f, 0);
    Vector3 warpInsideHouse = new Vector3(11.102f, 4.628f, 0);
    Vector3 warpOutsideHouse = new Vector3(10.02f, 12.27f, 0);
    Vector3 warpUnderCastleTrapDoor = new Vector3(1.252f, 2.154f, 0);
    Vector3 warpInsideHouseTrapDoor = new Vector3(10.054f, 5.686f, 0);
    Vector3 warpUnderCastleMiddle = new Vector3(-8.57f, 1.07f, 0);
    Vector3 warpOutsideCastleMiddle = new Vector3(-4.76f, 10.74f, 0);
    Vector3 warpOutsideSmallIsland = new Vector3(-12.95f, 11.01f, 0);
    Vector3 warpUnderCastleSmallIsland = new Vector3(-14.94f, 0.45f, 0);
    Vector3 warpInsideCastleGateTower = new Vector3(10.129f, -6.019f, 0);
    Vector3 warpOutsideCastleGateTower = new Vector3(1.009f, 5.151f, 0);
    Vector3 warpOnCastleGateTower = new Vector3(1.193f, 5.791f, 0);
    Vector3 warpOffCastleGateTower = new Vector3(9.87f, -5.73f, 0);
    Vector3 warpDungeonDefenderArea = new Vector3(5f, -5.48f, 0);
    Vector3 warpSmallCastle = new Vector3(8.02f, -1.23f, 0);
    Vector3 warpMountainHouseInside = new Vector3(10.37f, -24.64f, 0);
    Vector3 warpMountainHouseOutside = new Vector3(-5.933f, -13.48f, 0);
    Vector3 warpMountainHouseBasement = new Vector3(9.99f, -27.65f, 0);
    Vector3 warpBlacksmith = new Vector3(-9.182f, -20.319f, 0);
    Vector3 warpDungeonUnderBlacksmith = new Vector3(-7.359f, -10.808f, 0);
    Vector3 warpBlacksmithAboveDungeon = new Vector3(-8.492f, -19.057f, 0);
    Vector3 warpUnderLab = new Vector3(-12.12f, -6.65f, 0);
    Vector3 warpLab = new Vector3(-7.79f, -7.8f, 0);
    Vector3 warpUnderSecretIsland = new Vector3(0.29f, -9.35f, 0);
    Vector3 warpSecretIsland = new Vector3(11.586f, -8.374f, 0);
    Vector3 warpInHintHouse = new Vector3(20.391f, -30.652f, 0);
    Vector3 warpOutHintHouse = new Vector3(8.822f, -19.267f, 0);
    Vector3 warpQuestTower1 = new Vector3(11.33f, -5.31f, 0);
    Vector3 warpOutQuestTower1 = new Vector3(1.463f, 5.436f, 0);
    Vector3 warpQuestTower2 = new Vector3(7.89f, -6.03f, 0);
    Vector3 warpOutQuestTower2 = new Vector3(-1.448f, 5.062f, 0);
    Vector3 warpQuestTower3 = new Vector3(6.69f, -5.51f, 0);
    Vector3 warpOutQuestTower3 = new Vector3(-1.756f, 5.361f, 0);
    Vector3 warpQuestTower4 = new Vector3(7.58f, -2.72f, 0);
    Vector3 warpOutQuestTower4 = new Vector3(-1.11f, 7.3f, 0);
    Vector3 warpQuestTower5 = new Vector3(10.28f, -2.72f, 0);
    Vector3 warpOutQuestTower5 = new Vector3(0.89f, 7.3f, 0);
    Vector3 warpQuestTower6 = new Vector3(10.26f, -0.61f, 0);
    Vector3 warpOutQuestTower6 = new Vector3(1.14f, 9.11f, 0);
    Vector3 warpQuestTower7 = new Vector3(10.3f, 1.7f, 0);
    Vector3 warpOutQuestTower7 = new Vector3(1.11f, 10.39f, 0);
    Vector3 warpQuestTower8 = new Vector3(7.58f, 1.73f, 0);
    Vector3 warpOutQuestTower8 = new Vector3(-1.58f, 10.39f, 0);
    Vector3 warpQuestTower9 = new Vector3(6.83f, 5.98f, 0);
    Vector3 warpOutQuestTower9 = new Vector3(1.43f, 12.56f, 0);
    Vector3 warpQuestTower10 = new Vector3(3.67f, 6.02f, 0);
    Vector3 warpOutQuestTower10 = new Vector3(-0.08f, 13.53f, 0);
    Vector3 warpQuestTower11 = new Vector3(19.27f, -2.7f, 0);
    Vector3 warpOutQuestTower11 = new Vector3(8.33f, 9.45f, 0);
    Vector3 warpCastleChestHouse = new Vector3(16.084f, 5.042f, 0);
    Vector3 warpOutsideCastleChestHouse = new Vector3(-0.327f, 11.087f, 0);




    //Components and Objects
    PlayerMovement player;
    LevelManager levelManager;
    Battle battle;
    ChangeScene[] changeScene;
    Chests[] chests;
    LockedDoor[] lockedDoors;
    ShopInformation shopInformation;
    SaveSystem saveSystem;
    Failsafe failsafe;
    ThunderTower[] thunderTowers;
    Totem[] totems;
    AreaSignsVisited[] areaSignsVisited;

    //Bools

    bool sceneIsChanged;
    bool isPopulated;
    [HideInInspector] public bool playerIsAlive;
    [HideInInspector] public bool playerDrewSword;
    [HideInInspector] public bool otherSideDoor1Open;
    [HideInInspector] public bool bringerChaseStarted;
    [HideInInspector] public bool sceneChange;

    //Key items
    [HideInInspector] public bool playerHasSmallKey;
    [HideInInspector] public bool playerHasLargeKey;
    [HideInInspector] public bool playerHasWornDownKey;
    [HideInInspector] public bool playerHasDungeonKey;
    [HideInInspector] public bool otherSideKey1;
    [HideInInspector] public bool otherSideKey2;
    [HideInInspector] public bool playerHasRuneKey;

    //Player spells and abillites
    [HideInInspector] public bool jumpSmashAttackUnlocked;
    [HideInInspector] public bool comboAttasckUnlocked;
    [HideInInspector] public bool playerHasHolyBarrier;
    [HideInInspector] public bool playerHasProtect;
    [HideInInspector] public bool playerHasOneHitShield;
    [HideInInspector] public bool playerHasDoubleHp;
    [HideInInspector] public bool playerHasHeal;
    [HideInInspector] public bool playerHasLargeHeal;
    [HideInInspector] public bool playerHasRegen;
    [HideInInspector] public bool playerHasDamageBreak;
    [HideInInspector] public bool playerHasDefenceBreak;
    [HideInInspector] public bool playerHasWindProjectile;
    [HideInInspector] public bool playerHasWindBreath;
    [HideInInspector] public bool playerHasHaste;
    [HideInInspector] public bool playerHasSlow;
    [HideInInspector] public bool playerHasFireExplosion;
    [HideInInspector] public bool playerHasNukeExplosion;
    [HideInInspector] public bool playerHasWaterProjectile;
    [HideInInspector] public bool playerHasWaterTornado;
    [HideInInspector] public bool playerHasThunderProjectile;
    [HideInInspector] public bool playerHasThunderSplash;
    [HideInInspector] public bool playerHasThunderHawk;
    [HideInInspector] public bool playerHasThunderStrike;
    [HideInInspector] public bool playerHasIceProjectile;
    [HideInInspector] public bool playerHasIceSplash;
    [HideInInspector] public bool playerHasIceGround;
    [HideInInspector] public bool playerHasHolyProjectile;
    [HideInInspector] public bool playerHasHolyGround;
    [HideInInspector] public bool playerHasPoison;
    [HideInInspector] public bool playerHasDispel;


    //Number of spell casts
    [HideInInspector] public int holyBarrierCount;
    [HideInInspector] public int protectCount;
    [HideInInspector] public int oneHitShieldCount;
    [HideInInspector] public int doubleHpCount;
    [HideInInspector] public int healCount;
    [HideInInspector] public int largeHealCount;
    [HideInInspector] public int regenCount;
    [HideInInspector] public int damageBreakCount;
    [HideInInspector] public int fireExplosionCount;
    [HideInInspector] public int nukeExplosionCount;
    [HideInInspector] public int waterProjectileCount;
    [HideInInspector] public int waterTornadoCount;
    [HideInInspector] public int thunderProjectileCount;
    [HideInInspector] public int thunderSplashCount;
    [HideInInspector] public int thunderHawkCount;
    [HideInInspector] public int thunderStrikeCount;
    [HideInInspector] public int iceProjectileCount;
    [HideInInspector] public int iceSplashCount;
    [HideInInspector] public int iceGroundCount;
    [HideInInspector] public int holyProjectileCount;
    [HideInInspector] public int holyGroundCount;
    [HideInInspector] public int poisonCount;
    [HideInInspector] public int defenceBreakCount;
    [HideInInspector] public int windProjectileCount;
    [HideInInspector] public int windBreathCount;
    [HideInInspector] public int hasteCount;
    [HideInInspector] public int slowCount;
    [HideInInspector] public int dispelCount;

    //Player Attack and Defence values
    [HideInInspector] public int playerPhysicalDamage = 1;
    [HideInInspector] public int playerMagicDamage = 1;
    [HideInInspector] public int playerPhysicalDefence = 1;
    [HideInInspector] public int playerMaxHpLevel = 1;

    //Levers and Buttons
    [HideInInspector] public bool unlockCastleGate;
    [HideInInspector] public bool smallDoorOpen;
    [HideInInspector] public bool largeDoorOpen;
    [HideInInspector] public bool wornDoorOpen;
    [HideInInspector] public bool dungeonDoorOpen;
    [HideInInspector] public bool otherSide1DoorOpen;
    [HideInInspector] public bool otherSide2DoorOpen;
    [HideInInspector] public bool runeDoorOpen;


    //Puzzles
    [HideInInspector] public bool labPuzzleSolved;
    [HideInInspector] public bool bigClockSolved;
    [HideInInspector] public bool smallClockSolved;
    [HideInInspector] public bool holyWaterSolved;
    [HideInInspector] public bool passwordSolved;
    [HideInInspector] public bool castlePuzzleSolved;

    //Thunder towers
    [HideInInspector] public int thunderCrystals;
    [HideInInspector] public bool thunderTowersSolved;

    //Totems
    [HideInInspector] public bool totemsSolved;
    [HideInInspector] public bool energyBallSpawned;
    [HideInInspector] public int timesHitByEnergyBall;

    //Under tower puzzle
    [HideInInspector] public bool underTowerPuzzleSolved;

    //Bosses defeated
    [HideInInspector] public bool reaperDefeated;
    [HideInInspector] public bool defenderDefeated;
    [HideInInspector] public bool nightBorneDefeated;
    [HideInInspector] public bool bringerDefeated;

    [HideInInspector] public bool fullCompletion;

    //Additional achivements
    [HideInInspector] public bool maxPhysicalDamage;
    [HideInInspector] public bool maxMagicDamage;
    [HideInInspector] public bool maxPhysicalDefence;
    [HideInInspector] public bool maxMaxHp;

    [HideInInspector] public bool teleportUnlocked;
    [HideInInspector] public bool allChestsCollected;
    [HideInInspector] public bool allSpellsBought;
    [HideInInspector] public bool allAttacksLearned;
    [HideInInspector] public bool allAreasVisited;
    [HideInInspector] public bool allSignsRead;
    [HideInInspector] public bool allDoorsUnlocked;


    [HideInInspector] public bool oblivionSurvived;
    [HideInInspector] public bool demolitionSurvived;
    [HideInInspector] public bool diedFromWaiting;

    //Additional achivements variables
    [HideInInspector] public int numberOfChestsCollected = 0;



    private void Awake()
    {
        ManageSingleton();
        FindObjects();
    }
    private void Start()
    {
        listOfAbilities = new List<GameObject>();
        playerIsAlive = player.IsAlive();
        LoadSave();
    }


    private void Update()
    {
        FindObjects();
        PopulateList();
        TeleportPlayer();
        CheckForPlayerTeleportPlayer();
        CollectChest();
        CheckIfChestWasCollected();
        CheckIfTowerIsActivated();
        CheckIfTotemIsActivated();
        CheckForAbilities();
        UpdateAbilitesCount();
        RemoveKey();
        FullCompletion();
        IncreaseMaxPlayerHp();
        CheckForAdditionalAchivements();
        CheckIfAreaIsVisited();
        CheckIfSignIsVisited();
    }

    public int GetPLayerHp()
    {
        return playerHp ;
    }
    public void IncreasePlayerHp(int ammount)
    {
        playerHp += ammount;
    }
    public void RefucePlayerHp(int ammount)
    {
        playerHp -= ammount;
    }

    //Currency

    public void FoundMoney(int money)
    {
        moneyOwned += money;
    }
    public void SpentMoney(int money)
    {
        moneyOwned -= money;
    }
    public int MoneyOwned()
    {
        return moneyOwned;
    }

    public void FoundMagicGem(int quantity)
    {
        magicGemsOwned += quantity;
    }
    public void SpentMagicGem(int quantity)
    {
        magicGemsOwned -= quantity;
    }
    public int MagicGemsOwned()
    {
        return magicGemsOwned;
    }
    public void FoundSharpGem(int quantity)
    {
        sharpGemsOwned += quantity;
    }
    public void SpentSharpGem(int quantity)
    {
        sharpGemsOwned -= quantity;
    }
    public int SharpGemsOwned()
    {
        return sharpGemsOwned;
    }
    public void FoundSpecialGem(int quantity)
    {
        specialGemsOwned += quantity;
    }
    public void SpentSpecialGem(int quantity)
    {
        specialGemsOwned -= quantity;
    }
    public int SpecialGemsOwned()
    {
        return specialGemsOwned;
    }
    public void FoundThunderCrystal(int ammount)
    {
        thunderCrystals += ammount;
    }
    public void SpendThunderCrystals()
    {
        thunderCrystals--;
    }



    void CheckForPlayerTeleportPlayer()
    {
        for (int i = 0; i < changeScene.Length; i++)
        {
            if (changeScene[i].DoTeleport())
            {
                changeScene[i].ResetDoTeleport();
                if (changeScene[i].GetLocationOfWarp() == "Outside")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }

                else if (changeScene[i].GetLocationOfWarp() == "Dungeon")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Dungeon");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "DungeonLeftLadder")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Dungeon");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutsideLeftLadder")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "UnderCastleBottomLadder")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("UnderCastle");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "DungeonMiddleLadder")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Dungeon");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "InsideShop")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutsideShop")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "InsideHouse")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                    
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutsideHouse")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "InsideHouseTrapDoor")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "UnderCastleTrapDoor")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("UnderCastle");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutsideCastleMiddle")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "UnderCastleMiddle")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("UnderCastle");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "UnderCastleSmallIsland")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("UnderCastle");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutsideSmallIsland")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "InsideCastleGateTower")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutsideCastleGateTower")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OnCastleGateTower")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OffCastleGateTower")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "DungeonDefenderArea")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Dungeon");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "SmallCastle")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "MountainHouseInside")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "MountainHouseOutside")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "MountainHouseBasement")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "Blacksmith")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("UnderCastle");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "DungeonUnderBlacksmith")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Dungeon");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "BlacksmithAboveDungeon")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("UnderCastle");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "Lab")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "UnderLab")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Dungeon");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "UnderSecretIsland")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Dungeon");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "SecretIsland")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "HintHouse")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutHintHouse")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower1")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower2")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower3")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower4")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower5")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower6")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower7")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower8")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower9")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower10")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "QuestTower11")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower1")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower2")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower3")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower4")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower5")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower6")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower7")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower8")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower9")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower10")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutQuestTower11")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "CastleChestHouse")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Shop");
                    sceneIsChanged = true;
                }
                else if (changeScene[i].GetLocationOfWarp() == "OutCastleChestHouse")
                {
                    locationOfWarp = changeScene[i].GetLocationOfWarp();
                    levelManager.LoadScene("Outside");
                    sceneIsChanged = true;
                }

            }
        }      
    }
    void TeleportPlayer()
    {
        if (sceneIsChanged && locationOfWarp == "Outside")
        {
            locationOfWarp = "";
            player.transform.position = warpOutside;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "Dungeon")
        {
            locationOfWarp = "";
            player.transform.position = warpDungeon;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "DungeonLeftLadder")
        {
            locationOfWarp = "";
            player.transform.position = warpDungeonLeftLadder;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutsideLeftLadder")
        {
            locationOfWarp = "";
            player.transform.position = warpOutsideLeftLadder;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "UnderCastleBottomLadder")
        {
            locationOfWarp = "";
            player.transform.position = warpUnderCastleBottomLadder;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "DungeonMiddleLadder")
        {
            locationOfWarp = "";
            player.transform.position = warpDungeonMiddleLadder;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "InsideShop")
        {
            locationOfWarp = "";
            player.transform.position = warpInsideShop;
            sceneIsChanged = false;
            shopInformation.FindTimerText();
        }
        else if(sceneIsChanged && locationOfWarp == "OutsideShop")
        {
            locationOfWarp = "";
            player.transform.position = warpOutsideShop;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "InsideHouse")
        {
            locationOfWarp = "";
            player.transform.position = warpInsideHouse;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "OutsideHouse")
        {
            locationOfWarp = "";
            player.transform.position = warpOutsideHouse;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "InsideHouseTrapDoor")
        {
            locationOfWarp = "";
            player.transform.position = warpInsideHouseTrapDoor;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "UnderCastleTrapDoor")
        {
            locationOfWarp = "";
            player.transform.position = warpUnderCastleTrapDoor;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "UnderCastleMiddle")
        {
            locationOfWarp = "";
            player.transform.position = warpUnderCastleMiddle;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "OutsideCastleMiddle")
        {
            locationOfWarp = "";
            player.transform.position = warpOutsideCastleMiddle;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutsideSmallIsland")
        {
            locationOfWarp = "";
            player.transform.position = warpOutsideSmallIsland;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "UnderCastleSmallIsland")
        {
            locationOfWarp = "";
            player.transform.position = warpUnderCastleSmallIsland;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "InsideCastleGateTower")
        {
            locationOfWarp = "";
            player.transform.position = warpInsideCastleGateTower;
            sceneIsChanged = false;
        }
        else if(sceneIsChanged && locationOfWarp == "OutsideCastleGateTower")
        {
            locationOfWarp = "";
            player.transform.position = warpOutsideCastleGateTower;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OnCastleGateTower")
        {
            locationOfWarp = "";
            player.transform.position = warpOnCastleGateTower;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OffCastleGateTower")
        {
            locationOfWarp = "";
            player.transform.position = warpOffCastleGateTower;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "DungeonDefenderArea")
        {
            locationOfWarp = "";
            player.transform.position = warpDungeonDefenderArea;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "SmallCastle")
        {
            locationOfWarp = "";
            player.transform.position = warpSmallCastle;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "MountainHouseInside")
        {
            locationOfWarp = "";
            player.transform.position = warpMountainHouseInside;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "MountainHouseOutside")
        {
            locationOfWarp = "";
            player.transform.position = warpMountainHouseOutside;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "MountainHouseBasement")
        {
            locationOfWarp = "";
            player.transform.position = warpMountainHouseBasement;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "Blacksmith")
        {
            locationOfWarp = "";
            player.transform.position = warpBlacksmith;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "DungeonUnderBlacksmith")
        {
            locationOfWarp = "";
            player.transform.position = warpDungeonUnderBlacksmith;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "BlacksmithAboveDungeon")
        {
            locationOfWarp = "";
            player.transform.position = warpBlacksmithAboveDungeon;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "Lab")
        {
            locationOfWarp = "";
            player.transform.position = warpLab;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "UnderLab")
        {
            locationOfWarp = "";
            player.transform.position = warpUnderLab;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "UnderSecretIsland")
        {
            locationOfWarp = "";
            player.transform.position = warpUnderSecretIsland;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "SecretIsland")
        {
            locationOfWarp = "";
            player.transform.position = warpSecretIsland;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "HintHouse")
        {
            locationOfWarp = "";
            player.transform.position = warpInHintHouse;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutHintHouse")
        {
            locationOfWarp = "";
            player.transform.position = warpOutHintHouse;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower1")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower1;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower2")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower2;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower3")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower3;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower4")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower4;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower5")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower5;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower6")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower6;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower7")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower7;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower8")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower8;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower9")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower9;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower10")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower10;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutQuestTower11")
        {
            locationOfWarp = "";
            player.transform.position = warpOutQuestTower11;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower1")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower1;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower2")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower2;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower3")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower3;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower4")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower4;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower5")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower5;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower6")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower6;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower7")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower7;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower8")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower8;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower9")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower9;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower10")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower10;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "QuestTower11")
        {
            locationOfWarp = "";
            player.transform.position = warpQuestTower11;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "CastleChestHouse")
        {
            locationOfWarp = "";
            player.transform.position = warpCastleChestHouse;
            sceneIsChanged = false;
        }
        else if (sceneIsChanged && locationOfWarp == "OutCastleChestHouse")
        {
            locationOfWarp = "";
            player.transform.position = warpOutsideCastleChestHouse;
            sceneIsChanged = false;
        }
    }

    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType(GetType()).Length;
        if (instanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void CollectChest()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            if (chests[i].wasCollectedByPlayerInScene)
            {
                collectedChestsID.Add(chests[i].GetChestID());
            }
        }
    }
    void CheckIfChestWasCollected()
    {
        for (int i = 0; i < collectedChestsID.Count; i++)
        {
            for (int j = 0; j < chests.Length; j++)
            {
                if (collectedChestsID[i] == chests[j].GetChestID())
                {
                    chests[j].SetWasCollectedByPlayerInScene();
                    chests[j].SetChestSprite();
                }
            }
        }
    }
    void CheckIfTotemIsActivated()
    {
        for (int i = 0; i < activatedTotems.Count; i++)
        {
            for (int j = 0; j < totems.Length; j++)
            {
                if (activatedTotems[i] == totems[j].GetId())
                {
                    totems[j].SetActivated();
                }
            }
        }
    }
    void CheckIfTowerIsActivated()
    {
        for (int i = 0; i < activatedTowers.Count; i++)
        {
            for (int j = 0; j < thunderTowers.Length; j++)
            {
                if (activatedTowers[i] == thunderTowers[j].GetTowerId())
                {
                    thunderTowers[j].SolveTower();
                }
            }
        }
    }
    void CheckIfAreaIsVisited()
    {
        for (int i = 0; i < visitedAreas.Count; i++)
        {
            for(int j = 0; j < areaSignsVisited.Length; j++)
            {
                if (visitedAreas[i] == areaSignsVisited[i].GetId())
                {
                    areaSignsVisited[j].SetVisited();
                }
            }
        }
    }
    void CheckIfSignIsVisited()
    {
        for (int i = 0; i < visitedSigns.Count; i++)
        {
            for (int j = 0; j < areaSignsVisited.Length; j++)
            {
                if (visitedSigns[i] == areaSignsVisited[i].GetId())
                {
                    areaSignsVisited[j].SetVisited();
                }
            }
        }
    }
    void CheckForAbilities()
    {
        if(SceneManager.GetActiveScene().buildIndex != 1) { return; }
        if(playerIsAlive == false) { return; }
        for (int i = 0; i < listOfAbilities.Count; i++)
        {
            if (playerHasHolyBarrier && listOfAbilities[i].tag == "HolyShield")
            {
                if (playerDrewSword == false)
                {
                    listOfAbilities[i].SetActive(true);
                }
                else
                {
                    listOfAbilities[i].SetActive(false);
                }
                if(holyBarrierCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasDamageBreak && listOfAbilities[i].tag == "Break")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (damageBreakCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasDefenceBreak && listOfAbilities[i].tag == "DefenceBreak")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (defenceBreakCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasDoubleHp && listOfAbilities[i].tag == "DoubleHp")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (doubleHpCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasHeal && listOfAbilities[i].tag == "Heal")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (healCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasLargeHeal && listOfAbilities[i].tag == "LargeHeal")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (largeHealCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasRegen && listOfAbilities[i].tag == "Regen")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (regenCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasOneHitShield && listOfAbilities[i].tag == "OneHitShield")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (oneHitShieldCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasProtect && listOfAbilities[i].tag == "Protect")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (protectCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasWindProjectile && listOfAbilities[i].tag == "WindProjectile")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (windProjectileCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasWindBreath && listOfAbilities[i].tag == "WindBreath")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (windBreathCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            
           
            if (playerHasFireExplosion && listOfAbilities[i].tag == "FireExplosion")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (fireExplosionCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasNukeExplosion && listOfAbilities[i].tag == "NukeExplosion")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (nukeExplosionCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasWaterProjectile && listOfAbilities[i].tag == "WaterProjectile")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (waterProjectileCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasWaterTornado && listOfAbilities[i].tag == "WaterTornado")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (waterTornadoCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasThunderProjectile && listOfAbilities[i].tag == "ThunderProjectile")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (thunderProjectileCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasThunderSplash && listOfAbilities[i].tag == "ThunderSplash")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (thunderSplashCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasThunderHawk && listOfAbilities[i].tag == "ThunderHawk")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (thunderHawkCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasThunderStrike && listOfAbilities[i].tag == "ThunderStrike")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (thunderStrikeCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasIceProjectile && listOfAbilities[i].tag == "IceProjectile")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (iceProjectileCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasIceSplash && listOfAbilities[i].tag == "IceSplash")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (iceSplashCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if(playerHasIceGround && listOfAbilities[i].tag == "IceGround")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (iceGroundCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasHolyProjectile && listOfAbilities[i].tag == "HolyProjectile")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (holyProjectileCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasHolyGround && listOfAbilities[i].tag == "HolyGround")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (holyGroundCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasHaste && listOfAbilities[i].tag == "Haste")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (hasteCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasSlow && listOfAbilities[i].tag == "Slow")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (slowCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasPoison && listOfAbilities[i].tag == "Poison")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (poisonCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasDispel && listOfAbilities[i].tag == "Dispel")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (dispelCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (playerHasDispel && listOfAbilities[i].tag == "SelfDispel")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
                if (dispelCount <= 0) { listOfAbilities[i].SetActive(false); }
            }
            if (listOfAbilities[i].tag == "Attack")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(true);
                }
                else
                {
                    listOfAbilities[i].SetActive(false);
                }                              
            }
            if (listOfAbilities[i].tag == "SheatSword")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(true);
                }
                else
                {
                    listOfAbilities[i].SetActive(false);
                }
            }
            if (listOfAbilities[i].tag == "DrawSword")
            {
                if (playerDrewSword)
                {
                    listOfAbilities[i].SetActive(false);
                }
                else
                {
                    listOfAbilities[i].SetActive(true);
                }
            }
            if (listOfAbilities[i].tag == "JumpSmash" && jumpSmashAttackUnlocked)
            {
                if (playerDrewSword && jumpSmashAttackUnlocked)
                {
                    listOfAbilities[i].SetActive(true);
                }
                else
                {
                    listOfAbilities[i].SetActive(false);
                }
            }
            if (listOfAbilities[i].tag == "ComboAttack" && comboAttasckUnlocked)
            {
                if (playerDrewSword && comboAttasckUnlocked)
                {
                    listOfAbilities[i].SetActive(true);
                }
                else
                {
                    listOfAbilities[i].SetActive(false);
                }
            }
            

        }
    }

    void UpdateAbilitesCount()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1) { return; }
        if (playerIsAlive == false) { return; }
        for (int i = 0; i < abilitiesCount.Count; i++)
        {
            if (playerHasHolyBarrier && abilitiesCount[i].tag == "HolyShield")
            {
                abilitiesCount[i].text = "Holy Shield" + "\n" + "Casts: " + holyBarrierCount;
            }
            if (playerHasDamageBreak && abilitiesCount[i].tag == "Break")
            {
                abilitiesCount[i].text = "Damage Break" + "\n" + "Casts: " + damageBreakCount;
            }
            if (playerHasDoubleHp && abilitiesCount[i].tag == "DoubleHp")
            {
                abilitiesCount[i].text = "Double Hp" + "\n" + "Casts: " + doubleHpCount;
            }
            if (playerHasHeal && abilitiesCount[i].tag == "Heal")
            {
                abilitiesCount[i].text = "Heal" + "\n" + "Casts: " + healCount;
            }
            if (playerHasRegen && abilitiesCount[i].tag == "Regen")
            {
                abilitiesCount[i].text = "Regen" + "\n" + "Casts: " + regenCount;
            }
            if (playerHasOneHitShield && abilitiesCount[i].tag == "OneHitShield")
            {
                abilitiesCount[i].text = "One Hit Shield" + "\n" + "Casts: " + oneHitShieldCount;
            }
            if (playerHasProtect && abilitiesCount[i].tag == "Protect")
            {
                abilitiesCount[i].text = "Protect" + "\n" + "Casts: " + protectCount;
            }
            if (playerHasFireExplosion && abilitiesCount[i].tag == "FireExplosion")
            {
                abilitiesCount[i].text = "Fire explosion" + "\n" + "Casts: " + fireExplosionCount;
            }
            if (playerHasNukeExplosion && abilitiesCount[i].tag == "NukeExplosion")
            {
                abilitiesCount[i].text = "Nuke explosion" + "\n" + "Casts: " + nukeExplosionCount;
            }
            if (playerHasWaterProjectile && abilitiesCount[i].tag == "WaterProjectile")
            {
                abilitiesCount[i].text = "Water Projectile" + "\n" + "Casts: " + waterProjectileCount;
            }
            if (playerHasWaterTornado && abilitiesCount[i].tag == "WaterTornado")
            {
                abilitiesCount[i].text = "Water Tornado" + "\n" + "Casts: " + waterTornadoCount;
            }
            if (playerHasThunderProjectile && abilitiesCount[i].tag == "ThunderProjectile")
            {
                abilitiesCount[i].text = "Thunder Projectile" + "\n" + "Casts: " + thunderProjectileCount;
            }
            if (playerHasThunderSplash && abilitiesCount[i].tag == "ThunderSplash")
            {
                abilitiesCount[i].text = "Thunder Splash" + "\n" + "Casts: " + thunderSplashCount;
            }
            if (playerHasThunderHawk && abilitiesCount[i].tag == "ThunderHawk")
            {
                abilitiesCount[i].text = "Thunder Hawk" + "\n" + "Casts: " + thunderHawkCount;
            }
            if (playerHasThunderStrike && abilitiesCount[i].tag == "ThunderStrike")
            {
                abilitiesCount[i].text = "Thunder Strike" + "\n" + "Casts: " + thunderStrikeCount;
            }
            if (playerHasIceProjectile && abilitiesCount[i].tag == "IceProjectile")
            {
                abilitiesCount[i].text = "Ice Projectile" + "\n" + "Casts: " + iceProjectileCount;
            }
            if (playerHasIceSplash && abilitiesCount[i].tag == "IceSplash")
            {
                abilitiesCount[i].text = "Ice Splash" + "\n" + "Casts: " + iceSplashCount;
            }
            if (playerHasIceGround && abilitiesCount[i].tag == "IceGround")
            {
                abilitiesCount[i].text = "Ice Ground" + "\n" + "Casts: " + iceGroundCount;
            }
            if (playerHasHolyProjectile && abilitiesCount[i].tag == "HolyProjectile")
            {
                abilitiesCount[i].text = "Holy Projectile" + "\n" + "Casts: " + holyProjectileCount;
            }
            if (playerHasHolyGround && abilitiesCount[i].tag == "HolyGround")
            {
                abilitiesCount[i].text = "Holy Ground" + "\n" + "Casts: " + holyGroundCount;
            }
            if (playerHasPoison && abilitiesCount[i].tag == "Poison")
            {
                abilitiesCount[i].text = "Poison" + "\n" + "Casts: " + poisonCount;
            }
            if (playerHasLargeHeal && abilitiesCount[i].tag == "LargeHeal")
            {
                abilitiesCount[i].text = "Large Heal" + "\n" + "Casts: " + largeHealCount;
            }
            if (playerHasDefenceBreak && abilitiesCount[i].tag == "DefenceBreak")
            {
                abilitiesCount[i].text = "Defence Break" + "\n" + "Casts: " + defenceBreakCount;
            }
            if (playerHasWindProjectile && abilitiesCount[i].tag == "WindProjectile")
            {
                abilitiesCount[i].text = "Wind Projectile" + "\n" + "Casts: " + windProjectileCount;
            }
            if (playerHasWindBreath && abilitiesCount[i].tag == "WindBreath")
            {
                abilitiesCount[i].text = "Wind Breath" + "\n" + "Casts: " + windBreathCount;
            }
            if (playerHasHaste && abilitiesCount[i].tag == "Haste")
            {
                abilitiesCount[i].text = "Haste" + "\n" + "Casts: " + hasteCount;
            }
            if (playerHasSlow && abilitiesCount[i].tag == "Slow")
            {
                abilitiesCount[i].text = "Slow" + "\n" + "Casts: " + slowCount;
            }
            if (playerHasDispel && abilitiesCount[i].tag == "Dispel")
            {
                abilitiesCount[i].text = "Dispel" + "\n" + "Casts: " + dispelCount;
            }
            if (playerHasDispel && abilitiesCount[i].tag == "SelfDispel")
            {
                abilitiesCount[i].text = "Self Dispel" + "\n" + "Casts: " + dispelCount;
            }


        }
    }




    

    void FindObjects()
    {
        player = FindObjectOfType<PlayerMovement>();
        levelManager = FindObjectOfType<LevelManager>();
        battle = FindObjectOfType<Battle>();
        changeScene = FindObjectsOfType<ChangeScene>();
        chests = FindObjectsOfType<Chests>();
        shopInformation = FindObjectOfType<ShopInformation>();
        saveSystem = FindObjectOfType<SaveSystem>();
        failsafe = FindObjectOfType<Failsafe>();
        thunderTowers = FindObjectsOfType<ThunderTower>();
        totems = FindObjectsOfType<Totem>();
        areaSignsVisited = FindObjectsOfType<AreaSignsVisited>();
    }
    void PopulateList()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && isPopulated == false)
        {
            listOfAbilities = battle.GetAbilities();
            abilitiesCount = battle.GetAbilitiesCount();
            isPopulated = true;
            failsafe.SetAllStatuses();
            failsafe.SetBossesDestroyed();
        }
        else if(SceneManager.GetActiveScene().buildIndex != 1 && isPopulated)
        {
            isPopulated = false;
            failsafe.SetAllStatuses();
            failsafe.SetBossesDestroyed();
        }
    }

    public void CloseCastleGate()
    {
        if (unlockCastleGate)
        {
            StartCoroutine(WaitAndCloseCastleGate(15));
        }
    }
    void RemoveKey()
    {
        if (otherSideKey1 || otherSideKey2)
        {
            StartCoroutine(WaitAndRemoveKey(15));
        }
        
    }
    public void RevivePlayer()
    {
        StartCoroutine(WaitAndRevivePlayer(3));
    }
    IEnumerator WaitAndRevivePlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerIsAlive = true;
        isPopulated = false;
    }

    void FullCompletion()
    {
        if(labPuzzleSolved && passwordSolved && bigClockSolved && smallClockSolved && castlePuzzleSolved && holyWaterSolved && bringerDefeated && nightBorneDefeated && reaperDefeated && defenderDefeated)
        {
            fullCompletion = true;
        }
    }
    void IncreaseMaxPlayerHp()
    {
        if(playerMaxHpLevel == 1)
        {
            playerHp = 1500;
        }
        if (playerMaxHpLevel == 2)
        {
            playerHp = 1700;
        }
        if (playerMaxHpLevel == 3)
        {
            playerHp = 1900;
        }
        if (playerMaxHpLevel == 4)
        {
            playerHp = 2000;
        }
        if (playerMaxHpLevel == 5)
        {
            playerHp = 2100;
        }
        if (playerMaxHpLevel == 6)
        {
            playerHp = 2200;
        }
        if (playerMaxHpLevel == 7)
        {
            playerHp = 2250;
        }
        if (playerMaxHpLevel == 8)
        {
            playerHp = 2300;
        }
        if (playerMaxHpLevel == 9)
        {
            playerHp = 2350;
        }
        if (playerMaxHpLevel == 10)
        {
            playerHp = 2500;
        }
    }

    IEnumerator WaitAndCloseCastleGate(float delay)
    {
        yield return new WaitForSeconds(delay);
        unlockCastleGate = false;
    }
    IEnumerator WaitAndRemoveKey(float delay)
    {
        yield return new WaitForSeconds(delay);
        otherSideKey1 = false;
        otherSideKey2 = false;
    }

    public void LoadSave()
    {
        //Sidequests
        labPuzzleSolved = saveSystem.labPuzzleSovled;
        passwordSolved = saveSystem.passwordPuzzleSovled;
        bigClockSolved = saveSystem.bigClockSovled;
        smallClockSolved = saveSystem.smallClockSovled;
        castlePuzzleSolved = saveSystem.castlePuzzleSovled;
        holyWaterSolved = saveSystem.holyWaterPuzzleSovled;

        //Currency
        moneyOwned = saveSystem.moneyOwned;
        sharpGemsOwned = saveSystem.sharpGemsOwned;
        magicGemsOwned = saveSystem.magicGemsOwned;
        specialGemsOwned = saveSystem.specialGemsOwned;

        //Bosses
        bringerDefeated = saveSystem.bringerDefeated;
        defenderDefeated = saveSystem.defenderDefeated;
        reaperDefeated = saveSystem.reaperDefeated;
        nightBorneDefeated = saveSystem.nightBorneDefeated;

        //Attack and Defence
        playerPhysicalDamage = saveSystem.playerPhysicalDamage;
        playerMagicDamage = saveSystem.playerMagicDamage;
        playerPhysicalDefence = saveSystem.playerPhysicalDefence;
        playerMaxHpLevel = saveSystem.playerHpLevel;
        jumpSmashAttackUnlocked = saveSystem.hasJumpSmashAttack;
        comboAttasckUnlocked = saveSystem.hasComboAttack;

        //Key items
        playerHasDungeonKey = saveSystem.hasDungeonKey;
        playerHasLargeKey = saveSystem.hasLargeKey;
        playerHasSmallKey = saveSystem.hasSmallKey;
        playerHasWornDownKey = saveSystem.hasWornDownKey;
        playerHasRuneKey = saveSystem.hasRuneKey;

        //Doors and Levers
        dungeonDoorOpen = saveSystem.dungeonDoorOpen;
        largeDoorOpen = saveSystem.largeDoorOpen;
        smallDoorOpen = saveSystem.smallDoorOpen;
        wornDoorOpen = saveSystem.wornDownDoorOpen;
        otherSide1DoorOpen = saveSystem.otherSideDoor1Open;
        otherSide2DoorOpen = saveSystem.otherSideDoor2Open;
        runeDoorOpen = saveSystem.runeDoorOpen;

        //Chests
        collectedChestsID = saveSystem.openedChests;

        //Player spells
        playerHasHolyBarrier = saveSystem.hasHolyBarrier;
        playerHasProtect = saveSystem.hasProtect;
        playerHasOneHitShield = saveSystem.hasOneHitShield;
        playerHasDoubleHp = saveSystem.hasDoubleHp;
        playerHasHeal = saveSystem.hasHeal;
        playerHasLargeHeal = saveSystem.hasLargeHeal;
        playerHasRegen = saveSystem.hasRegen;
        playerHasDamageBreak = saveSystem.hasDamageBreak;
        playerHasDefenceBreak = saveSystem.hasDefenceBreak;
        playerHasWindProjectile = saveSystem.hasWindProjectile;
        playerHasWindBreath = saveSystem.hasWindBreath;
        playerHasHaste = saveSystem.hasHaste;
        playerHasSlow = saveSystem.hasSlow;
        playerHasFireExplosion = saveSystem.hasFireExplosion;
        playerHasNukeExplosion = saveSystem.hasNukeExplosion;
        playerHasWaterProjectile = saveSystem.hasWaterProjectile;
        playerHasWaterTornado = saveSystem.hasWaterTornado;
        playerHasThunderProjectile = saveSystem.hasThunderProjectile;
        playerHasThunderHawk = saveSystem.hasThunderHawk;
        playerHasThunderSplash = saveSystem.hasThunderSplash;
        playerHasThunderStrike = saveSystem.hasThunderStrike;
        playerHasIceProjectile = saveSystem.hasIceProjectile;
        playerHasIceSplash = saveSystem.hasIceSplash;
        playerHasIceGround = saveSystem.hasIceGround;
        playerHasHolyProjectile = saveSystem.hasHolyProjectile;
        playerHasHolyGround = saveSystem.hasHolyGround;
        playerHasPoison = saveSystem.hasPoison;
        playerHasDispel = saveSystem.hasDispel;

        //Spell count
        holyBarrierCount = saveSystem.holyBarrierCount;
        protectCount = saveSystem.protectCount;
        oneHitShieldCount = saveSystem.oneHitShieldCount;
        doubleHpCount = saveSystem.doubleHpCount;
        healCount = saveSystem.healCount;
        largeHealCount = saveSystem.largeHealCount;
        regenCount = saveSystem.regenCount;
        damageBreakCount = saveSystem.damageBreakCount;
        defenceBreakCount = saveSystem.defenceBreakCount;
        windProjectileCount = saveSystem.windProjectileCount;
        windBreathCount = saveSystem.windBreathCount;
        hasteCount = saveSystem.hasteCount;
        slowCount = saveSystem.slowCount;
        fireExplosionCount = saveSystem.fireExplosionCount;
        nukeExplosionCount = saveSystem.nukeExplosionCount;
        waterProjectileCount = saveSystem.waterProjectileCount;
        waterTornadoCount = saveSystem.waterTornadoCount;
        thunderProjectileCount = saveSystem.thunderProjectileCount;
        thunderHawkCount = saveSystem.thunderHawkCount;
        thunderSplashCount = saveSystem.thunderSplashCount;
        thunderStrikeCount = saveSystem.thunderStrikeCount;
        iceProjectileCount = saveSystem.iceProjectileCount;
        iceSplashCount = saveSystem.iceSplashCount;
        iceGroundCount = saveSystem.iceGroundCount;
        holyProjectileCount = saveSystem.holyProjectileCount;
        holyGroundCount = saveSystem.holyGroundCount;
        poisonCount = saveSystem.poisonCount;
        dispelCount = saveSystem.dispelCount;

        //Thunder towers
        thunderCrystals = saveSystem.thunderCrystals;
        activatedTowers = saveSystem.towersActivated;
        thunderTowersSolved = saveSystem.thunderTowerSolved;

        //Totems
        totemsSolved = saveSystem.totemsSolved;
        activatedTotems = saveSystem.totemsActivated;

        //Under tower puzzle
        underTowerPuzzleSolved = saveSystem.underTowerPuzzleSolved;

        //Additional achivements
        numberOfChestsCollected = saveSystem.numberOfChestsCollected;
        oblivionSurvived = saveSystem.oblivionSurvived;
        demolitionSurvived = saveSystem.demolitionSurvived;
        diedFromWaiting = saveSystem.diedOfWaiting;
        allAreasVisited = saveSystem.allAreasVisited;
        allSignsRead = saveSystem.allSignsRead;
        visitedAreas = saveSystem.areasVisited;
        visitedSigns = saveSystem.signsRead;
    }

    void CheckForAdditionalAchivements()
    {
        if(maxPhysicalDamage == false && playerPhysicalDamage == 10)
        {
            maxPhysicalDamage = true;
        }
        if(maxPhysicalDefence == false && playerPhysicalDefence == 10)
        {
            maxPhysicalDefence = true;
        }
        if(maxMagicDamage == false && playerMagicDamage == 10)
        {
            maxMagicDamage = true;
        }
        if(maxMaxHp == false && playerMaxHpLevel == 10)
        {
            maxMaxHp = true;
        }
        if(allChestsCollected == false && numberOfChestsCollected == 148)
        {
            allChestsCollected = true;
        }      
        if(jumpSmashAttackUnlocked && comboAttasckUnlocked)
        {
            allAttacksLearned = true;
        }
        if(allAreasVisited == false && visitedAreas.Count == 11)
        {
            allAreasVisited = true;
        }
        if(allSignsRead == false && visitedSigns.Count == 19)
        {
            allSignsRead = true;
        }
        CheckForAchivsWithLargeIfStatements();

    }
    void CheckForAchivsWithLargeIfStatements()
    {
        if(playerHasHolyBarrier && playerHasProtect && playerHasOneHitShield && playerHasDoubleHp && playerHasHeal && playerHasLargeHeal && playerHasRegen && playerHasDamageBreak && playerHasDefenceBreak && playerHasWindProjectile && playerHasWindBreath && playerHasHaste && playerHasSlow && playerHasFireExplosion && playerHasNukeExplosion && playerHasWaterProjectile && playerHasWaterTornado && playerHasThunderProjectile && playerHasThunderHawk && playerHasThunderSplash && playerHasThunderStrike && playerHasIceProjectile && playerHasIceSplash && playerHasIceGround && playerHasHolyProjectile && playerHasHolyGround && playerHasPoison && playerHasDispel)
        {
            allSpellsBought = true;
        }
        if(smallDoorOpen && largeDoorOpen && wornDoorOpen && runeDoorOpen && dungeonDoorOpen && otherSide1DoorOpen && otherSide2DoorOpen)
        {
            allDoorsUnlocked = true;
        }
    }


}
