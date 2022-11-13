using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSideOfDoor : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] bool otherSide1;
    [SerializeField] bool otherSide2;



    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && otherSide1)
        {
            gameManager.otherSideKey1 = true;
        }
        else if(collision.tag == "Player" && otherSide2)
        {
            gameManager.otherSideKey2 = true;
        }
    }

    
}
