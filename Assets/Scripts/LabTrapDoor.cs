using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabTrapDoor : MonoBehaviour
{
    [SerializeField] Sprite closedSprite;
    [SerializeField] Sprite openSprite;
    [SerializeField] LabPuzzle labPuzzle;
    [SerializeField] BoxCollider2D boxCollider;


    SpriteRenderer spriteRenderer;
    GameManager gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CheckIfPressurePlateClicked();
    }






    void CheckIfPressurePlateClicked()
    {
        if (gameManager.labPuzzleSolved) { spriteRenderer.sprite = openSprite; boxCollider.enabled = true; return; }
        if (labPuzzle.isClicked)
        {
            spriteRenderer.sprite = openSprite;
            boxCollider.enabled = true;
            gameManager.labPuzzleSolved = true;
            //StartCoroutine(WaitAndCloseTrapDoor(20));
        }

    }



    IEnumerator WaitAndCloseTrapDoor(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = closedSprite;
        boxCollider.enabled = false;
        labPuzzle.isClicked = false;
    }


    

}
