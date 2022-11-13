using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchIslandFire : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    [SerializeField] AudioSource thunderTorch;

    GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Update()
    {
        if (gameManager.bringerDefeated)
        {
            thunderTorch.Stop();
        }
        if (gameManager.fullCompletion)
        {
            sound.Stop();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sound.Play();
            thunderTorch.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            sound.Stop();
            thunderTorch.Stop();
        }
    }
}
