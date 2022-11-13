using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalCollision : MonoBehaviour
{
    [SerializeField] GameObject conditionalCollision;


    TriggerLayer triggerLayer;

    private void Awake()
    {
        triggerLayer = FindObjectOfType<TriggerLayer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && triggerLayer.changeLayer == false)
        {
            conditionalCollision.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            conditionalCollision.SetActive(false);
        }
    }
}
