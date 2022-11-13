using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class Battle : MonoBehaviour
{
    GameManager gameManager;
    TriggerPlayerStatusScreen statusScreen;
    SaveSystem saveSystem;
    
    //Variables
    int turnCount = 0;
    bool elementalPhase;
    [HideInInspector] public bool battleCanvasIsOn;
    [HideInInspector] public int maxPlayerHp;
    int maxBossHealth;
    [HideInInspector] public int timesHitWithSword = 0;
    [HideInInspector] public int timesHitWithMagic = 0;
    [HideInInspector] public int timesHitWithWeakToElement = 0;
    [HideInInspector] public int holyBarrierTurns = 4;
    [HideInInspector] public int hasteTurn = 0;
    [HideInInspector] public int slowTurn = 0;
    [HideInInspector] public int hasteAndSlowTurn = 0;
    int poisonTurns;
    int darkPhaseTurns = 0;
    bool regained;
    [HideInInspector] public bool spellAmmountReduced;


    //Bools
    bool playerMove = true;
    bool bossMove = false;
    bool playerIsAlive = true;
    bool bossIsAlive = true;
    bool startBattle;
    bool firePhase = true;
    bool waterPhase;
    bool thunderPhase;
    bool icePhase;
    bool turnsReset;
    bool oblivionPhase = true;
    bool darkPhase;
    [HideInInspector] public bool playerIsHit;
    [HideInInspector] public bool bringerFightStart;

    //SerializeFields
    [Header("Player and Boss stats")]
    [SerializeField] int playerHealth = 3;
    [SerializeField] int bossHealth = 10;
    [SerializeField] List<GameObject> listOfAbilities;
    [SerializeField] List<TextMeshProUGUI> abilitiesCount;

    [Header("Object references")]
    [SerializeField] PlayerMovement player;
    [SerializeField] Boss boss;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject battleSystem;
    [SerializeField] GameObject instantDeathZone;
    [SerializeField] GameObject healthCanvas;
    [SerializeField] TextMeshProUGUI bossHealthText;
    [SerializeField] TextMeshProUGUI playerHealthText;
    [SerializeField] TextMeshProUGUI playerHealText;
    [SerializeField] CantCollectYet cantCollectYet;
    [SerializeField] Chests chest1;
    [SerializeField] Chests chest2;
    [SerializeField] Chests chest3;
    [SerializeField] TextMeshProUGUI battleText;
    [SerializeField] TextMeshProUGUI damageTextPlayer;
    [SerializeField] TextMeshProUGUI damageTextBoss;
    [SerializeField] Torch torch;
    [SerializeField] AudioSource bringerBattleMusic;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] GameObject backgroundMusicObject;
    [SerializeField] GameObject bringerChaseMusic;

    //-------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Awake(), Start(), Update() methods

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        statusScreen = FindObjectOfType<TriggerPlayerStatusScreen>();
        saveSystem = FindObjectOfType<SaveSystem>();
    }
    private void Start()
    {
        playerHealth = gameManager.playerHp;
        maxBossHealth = bossHealth;
        maxPlayerHp = gameManager.playerHp;
        bossHealthText.text = bossHealth + "/" + maxBossHealth;
        playerHealthText.text = playerHealth + "/" + maxPlayerHp;
        damageTextBoss.text = "";
        damageTextPlayer.text = "";
        
    }

    private void Update()
    {
        if (StartBattle() && playerIsAlive && bossIsAlive)
        {
            ChangePhase();         
            CheckForPlayerDeath();
            ElementalPhase();
            if (playerMove && playerIsAlive)
            {             
                PlayerTurn();
            }
            else if(bossMove && bossIsAlive)
            {
                BossTurn();
            }          
        }
    }
