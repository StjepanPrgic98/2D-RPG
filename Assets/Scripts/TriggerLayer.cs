using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLayer : MonoBehaviour
{
    [SerializeField] Renderer objectToChange;

    [SerializeField] GameObject aditionalConditionalCollision;

    [HideInInspector] public bool changeLayer;

    int counter;

    private void Start()
    {
        counter = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(counter == 1)
        {
            counter = 0;
            objectToChange.sortingOrder = -1;
            changeLayer = false;
            aditionalConditionalCollision.SetActive(false);
            return;
        }
        if(collision.tag == "Player")
        {
            objectToChange.sortingOrder = 1;
            changeLayer = true;
            counter++;
            aditionalConditionalCollision.SetActive(true);
        }
    }
}
