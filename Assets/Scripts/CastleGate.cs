using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleGate : MonoBehaviour
{
    GameManager gameManager;
    BoxCollider2D boxCollider2D;
    [SerializeField] GameObject castleGateCanvas;




    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }



    private void Update()
    {
        if (gameManager.unlockCastleGate)
        {
            castleGateCanvas.SetActive(false);
            boxCollider2D.enabled = false;
        }
        else
        {
            castleGateCanvas.SetActive(true);
            boxCollider2D.enabled = true;
        }
    }
}
