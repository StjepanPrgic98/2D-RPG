using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReaperBattleSystem : MonoBehaviour
{
    [SerializeField] GameObject battleUI;
    [SerializeField] TextMeshProUGUI battleText;
    [SerializeField] GameObject healthCanvas;
    [SerializeField] int playerHp;
    [SerializeField] int bossHp;
    [SerializeField] TextMeshProUGUI playerHpText;
    [SerializeField] TextMeshProUGUI bossHpText;
    [SerializeField] TextMeshProUGUI playerDamageText;
    [SerializeField] TextMeshProUGUI bossDamageText;
    [SerializeField] TextMeshProUGUI playerHealText;
    [SerializeField] GameObject dungeonMusic;
    [SerializeField] GameObject reaperBattleMusic;
    [HideInInspector] public int maxPlayerHp;
    [HideInInspector] public int holyBarrierTurns = 4;
    List<string> negativeEffect = new List<string>() { "Hp Halved", "Defence Break", "Magic Break", "Damage Break", "Heal Break" };
    int maxBossHp;
    bool inflicted;
    bool regained;
    [HideInInspector] public int reaperTimesHit;
    bool firstSummonDestroyed;
    bool secondSummonDestroyed;
    bool thirdSummonDestroyed;
    bool fourthSummonDestroyed;
    int hasteTurns = 0;
    int slowTurns = 0;
    int hasteAndSlowTurns = 0;
    bool reaperIsAlive = true;

    [HideInInspector] public bool reaperDefenceBreak;



    [HideInInspector] public bool reaperStartBattle;
    bool playerMove = true;
    bool bossMove;
    int reaperTurnCount = 0;

    PlayerMovement player;
    GameManager gameManager;
    Reaper reaper;
    Summon[] summons;
    TriggerPlayerStatusScreen statusScreen;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        reaper = FindObjectOfType<Reaper>();
        summons = FindObjectsOfType<Summon>();
        gameManager = FindObjectOfType<GameManager>();
        statusScreen = FindObjectOfType<TriggerPlayerStatusScreen>();
    }

    private void Start()
    {
        playerHp = gameManager.playerHp;
        maxPlayerHp = gameManager.playerHp;
        maxBossHp = bossHp;
        playerHpText.text = playerHp + "/" + maxPlayerHp;
        bossHpText.text = bossHp + "/" + maxBossHp;
    }
    private void Update()
    {
        CheckForPlayerDeath();
        if (reaperStartBattle && player.IsAlive() && reaper.isAlive == true)
        {
            CheckToDestroySummon();
            if (playerMove)
            {
                PlayerTurn();
            }
            else if (bossMove)
            {
                BossTurn();
            }
        }
    }








    void PlayerTurn()
    {
        CheckForRegen();
        CheckForPlayerPoisonAndBleed();
        if (reaperStartBattle && player.IsActionTaken())
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
        CheckForBossPoisonAndBleed();
        SwitchTurnToPlayer();
        reaperTurnCount++;
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

    void ReaperActionGenerator()
    {
        CheckForBossDeath();
        if (reaperIsAlive == false) { return; }
        int randomNumber = Random.Range(1, 3);

        if (player.HasHaste() && hasteTurns >= 4)
        {
            randomNumber = 4;
        }
        if (reaper.hasSlow && slowTurns >= 4)
        {
            randomNumber = 5;
        }
        if (player.HasHaste() && reaper.hasSlow && hasteAndSlowTurns >= 2)
        {
            randomNumber = 6;
        }
        CheckForHasteAndSlow();

        if (reaperTurnCount == 1)
        {
            randomNumber = 3;
        }
        if (reaperTurnCount == 2)
        {
            randomNumber = 1;
        }
        if (reaperTurnCount == 3)
        {
            randomNumber = 2;
        }
        if(reaperTurnCount == 15 || reaperTurnCount == 16)
        {
            randomNumber = 3;
            reaperTurnCount = 17;
        }
        if(reaperTurnCount == 35 || reaperTurnCount == 36)
        {
            randomNumber = 3;
            reaperTurnCount = 37;
        }

        

        if (reaperTimesHit >= 5)
        {
            randomNumber = 3;
            reaperTimesHit = 0;
        }
        


        if (randomNumber == 1)
        {
            reaper.reaperAttack = true;
            TurnCanvasOn(5);
            
        }
        else if (randomNumber == 2)
        {
            reaper.reaperAttack = true;
            reaper.reaperSpikeAttack = true;
            TurnCanvasOn(5);
            
        }
        else if (randomNumber == 3)
        {
            reaper.ReaperSummon();
        }
        else if(randomNumber == 4)
        {
            BattleText("Skipped due to Haste Effect!");
            hasteTurns = 0;
        }
        else if(randomNumber == 5)
        {
            BattleText("Skipped due to Slow Effect!");
            slowTurns = 0;
        }
        else if(randomNumber == 6)
        {
            BattleText("Skipped due to Haste and Slow Effects!");
            hasteAndSlowTurns = 0;
        }

    }

    public void IsHit(string whoIsHit, int damage, bool hurtPlayer)
    {
        if (whoIsHit == "Player")
        {

            playerHp -= damage;
            playerHpText.text = Mathf.Clamp(playerHp, 0, maxPlayerHp) + "/" + maxPlayerHp;
            playerDamageText.text = "-" + damage;
            StartCoroutine(WaitAndTurnOffDamageText(1));
            if (hurtPlayer == false) { return; }
            player.PlayerIsHurt();
        }
        else if (whoIsHit == "Boss")
        {
            if (reaperDefenceBreak) { damage += (damage * 50) / 100; }
            bossHp -= damage;
            bossHpText.text = Mathf.Clamp(bossHp, 0, maxBossHp) + "/" + maxBossHp;
            bossDamageText.text = "-" + damage;
            StartCoroutine(WaitAndTurnOffDamageText(1));
        }
    }



    void TurnCanvasOn(float delay)
    {
        StartCoroutine(WaitAndTurnBattleUIOn(delay));
    }
    public void HealPlayer(int hpAmmount)
    {
        
        if (player.hasDoubleHp)
        {
            playerHp = Mathf.Clamp(playerHp + hpAmmount, 0, maxPlayerHp);
            playerHpText.text = Mathf.Clamp(Mathf.Clamp(playerHp, 0, maxPlayerHp), 0, maxPlayerHp) + "/" + maxPlayerHp;
        }
        else
        {
            playerHp = Mathf.Clamp(playerHp + hpAmmount, 0, maxPlayerHp);
            playerHpText.text = Mathf.Clamp(Mathf.Clamp(playerHp, maxPlayerHp, 2500), 0, maxPlayerHp) + "/" + maxPlayerHp;
        }
        playerHealText.text = "+" + hpAmmount;
        StartCoroutine(WaitAndTurnOffHealText(1));

    }
    public void DoubleHp()
    {
        maxPlayerHp *= 2;
        playerHpText.text = Mathf.Clamp(playerHp, 0, maxPlayerHp) + "/" + maxPlayerHp;
    }
    public void HalvePlayerHp()
    {
        player.hpHalved = true;
        maxPlayerHp /= 2;
        playerHp = Mathf.Clamp(playerHp, 0, maxPlayerHp);
        playerHpText.text = Mathf.Clamp(Mathf.Clamp(playerHp, 0, maxPlayerHp), 0, maxPlayerHp) + "/" + maxPlayerHp;
    }

    void CheckToDestroySummon()
    {
        summons = FindObjectsOfType<Summon>();
        if (bossHp <= 120000 && firstSummonDestroyed == false && reaper.numberOfSummons > 0)
        {
            reaper.numberOfSummons--;
            summons[0].isAlive = false;
            firstSummonDestroyed = true;
            CheckWhatSummonWasDestroyed();
        }
        if(bossHp <= 90000 && firstSummonDestroyed && secondSummonDestroyed == false && reaper.numberOfSummons > 0)
        {
            reaper.numberOfSummons--;
            summons[0].isAlive = false;
            secondSummonDestroyed = true;
            CheckWhatSummonWasDestroyed();
        }
        if(bossHp <= 60000 && firstSummonDestroyed && secondSummonDestroyed && thirdSummonDestroyed == false && reaper.numberOfSummons > 0)
        {
            reaper.numberOfSummons--;
            summons[0].isAlive = false;
            thirdSummonDestroyed = true;
            CheckWhatSummonWasDestroyed();
        }
        if (bossHp <= 20000 && firstSummonDestroyed && secondSummonDestroyed && thirdSummonDestroyed && fourthSummonDestroyed == false && reaper.numberOfSummons > 0)
        {
            reaper.numberOfSummons--;
            summons[0].isAlive = false;
            fourthSummonDestroyed = true;
            CheckWhatSummonWasDestroyed();
        }
    }

    void CheckWhatSummonWasDestroyed()
    {
        if(firstSummonDestroyed && secondSummonDestroyed == false)
        {
            if (player.playerNegativeStatusList[0] == "Hp Halved")
            {
                reaper.hpHalved = false;
                player.hpHalved = false;
                DoubleHp();
                player.RemoveNegativeStatus("Hp Halved");
            }
            else if (player.playerNegativeStatusList[0] == "Defence Break")
            {
                reaper.RemoveDefenceBreak();
                player.RemoveNegativeStatus("Defence Break");
            }
            else if (player.playerNegativeStatusList[0] == "Magic Break")
            {
                reaper.RemoveMagicBreak();
                player.RemoveNegativeStatus("Magic Break");
            }
            else if (player.playerNegativeStatusList[0] == "Damage Break")
            {
                reaper.RemoveDamageBreak();
                player.RemoveNegativeStatus("Damage Break");
            }
            else if (player.playerNegativeStatusList[0] == "Heal Break")
            {
                reaper.RemoveHealBreak();
                player.RemoveNegativeStatus("Heal Break");
            }
        }
        if(firstSummonDestroyed && secondSummonDestroyed && thirdSummonDestroyed == false)
        {
            int index = reaper.numberOfSummons + 2;
            if (player.playerNegativeStatusList[index] == "Hp Halved")
            {
                reaper.hpHalved = false;
                player.hpHalved = false;
                DoubleHp();
                player.RemoveNegativeStatus("Hp Halved");
            }
            else if (player.playerNegativeStatusList[index] == "Defence Break")
            {
                reaper.RemoveDefenceBreak();
                player.RemoveNegativeStatus("Defence Break");
            }
            else if (player.playerNegativeStatusList[index] == "Magic Break")
            {
                reaper.RemoveMagicBreak();
                player.RemoveNegativeStatus("Magic Break");
            }
            else if (player.playerNegativeStatusList[index] == "Damage Break")
            {
                reaper.RemoveDamageBreak();
                player.RemoveNegativeStatus("Damage Break");
            }
            else if (player.playerNegativeStatusList[index] == "Heal Break")
            {
                reaper.RemoveHealBreak();
                player.RemoveNegativeStatus("Heal Break");
            }
        }
        if(firstSummonDestroyed && secondSummonDestroyed && thirdSummonDestroyed && fourthSummonDestroyed == false)
        {
            int index = reaper.numberOfSummons + 2;
            if (player.playerNegativeStatusList[index] == "Hp Halved")
            {
                reaper.hpHalved = false;
                player.hpHalved = false;
                DoubleHp();
                player.RemoveNegativeStatus("Hp Halved");
            }
            else if (player.playerNegativeStatusList[index] == "Defence Break")
            {
                reaper.RemoveDefenceBreak();
                player.RemoveNegativeStatus("Defence Break");
            }
            else if (player.playerNegativeStatusList[index] == "Magic Break")
            {
                reaper.RemoveMagicBreak();
                player.RemoveNegativeStatus("Magic Break");
            }
            else if (player.playerNegativeStatusList[index] == "Damage Break")
            {
                reaper.RemoveDamageBreak();
                player.RemoveNegativeStatus("Damage Break");
            }
            else if (player.playerNegativeStatusList[index] == "Heal Break")
            {
                reaper.RemoveHealBreak();
                player.RemoveNegativeStatus("Heal Break");
            }
        }
        if (firstSummonDestroyed && secondSummonDestroyed && thirdSummonDestroyed && fourthSummonDestroyed)
        {
            int index = reaper.numberOfSummons + 2;
            if (player.playerNegativeStatusList[index] == "Hp Halved")
            {
                reaper.hpHalved = false;
                player.hpHalved = false;
                DoubleHp();
                player.RemoveNegativeStatus("Hp Halved");
            }
            else if (player.playerNegativeStatusList[index] == "Defence Break")
            {
                reaper.RemoveDefenceBreak();
                player.RemoveNegativeStatus("Defence Break");
            }
            else if (player.playerNegativeStatusList[index] == "Magic Break")
            {
                reaper.RemoveMagicBreak();
                player.RemoveNegativeStatus("Magic Break");
            }
            else if (player.playerNegativeStatusList[index] == "Damage Break")
            {
                reaper.RemoveDamageBreak();
                player.RemoveNegativeStatus("Damage Break");
            }
            else if (player.playerNegativeStatusList[index] == "Heal Break")
            {
                reaper.RemoveHealBreak();
                player.RemoveNegativeStatus("Heal Break");
            }
        }

    }


        IEnumerator WaitAndDoBossAction(float delay)
        {
            yield return new WaitForSeconds(delay);
            ReaperActionGenerator();
            if (reaper.reaperAttack == true) { yield break; }
            TurnCanvasOn(1);
        }

        IEnumerator WaitAndTurnBattleUIOn(float delay)
        {
            yield return new WaitForSeconds(delay);
            battleUI.SetActive(true);
            inflicted = false;
            regained = false;
        }

        void BattleText(string text)
        {
            StartCoroutine(WaitAndTurnOffBattleText(2, text));
        }
        IEnumerator WaitAndTurnOffBattleText(float delay, string text)
        {
            battleText.text = text;
            yield return new WaitForSeconds(delay);
            battleText.text = "";
        }
        IEnumerator WaitAndTurnOffDamageText(float delay)
        {
            yield return new WaitForSeconds(delay);
            playerDamageText.text = "";
            bossDamageText.text = "";
        }


        void CheckForPlayerDeath()
        {
            if (player.IsAlive() == false) { return; }
            if (playerHp <= 0)
            {
                player.PlayerDeath();
            }
            
            if (reaper.numberOfSummons == 6)
            {
                player.PlayerDeath();
            }
        }

    void CheckForBossDeath()
    {
        if (bossHp <= 0)
        {
            dungeonMusic.SetActive(true);
            reaperBattleMusic.SetActive(false);
            reaperIsAlive = false;
            gameManager.reaperDefeated = true;
            reaper.ReaperDeath();
            reaperStartBattle = false;
            battleUI.SetActive(false);
            playerHpText.text = "";
            bossHpText.text = "";
            player.SheatSword();
            StartCoroutine(WaitAndTurnOffDamageText(0));
            StartCoroutine(WaitAndClearNegativeStatues(0));
            statusScreen.TriggerStatusButton();

        }
    }
    void ClearAllStatuses()
    {
        player.DispelPlayer();
        player.ClearPositiveEffects();
        player.ClearNegativeEffects();
        reaper.ClearNegativeStatusEffects();
    }

   

    void CheckForHasteAndSlow()
    {
        if(player.HasHaste() && reaper.hasSlow)
        {
            hasteAndSlowTurns++;
        }
        else if (player.HasHaste())
        {
            hasteTurns++;
        }
        else if (reaper.hasSlow)
        {
            slowTurns++;
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




        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                this.enabled = true;
                dungeonMusic.SetActive(false);
                reaperBattleMusic.SetActive(true);
                battleUI.SetActive(true);
                reaperStartBattle = true;
                reaper.isAlive = true;
                healthCanvas.SetActive(true);
                statusScreen.TriggerStatusButton();
            }
        }

        void CheckForPlayerPoisonAndBleed()
        {
            if (inflicted) { return; }
            StartCoroutine(WaitAndInflictStatusDamageToPlayer(0.5f));
        }
    void CheckForBossPoisonAndBleed()
    {
        StartCoroutine(WaitAndInflictStatusDamageToBoss(0.5f));
    }

        IEnumerator WaitAndInflictStatusDamageToPlayer(float delay)
        {
            inflicted = true;
            if (player.HasBleed())
            {
                IsHit("Player", 150, false);
            }
            yield return new WaitForSeconds(delay);
            if (player.HasPoison())
            {
                IsHit("Player", 170, false);
            }
        }
    IEnumerator WaitAndInflictStatusDamageToBoss(float delay)
    {
        if (reaper.hasPoison)
        {
            IsHit("Boss", player.IncreaseMagicDamage(150), false);
        }
        yield return new WaitForSeconds(delay);
    }
    IEnumerator WaitAndClearNegativeStatues(float delay)
    {
        yield return new WaitForSeconds(delay);
        ClearAllStatuses();
        this.enabled = false;
    }
    IEnumerator WaitAndTurnOffHealText(float delay)
    {
        yield return new WaitForSeconds(delay);
        playerHealText.text = "";
    }
    }

