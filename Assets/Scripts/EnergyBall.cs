using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    PlayerMovement player;
    Animator animator;
    GameManager gameManager;
    AudioPlayer audioPlayer;

    bool destroyed;
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        audioPlayer.PlayEnergyBallLoop();
    }


    private void Update()
    {
        if (destroyed) { return; }
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 3f * Time.deltaTime);
        transform.right = player.transform.position - transform.position;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioPlayer.PlayEnergyBallImpact();
            gameManager.timesHitByEnergyBall++;
            animator.SetBool("didHit", true);
            Destroy(gameObject, 1);
            destroyed = true;
            if (gameManager.timesHitByEnergyBall == 7)
            {
                gameManager.energyBallSpawned = true;
            }
        }
    }
}
