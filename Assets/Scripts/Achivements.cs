using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Achivements : MonoBehaviour
{

    GameManager gameManager;

    [SerializeField] List<TextMeshProUGUI> achivements;


    [SerializeField] Color completedColor;
    [SerializeField] Color notCompletedColor;





    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        CheckForAchivements();
    }
    void CheckForAchivements()
    {
        for (int i = 0; i < achivements.Count; i++)
        {
            if (achivements[i].tag == "defeatDefender")
            {
                if (gameManager.defenderDefeated)
                {
                    achivements[i].text = "Defeat Defender! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Defeat Defender! " + CompletedStatus(false);
                }
                
            }
            if (achivements[i].tag == "defeatReaper")
            {
                if (gameManager.reaperDefeated)
                {
                    achivements[i].text = "Defeat Reaper! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Defeat Reaper! " + CompletedStatus(false);
                }

            }
            if (achivements[i].tag == "defeatBringer")
            {
                if (gameManager.bringerDefeated)
                {
                    achivements[i].text = "Defeat Bringer! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Defeat Bringer! " + CompletedStatus(false);
                }

            }
            if (achivements[i].tag == "defeatNightborne")
            {
                if (gameManager.nightBorneDefeated)
                {
                    achivements[i].text = "Defeat NightBorne! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Defeat NightBorne! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "labQuestComplete")
            {
                if (gameManager.labPuzzleSolved)
                {
                    achivements[i].text = "Labyrinth puzzle! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Labyrinth puzzle! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "passwordQuestComplete")
            {
                if (gameManager.passwordSolved)
                {
                    achivements[i].text = "Island Password puzzle! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Island puzzle puzzle! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "bigClockComplete")
            {
                if (gameManager.bigClockSolved)
                {
                    achivements[i].text = "Big Clock puzzle! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Big Clock puzzle! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "smallClockComplete")
            {
                if (gameManager.smallClockSolved)
                {
                    achivements[i].text = "Small Clock puzzle! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Small Clock puzzle! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "holyWaterComplete")
            {
                if (gameManager.holyWaterSolved)
                {
                    achivements[i].text = "Holy Water puzzle! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Holy Water puzzle! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "castlePasswordComplete")
            {
                if (gameManager.castlePuzzleSolved)
                {
                    achivements[i].text = "Castle password puzzle! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Castle password puzzle! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "thunderTowersComplete")
            {
                if (gameManager.thunderTowersSolved)
                {
                    achivements[i].text = "Thunder Towers! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Thunder Towers! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "EnergyTotemsComplete")
            {
                if (gameManager.totemsSolved)
                {
                    achivements[i].text = "Energy Totems! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Energy Totems! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "castleTombComplete")
            {
                if (gameManager.underTowerPuzzleSolved)
                {
                    achivements[i].text = "Castle Tomb! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Castle Tomb! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "maxDamageComplete")
            {
                if (gameManager.maxPhysicalDamage)
                {
                    achivements[i].text = "Maxed Physical Damage! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Maxed Physical Damage! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "maxMagicComplete")
            {
                if (gameManager.maxMagicDamage)
                {
                    achivements[i].text = "Maxed Magic Damage! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Maxed Magic Damage! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "maxDefenceComplete")
            {
                if (gameManager.maxPhysicalDamage)
                {
                    achivements[i].text = "Maxed Physical Defence! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Maxed Physical Defence! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "maxHpComplete")
            {
                if (gameManager.maxMaxHp)
                {
                    achivements[i].text = "Maxed Hp! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Maxed Hp! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "teleportUnlocked")
            {
                if (gameManager.teleportUnlocked)
                {
                    achivements[i].text = "Warp unlocked! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Warp unlocked! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "allChestsComplete")
            {
                if (gameManager.allChestsCollected)
                {
                    achivements[i].text = "All chests collected! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "All chests collected! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "allSpellsComplete")
            {
                if (gameManager.allSpellsBought)
                {
                    achivements[i].text = "All spell bought! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "All spells bought! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "allAttacksComplete")
            {
                if (gameManager.allAttacksLearned)
                {
                    achivements[i].text = "All attacks learned! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "All attacks learned! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "allAreasComplete")
            {
                if (gameManager.allAreasVisited)
                {
                    achivements[i].text = "All areas visited! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "All areas visited! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "allSignsComplete")
            {
                if (gameManager.allSignsRead)
                {
                    achivements[i].text = "All signs read! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "All signs read! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "allDoorsComplete")
            {
                if (gameManager.allDoorsUnlocked)
                {
                    achivements[i].text = "All doors unlocked! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "All doors unlocked! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "oblivionComplete")
            {
                if (gameManager.oblivionSurvived)
                {
                    achivements[i].text = "Oblivion survived! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Oblivion survived! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "demolitionComplete")
            {
                if (gameManager.demolitionSurvived)
                {
                    achivements[i].text = "Demolition survived! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Demolition survived! " + CompletedStatus(false);
                }
            }
            if (achivements[i].tag == "diedWaitingComplete")
            {
                if (gameManager.diedFromWaiting)
                {
                    achivements[i].text = "Died from waiting! " + CompletedStatus(true);
                    achivements[i].color = Color.green;
                }
                else
                {
                    achivements[i].text = "Died from waiting! " + CompletedStatus(false);
                }
            }

        }
    }

    string CompletedStatus(bool argument)
    {
        if (argument)
        {
            return "Status : Complete!";
        }
        else
        {
            return "Status: Not Complete!";
        }
    }

    
       
    
}
