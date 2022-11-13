using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summon : MonoBehaviour
{
    Animator animator;
    Reaper reaper;
    ReaperBattleSystem reaperBattleSystem;
    [HideInInspector] public bool isAlive = true;






    private void Awake()
    {
        animator = GetComponent<Animator>();
        reaper = FindObjectOfType<Reaper>();
        reaperBattleSystem = FindObjectOfType<ReaperBattleSystem>();

    }

    private void Start()
    {
        transform.parent = reaper.transform;

    }

    private void Update()
    {
        if (isAlive)
        {
            SummonIdle();   
        }
        else
        {
            SummonDeath();
        }
    }



    public void SummonIdle()
    {
        StartCoroutine(WaitAndReturnToIdle(0.5f));
    }
    public void SummonDeath()
    {
        animator.SetBool("isIdle", false);
        animator.SetBool("isDead", true);
        Destroy(gameObject, 1);
    }



    IEnumerator WaitAndReturnToIdle(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("isIdle", true);
    }
}