//-----------------------------------------------------------------------------------------------------------------------------------------------------------

    //Getter methods

    public bool StartBattle()
    {
        return startBattle;
    }
    public int GetPlayerHealth()
    {
        return playerHealth;
    }
    public int GetBossHealth()
    {
        return bossHealth;
    }

    public string GetPhase()
    {
        if (firePhase)
        {
            return "fire";
        }
        else if (waterPhase)
        {
            return "water";
        }
        else if (thunderPhase)
        {
            return "thunder";
        }
        else if (icePhase)
        {
            return "ice";
        }
        else
        {
            return "dark";
        }
    }
    public List<GameObject> GetAbilities()
    {
        return listOfAbilities;
    }
    public List<TextMeshProUGUI> GetAbilitiesCount()
    {
        return abilitiesCount;
    }
    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Setter methods
    public void HealPlayer(int hpAmmount)
    {

        if (player.hasDoubleHp)
        {
            playerHealth = Mathf.Clamp(playerHealth + hpAmmount, 0, maxPlayerHp);
            playerHealthText.text = Mathf.Clamp(Mathf.Clamp(playerHealth, 0, maxPlayerHp), 0, maxPlayerHp) + "/" + maxPlayerHp;
        }
        else
        {
            playerHealth = Mathf.Clamp(playerHealth + hpAmmount, 0, maxPlayerHp);
            playerHealthText.text = Mathf.Clamp(Mathf.Clamp(playerHealth, maxPlayerHp, 2500), 0, maxPlayerHp) + "/" + maxPlayerHp;
        }
        playerHealText.text = "+" + hpAmmount;
        StartCoroutine(WaitAndTurnOffHealText(1));

    }
    public void ReducePlayerMaxHp()
    {
        maxPlayerHp = gameManager.playerHp;
        playerHealth = Mathf.Clamp(playerHealth, 0, maxPlayerHp);
        playerHealthText.text = Mathf.Clamp(Mathf.Clamp(playerHealth, 0, maxPlayerHp), 0, maxPlayerHp) + "/" + maxPlayerHp;
    }
    public void SetPlayerMaxHpToNormal()
    {
        if (player.hasDoubleHp)
        {
            maxPlayerHp = gameManager.playerHp * 2;
        }
        else
        {
            maxPlayerHp = gameManager.playerHp;
        }
        playerHealth = Mathf.Clamp(playerHealth, 0, maxPlayerHp);
        playerHealthText.text = Mathf.Clamp(Mathf.Clamp(playerHealth, 0, maxPlayerHp), 0, maxPlayerHp) + "/" + maxPlayerHp;
    }
    IEnumerator WaitAndTurnOffHealText(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerHealText.text = "";
    }

    //--------------------------------------------------------------------------------------------------------------------------------------------------------------

    //OnCollision and OnTrigger methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && gameManager.bringerChaseStarted == false)
        {
            this.enabled = true;
            startBattle = true;
            healthCanvas.SetActive(true);       
            bringerFightStart = true;
            bringerBattleMusic.Play();
            backgroundMusic.Stop();
            statusScreen.TriggerStatusButton();
        }
        if (collision.tag == "Player" && gameManager.bringerChaseStarted == true)
        {
            this.enabled = true;
            startBattle = true;
            healthCanvas.SetActive(true);
            bringerFightStart = true;
            //bringerBattleMusic.Play();
            //backgroundMusic.Stop();
            statusScreen.TriggerStatusButton();
        }
    }
//-------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Battle System methods

    public void PlayerTurn()                                                //Called in Update(), waits for player Attack or Spell called from button press
    {
                                                                         //to set actionIsTaken to true, then it gives turn to the boss
        if (player.IsActionTaken())
        {
            SwitchTurnToBoss();
            player.SetActionTaken(false);
            if (player.HasHolyBarrier())
            {
                holyBarrierTurns--;
            }
        }
    }

    void BossTurn()                                                         //Called in Update(), waits for some time, and does an Attack or Spell
    {
        CheckForBossDeath();
        if(bossIsAlive == false) { return; }
        SwitchTurnToPlayer();
        if (darkPhase)
        {
            darkPhaseTurns++;
        }
        else
        {
            turnCount++;
        }
        StartCoroutine(WaitForBossAction(0.8f));
    }
    void SwitchTurnToPlayer()
    {
        playerMove = true;
        bossMove = false;
    }
    void SwitchTurnToBoss()
    {
        playerMove = false;
        bossMove = true;
    }
    void CheckForPlayerDeath()                                               //Called in Update(), checks if player is alive, if not calls PlayerDeath() method
    {                                                                        //from player script
        if (playerHealth <= 0)
        {
            playerIsAlive = false;
            player.PlayerDeath();
        }
    }
    void CheckForBossDeath()
    {
        if(bossHealth <= 0)
        {
            gameManager.bringerDefeated = true;
            bossIsAlive = false;
            boss.BossDeath();
            StartCoroutine(WaitForEndOfBattle(1));
            player.DispelPlayer();
        }
    }

    public void IsHit(string whoWasHit, int damageDealth)                                     //Called in player and boss scripts on Attacks and Spells
    {
        if(whoWasHit == "Player")
        {
            playerHealth -= damageDealth;
            if(damageDealth == 0) { return; }
            playerHealthText.text = Mathf.Clamp(playerHealth,0,maxPlayerHp) + "/" + maxPlayerHp;
            damageTextPlayer.text = "-" + damageDealth;
            player.RemoveOneHitBarrier();
            playerIsHit = true;
            StartCoroutine(WaitAndResetDamageText(0.5f));
            
        }
        else if(whoWasHit == "Boss")
        {
            if (boss.BossHasShield()) { damageDealth -= (damageDealth * 50) / 100; }
            if (boss.defenceBreak) { damageDealth += (damageDealth * 50) / 100; }
            bossHealth -= damageDealth;
            bossHealthText.text = Mathf.Clamp(bossHealth,0,256000) + "/" + maxBossHealth;
            damageTextBoss.text = "-" + damageDealth;
            if (boss.BossHasReflectShield())
            {
                playerHealth -= damageDealth / 2;
                playerHealthText.text = Mathf.Clamp(playerHealth, 0, maxPlayerHp) + "/" + maxPlayerHp;
                damageTextPlayer.text = "-" + damageDealth / 2;
            }
            StartCoroutine(WaitAndResetDamageText(0.8f));
        }
    }
