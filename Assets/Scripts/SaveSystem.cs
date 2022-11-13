using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SaveSystem : MonoBehaviour
{
    GameManager gameManager;
    List<string> tempList;

    [SerializeField] GameObject saveButton;
    [SerializeField] GameObject warpButton;
    [SerializeField] TextMeshProUGUI savedText;
    [SerializeField] GameObject savepointIcon1;
    [SerializeField] GameObject savepointIcon2;
    [SerializeField] GameObject savepointIcon3;
    int numberOfBossesDefeated = 0;
    [HideInInspector] public bool teleportUnlocked;
    [HideInInspector] public bool introChestCollected;

    [HideInInspector] public bool saveFileExists;
    const string saveFileExistsKey = "SaveFileExists";

    //Sidequests
    [HideInInspector] public bool labPuzzleSovled;
    [HideInInspector] public bool passwordPuzzleSovled;
    [HideInInspector] public bool bigClockSovled;
    [HideInInspector] public bool smallClockSovled;
    [HideInInspector] public bool castlePuzzleSovled;
    [HideInInspector] public bool holyWaterPuzzleSovled;
    const string labPuzzleSolvedKey = "LabPuzzleSolved";
    const string passwordPuzzleSolvedKey = "PasswordPuzzleSolved";
    const string bigClockSolvedKey = "BigClockSolved";
    const string smallClockSolvedKey = "SmallClockSolved";
    const string castlePuzzleSolvedKey = "CastlePuzzleSolved";
    const string holyWaterPuzzleSolvedKey = "HolyWaterPuzzleSolved";

    //Currency
    [HideInInspector] public int moneyOwned;
    [HideInInspector] public int sharpGemsOwned;
    [HideInInspector] public int magicGemsOwned;
    [HideInInspector] public int specialGemsOwned;
    const string moneyOwnedKey = "MoneyOwned";
    const string sharpGemsOwnedKey = "SharpGemsOwned";
    const string magicGemsOwnedKey = "MagicGemsOwned";
    const string specialGemsOwnedKey = "SpecialGemsOwned";

    //Bosses
    [HideInInspector] public bool bringerDefeated;
    [HideInInspector] public bool defenderDefeated;
    [HideInInspector] public bool reaperDefeated;
    [HideInInspector] public bool nightBorneDefeated;
    const string bringerDefeatedKey = "BringerDefeated";
    const string defenderDefeatedKey = "DefenderDefeated";
    const string reaperDefeatedKey = "ReaperDefeated";
    const string nightBorneDefeatedKey = "NightBorneDefeated";

    //Attack and Defence
    [HideInInspector] public int playerPhysicalDamage;
    [HideInInspector] public int playerMagicDamage;
    [HideInInspector] public int playerPhysicalDefence;
    [HideInInspector] public int playerHpLevel;
    [HideInInspector] public bool hasJumpSmashAttack;
    [HideInInspector] public bool hasComboAttack;
    const string playerPhysicalDamageKey = "PlayerPhysicalDamage";
    const string playerMagicDamageKey = "PlayerMagicDamage";
    const string playerPhysicalDefenceKey = "PlayerPhysicalDefence";
    const string playerHpLevelKey = "PlayerHpLevel";
    const string hasJumpSmashAttackKey = "HasJumpSmashAttack";
    const string hasComboAttackKey = "HasComboAttack";

    //Key items
    [HideInInspector] public bool hasDungeonKey;
    [HideInInspector] public bool hasLargeKey;
    [HideInInspector] public bool hasSmallKey;
    [HideInInspector] public bool hasWornDownKey;
    [HideInInspector] public bool hasRuneKey;
    const string hasDungeonKeyKey = "HasDungeonKey";
    const string hasLargeKeyKey = "HasLargeKey";
    const string hasSmallKeyKey = "HasSmallKey";
    const string hasWornDownKeyKey = "HasWornDownKey";
    const string hasRuneKeyKey = "HasRuneKey";

    //Doors and Levers
    [HideInInspector] public bool dungeonDoorOpen;
    [HideInInspector] public bool largeDoorOpen;
    [HideInInspector] public bool smallDoorOpen;
    [HideInInspector] public bool wornDownDoorOpen;
    [HideInInspector] public bool otherSideDoor1Open;
    [HideInInspector] public bool otherSideDoor2Open;
    [HideInInspector] public bool runeDoorOpen;
    const string dungeonDoorOpenKey = "DungeonDoorOpen";
    const string largeDoorOpenKey = "LargeDoorOpen";
    const string smallDoorOpenKey = "SmallDoorOpen";
    const string wornDownDoorOpenKey = "WornDownDoorOpen";
    const string otherSideDoor1OpenKey = "OtherSideDoor1Open";
    const string otherSideDoor2OpenKey = "OtherSideDoor2Open";
    const string runeDoorOpenKey = "RuneDoorOpen";

    //Chests
    [HideInInspector]  List<string> openedChestsTemp;
    bool chestsWereSaved;
    [HideInInspector] public List<int> openedChests;
    const string openedChestsKey = "OpenedChests";
    const string chestsWereSavedKey = "ChestsWereSaved";

    //Player spells
    [HideInInspector] public bool hasHolyBarrier;
    [HideInInspector] public bool hasProtect;
    [HideInInspector] public bool hasOneHitShield;
    [HideInInspector] public bool hasDoubleHp;
    [HideInInspector] public bool hasHeal;
    [HideInInspector] public bool hasLargeHeal;
    [HideInInspector] public bool hasRegen;
    [HideInInspector] public bool hasDamageBreak;
    [HideInInspector] public bool hasDefenceBreak;
    [HideInInspector] public bool hasWindProjectile;
    [HideInInspector] public bool hasWindBreath;
    [HideInInspector] public bool hasHaste;
    [HideInInspector] public bool hasSlow;
    [HideInInspector] public bool hasFireExplosion;
    [HideInInspector] public bool hasNukeExplosion;
    [HideInInspector] public bool hasWaterProjectile;
    [HideInInspector] public bool hasWaterTornado;
    [HideInInspector] public bool hasThunderProjectile;
    [HideInInspector] public bool hasThunderHawk;
    [HideInInspector] public bool hasThunderSplash;
    [HideInInspector] public bool hasThunderStrike;
    [HideInInspector] public bool hasIceProjectile;
    [HideInInspector] public bool hasIceSplash;
    [HideInInspector] public bool hasIceGround;
    [HideInInspector] public bool hasHolyProjectile;
    [HideInInspector] public bool hasHolyGround;
    [HideInInspector] public bool hasPoison;
    [HideInInspector] public bool hasDispel;
    const string hasHolyBarrierKey = "HasHolyBarrier";
    const string hasProtectKey = "HasProtect";
    const string hasOneHitShieldKey = "HasOneHitShield";
    const string hasDoubleHpKey = "HasDoubleHp";
    const string hasHealKey = "HasHeal";
    const string hasLargeHealKey = "HasLargeHeal";
    const string hasRegenKey = "HasRegen";
    const string hasDamageBreakKey = "HasDamageBreak";
    const string hasDefenceBreakKey = "HasDefenceBreak";
    const string hasWindProjectileKey = "HasWindProjectile";
    const string hasWindBreathKey = "HasWindBreath";
    const string hasHasteKey = "HasHaste";
    const string hasSlowKey = "HasSlow";
    const string hasFireExplosionKey = "HasFireExplosion";
    const string hasNukeExplosionKey = "HasNukeExplosion";
    const string hasWaterProjectileKey = "HasWaterProjectile";
    const string hasWaterTornadoKey = "HasWaterTornado";
    const string hasThunderProjectileKey = "HasThunderProjectile";
    const string hasThunderHawkKey = "HasThunderHawk";
    const string hasThunderSplashKey = "HasThunderSplash";
    const string hasThunderStrikeKey = "HasThunderStrike";
    const string hasIceProjectileKey = "HasIceProjectile";
    const string hasIceSplashKey = "HasIceSplash";
    const string hasIceGroundKey = "HasIceGround";
    const string hasHolyProjectileKey = "HasHolyProjectile";
    const string hasHolyGroundKey = "HasHolyGround";
    const string hasPoisonKey = "HasPoison";
    const string hasDispelKey = "HasDispel";

    //Spell count
    [HideInInspector] public int holyBarrierCount;
    [HideInInspector] public int protectCount;
    [HideInInspector] public int oneHitShieldCount;
    [HideInInspector] public int doubleHpCount;
    [HideInInspector] public int healCount;
    [HideInInspector] public int largeHealCount;
    [HideInInspector] public int regenCount;
    [HideInInspector] public int damageBreakCount;
    [HideInInspector] public int defenceBreakCount;
    [HideInInspector] public int windProjectileCount;
    [HideInInspector] public int windBreathCount;
    [HideInInspector] public int hasteCount;
    [HideInInspector] public int slowCount;
    [HideInInspector] public int fireExplosionCount;
    [HideInInspector] public int nukeExplosionCount;
    [HideInInspector] public int waterProjectileCount;
    [HideInInspector] public int waterTornadoCount;
    [HideInInspector] public int thunderProjectileCount;
    [HideInInspector] public int thunderHawkCount;
    [HideInInspector] public int thunderSplashCount;
    [HideInInspector] public int thunderStrikeCount;
    [HideInInspector] public int iceProjectileCount;
    [HideInInspector] public int iceSplashCount;
    [HideInInspector] public int iceGroundCount;
    [HideInInspector] public int holyProjectileCount;
    [HideInInspector] public int holyGroundCount;
    [HideInInspector] public int poisonCount;
    [HideInInspector] public int dispelCount;
    const string holyBarrierCountKey = "HolyBarrierCount";
    const string protectCountKey = "ProtectCount";
    const string oneHitShieldCountKey = "OneHitShieldCount";
    const string doubleHpCountKey = "DoubleHpCount";
    const string healCountKey = "HealCount";
    const string largeHealCountKey = "LargeHealCount";
    const string regenCountKey = "RegenCount";
    const string damageBreakCountKey = "DamageBreakCount";
    const string defenceBreakCountKey = "DefenceBreakCount";
    const string windProjectileCountKey = "WindProjectileCount";
    const string windBreathCountKey = "WindBreathCount";
    const string hasteCountKey = "HasteCount";
    const string slowCountKey = "SlowCount";
    const string fireExplosionCountKey = "FireExplosionCount";
    const string nukeExplosionCountKey = "NukeExplosionCount";
    const string waterProjectileCountKey = "WaterProjectileCount";
    const string waterTornadoCountKey = "WaterTornadoCount";
    const string thunderProjectileCountKey = "ThunderProjectileCount";
    const string thunderHawkCountKey = "ThunderHawkCount";
    const string thunderSplashCountKey = "ThunderSplashCount";
    const string thunderStrikeCountKey = "ThunderStrikeCount";
    const string iceProjectileCountKey = "IceProjectileCount";
    const string iceSplashCountKey = "IceSplashCount";
    const string iceGroundCountKey = "IceGroundCount";
    const string holyProjectileCountKey = "HolyProjectileCount";
    const string holyGroundCountKey = "HolyGroundCount";
    const string poisonCountKey = "PoisonCount";
    const string dispelCountKey = "DispelCount";

    //Thunder towers
    [HideInInspector] public int thunderCrystals;
    const string thunderCrystalsKey = "ThunderCrystals";
    [HideInInspector] public List<string> towersActivated;
    const string towersActivatedKey = "TowersActivated";
    [HideInInspector] public bool thunderTowerSolved;
    const string thunderTowerSolvedKey = "ThunderTowerSolved";

    //Totems
    [HideInInspector] public bool totemsSolved;
    [HideInInspector] public List<string> totemsActivated;
    const string totemsSolvedKey = "TotemsSolved";
    const string totemsActivatedKey = "TotemsActivated";

    //Under tower puzzle
    [HideInInspector] public bool underTowerPuzzleSolved;
    const string underTowerPuzzleSolvedKey = "UnderTowerPuzzleSolved";

    //Additional achivements
    [HideInInspector] public int numberOfChestsCollected = 0;
    [HideInInspector] public bool oblivionSurvived;
    [HideInInspector] public bool demolitionSurvived;
    [HideInInspector] public bool diedOfWaiting;
    [HideInInspector] public bool allAreasVisited;
    [HideInInspector] public bool allSignsRead;
    [HideInInspector] public List<string> areasVisited;
    [HideInInspector] public List<string> signsRead;
    const string numberOfChestsCollectedKey = "NumberOfChestsCollected";
    const string oblivionSurvivedKey = "OblivionSurvived";
    const string demolitionSurvivedKey = "DemolitionSurvived";
    const string diedOfWaitingKey = "DiedOfWaiting";
    const string allAreasVisitedKey = "AllAreasVisited";
    const string allSignsReadKey = "AllSignsRead";
    const string areasVisitedKey = "AreasVisited";
    const string signsReadKey = "SignsRead";

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        LoadSave();
    }
    private void Start()
    {
        if (gameManager.bringerDefeated) { numberOfBossesDefeated++; }
        if (gameManager.defenderDefeated) { numberOfBossesDefeated++; }
        if (gameManager.reaperDefeated) { numberOfBossesDefeated++; }
        if (gameManager.nightBorneDefeated) { numberOfBossesDefeated++; }
        if(numberOfBossesDefeated == 1) { savepointIcon1.SetActive(true); }
        if(numberOfBossesDefeated == 2) { savepointIcon1.SetActive(true); savepointIcon2.SetActive(true); }
        if(numberOfBossesDefeated >= 3) { savepointIcon1.SetActive(true); savepointIcon2.SetActive(true); savepointIcon3.SetActive(true); teleportUnlocked = true; gameManager.teleportUnlocked = true; }
    }





    public void SaveGame()
    {
        CreateSaveFile();
        SaveSideQuests();
        SaveCurrency();
        SaveBosses();
        SaveAttackAndDefence();
        SaveKeyItems();
        SaveDoorsAndLevers();
        SaveChests();
        SaveSpells();
        SaveSpellCount();
        SaveThunderCrystals();
        SaveTotems();
        SaveUnderTowerPuzzle();
        SaveAdditionalAchivements();
    }
    public void LoadSave()
    {
        LoadSaveFile();
        LoadSideQuests();
        LoadCurrency();
        LoadBosses();
        LoadAttackAndDefence();
        LoadKeyItems();
        LoadDoorsAndLevers();
        LoadChests();
        LoadSpells();
        LoadSpellCount();
        LoadThunderCrystals();
        LoadTotems();
        LoadUnderTowerPuzzle();
        LoadAdditionalAchivements();
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }

    void SaveText()
    {
        savedText.text = "SAVED!";
        Invoke(nameof(TurnOffSaveText), 2);
    }
    void TurnOffSaveText()
    {
        savedText.text = "";
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            saveButton.SetActive(true);
            if (teleportUnlocked) { warpButton.SetActive(true); }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            saveButton.SetActive(false);
            if (teleportUnlocked) { warpButton.SetActive(false); }
        }
    }
    void CreateSaveFile()
    {
        SaveText();
        saveFileExists = true;
        PlayerPrefs.SetInt(saveFileExistsKey, Convert.ToInt32(saveFileExists));
    }
    void LoadSaveFile()
    {
        saveFileExists = Convert.ToBoolean(PlayerPrefs.GetInt(saveFileExistsKey));
    }


    void SaveSideQuests()
    {
        PlayerPrefs.SetInt(labPuzzleSolvedKey, Convert.ToInt32(gameManager.labPuzzleSolved));
        PlayerPrefs.SetInt(passwordPuzzleSolvedKey, Convert.ToInt32(gameManager.passwordSolved));
        PlayerPrefs.SetInt(bigClockSolvedKey, Convert.ToInt32(gameManager.bigClockSolved));
        PlayerPrefs.SetInt(smallClockSolvedKey, Convert.ToInt32(gameManager.smallClockSolved));
        PlayerPrefs.SetInt(castlePuzzleSolvedKey, Convert.ToInt32(gameManager.castlePuzzleSolved));
        PlayerPrefs.SetInt(holyWaterPuzzleSolvedKey, Convert.ToInt32(gameManager.holyWaterSolved));
    }
    void LoadSideQuests()
    {
        labPuzzleSovled = Convert.ToBoolean(PlayerPrefs.GetInt(labPuzzleSolvedKey));
        passwordPuzzleSovled = Convert.ToBoolean(PlayerPrefs.GetInt(passwordPuzzleSolvedKey));
        bigClockSovled = Convert.ToBoolean(PlayerPrefs.GetInt(bigClockSolvedKey));
        smallClockSovled = Convert.ToBoolean(PlayerPrefs.GetInt(smallClockSolvedKey));
        castlePuzzleSovled = Convert.ToBoolean(PlayerPrefs.GetInt(castlePuzzleSolvedKey));
        holyWaterPuzzleSovled = Convert.ToBoolean(PlayerPrefs.GetInt(holyWaterPuzzleSolvedKey));
    }
    void SaveCurrency()
    {
        PlayerPrefs.SetInt(moneyOwnedKey, gameManager.moneyOwned);
        PlayerPrefs.SetInt(sharpGemsOwnedKey, gameManager.sharpGemsOwned);
        PlayerPrefs.SetInt(magicGemsOwnedKey, gameManager.magicGemsOwned);
        PlayerPrefs.SetInt(specialGemsOwnedKey, gameManager.specialGemsOwned);
    }
    void LoadCurrency()
    {
        moneyOwned = PlayerPrefs.GetInt(moneyOwnedKey);
        sharpGemsOwned = PlayerPrefs.GetInt(sharpGemsOwnedKey);
        magicGemsOwned = PlayerPrefs.GetInt(magicGemsOwnedKey);
        specialGemsOwned = PlayerPrefs.GetInt(specialGemsOwnedKey);
        if (introChestCollected) { moneyOwned = 5000; }
    }
    
    void SaveBosses()
    {
        PlayerPrefs.SetInt(bringerDefeatedKey, Convert.ToInt32(gameManager.bringerDefeated));
        PlayerPrefs.SetInt(defenderDefeatedKey, Convert.ToInt32(gameManager.defenderDefeated));
        PlayerPrefs.SetInt(reaperDefeatedKey, Convert.ToInt32(gameManager.reaperDefeated));
        PlayerPrefs.SetInt(nightBorneDefeatedKey, Convert.ToInt32(gameManager.nightBorneDefeated));
    }
    void LoadBosses()
    {
        bringerDefeated = Convert.ToBoolean(PlayerPrefs.GetInt(bringerDefeatedKey));
        defenderDefeated = Convert.ToBoolean(PlayerPrefs.GetInt(defenderDefeatedKey));
        reaperDefeated = Convert.ToBoolean(PlayerPrefs.GetInt(reaperDefeatedKey));
        nightBorneDefeated = Convert.ToBoolean(PlayerPrefs.GetInt(nightBorneDefeatedKey));
    }
    void SaveAttackAndDefence()
    {
        PlayerPrefs.SetInt(playerPhysicalDamageKey, gameManager.playerPhysicalDamage);
        PlayerPrefs.SetInt(playerMagicDamageKey, gameManager.playerMagicDamage);
        PlayerPrefs.SetInt(playerPhysicalDefenceKey, gameManager.playerPhysicalDefence);
        PlayerPrefs.SetInt(playerHpLevelKey, gameManager.playerMaxHpLevel);
        PlayerPrefs.SetInt(hasJumpSmashAttackKey, Convert.ToInt32(gameManager.jumpSmashAttackUnlocked));
        PlayerPrefs.SetInt(hasComboAttackKey, Convert.ToInt32(gameManager.comboAttasckUnlocked));
    }
    void LoadAttackAndDefence()
    {
        if(saveFileExists == false)
        {
            playerPhysicalDamage = 1;
            playerMagicDamage = 1;
            playerPhysicalDefence = 1;
            playerHpLevel = 1;
            hasJumpSmashAttack = false;
            hasComboAttack = false;
            return;
        }
      
        playerPhysicalDamage = PlayerPrefs.GetInt(playerPhysicalDamageKey);
        playerMagicDamage = PlayerPrefs.GetInt(playerMagicDamageKey);
        playerPhysicalDefence = PlayerPrefs.GetInt(playerPhysicalDefenceKey);
        playerHpLevel = PlayerPrefs.GetInt(playerHpLevelKey);
        hasJumpSmashAttack = Convert.ToBoolean(PlayerPrefs.GetInt(hasJumpSmashAttackKey));
        hasComboAttack = Convert.ToBoolean(PlayerPrefs.GetInt(hasComboAttackKey));
    }
    void SaveKeyItems()
    {
        PlayerPrefs.SetInt(hasDungeonKeyKey ,Convert.ToInt32(gameManager.playerHasDungeonKey));
        PlayerPrefs.SetInt(hasLargeKeyKey ,Convert.ToInt32(gameManager.playerHasLargeKey));
        PlayerPrefs.SetInt(hasSmallKeyKey ,Convert.ToInt32(gameManager.playerHasSmallKey));
        PlayerPrefs.SetInt(hasWornDownKeyKey ,Convert.ToInt32(gameManager.playerHasWornDownKey));
        PlayerPrefs.SetInt(hasRuneKeyKey ,Convert.ToInt32(gameManager.playerHasRuneKey));
    }
    void LoadKeyItems()
    {
        hasDungeonKey = Convert.ToBoolean(PlayerPrefs.GetInt(hasDungeonKeyKey));
        hasLargeKey = Convert.ToBoolean(PlayerPrefs.GetInt(hasLargeKeyKey));
        hasSmallKey = Convert.ToBoolean(PlayerPrefs.GetInt(hasSmallKeyKey));
        hasWornDownKey = Convert.ToBoolean(PlayerPrefs.GetInt(hasWornDownKeyKey));
        hasRuneKey = Convert.ToBoolean(PlayerPrefs.GetInt(hasRuneKeyKey));
    }
    void SaveDoorsAndLevers()
    {
        PlayerPrefs.SetInt(dungeonDoorOpenKey, Convert.ToInt32(gameManager.dungeonDoorOpen));
        PlayerPrefs.SetInt(largeDoorOpenKey, Convert.ToInt32(gameManager.largeDoorOpen));
        PlayerPrefs.SetInt(smallDoorOpenKey, Convert.ToInt32(gameManager.smallDoorOpen));
        PlayerPrefs.SetInt(wornDownDoorOpenKey, Convert.ToInt32(gameManager.wornDoorOpen));
        PlayerPrefs.SetInt(otherSideDoor1OpenKey, Convert.ToInt32(gameManager.otherSide1DoorOpen));
        PlayerPrefs.SetInt(otherSideDoor2OpenKey, Convert.ToInt32(gameManager.otherSide2DoorOpen));
        PlayerPrefs.SetInt(runeDoorOpenKey, Convert.ToInt32(gameManager.runeDoorOpen));
    }
    void LoadDoorsAndLevers()
    {
        dungeonDoorOpen = Convert.ToBoolean(PlayerPrefs.GetInt(dungeonDoorOpenKey));
        largeDoorOpen = Convert.ToBoolean(PlayerPrefs.GetInt(largeDoorOpenKey));
        smallDoorOpen = Convert.ToBoolean(PlayerPrefs.GetInt(smallDoorOpenKey));
        wornDownDoorOpen = Convert.ToBoolean(PlayerPrefs.GetInt(wornDownDoorOpenKey));
        otherSideDoor1Open = Convert.ToBoolean(PlayerPrefs.GetInt(otherSideDoor1OpenKey));
        otherSideDoor2Open = Convert.ToBoolean(PlayerPrefs.GetInt(otherSideDoor2OpenKey));
        runeDoorOpen = Convert.ToBoolean(PlayerPrefs.GetInt(runeDoorOpenKey));
    }
    void SaveChests()
    {
        chestsWereSaved = true;
        if(gameManager.collectedChestsID.Count < 1) { chestsWereSaved = false; }
        PlayerPrefs.SetInt(chestsWereSavedKey, Convert.ToInt32(chestsWereSaved));
        PlayerPrefs.SetString(openedChestsKey, string.Join("###", gameManager.collectedChestsID));
    }
    void LoadChests()
    {
        chestsWereSaved = Convert.ToBoolean(PlayerPrefs.GetInt(chestsWereSavedKey));
        openedChests.Clear();
        if (chestsWereSaved == false) { return; }
        var anArray = PlayerPrefs.GetString(openedChestsKey).Split(new[] { "###" }, StringSplitOptions.None);
        openedChestsTemp = anArray.ToList();
        openedChests = openedChestsTemp.Select(int.Parse).ToList();

    }
    void SaveSpells()
    {
        PlayerPrefs.SetInt(hasHolyBarrierKey, Convert.ToInt32(gameManager.playerHasHolyBarrier));
        PlayerPrefs.SetInt(hasProtectKey, Convert.ToInt32(gameManager.playerHasProtect));
        PlayerPrefs.SetInt(hasOneHitShieldKey, Convert.ToInt32(gameManager.playerHasOneHitShield));
        PlayerPrefs.SetInt(hasDoubleHpKey, Convert.ToInt32(gameManager.playerHasDoubleHp));
        PlayerPrefs.SetInt(hasHealKey, Convert.ToInt32(gameManager.playerHasHeal));
        PlayerPrefs.SetInt(hasLargeHealKey, Convert.ToInt32(gameManager.playerHasLargeHeal));
        PlayerPrefs.SetInt(hasRegenKey, Convert.ToInt32(gameManager.playerHasRegen));
        PlayerPrefs.SetInt(hasDamageBreakKey, Convert.ToInt32(gameManager.playerHasDamageBreak));
        PlayerPrefs.SetInt(hasDefenceBreakKey, Convert.ToInt32(gameManager.playerHasDefenceBreak));
        PlayerPrefs.SetInt(hasWindProjectileKey, Convert.ToInt32(gameManager.playerHasWindProjectile));
        PlayerPrefs.SetInt(hasWindBreathKey, Convert.ToInt32(gameManager.playerHasWindBreath));
        PlayerPrefs.SetInt(hasHasteKey, Convert.ToInt32(gameManager.playerHasHaste));
        PlayerPrefs.SetInt(hasSlowKey, Convert.ToInt32(gameManager.playerHasSlow));
        PlayerPrefs.SetInt(hasFireExplosionKey, Convert.ToInt32(gameManager.playerHasFireExplosion));
        PlayerPrefs.SetInt(hasNukeExplosionKey, Convert.ToInt32(gameManager.playerHasNukeExplosion));
        PlayerPrefs.SetInt(hasWaterProjectileKey, Convert.ToInt32(gameManager.playerHasWaterProjectile));
        PlayerPrefs.SetInt(hasWaterTornadoKey, Convert.ToInt32(gameManager.playerHasWaterTornado));
        PlayerPrefs.SetInt(hasThunderProjectileKey, Convert.ToInt32(gameManager.playerHasThunderProjectile));
        PlayerPrefs.SetInt(hasThunderHawkKey, Convert.ToInt32(gameManager.playerHasThunderHawk));
        PlayerPrefs.SetInt(hasThunderSplashKey, Convert.ToInt32(gameManager.playerHasThunderSplash));
        PlayerPrefs.SetInt(hasThunderStrikeKey, Convert.ToInt32(gameManager.playerHasThunderStrike));
        PlayerPrefs.SetInt(hasIceProjectileKey, Convert.ToInt32(gameManager.playerHasIceProjectile));
        PlayerPrefs.SetInt(hasIceSplashKey, Convert.ToInt32(gameManager.playerHasIceSplash));
        PlayerPrefs.SetInt(hasIceGroundKey, Convert.ToInt32(gameManager.playerHasIceGround));
        PlayerPrefs.SetInt(hasHolyProjectileKey, Convert.ToInt32(gameManager.playerHasHolyProjectile));
        PlayerPrefs.SetInt(hasHolyGroundKey, Convert.ToInt32(gameManager.playerHasHolyGround));
        PlayerPrefs.SetInt(hasPoisonKey, Convert.ToInt32(gameManager.playerHasPoison));
        PlayerPrefs.SetInt(hasDispelKey, Convert.ToInt32(gameManager.playerHasDispel));
    }
    void LoadSpells()
    {
        hasHolyBarrier = Convert.ToBoolean(PlayerPrefs.GetInt(hasHolyBarrierKey));
        hasProtect = Convert.ToBoolean(PlayerPrefs.GetInt(hasProtectKey));
        hasOneHitShield = Convert.ToBoolean(PlayerPrefs.GetInt(hasOneHitShieldKey));
        hasDoubleHp = Convert.ToBoolean(PlayerPrefs.GetInt(hasDoubleHpKey));
        hasHeal = Convert.ToBoolean(PlayerPrefs.GetInt(hasHealKey));
        hasLargeHeal = Convert.ToBoolean(PlayerPrefs.GetInt(hasLargeHealKey));
        hasRegen = Convert.ToBoolean(PlayerPrefs.GetInt(hasRegenKey));
        hasDamageBreak = Convert.ToBoolean(PlayerPrefs.GetInt(hasDamageBreakKey));
        hasDefenceBreak = Convert.ToBoolean(PlayerPrefs.GetInt(hasDefenceBreakKey));
        hasWindProjectile = Convert.ToBoolean(PlayerPrefs.GetInt(hasWindProjectileKey));
        hasWindBreath = Convert.ToBoolean(PlayerPrefs.GetInt(hasWindBreathKey));
        hasHaste = Convert.ToBoolean(PlayerPrefs.GetInt(hasHasteKey));
        hasSlow = Convert.ToBoolean(PlayerPrefs.GetInt(hasSlowKey));
        hasFireExplosion = Convert.ToBoolean(PlayerPrefs.GetInt(hasFireExplosionKey));
        hasNukeExplosion = Convert.ToBoolean(PlayerPrefs.GetInt(hasNukeExplosionKey));
        hasWaterProjectile = Convert.ToBoolean(PlayerPrefs.GetInt(hasWaterProjectileKey));
        hasWaterTornado = Convert.ToBoolean(PlayerPrefs.GetInt(hasWaterTornadoKey));
        hasThunderProjectile = Convert.ToBoolean(PlayerPrefs.GetInt(hasThunderProjectileKey));
        hasThunderHawk = Convert.ToBoolean(PlayerPrefs.GetInt(hasThunderHawkKey));
        hasThunderSplash = Convert.ToBoolean(PlayerPrefs.GetInt(hasThunderSplashKey));
        hasThunderStrike = Convert.ToBoolean(PlayerPrefs.GetInt(hasThunderStrikeKey));
        hasIceProjectile = Convert.ToBoolean(PlayerPrefs.GetInt(hasIceProjectileKey));
        hasIceSplash = Convert.ToBoolean(PlayerPrefs.GetInt(hasIceSplashKey));
        hasIceGround = Convert.ToBoolean(PlayerPrefs.GetInt(hasIceGroundKey));
        hasHolyProjectile = Convert.ToBoolean(PlayerPrefs.GetInt(hasHolyProjectileKey));
        hasHolyGround = Convert.ToBoolean(PlayerPrefs.GetInt(hasHolyGroundKey));
        hasPoison = Convert.ToBoolean(PlayerPrefs.GetInt(hasPoisonKey));
        hasDispel = Convert.ToBoolean(PlayerPrefs.GetInt(hasDispelKey));
    }
    void SaveSpellCount()
    {
        PlayerPrefs.SetInt(holyBarrierCountKey, gameManager.holyBarrierCount);
        PlayerPrefs.SetInt(protectCountKey, gameManager.protectCount);
        PlayerPrefs.SetInt(oneHitShieldCountKey, gameManager.oneHitShieldCount);
        PlayerPrefs.SetInt(doubleHpCountKey, gameManager.doubleHpCount);
        PlayerPrefs.SetInt(healCountKey, gameManager.healCount);
        PlayerPrefs.SetInt(largeHealCountKey, gameManager.largeHealCount);
        PlayerPrefs.SetInt(regenCountKey, gameManager.regenCount);
        PlayerPrefs.SetInt(damageBreakCountKey, gameManager.damageBreakCount);
        PlayerPrefs.SetInt(defenceBreakCountKey, gameManager.defenceBreakCount);
        PlayerPrefs.SetInt(windProjectileCountKey, gameManager.windProjectileCount);
        PlayerPrefs.SetInt(windBreathCountKey, gameManager.windBreathCount);
        PlayerPrefs.SetInt(hasteCountKey, gameManager.hasteCount);
        PlayerPrefs.SetInt(slowCountKey, gameManager.slowCount);
        PlayerPrefs.SetInt(fireExplosionCountKey, gameManager.fireExplosionCount);
        PlayerPrefs.SetInt(nukeExplosionCountKey, gameManager.nukeExplosionCount);
        PlayerPrefs.SetInt(waterProjectileCountKey, gameManager.waterProjectileCount);
        PlayerPrefs.SetInt(waterTornadoCountKey, gameManager.waterTornadoCount);
        PlayerPrefs.SetInt(thunderProjectileCountKey, gameManager.thunderProjectileCount);
        PlayerPrefs.SetInt(thunderHawkCountKey, gameManager.thunderHawkCount);
        PlayerPrefs.SetInt(thunderSplashCountKey, gameManager.thunderSplashCount);
        PlayerPrefs.SetInt(thunderStrikeCountKey, gameManager.thunderStrikeCount);
        PlayerPrefs.SetInt(iceProjectileCountKey, gameManager.iceProjectileCount);
        PlayerPrefs.SetInt(iceSplashCountKey, gameManager.iceSplashCount);
        PlayerPrefs.SetInt(iceGroundCountKey, gameManager.iceGroundCount);
        PlayerPrefs.SetInt(holyProjectileCountKey, gameManager.holyProjectileCount);
        PlayerPrefs.SetInt(holyGroundCountKey, gameManager.holyGroundCount);
        PlayerPrefs.SetInt(poisonCountKey, gameManager.poisonCount);
        PlayerPrefs.SetInt(dispelCountKey, gameManager.dispelCount);
    }
    void LoadSpellCount()
    {
        holyBarrierCount = PlayerPrefs.GetInt(holyBarrierCountKey);
        protectCount = PlayerPrefs.GetInt(protectCountKey);
        oneHitShieldCount = PlayerPrefs.GetInt(oneHitShieldCountKey);
        doubleHpCount = PlayerPrefs.GetInt(doubleHpCountKey);
        healCount = PlayerPrefs.GetInt(healCountKey);
        largeHealCount = PlayerPrefs.GetInt(largeHealCountKey);
        regenCount = PlayerPrefs.GetInt(regenCountKey);
        damageBreakCount = PlayerPrefs.GetInt(damageBreakCountKey);
        defenceBreakCount = PlayerPrefs.GetInt(defenceBreakCountKey);
        windProjectileCount = PlayerPrefs.GetInt(windProjectileCountKey);
        windBreathCount = PlayerPrefs.GetInt(windBreathCountKey);
        hasteCount = PlayerPrefs.GetInt(hasteCountKey);
        slowCount = PlayerPrefs.GetInt(slowCountKey);
        fireExplosionCount = PlayerPrefs.GetInt(fireExplosionCountKey);
        nukeExplosionCount = PlayerPrefs.GetInt(nukeExplosionCountKey);
        waterProjectileCount = PlayerPrefs.GetInt(waterProjectileCountKey);
        waterTornadoCount = PlayerPrefs.GetInt(waterTornadoCountKey);
        thunderProjectileCount = PlayerPrefs.GetInt(thunderProjectileCountKey);
        thunderHawkCount = PlayerPrefs.GetInt(thunderHawkCountKey);
        thunderSplashCount = PlayerPrefs.GetInt(thunderSplashCountKey);
        thunderStrikeCount = PlayerPrefs.GetInt(thunderStrikeCountKey);
        iceProjectileCount = PlayerPrefs.GetInt(iceProjectileCountKey);
        iceSplashCount = PlayerPrefs.GetInt(iceSplashCountKey);
        iceGroundCount = PlayerPrefs.GetInt(iceGroundCountKey);
        holyProjectileCount = PlayerPrefs.GetInt(holyProjectileCountKey);
        holyGroundCount = PlayerPrefs.GetInt(holyGroundCountKey);
        poisonCount = PlayerPrefs.GetInt(poisonCountKey);
        dispelCount = PlayerPrefs.GetInt(dispelCountKey);
    }
    void SaveThunderCrystals()
    {
        PlayerPrefs.SetInt(thunderCrystalsKey, gameManager.thunderCrystals);
        PlayerPrefs.SetString(towersActivatedKey, string.Join("###", gameManager.activatedTowers));
        PlayerPrefs.SetInt(thunderTowerSolvedKey, Convert.ToInt32(gameManager.thunderTowersSolved));
    }
    void LoadThunderCrystals()
    {
        thunderCrystals = PlayerPrefs.GetInt(thunderCrystalsKey);
        thunderTowerSolved = Convert.ToBoolean(PlayerPrefs.GetInt(thunderTowerSolvedKey));
        towersActivated.Clear();
        if (saveFileExists == false) { return; }
        var anArray1 = PlayerPrefs.GetString(towersActivatedKey).Split(new[] { "###" }, StringSplitOptions.None);
        towersActivated = anArray1.ToList();
    }
    void SaveTotems()
    {
        PlayerPrefs.SetInt(totemsSolvedKey,Convert.ToInt32(gameManager.totemsSolved));
        PlayerPrefs.SetString(totemsActivatedKey, string.Join("###", gameManager.activatedTotems));
    }
    void LoadTotems()
    {
        totemsSolved = Convert.ToBoolean(PlayerPrefs.GetInt(totemsSolvedKey));
        totemsActivated.Clear();
        if(saveFileExists == false) { return; }
        var anArray2 = PlayerPrefs.GetString(totemsActivatedKey).Split(new[] { "###" }, StringSplitOptions.None);
        totemsActivated = anArray2.ToList();
    }
    void SaveUnderTowerPuzzle()
    {
        PlayerPrefs.SetInt(underTowerPuzzleSolvedKey, Convert.ToInt32(gameManager.underTowerPuzzleSolved));
    }
    void LoadUnderTowerPuzzle()
    {
        underTowerPuzzleSolved = Convert.ToBoolean(PlayerPrefs.GetInt(underTowerPuzzleSolvedKey));
    }
    void SaveAdditionalAchivements()
    {
        PlayerPrefs.SetInt(numberOfChestsCollectedKey, gameManager.numberOfChestsCollected);
        PlayerPrefs.SetInt(oblivionSurvivedKey, Convert.ToInt32(gameManager.oblivionSurvived));
        PlayerPrefs.SetInt(demolitionSurvivedKey, Convert.ToInt32(gameManager.demolitionSurvived));
        PlayerPrefs.SetInt(diedOfWaitingKey, Convert.ToInt32(gameManager.diedFromWaiting));
        PlayerPrefs.SetInt(allAreasVisitedKey,Convert.ToInt32(gameManager.allAreasVisited));
        PlayerPrefs.SetInt(allSignsReadKey,Convert.ToInt32(gameManager.allSignsRead));
        PlayerPrefs.SetString(areasVisitedKey, string.Join("###", gameManager.visitedAreas));
        PlayerPrefs.SetString(signsReadKey, string.Join("###", gameManager.visitedSigns));
    }
    void LoadAdditionalAchivements()
    {
        numberOfChestsCollected = PlayerPrefs.GetInt(numberOfChestsCollectedKey);
        if (introChestCollected) { numberOfChestsCollected++; }
        oblivionSurvived = Convert.ToBoolean(PlayerPrefs.GetInt(oblivionSurvivedKey));
        demolitionSurvived = Convert.ToBoolean(PlayerPrefs.GetInt(demolitionSurvivedKey));
        diedOfWaiting = Convert.ToBoolean(PlayerPrefs.GetInt(diedOfWaitingKey));
        allAreasVisited = Convert.ToBoolean(PlayerPrefs.GetInt(allAreasVisitedKey));
        allSignsRead = Convert.ToBoolean(PlayerPrefs.GetInt(allSignsReadKey));
        areasVisited.Clear();
        signsRead.Clear();
        if (saveFileExists == false) { return; }
        var anArray1 = PlayerPrefs.GetString(areasVisitedKey).Split(new[] { "###" }, StringSplitOptions.None);
        areasVisited = anArray1.ToList();
        var anArray2 = PlayerPrefs.GetString(signsReadKey).Split(new[] { "###" }, StringSplitOptions.None);
        signsRead = anArray2.ToList();
    }

    //Extra methods for saving during gameplay
    public void SaveOblivionSurvived()
    {
        PlayerPrefs.SetInt(oblivionSurvivedKey, Convert.ToInt32(gameManager.oblivionSurvived));
    }
    public void SaveDemolitionSurvived()
    {
        PlayerPrefs.SetInt(demolitionSurvivedKey, Convert.ToInt32(gameManager.demolitionSurvived));
    }
    public void SaveDiedOfWaiting()
    {
        PlayerPrefs.SetInt(diedOfWaitingKey, Convert.ToInt32(gameManager.diedFromWaiting));
    }
}
