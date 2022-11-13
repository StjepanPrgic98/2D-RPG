using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefenderBattleSystem : MonoBehaviour
{
    PlayerMovement player;
    GameManager gameManager;
    Defender defender;
    AudioPlayer audioPlayer;
    TriggerPlayerStatusScreen statusScreen;
    Failsafe failsafe;
    SaveSystem saveSystem;
    [SerializeField] GameObject battleUI;
    [SerializeField] TextMeshProUGUI battleText;
    [SerializeField] GameObject firstPosition;
    [SerializeField] GameObject secondPosition;
    [SerializeField] GameObject healthCanvas;
    [SerializeField] TextMeshProUGUI playerHealText;
    [SerializeField] int playerHp;
    [SerializeField] int bossHp;
    [SerializeField] TextMeshProUGUI playerHpText;
    [SerializeField] TextMeshProUGUI bossHpText;
    [SerializeField] TextMeshProUGUI playerDamageText;
    [SerializeField] TextMeshProUGUI bossDamageText;
    [SerializeField] TextMeshProUGUI countdownTimer;
    [SerializeField] GameObject countdownTimerObject;
    [SerializeField] GameObject retreat;
    [SerializeField] GameObject dungeonMusic;
    [SerializeField] GameObject defenderBattleMusic;
    [HideInInspector] public int holyBarrierTurns = 4;
    int maxPlayerHp;
    int maxBossHp;
    Vector3 placeOfEncounter;
    bool reflectHit;
    bool inflicted;
    bool playerSlaped;
    

    bool bossWalkToPlayer;
    [HideInInspector] public bool playerzeroPosition = true;
    [HideInInspector] public bool playerfirstPosition;
    [HideInInspector] public bool playersecondPosition;
    bool bossZeroPosition = true;
    bool bossEncounterPosition;
    bool bossFirstPosition;
    bool bossSecondPosition;
    bool regained;
    int hasteTurns = 0;
    int slowTurns = 0;
    int playerSlowTurns = 0;
    int hasteAndSlowTurns = 0;
    [HideInInspector] public List<int> generatedNumbers = new List<int>();
    
    int currentTime = 0;
    int startTime = 3000;
    bool reducedTimer;
    bool timerStarted;
    bool waiting;
    bool skipPlayerTurn;
    bool playerTurnSkiped;

    bool doCounterAction;
    string counterAction;
    int bossMovesDone = 0;
    int tempBossMovesDone = 0;
    [HideInInspector] public bool selfDispel;


    

    

    


    [HideInInspector] public bool defenderBattleStarted = false;

    bool playerMove = true;
    bool bossMove;
    int defenderTurnCount = 0;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        defender = FindObjectOfType<Defender>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        statusScreen = FindObjectOfType<TriggerPlayerStatusScreen>();
        failsafe = FindObjectOfType<Failsafe>();
        saveSystem = FindObjectOfType<SaveSystem>();
    }
    private void Start()
    {
        playerHp = gameManager.playerHp;
        maxBossHp = bossHp;
        maxPlayerHp = gameManager.playerHp;
        playerHpText.text = playerHp + "/" + maxPlayerHp;
        bossHpText.text = bossHp + "/" + maxBossHp;
        currentTime = startTime;
    }

    private void Update()
    {
        if (gameManager.oblivionSurvived) { CheckForBossDeath();return; }
        CheckForPlayerDeathTime();
        Timer();
        if (defenderBattleStarted && player.IsAlive() && defender.isAlive == true)
        {
            if (playerMove)
            {
                PlayerTurn();
            }
            else if (bossMove)
            {
                defenderTurnCount++;
                BossTurn();
            }
        }
        if (bossWalkToPlayer)
        {
            if (playerzeroPosition && bossZeroPosition)
            {
                defender.DefenderWalk();
                defender.transform.position = Vector3.MoveTowards(defender.transform.position, player.transform.position + new Vector3(0.4f, 0.4f, 0), 0.005f);
                if (defender.transform.position == player.transform.position + new Vector3(0.4f, 0.4f, 0))
                {
                    bossWalkToPlayer = false;
                    defender.DefenderAttack();
                    BattleText("Demolition", 2);
                    //StartCoroutine(WaitAndKillPlayer(2f));
                }
            }
            
            else if (playerfirstPosition && bossZeroPosition)
            {
                defender.DefenderWalk();
                defender.transform.position = Vector3.MoveTowards(defender.transform.position, placeOfEncounter + new Vector3(0.4f, 0.2f, 0), 0.005f);
                if (defender.transform.position == placeOfEncounter + new Vector3(0.4f, 0.2f, 0))
                {
                    bossZeroPosition = false;
                    bossFirstPosition = true;
                    bossWalkToPlayer = false;
                    defender.DefenderIdle(0.1f, "isWalking");
                    TurnCanvasOn(1);
                }
            }
            else if(playerfirstPosition && bossFirstPosition)
            {
                defender.DefenderWalk();
                defender.transform.position = Vector3.MoveTowards(defender.transform.position, player.transform.position + new Vector3(0.4f, 0.4f, 0), 0.005f);
                if (defender.transform.position == player.transform.position + new Vector3(0.4f, 0.4f, 0))
                {
                    bossWalkToPlayer = false;
                    defender.DefenderAttack();
                    BattleText("Demolition", 2);
                    //StartCoroutine(WaitAndKillPlayer(2f));
                }
            }
            else if(playersecondPosition && bossFirstPosition)
            {
                defender.DefenderWalk();
                defender.transform.position = Vector3.MoveTowards(defender.transform.position, firstPosition.transform.position + new Vector3(0f, 0.2f, 0), 0.005f);
                if (defender.transform.position == firstPosition.transform.position + new Vector3(0f, 0.2f, 0))
                {
                    bossFirstPosition = false;
                    bossSecondPosition = true;
                    bossWalkToPlayer = false;
                    defender.DefenderIdle(0.1f, "isWalking");
                    TurnCanvasOn(1);
                }
            }
            else if(playersecondPosition && bossSecondPosition)
            {
                defender.DefenderWalk();
                defender.transform.position = Vector3.MoveTowards(defender.transform.position, player.transform.position + new Vector3(0.4f, 0.4f, 0), 0.005f);
                if (defender.transform.position == player.transform.position + new Vector3(0.4f, 0.4f, 0))
                {
                    bossWalkToPlayer = false;
                    defender.DefenderAttack();
                    BattleText("Demolition", 2);
                    //StartCoroutine(WaitAndKillPlayer(2f));
                }
            }
            
        }
    }

    void PlayerTurn()
    {
        CheckForRegen();
        CheckForPlayerPoisonAndBleed();
        if(playerSlowTurns == 4)
        {
            StartCoroutine(WaitAndStartSkippingPlayerTurn(2));
        }
        if (defenderBattleStarted && player.IsActionTaken())
        {
            SwitchTurnToBoss();
            player.SetActionTaken(false);
            if (player.HasHolyBarrier())
            {
                holyBarrierTurns--;
            }
        }
    }

    void BossTurn()
    {
        SwitchTurnToPlayer();
        StartCoroutine(WaitAndDoBossAction(1));
    }


    void SwitchTurnToBoss()
    {
        playerMove = false;
        bossMove = true;
    }
    void SwitchTurnToPlayer()
    {
        playerMove = true;
        bossMove = false;
    }

    void DefenderActionGenerator()
    {
        CheckForBossPoisonAndBleed();
        if (playerfirstPosition && bossZeroPosition)
        {
            bossWalkToPlayer = true;
        }
        if(playersecondPosition && bossFirstPosition)
        {
            bossWalkToPlayer = true;
        }
        if (bossWalkToPlayer) { return; }
        if (player.HasHaste() && hasteTurns >= 4 && doCounterAction == false)
        {
            tempBossMovesDone = bossMovesDone;
            bossMovesDone = 100;
        }
        if (defender.hasSlow && slowTurns >= 4 && doCounterAction == false)
        {
            tempBossMovesDone = bossMovesDone;
            bossMovesDone = 101;
        }
        if (player.HasHaste() && defender.hasSlow && hasteAndSlowTurns >= 2 && doCounterAction == false)
        {
            tempBossMovesDone = bossMovesDone;
            bossMovesDone = 102;
        }
        CheckForHasteAndSlow();

        //Regular battle
        if (bossMovesDone == 0 && doCounterAction == false)
        {
            BattleText("Reflect Shield", 2);
            defender.DefenderReflectShield();
            bossMovesDone++;
        }
        else if (bossMovesDone == 1 && doCounterAction == false)
        {
            BattleText("Protect", 2);
            defender.DefenderProtect();
            bossMovesDone++;
        }
        else if (bossMovesDone == 2 && doCounterAction == false)
        {
            BattleText("One Hit Shield", 2);
            defender.DefenderOneHitShield();
            bossMovesDone++;
        }
        else if (bossMovesDone == 3 && doCounterAction == false)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if (bossMovesDone == 4 && doCounterAction == false)
        {
            BattleText("Waiting...!", 10);
            waiting = true;
            bossMovesDone++;
        }
        else if (bossMovesDone == 5 && doCounterAction == false)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 6 && doCounterAction == false)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 7 && doCounterAction == false)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 8 && doCounterAction == false)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 9 && doCounterAction == false)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 10 && doCounterAction == false)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if (bossMovesDone > 10 && bossMovesDone < 15 && doCounterAction == false)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 15 && doCounterAction == false)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if (bossMovesDone > 15 && bossMovesDone < 20 && doCounterAction == false)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 20 && doCounterAction == false)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if (bossMovesDone > 20 && bossMovesDone < 25 && doCounterAction == false)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 25 && doCounterAction == false)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if (bossMovesDone > 25 && bossMovesDone < 30 && doCounterAction == false && defender.defenderChargeCounter != 5)
        {
            GenerateRandomDefenderMove();
        }
        else if(bossMovesDone == 30 && doCounterAction == false && defender.defenderChargeCounter != 5)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if (bossMovesDone > 30 && bossMovesDone < 35 && doCounterAction == false && defender.defenderChargeCounter != 5)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 35 && doCounterAction == false && defender.defenderChargeCounter != 5)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if (bossMovesDone > 35 && bossMovesDone < 40 && doCounterAction == false && defender.defenderChargeCounter != 5)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 40 && doCounterAction == false && defender.defenderChargeCounter != 5)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if (bossMovesDone > 40 && bossMovesDone < 50 && doCounterAction == false && defender.defenderChargeCounter != 5)
        {
            GenerateRandomDefenderMove();
        }
        else if (bossMovesDone == 50 && doCounterAction == false && defender.defenderChargeCounter != 5)
        {
            BattleText("Charging", 2);
            defender.DefenderCharge();
            bossMovesDone++;
        }
        else if(defender.defenderChargeCounter == 5 && doCounterAction == false)
        {
            bossWalkToPlayer = true;
        }
        if (selfDispel)
        {
            tempBossMovesDone = bossMovesDone;
            bossMovesDone = 103;
        }
        

        //Counter moves
        if (counterAction == "Poison" && selfDispel == false)
        {
            BattleText("Poison", 2);
            counterAction = "";
            defender.PoisonPlayer();
            doCounterAction = false;
        }
        if(counterAction == "Slow" && selfDispel == false)
        {
            BattleText("Slow", 2);
            counterAction = "";
            defender.SlowPlayer();
            doCounterAction = false;
        }
        if(counterAction == "Break Damage" && selfDispel == false)
        {
            BattleText("Break Damage", 2);
            counterAction = "";
            defender.BreakPlayerDamage();
            doCounterAction = false;
        }
        if(counterAction == "Break Defence" && selfDispel == false)
        {
            BattleText("Break Defence", 2);
            counterAction = "";
            defender.BreakPlayerDefence();
            doCounterAction = false;
        }
        if(counterAction == "Dispel")
        {
            BattleText("Dispel", 2);
            counterAction = "";
            defender.DispelPlayer();
            doCounterAction = false;          
        }

        //Condition moves

        if (bossMovesDone == 100)
        {
            BattleText("Skipped due to Haste Effect!", 2);
            hasteTurns = 0;
            bossMovesDone = tempBossMovesDone;
        }
        if (bossMovesDone == 101)
        {
            BattleText("Skipped due to Slow Effect!", 2);
            slowTurns = 0;
            bossMovesDone = tempBossMovesDone;
        }
        if (bossMovesDone == 102)
        {
            BattleText("Skipped due to Haste and Slow Effects!", 2);
            hasteAndSlowTurns = 0;
            slowTurns = 0;
            hasteTurns = 0;
            bossMovesDone = tempBossMovesDone;
        }
        if(bossMovesDone == 103)
        {
            BattleText("Dispel!", 2);
            defender.DefenderDispel();
            selfDispel = false;
            bossMovesDone = tempBossMovesDone;
        }
        
            
        




    }

    void GenerateRandomDefenderMove()
    {
        int randomNum = Random.Range(1, 11);
        randomNum = CheckForRepeatingRandomNumber(randomNum);
        if(randomNum == 1)
        {
            BattleText("Poison", 2);
            defender.PoisonPlayer();
            generatedNumbers.Add(randomNum);
        }
        if(randomNum == 2)
        {
            BattleText("Slow", 2);
            defender.SlowPlayer();
            generatedNumbers.Add(randomNum);
        }
        if (randomNum == 3)
        {
            BattleText("Break Damage", 2);
            defender.BreakPlayerDamage();
            generatedNumbers.Add(randomNum);
        }
        if (randomNum == 4)
        {
            BattleText("Break Defence", 2);
            defender.BreakPlayerDefence();
            generatedNumbers.Add(randomNum);
        }
        if (randomNum == 5)
        {
            BattleText("Dispel!", 2);
            defender.DispelPlayer();
            generatedNumbers.Add(randomNum);
        }
        if(randomNum == 6)
        {
            BattleText("Magic Break!", 2);
            defender.BreakPlayerMagic();
            generatedNumbers.Add(randomNum);
        }
        if(randomNum == 7)
        {
            if (defender.hasProtect) { BossImmune(); bossMovesDone++; return; }
            BattleText("Protect!", 2);
            defender.DefenderProtect();
            generatedNumbers.Add(randomNum);
        }
        if (randomNum == 8)
        {
            if (defender.hasOneHitShield) { BossImmune(); bossMovesDone++; return; }
            BattleText("One Hit Shield!", 2);
            defender.DefenderOneHitShield();
            generatedNumbers.Add(randomNum);
        }
        if (randomNum == 9)
        {
            if (defender.hasReflectShield) { BossImmune(); bossMovesDone++; return; }
            BattleText("Reflect Shield!", 2);
            defender.DefenderReflectShield();
            generatedNumbers.Add(randomNum);
        }
        if (randomNum == 10)
        {
            BattleText("Waiting...!", 10);
            waiting = true;
        }
        bossMovesDone++;
    }
    public void Immune()
    {
        playerHealText.text = "Immune";
        StartCoroutine(WaitAndTurnOffHealText(1));
    }
    public void BossImmune()
    {
        BattleText("Immune!", 1);
    }
    int CheckForRepeatingRandomNumber(int randomNum)
    {
        while(generatedNumbers.Contains(randomNum))
        {
            randomNum = Random.Range(1, 11);
            Debug.Log("Number generated in while loop: " + randomNum);
        }
        return randomNum;
        
    }
    public void PlayerRetreat()
    {
        if (playerzeroPosition)
        {
            player.retreatToFirstPosition = true;
            playerzeroPosition = false;
            defender.defenderChargeCounter -= 2;
        }
        if (playerfirstPosition)
        {
            player.retreatToSecondPosition = true;
            playerfirstPosition = false;
            defender.defenderChargeCounter -= 2;
        }
    }
    public void DoubleHp()
    {
        maxPlayerHp *= 2;
        playerHpText.text = Mathf.Clamp(playerHp, 0, maxPlayerHp) + "/" + maxPlayerHp;
    }
    public void HalvePlayerHp()
    {
        if(player.hasDoubleHp == false) { return; }
        player.hasDoubleHp = false;
        maxPlayerHp /= 2;
        playerHp = Mathf.Clamp(playerHp, 0, maxPlayerHp);
        playerHpText.text = Mathf.Clamp(Mathf.Clamp(playerHp, 0, maxPlayerHp), 0, maxPlayerHp) + "/" + maxPlayerHp;
    }

    public void IsHit(string whoIsHit, int damage)
    {
        if (whoIsHit == "Player")
        {
            player.PlayerIsHurt();
            playerHp -= damage;
            if (reflectHit)
            {
                reflectHit = false;
                playerHpText.text = Mathf.Clamp(playerHp, 1, maxPlayerHp) + "/" + maxPlayerHp;
                if (playerHp <= 1) { playerHp = 1; }
            }
            else
            {
                playerHpText.text = Mathf.Clamp(playerHp, 0, maxPlayerHp) + "/" + maxPlayerHp;
            }          
            playerDamageText.text = "-" + damage;
            StartCoroutine(WaitAndTurnOffDamageText(1));
            CheckForPlayerDeath();
        }
        else if (whoIsHit == "Boss")
        {
            damage = IncreaseDamageToDefender(damage);
            damage = ReduceDamageToDefender(damage);
            if (defender.hasReflectShield) { reflectHit = true; IsHit("Player", damage / 2);}
            bossHp -= damage;
            bossHpText.text = Mathf.Clamp(bossHp,0, maxBossHp) + "/" + maxBossHp;
            bossDamageText.text = "-" + damage;
            StartCoroutine(WaitAndTurnOffDamageText(1));
            defender.hasOneHitShield = false;
            defender.RemoveStatusEffect("- One Hit Shield");

        }
    }

    public void HealPlayer(int hpAmmount)
    {
        playerHp = Mathf.Clamp(playerHp + hpAmmount, 0, maxPlayerHp);
        playerHpText.text = Mathf.Clamp(Mathf.Clamp(playerHp, 0, maxPlayerHp), 0, maxPlayerHp) + "/" + maxPlayerHp;     
        playerHealText.text = "+" + hpAmmount;
        StartCoroutine(WaitAndTurnOffHealText(1));
    }
    void CheckForPlayerPoisonAndBleed()
    {
        if (inflicted) { return; }
        StartCoroutine(WaitAndInflictStatusDamageToPlayer(0.5f));
    }
    void CheckForBossPoisonAndBleed()
    {
        StartCoroutine(WaitAndInflictStatusDamageToBoss(2f));
    }

    IEnumerator WaitAndInflictStatusDamageToPlayer(float delay)
    {
        inflicted = true;
        if (player.HasBleed())
        {
            IsHit("Player", 150);
        }
        yield return new WaitForSeconds(delay);
        if (player.HasPoison())
        {
            IsHit("Player", 170);
        }
    }
    IEnumerator WaitAndInflictStatusDamageToBoss(float delay)
    {
        if (defender.hasPoison)
        {
            IsHit("Boss", player.IncreaseMagicDamage(150));
        }
        yield return new WaitForSeconds(delay);
    }

    void CheckForHasteAndSlow()
    {
        if (player.HasSlow())
        {
            playerSlowTurns++;
        }
        if (player.HasHaste() && defender.hasSlow)
        {
            hasteAndSlowTurns++;
        }
        else if (player.HasHaste())
        {
            hasteTurns++;
        }
        else if (defender.hasSlow)
        {
            slowTurns++;
        }
    }

    int ReduceDamageToDefender(int damage)
    {
        
        if (defender.hasProtect)
        {
            damage /= 2;
        }
        if (defender.hasOneHitShield)
        {
            damage /= 4;
        }
        return damage;
    }
    int IncreaseDamageToDefender(int damage)
    {
        if (defender.defenceBreak)
        {
            damage += (damage * 50) / 100;
        }
        return damage;
    }









    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            dungeonMusic.SetActive(false);
            defenderBattleMusic.SetActive(true);
            if (defenderBattleStarted) { return; }
            this.enabled = true;
            defenderBattleStarted = true;
            battleUI.SetActive(true);
            placeOfEncounter = player.transform.position;
            healthCanvas.SetActive(true);
            retreat.SetActive(true);
            statusScreen.TriggerStatusButton();
        }
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
    void CheckForPlayerDeath()
    {
        if(playerHp <= 0)
        {
            player.PlayerDeath();
            countdownTimerObject.SetActive(false);
            defenderBattleStarted = false;
            battleUI.SetActive(false);
        }
    }
    void CheckForPlayerDeathTime()
    {
        if (currentTime <= 0)
        {
            player.PlayerDeath();
            countdownTimerObject.SetActive(false);
            defenderBattleStarted = false;
            battleUI.SetActive(false);
        }
    }
    void CheckForBossDeath()
    {       
        failsafe.defenderDestroyed = true;
        countdownTimerObject.SetActive(false);
        defenderBattleStarted = false;
        battleUI.SetActive(false);
        defender.DefenderDeath();
        player.SheatSword();
        statusScreen.TriggerStatusButton();
        healthCanvas.SetActive(false);
        ClearAllStatuses();
        dungeonMusic.SetActive(true);
        defenderBattleMusic.SetActive(false);
        bossDamageText.text = "";
        failsafe.FixAllPlayerStatuses();
        defender.RemovePlayerBreak();
        this.enabled = false;      
    }

    void ClearAllStatuses()
    {
        player.DispelPlayer();
        defender.DispelDefender();
    }


    void Timer()
    {
        if(timerStarted == false) { countdownTimerObject.SetActive(true); timerStarted = true; }
        if(currentTime <= 0) { return; }
        if(reducedTimer == false)
        {
            StartCoroutine(WaitAndReduceTimer(0.1f));
        }
    }
    public void CheckForCounterAction(bool argument, string counterActionArgument)
    {
        doCounterAction = argument;
        counterAction = counterActionArgument;
    }

    IEnumerator WaitAndReduceTimer(float delay)
    {
        reducedTimer = true;
        yield return new WaitForSeconds(delay);
        currentTime -= 1;
        countdownTimer.text = currentTime.ToString();
        reducedTimer = false;
    }

    void TurnCanvasOn(float delay)
    {
        if(playerSlowTurns == 4) { return; }
        StartCoroutine(WaitAndTurnBattleUIOn(delay));
    }


    IEnumerator WaitAndDoBossAction(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bossHp <= 0 || gameManager.demolitionSurvived) { CheckForBossDeath(); yield break; }
        DefenderActionGenerator();
        if (bossWalkToPlayer) { yield break; }
        if (waiting)
        {
            waiting = false;
            yield return new WaitForSeconds(10);
            IsHit("Player", 1);
            audioPlayer.PlaySlapClip();
            playerSlaped = true;
            CheckIfDiedFromSlap();
        }
        inflicted = false;
        TurnCanvasOn(1);
    }

    IEnumerator WaitAndTurnBattleUIOn(float delay)
    {
        yield return new WaitForSeconds(delay);
        battleUI.SetActive(true);
        regained = false;
    }
    IEnumerator WaitAndStartSkippingPlayerTurn(float delay)
    {
        playerSlowTurns = 0;
        yield return new WaitForSeconds(delay);
        BattleText("Player turn skipped due to Slow effect!", 5);
        skipPlayerTurn = true;
        StartCoroutine(WaitAndSkipPlayerTurn(5));
    }
    IEnumerator WaitAndSkipPlayerTurn(float delay)
    {
        skipPlayerTurn = false;
        yield return new WaitForSeconds(delay);
        player.SetActionTaken(true);
        
    }

    void BattleText(string text, float delay)
    {
        StartCoroutine(WaitAndTurnOffBattleText(delay, text));
    }
    IEnumerator WaitAndTurnOffBattleText(float delay, string text)
    {
        battleText.text = text;
        yield return new WaitForSeconds(delay);
        battleText.text = "";
    }
    IEnumerator WaitAndKillPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        player.PlayerDeath();
    }
    IEnumerator WaitAndTurnOffDamageText(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerDamageText.text = "";
        bossDamageText.text = "";
    }
    IEnumerator WaitAndTurnOffHealText(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerHealText.text = "";
    }
    public void CheckIfSurvivedDemolition()
    {
        StartCoroutine(WaitAndCheckIfSurvivedDemolition(3));
    }
    IEnumerator WaitAndCheckIfSurvivedDemolition(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(playerHp > 0 && defender.didDemolition)
        {
            gameManager.demolitionSurvived = true;
            saveSystem.SaveDemolitionSurvived();
            CheckForBossDeath();
        }
    }
    public void CheckIfDiedFromSlap()
    {
        StartCoroutine(WaitAndCheckIfDiedFromSlap(5));
    }
    IEnumerator WaitAndCheckIfDiedFromSlap(float delay)
    {
        yield return new WaitForSeconds(delay);
        if(playerHp <= 0 && playerSlaped)
        {
            gameManager.diedFromWaiting = true;
            saveSystem.SaveDiedOfWaiting();
        }
    }
    IEnumerator WaitAndSwitchTurnToPlayer()
    {
        yield return new WaitForSeconds(3);
        SwitchTurnToPlayer();
    }



    void DoesNothing()
    {
        if (skipPlayerTurn) { return; }
    }
}