//------------------------------------------------------------------------------------------------------------------------------------------------------------

    //UI Canvas methods

    void TurnCanvasOn()                                                     //Sets buttons to not be interactable, then turns UI on, then WaitForCanvas()
    {                                                                       //Corutine sets buttons to interactable after some time
        if (!playerIsAlive) { return; } 
        
        canvas.SetActive(true);
        CheckForRegen();
    }
    public void TurnCanvasOff()
    {
        canvas.SetActive(false);
    }
    public void DoubleHp()
    {
        maxPlayerHp *= 2;
        playerHealthText.text = Mathf.Clamp(playerHealth, 0, maxPlayerHp) + "/" + maxPlayerHp;
    }
    public void HalvePlayerHp()
    {
        player.hpHalved = true;
        maxPlayerHp /= 2;
        playerHealth = Mathf.Clamp(playerHealth, 0, maxPlayerHp);
        playerHealthText.text = Mathf.Clamp(Mathf.Clamp(playerHealth, 0, maxPlayerHp), 0, maxPlayerHp) + "/" + maxPlayerHp;
    }
    void CheckForRegen()
    {
        if (regained) { return; }
        if (player.hasRegen)
        {
            regained = true;
            HealPlayer(player.IncreaseMagicDamage(150));
        }
    }

    //------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Wait for time methods

    IEnumerator WaitForBossAction(float delayAction)                        //After player has done a move, it waits for some time before boss does a move
    {      
        if(bossIsAlive == false) { yield break; }
        yield return new WaitForSeconds(delayAction);
        CheckForPoison();
        BossActionGenerator();
        
    }
    void BossActionGenerator()
    {
        OblivionPhase();
        if(oblivionPhase) { return; }
        int randomNumber = Random.Range(1, 5);     
        if(turnCount == 1 || darkPhaseTurns == 3)
        {
            randomNumber = 100;
        }
        if(turnCount == 2 || darkPhaseTurns == 4)
        {
            randomNumber = 101;
        }
        if(darkPhaseTurns == 1)
        {
            randomNumber = 97;
            timesHitWithMagic = 0;
            timesHitWithSword = 0;
            hasteAndSlowTurn = 0;
            hasteTurn = 0;
            slowTurn = 0;
        }
        if(darkPhaseTurns == 2)
        {
            randomNumber = 96;
            timesHitWithMagic = 0;
            timesHitWithSword = 0;
            hasteAndSlowTurn = 0;
            hasteTurn = 0;
            slowTurn = 0;
        }
        if(timesHitWithSword == 5)
        {
            timesHitWithSword = 0;
            randomNumber = 98;
        }
        if(timesHitWithMagic == 5)
        {
            timesHitWithMagic = 0;
            randomNumber = 99;
        }
        if (player.HasHaste() && hasteTurn == 4)
        {
            randomNumber = 102;
        }
        if(boss.BossHasSlow() && slowTurn == 4)
        {
            randomNumber = 103;
        }
        if(player.HasHaste() && boss.BossHasSlow() && hasteAndSlowTurn == 2)
        {
            randomNumber = 104;
        }
        CheckForHasteAndSlow();
        



        if (randomNumber == 1)
        {
            boss.BossMagic("regular");
            BattleInformationText("Magic");
        }
        else if (randomNumber == 2)
        {
            boss.BossAttack();
            BattleInformationText("Regular");
        }
        else if (randomNumber == 3)
        {
            boss.DarkBoulder();
            BattleInformationText("Dark boulder!");
        }
        else if (randomNumber == 4)
        {
            boss.DarkBolt();
            BattleInformationText("Dark bolt!");
        }
        else if(randomNumber == 96)
        {
            boss.ReflectShield();
            BattleInformationText("Reflect Shield!");
        }
        else if(randomNumber == 97)
        {
            boss.Dispel();
            BattleInformationText("Dispel!");
        }
        else if (randomNumber == 98)
        {
            boss.BossMagic("mega");
            BattleInformationText("Mega");
        }
        else if(randomNumber == 99)
        {
            boss.BossProjectile();
            BattleInformationText("Lost souls");
        }
        else if (randomNumber == 101)
        {
            boss.BossCurseSpell(); 
            BattleInformationText("Break");
        }
        else if (randomNumber == 100)
        {
            boss.BossShield(); 
            BattleInformationText("Dark shield");
        }
        else if(randomNumber == 102)
        {
            BattleInformationText("Skiped due to Haste Effect!");
            hasteTurn = 0;
            
        }
        else if(randomNumber == 103)
        {
            BattleInformationText("Skiped due to Slow Effect!");
            slowTurn = 0;
        }
        else if(randomNumber == 104)
        {
            BattleInformationText("Skiped due to Haste and Slow Effects!");
            hasteAndSlowTurn = 0;
        }



        
        StartCoroutine(WaitForPlayerAction(1f));

    }

    void BattleInformationText(string text)
    {
        battleText.text = text;
        StartCoroutine(WaitAndResetBattleText(2));
    }

    void OblivionPhase()
    {
        if (oblivionPhase == false) { return; }
        if (turnCount == 1)
        {
            battleText.text = "Oblivion in 5 turns!";
            StartCoroutine(WaitAndResetBattleText(2));
        }
        if (turnCount == 2)
        {
            battleText.text = "Oblivion in 4 turns!";
            StartCoroutine(WaitAndResetBattleText(2));
        }
        if (turnCount == 3)
        {
            battleText.text = "Oblivion in 3 turns!";
            StartCoroutine(WaitAndResetBattleText(2));
        }
        if (turnCount == 4)
        {
            battleText.text = "Oblivion in 2 turns!";
            StartCoroutine(WaitAndResetBattleText(2));
        }
        if (turnCount == 5)
        {
            battleText.text = "Oblivion in 1 turns!";
            StartCoroutine(WaitAndResetBattleText(2));
        }
        if (turnCount == 6)
        {
            turnCount = 0;
            timesHitWithMagic = 0;
            battleText.text = "Oblivion!";
            boss.Oblivion();
            StartCoroutine(WaitAndResetBattleText(2));
            StartCoroutine(WaitAndStartFightAfterOblivion(2));
        }
        StartCoroutine(WaitForPlayerAction(1f));
    }
    void ElementalPhase()
    {
        if (firePhase)
        {
            torch.SetAnimatorToFalse();
            torch.ChangeColor("fire");
        }
        else if (thunderPhase)
        {
            torch.ChangeColor("thunder");
        }
        else if (waterPhase)
        {
            torch.SetAnimatorToFalse();
            torch.ChangeColor("water");
        }
        else if (icePhase)
        {
            torch.SetAnimatorToFalse();
            torch.ChangeColor("ice");
        }
        else if (darkPhase)
        {
            torch.SetAnimatorToFalse();
            torch.ChangeColor("dark");
        }
    }
    void ChangePhase()
    {
        if(bossHealth <= 100000 && darkPhase == false)
        {
            SetPhaseToFalse();
            darkPhase = true;
            timesHitWithMagic = 0;
            timesHitWithSword = 0;
            hasteAndSlowTurn = 0;
            hasteTurn = 0;
            slowTurn = 0;
        }
        if(bossHealth <= 228000 && turnsReset == false && oblivionPhase)
        {
            oblivionPhase = false;
            turnCount = 1;
            timesHitWithMagic = 0;
            turnsReset = true;
        }
        if (timesHitWithWeakToElement == 2 && darkPhase == false)
        {
            timesHitWithWeakToElement = 0;
            int randNum = Random.Range(1, 4);
            if (firePhase)
            {
                firePhase = false;
                if(randNum == 1)
                {
                    waterPhase = true;
                }
                else if(randNum == 2)
                {
                    icePhase = true;
                }
                else if(randNum == 3)
                {
                    thunderPhase = true;
                }
            }
            else if (waterPhase)
            {
                waterPhase = false;
                if(randNum == 1)
                {
                    firePhase = true;
                }
                else if(randNum == 2)
                {
                    icePhase = true;
                }
                else if(randNum == 3)
                {
                    thunderPhase = true;
                }
            }
            else if (icePhase)
            {
                icePhase = false;
                if(randNum == 1)
                {
                    firePhase = true;
                }
                else if(randNum == 2)
                {
                    waterPhase = true;
                }
                else if(randNum == 3)
                {
                    thunderPhase = true;
                }
            }
            else if (thunderPhase)
            {
                thunderPhase = false;
                if(randNum == 1)
                {
                    firePhase = true;
                }
                else if(randNum == 2)
                {
                    waterPhase = true;
                }
                else if(randNum == 3)
                {
                    icePhase = true;
                }
            }
        }
        
    }

    void SetPhaseToFalse()
    {
        firePhase = false;
        waterPhase = false;
        icePhase = false;
        thunderPhase = false;
    }

    void CheckForHasteAndSlow()
    {
        if (player.HasHaste() && boss.BossHasSlow())
        {
            hasteAndSlowTurn++;
        }
        else if (player.HasHaste())
        {
            hasteTurn++;
        }
        else if (boss.BossHasSlow())
        {
            slowTurn++;
        }
    }

    void CheckForPoison()
    {
        if (boss.BossHasPoison())
        {
            if (poisonTurns == 5)
            {
                boss.RemoveBossPoison();
            }
            else
            {
                IsHit("Boss", 750);
            }
            poisonTurns++;
        }
    }
    public void ReduceSpellAmmountText()
    {
        if (spellAmmountReduced) { return; }
        spellAmmountReduced = true;
        BattleInformationText("Number of spell casts reduced!");
    }
    IEnumerator WaitForPlayerAction(float delayAction)                     //After boss has done a move, it wasits for some time before player can do a move
    {
        yield return new WaitForSeconds(delayAction);
        StartCoroutine(WaitForCanvas(1.5f));      
    }

    IEnumerator WaitForCanvas(float delay)                                 //After boss move, waits some time to turn the canvas on, then waits bit more to
    {                                                                      //make buttons interactable
        yield return new WaitForSeconds(delay);
        TurnCanvasOn();
        player.ResetDefenceState();
        yield return new WaitForSeconds(1f);
        
    }    
    IEnumerator WaitForEndOfBattle(float delay)
    {
        yield return new WaitForSeconds(delay);
        startBattle = false;
        battleSystem.SetActive(false);
        healthCanvas.SetActive(false);
        instantDeathZone.SetActive(false);
        player.ClearNegativeEffects();
        player.ClearPositiveEffects();
        boss.DispelBoss();
        chest1.MakeChestGood();
        chest2.MakeChestGood();
        chest3.MakeChestGood();
        player.SheatSword();
        statusScreen.TriggerStatusButton();
        bringerChaseMusic.SetActive(false);
        backgroundMusicObject.SetActive(true);
        backgroundMusic.Play();
        this.enabled = false;
    }
    
    IEnumerator WaitAndResetBattleText(float delay)
    {
        yield return new WaitForSeconds(delay);
        battleText.text = "";
        
    }
    IEnumerator WaitAndResetDamageText(float delay)
    {
        yield return new WaitForSeconds(delay);
        damageTextBoss.text = "";
        damageTextPlayer.text = "";
    }
    IEnumerator WaitAndStartFightAfterOblivion(float delay)
    {
        yield return new WaitForSeconds(delay);
        oblivionPhase = false;
    }
    public void CheckIfOblivionIsSurvived()
    {
        StartCoroutine(WaitAndCheckIfOblivionIsSurvived(3));
    }
    IEnumerator WaitAndCheckIfOblivionIsSurvived(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(playerHealth > 0 && boss.didOblivion)
        {
            gameManager.oblivionSurvived = true;
            saveSystem.SaveOblivionSurvived();
        }
    }
}
