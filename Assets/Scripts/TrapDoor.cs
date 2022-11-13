using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] Sprite closedSprite;
    [SerializeField] Sprite openSprite;
    [SerializeField] PressurePlate pressurePlate;


    SpriteRenderer spriteRenderer;
    [SerializeField] BoxCollider2D boxCollider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Update()
    {
        CheckIfPressurePlateClicked();
    }



    void CheckIfPressurePlateClicked()
    {
        if (pressurePlate.isClicked)
        {
            spriteRenderer.sprite = openSprite;
            boxCollider.enabled = true;
            StartCoroutine(WaitAndCloseTrapDoor(15));
        }
        
    }




    IEnumerator WaitAndCloseTrapDoor(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = closedSprite;
        boxCollider.enabled = false;
        pressurePlate.isClicked = false;
    }
}
