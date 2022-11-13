using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRiverSound : MonoBehaviour
{
    [SerializeField] GameObject riverSound;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            riverSound.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            riverSound.SetActive(false);
        }
    }
}
