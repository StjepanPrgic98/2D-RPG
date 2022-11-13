using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacksmithSound : MonoBehaviour
{
    [SerializeField] AudioSource blacksmithSound;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            blacksmithSound.Play();
        }
    }
}
