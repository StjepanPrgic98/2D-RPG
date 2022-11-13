using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    SpriteRenderer sprite;
    Animator animator;
    GameManager gameManager;

    [SerializeField] bool bossTorch;

    
    [SerializeField] Color fire;
    [SerializeField] Color water;
    [SerializeField] Color ice;
    [SerializeField] Color thunder;
    [SerializeField] Color dark;
    [SerializeField] Color red;
    [SerializeField] Color off;
    [SerializeField] Color fullCompletionColor;

    [SerializeField] bool isFire;
    [SerializeField] bool isWater;
    [SerializeField] bool isIce;
    [SerializeField] bool isThunder;
    [SerializeField] bool isDark;
    [SerializeField] bool isRed;

    [SerializeField] bool labQuest;
    [SerializeField] bool bigClockQuest;
    [SerializeField] bool smallClockQuest;
    [SerializeField] bool holyWaterQuest;
    [SerializeField] bool passwordQuest;
    [SerializeField] bool CastleQuest;
    [SerializeField] bool bringer;
    [SerializeField] bool defender;
    [SerializeField] bool reaper;
    [SerializeField] bool nightBorne;
    [SerializeField] bool fullCompletion;


    

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (isFire)
        {
            animator.SetBool("isThunder", false);
            sprite.color = fire;
        }
        if (isWater)
        {
            animator.SetBool("isThunder", false);
            sprite.color = water;
        }
        if (isIce)
        {
            animator.SetBool("isThunder", false);
            sprite.color = ice;
        }
        if (isThunder)
        {
            animator.SetBool("isThunder", true);
            sprite.color = fire;
        }
        if (isDark)
        {
            animator.SetBool("isThunder", false);
            sprite.color = dark;
        }
        if (isRed)
        {
            animator.SetBool("isThunder", false);
            sprite.color = red;
        }
    }

    private void Update()
    {
        CheckForCompletedQuest();
    }

    public void ChangeColor(string phase)
    { 
        if(phase == "fire")
        {
            animator.SetBool("isThunder", false);
            sprite.color = fire;
        }
        else if(phase == "water")
        {
            animator.SetBool("isThunder", false);
            sprite.color = water;
        }
        else if(phase == "ice")
        {
            animator.SetBool("isThunder", false);
            sprite.color = ice;
        }
        else if(phase == "thunder")
        {
            sprite.color = fire;
            animator.SetBool("isThunder", true);
        }
        else if(phase == "dark")
        {
            sprite.color = dark;
            animator.SetBool("isThunder", false);
        }
    }

    public void SetAnimatorToFalse()
    {
        animator.SetBool("isThunder", false);
    }

    void CheckForCompletedQuest()
    {
        if (fullCompletion && gameManager.fullCompletion)
        {
            sprite.color = dark;
        }
        if (labQuest && gameManager.labPuzzleSolved)
        {
            sprite.color = off;
        }
        if(bigClockQuest && gameManager.bigClockSolved)
        {
            sprite.color = off;
        }
        if(smallClockQuest && gameManager.smallClockSolved)
        {
            sprite.color = off;
        }
        if(holyWaterQuest && gameManager.holyWaterSolved)
        {
            sprite.color = off;
        }
        if(passwordQuest && gameManager.passwordSolved)
        {
            sprite.color = off;
        }
        if(CastleQuest && gameManager.castlePuzzleSolved)
        {
            sprite.color = off;
        }
        if(nightBorne && gameManager.nightBorneDefeated)
        {
            sprite.color = off;
        }
        if(reaper && gameManager.reaperDefeated)
        {
            sprite.color = off;
        }
        if(defender && gameManager.defenderDefeated)
        {
            sprite.color = off;
        }
        if(bringer && gameManager.bringerDefeated)
        {
            sprite.color = off;
        }
       
    }

   
    

}
