using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfChaseDeath : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] PlayerMovement player;
    [SerializeField] PathFinder pathFinder;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && pathFinder.HasReachedEnd() && boss.IsBossAlive() && pathFinder.HasBossDissappeared() == false)
        {
            player.PlayerDeath();
            boss.BossMagic("mega");
        }
    }
}
