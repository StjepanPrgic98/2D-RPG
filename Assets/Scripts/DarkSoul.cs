using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DarkSoul : MonoBehaviour
{
    PlayerMovement player;
    Animator animator;
    AudioPlayer audioPlayer;
    GameManager gameManager;
    PuzzleUnderTower puzzle;
    Chests[] chests;
    bool didHit;

    int target;

    List<Chests> chestsList;

    Vector3 firstTarget = new Vector3();
    Vector3 secondTarget = new Vector3();
    Vector3 thirdTarget = new Vector3();
    Vector3 fourthTarget = new Vector3();
    Vector3 fifthTarget = new Vector3();
    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameManager = FindObjectOfType<GameManager>();
        puzzle = FindObjectOfType<PuzzleUnderTower>();
        chests = FindObjectsOfType<Chests>();
        chestsList = chests.ToList();
        for (int i = 0; i < chests.Length; i++)
        {
            if (chests[i].GetChestID() == 3081) { firstTarget = chests[i].transform.position; chestsList[0] = chests[i]; }
            if (chests[i].GetChestID() == 3082) { secondTarget = chests[i].transform.position; chestsList[1] = chests[i]; }
            if (chests[i].GetChestID() == 3083) { thirdTarget = chests[i].transform.position; chestsList[2] = chests[i]; }
            if (chests[i].GetChestID() == 3084) { fourthTarget = chests[i].transform.position; chestsList[3] = chests[i]; }
            if (chests[i].GetChestID() == 3085) { fifthTarget = chests[i].transform.position; chestsList[4] = chests[i]; }
        }
    }
    private void Start()
    {
        target = puzzle.target;
    }

    private void Update()
    {
        
        if (didHit) { return; }
        if (gameManager.underTowerPuzzleSolved)
        {
            if(target == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, firstTarget, 0.5f * Time.deltaTime);
                if(transform.position == firstTarget)
                {
                    audioPlayer.PlaySlapClip();
                    animator.SetBool("didHit", true);
                    didHit = true;
                    Destroy(gameObject, 1);
                    chestsList[0].GetComponent<SpriteRenderer>().enabled = true;
                    chestsList[0].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            if (target == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position, secondTarget, 0.5f * Time.deltaTime);
                if (transform.position == secondTarget)
                {
                    audioPlayer.PlaySlapClip();
                    animator.SetBool("didHit", true);
                    didHit = true;
                    Destroy(gameObject, 1);
                    chestsList[1].GetComponent<SpriteRenderer>().enabled = true;
                    chestsList[1].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            if (target == 3)
            {
                transform.position = Vector3.MoveTowards(transform.position, thirdTarget, 0.5f * Time.deltaTime);
                if (transform.position == thirdTarget)
                {
                    audioPlayer.PlaySlapClip();
                    animator.SetBool("didHit", true);
                    didHit = true;
                    Destroy(gameObject, 1);
                    chestsList[2].GetComponent<SpriteRenderer>().enabled = true;
                    chestsList[2].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            if (target == 4)
            {
                transform.position = Vector3.MoveTowards(transform.position, fourthTarget, 0.5f * Time.deltaTime);
                if (transform.position == fourthTarget)
                {
                    audioPlayer.PlaySlapClip();
                    animator.SetBool("didHit", true);
                    didHit = true;
                    Destroy(gameObject, 1);
                    chestsList[3].GetComponent<SpriteRenderer>().enabled = true;
                    chestsList[3].GetComponent<BoxCollider2D>().enabled = true;
                }
            }
            if (target == 5)
            {
                transform.position = Vector3.MoveTowards(transform.position, fifthTarget, 0.5f * Time.deltaTime);
                if (transform.position == fifthTarget)
                {
                    audioPlayer.PlaySlapClip();
                    animator.SetBool("didHit", true);
                    didHit = true;
                    Destroy(gameObject, 1);
                    chestsList[4].GetComponent<SpriteRenderer>().enabled = true;
                    chestsList[4].GetComponent<BoxCollider2D>().enabled = true;
                }
            }

        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 0.5f * Time.deltaTime);
        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && gameManager.underTowerPuzzleSolved == false)
        {
            audioPlayer.PlaySlapClip();
            animator.SetBool("didHit", true);
            didHit = true;
            player.PlayerDeath();
            Destroy(gameObject, 1);
        }
    }
}
