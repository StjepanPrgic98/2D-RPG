using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomOutHandler : MonoBehaviour
{
    [SerializeField] GameObject conditionalCollision;
    [SerializeField] GameObject triggerZoomPosition;
    [SerializeField] Renderer pillarDirt;
    [SerializeField] Renderer pillarLadder;
    [SerializeField] BoxCollider2D zoom;


    int entries;




    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            zoom.enabled = true;
            conditionalCollision.SetActive(true);
            triggerZoomPosition.SetActive(true);
            pillarDirt.sortingOrder = -2;
            pillarLadder.sortingOrder = -1;
            entries++;
        }
        if(entries == 2)
        {
            zoom.enabled = false;
            entries = 0;
            conditionalCollision.SetActive(false);
            triggerZoomPosition.SetActive(false);
            pillarDirt.sortingOrder = 1;
            pillarLadder.sortingOrder = 2;
        }
    }
}
