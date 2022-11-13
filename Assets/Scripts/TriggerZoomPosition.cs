using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZoomPosition : MonoBehaviour
{
    [SerializeField] GameObject zoom;
    [SerializeField] BoxCollider2D zoomCollider;
    [SerializeField] bool thirdPosition;
    Vector3 zoomOriginalPosition = new Vector3(-6.112f, -0.278f);
    Vector3 zoomSecondPosition = new Vector3(3.35f, 9.63f, 0);
    Vector3 zoomThirdPosition = new Vector3(0.852f, 5.525f, 0);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        zoomCollider.enabled = true;
        if (collision.tag == "Player")
        {
            zoom.transform.position = zoomSecondPosition;
        }
        if(collision.tag == "Player" && thirdPosition)
        {

            zoom.transform.position = zoomThirdPosition;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        zoomCollider.enabled = false;
        zoom.transform.position = zoomOriginalPosition;
    }



}
