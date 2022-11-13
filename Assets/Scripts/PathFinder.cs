using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    [SerializeField] ChaseSequenceSO startingPath;
    [SerializeField] ChaseSequenceSO pathToClock;
    [SerializeField] ChaseSequenceSO clockDecision1;
    [SerializeField] ChaseSequenceSO clockDecision2;
    [SerializeField] ChaseSequenceSO clockDecision3;
    [SerializeField] ChaseSequenceSO clockDecision4;
    [SerializeField] ChaseSequenceSO clockDecision5;
    [SerializeField] ChaseSequenceSO clockDecision6;
    [SerializeField] ChaseSequenceSO clockDecision7;
    [SerializeField] ChaseSequenceSO clockDecisionToStart;
    [SerializeField] ChaseSequenceSO afterClockDecision1;
    [SerializeField] ChaseSequenceSO afterClockDecision2;
    [SerializeField] ChaseSequenceSO afterClockDecision3;
    [SerializeField] ChaseSequenceSO afterClockDecision4;
    [SerializeField] ChaseSequenceSO middleDecision1;
    [SerializeField] ChaseSequenceSO middleDecision2;
    [SerializeField] ChaseSequenceSO decisionFromLadder;
    [SerializeField] ChaseSequenceSO decisionFromLadderToStart1;
    [SerializeField] ChaseSequenceSO decisionFromLadderToStart2;
    [SerializeField] ChaseSequenceSO decisionFromBottom1;
    [SerializeField] ChaseSequenceSO decisionFromBottom2;
    [SerializeField] ChaseSequenceSO fromStartToClock;
    [SerializeField] Button button;

    Boss boss;
    NightBorne nightBorne;
    Battle battle;
    NightBorneBattleSystem nightBorneBattleSystem;
    GameManager gameManager;
    [SerializeField] bool isNightBorne;
    [SerializeField] GameObject dungeonMusic;
    [SerializeField] GameObject bringerChaseMusic;
    List<Transform> waypoints;
    Vector3 clockDecisionPosition = new Vector3(-4.9f, 0.04f, 0);
    Vector3 afterClockDecisionPosition = new Vector3(-12.05f, 0.11f, 0);
    Vector3 middleDecisionPosition1 = new Vector3(-8.5f, 0.87f, 0);
    Vector3 middleDecisionPosition2 = new Vector3(-8.5f, -1, 0);
    Vector3 middleDecisionPosition3 = new Vector3(-8.5f, 0.02f, 0);
    Vector3 ladderPosition = new Vector3(0.92f, 1.61f, 0);
    Vector3 bottomDecision = new Vector3(-8.54f, -4.19f, 0);
    Vector3 ladderToStartMidPosition = new Vector3(-0.728f, -0.149f, 0);
    Vector3 startPosition = new Vector3(2.19f, -2.16f, 0);
    int upperLimit = 0;
    int lowerLimit = 0;
    int waypointsIndex = 0;
    bool chaseStarted;
    bool reachedTheEnd;
    bool hasDissappeared;

    bool firstPath = true;
    bool bossThinking;
    bool onlyOnce = true;


    private void Awake()
    {
        boss = FindObjectOfType<Boss>();
        battle = FindObjectOfType<Battle>();
        nightBorne = FindObjectOfType<NightBorne>();
        nightBorneBattleSystem = FindObjectOfType<NightBorneBattleSystem>();
        gameManager = FindObjectOfType<GameManager>();
    }




    void Update()
    {
        if(chaseStarted == false)
        {
            StartChase();
        }
        else
        {
            FollowPath();
        }
        
    }
    public bool HasReachedEnd()
    {
        return reachedTheEnd;
    }
    public bool HasBossDissappeared()
    {
        return hasDissappeared;
    }
    void StartChase()
    {
        waypoints = startingPath.GetWayPoints();
        transform.position = waypoints[waypointsIndex].position;
        chaseStarted = true;
    }

    void FollowPath()
    {
        if (isNightBorne == false)
        {
            if (waypointsIndex < waypoints.Count && battle.StartBattle() == false && boss.IsBossAlive())
            {
                boss.BossWalk();
                Vector3 targetPosition = waypoints[waypointsIndex].position;
                float delta = startingPath.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
                if (transform.position == targetPosition)
                {
                    waypointsIndex++;
                }

            }
            else
            {
                boss.StopMovement();
                //bringerChaseMusic.SetActive(false);
                if (battle.StartBattle()) { return; }
                reachedTheEnd = true;
                if (hasDissappeared) { return; }
                StartCoroutine(WaitForBossDissappear());
            }
        }
        else if (isNightBorne) 
        {
            
            if (waypointsIndex < waypoints.Count)
            {              
                nightBorne.BossRun();
                Vector3 targetPosition = waypoints[waypointsIndex].position;
                float delta = startingPath.GetMoveSpeed() * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
                if (transform.position == targetPosition)
                {
                    waypointsIndex++;
                }
            }
            else
            {
                
                nightBorne.BossIdle("isRunning");
                ResetWaypoints();
                if (bossThinking) { return; }
                StartCoroutine(WaitAndChoosePath(3));

            }
        }
        
        
    }

    void ResetWaypoints()
    {
        waypoints.Clear();
        waypointsIndex = 0;
    }

    IEnumerator WaitForBossDissappear()
    {
        yield return new WaitForSeconds(5);
        hasDissappeared = true;
        boss.BossDissappear();
        bringerChaseMusic.SetActive(false);
        dungeonMusic.SetActive(true);
    }

    IEnumerator WaitAndChoosePath(float delay)
    {
        bossThinking = true;
        yield return new WaitForSeconds(delay);   
        if(transform.position == clockDecisionPosition && button.nearButton && onlyOnce)
        {
            onlyOnce = false;
            lowerLimit = 8;
            upperLimit = 9;           
        }                
        else if(transform.position == clockDecisionPosition)
        {
            lowerLimit = 1;
            upperLimit = 9;
            nightBorne.FlipNightBorneSprite("left");
            nightBorneBattleSystem.moveAwayFromPlayer = new Vector3(0.25f, 0.1f, 0);
        }       
        else if(transform.position == afterClockDecisionPosition)
        {
            lowerLimit = 9;
            upperLimit = 13;
            nightBorne.FlipNightBorneSprite("right");
            nightBorneBattleSystem.moveAwayFromPlayer = new Vector3(-0.25f, 0.1f, 0);
        }
        else if(transform.position == middleDecisionPosition1 || transform.position == middleDecisionPosition2 || transform.position == middleDecisionPosition3)
        {
            lowerLimit = 13;
            upperLimit = 15;
        }
        else if(transform.position == bottomDecision)
        {
            nightBorne.FlipNightBorneSprite("left");
            lowerLimit = 18;
            upperLimit = 20;
        }
        else if(transform.position == ladderPosition)
        {
            lowerLimit = 15;
            upperLimit = 17;
            nightBorne.FlipNightBorneSprite("left");
        }
        else if(transform.position == ladderToStartMidPosition)
        {
            nightBorne.FlipNightBorneSprite("left");
            lowerLimit = 17;
            upperLimit = 18;
        }
        else if(transform.position == startPosition)
        {
            nightBorne.FlipNightBorneSprite("left");
            lowerLimit = 20;
            upperLimit = 21;
        }
        int randomNum = Random.Range(lowerLimit, upperLimit);
        if (firstPath)
        {
            randomNum = 100;
            nightBorneBattleSystem.moveAwayFromPlayer = new Vector3(0.25f, 0.1f, 0);

        }
        if(randomNum == 100)
        {          
            waypoints = pathToClock.GetWayPoints();
            firstPath = false;
            bossThinking = false;
        }
        else if (randomNum == 1)
        {           
            waypoints = clockDecision1.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 2)
        {            
            waypoints = clockDecision2.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 3)
        {
            waypoints = clockDecision3.GetWayPoints();
            bossThinking = false;
        }
        else if(randomNum == 4)
        {
            waypoints = clockDecision4.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 5)
        {
            waypoints = clockDecision5.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 6)
        {
            waypoints = clockDecision6.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 7)
        {
            nightBorne.FlipNightBorneSprite("right");
            waypoints = clockDecision7.GetWayPoints();
            bossThinking = false;
        }
        else if(randomNum == 8)
        {
            nightBorne.FlipNightBorneSprite("right");
            waypoints = clockDecisionToStart.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 9)
        {
            waypoints = afterClockDecision1.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 10)
        {
            waypoints = afterClockDecision2.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 11)
        {
            waypoints = afterClockDecision3.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 12)
        {
            waypoints = afterClockDecision4.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 13)
        {
            nightBorne.FlipNightBorneSprite("left");
            waypoints = middleDecision1.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 14)
        {
            nightBorne.FlipNightBorneSprite("right");
            waypoints = middleDecision2.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 15)
        {           
            waypoints = decisionFromLadder.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 16)
        {
            waypoints = decisionFromLadderToStart1.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 17)
        {
            nightBorne.FlipNightBorneSprite("right");
            waypoints = decisionFromLadderToStart2.GetWayPoints();
            bossThinking = false;
        }

        else if (randomNum == 18)
        {
            nightBorne.FlipNightBorneSprite("right");
            waypoints = decisionFromBottom1.GetWayPoints();
            bossThinking = false;
        }
        else if (randomNum == 19)
        {
            waypoints = decisionFromBottom2.GetWayPoints();
            bossThinking = false;
        }
        else if(randomNum == 20)
        {
            waypoints = fromStartToClock.GetWayPoints();
            bossThinking = false;
        }
    }
}
