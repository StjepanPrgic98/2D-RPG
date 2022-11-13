using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReplica : MonoBehaviour
{
    [SerializeField] GameObject waypointMiddle;
    [SerializeField] GameObject darkLightning;
    [SerializeField] GameObject regularLightning;
    [SerializeField] GameObject blueLightning;
    [SerializeField] GameObject chest;
    

    [SerializeField] List<GameObject> spellPositions;
    [SerializeField] List<GameObject> blueLightningPositions;

    Animator animator;
    StartScreen startScreen;
    bool runToMiddle;
    bool boltHit;
    bool runToChest;

    int currentTime = 30;
    bool secondPassed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        startScreen = FindObjectOfType<StartScreen>();
    }
    private void Start()
    {
        runToMiddle = true;
    }

    private void Update()
    {
        Timer();
        RunToMiddle();
    }


    void RunToMiddle()
    {
        if (runToMiddle)
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, waypointMiddle.transform.position, 0.25f * Time.deltaTime);
            if(transform.position == waypointMiddle.transform.position)
            {
                runToMiddle = false;
                animator.SetBool("isRunning", false);
                InstantiateDarkLightningInCircle(0.5f, "First");
            }
            
        }
        if (runToChest)
        {
            animator.SetBool("isRunning", true);
            transform.position = Vector3.MoveTowards(transform.position, chest.transform.position, 0.004f);
            if(transform.position == chest.transform.position)
            {
                runToChest = false;
                animator.SetBool("isRunning", false);
            }
        }
        
    }


    void InstantiateDarkLightningInCircle(float delay, string number)
    {
        StartCoroutine(WaitAndInstantiateDarkLightning(delay, number));
    }

    IEnumerator WaitAndInstantiateDarkLightning(float delay, string number)
    {
        GameObject darklightningEffect1 = Instantiate(darkLightning, spellPositions[0].transform.position, Quaternion.identity);
        Destroy(darklightningEffect1, 1);
        yield return new WaitForSeconds(delay);
        GameObject darklightningEffect2 = Instantiate(darkLightning, spellPositions[1].transform.position, Quaternion.identity);
        Destroy(darklightningEffect2, 1);
        yield return new WaitForSeconds(delay);
        GameObject darklightningEffect3 = Instantiate(darkLightning, spellPositions[2].transform.position, Quaternion.identity);
        Destroy(darklightningEffect3, 1);
        yield return new WaitForSeconds(delay);
        GameObject darklightningEffect4 = Instantiate(darkLightning, spellPositions[3].transform.position, Quaternion.identity);
        Destroy(darklightningEffect4, 1);
        yield return new WaitForSeconds(delay);
        GameObject darklightningEffect5 = Instantiate(darkLightning, spellPositions[4].transform.position, Quaternion.identity);
        Destroy(darklightningEffect5, 1);
        yield return new WaitForSeconds(delay);
        GameObject darklightningEffect6 = Instantiate(darkLightning, spellPositions[5].transform.position, Quaternion.identity);
        Destroy(darklightningEffect6, 1);
        yield return new WaitForSeconds(delay);
        GameObject darklightningEffect7 = Instantiate(darkLightning, spellPositions[6].transform.position, Quaternion.identity);
        Destroy(darklightningEffect7, 1);
        yield return new WaitForSeconds(delay);
        GameObject darklightningEffect8 = Instantiate(darkLightning, spellPositions[7].transform.position, Quaternion.identity);
        Destroy(darklightningEffect8, 1);
        yield return new WaitForSeconds(delay);
        if (number == "First")
        {
            number = "";
            InstantiateDarkLightningInCircle(0.2f, "Second");
        }
        if(number == "Second")
        {
            InstantiateDarkLightningInCircle(0.1f,"Third");
        }
        if(number == "Third")
        {
            animator.SetBool("isAttacking", true);
            StartCoroutine(WaitAndTurnOffAttack(2.3f));
            SummonThunder(1);
        }
        
        
        

    }

    IEnumerator WaitAndTurnOffAttack(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isAttacking", false);
    }

    void SummonThunder(float delay)
    {
        StartCoroutine(WaitAndSummonThunder(delay));
    }

    IEnumerator WaitAndSummonThunder(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject regularLightningEffect1 = Instantiate(regularLightning, spellPositions[3].transform.position, Quaternion.identity);
        Destroy(regularLightningEffect1, 1);
        yield return new WaitForSeconds(0);
        GameObject regularLightningEffect2 = Instantiate(regularLightning, spellPositions[5].transform.position, Quaternion.identity);
        Destroy(regularLightningEffect2, 1);
        yield return new WaitForSeconds(0);
    }

    void HitSelectedButtonWithBlueThunder()
    {
       
        GameObject blueLightningEffect1 = Instantiate(blueLightning, blueLightningPositions[0].transform.position, Quaternion.identity);
        Destroy(blueLightningEffect1, 1);
        
        
    }

    void Timer()
    {
        if (currentTime <= 0 && boltHit == false)
        {
            boltHit = true;
            HitSelectedButtonWithBlueThunder();
            StartCoroutine(WaitAndSpawnChest(0.5f));
            return;
        }
        if(secondPassed) { return; }
        if (currentTime <= 0) { return; }
        StartCoroutine(WaitAndReduceTimer(1));
        
    }
    IEnumerator WaitAndReduceTimer(float delay)
    {
        secondPassed = true;
        yield return new WaitForSeconds(delay);
        currentTime--;
        secondPassed = false;
    }

    IEnumerator WaitAndSpawnChest(float delay)
    {
        yield return new WaitForSeconds(delay);
        chest.SetActive(true);
        runToChest = true;
    }


    
}
