using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideFromBoss : MonoBehaviour
{
    [SerializeField] Zoom zoom;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] bool dontDisableBoxCollider;
    [SerializeField] NightBorneBattleSystem nightBorneBattleSystem;
    Vector3 orginalPositionOfZoom;

    GameManager gameManager;


    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        orginalPositionOfZoom = zoom.transform.position;
        if (gameManager.nightBorneDefeated)
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && nightBorneBattleSystem.isAlive)
        {           
            zoom.transform.position = transform.position;
            if (dontDisableBoxCollider) { return; }
            boxCollider2D.enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {         
            zoom.transform.position = orginalPositionOfZoom;
            if(nightBorneBattleSystem.isAlive == false) { return; }
            boxCollider2D.enabled = true;
        }
    }

    IEnumerator WaitAndReturnZoomToOriginalPosition(float delay)
    {
        yield return new WaitForSeconds(delay);
        zoom.transform.position = orginalPositionOfZoom;
    }
}
