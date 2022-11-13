using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalCollisionTrigger : MonoBehaviour
{
    [SerializeField] GameObject conditionalCollision;

    int entries = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            if(entries < 1)
            {
                entries++;
                conditionalCollision.SetActive(true);
            }
            else
            {
                conditionalCollision.SetActive(false);
                entries = 0;
            }
            
        }
    }

}
