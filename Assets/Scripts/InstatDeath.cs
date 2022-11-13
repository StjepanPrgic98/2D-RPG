using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatDeath : MonoBehaviour
{
    Boss boss;
    Animator animator;
    PlayerMovement player;
    BoxCollider2D boxCollider;

    private void Awake()
    {
        boss = FindObjectOfType<Boss>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.PlayerDeath();
            boss.BossMagic("mega");
            animator.SetBool("isZoomed", true);
            StartCoroutine(WaitForAnimationEnd());          
        }
    }
    IEnumerator WaitForAnimationEnd()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("isZoomed", false);
    }
}
