using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabButton : MonoBehaviour
{
    [SerializeField] Sprite normalSprite; 
    [SerializeField] Sprite clickedSprite;
    [SerializeField] int buttonId;
    bool clicked;

    SpriteRenderer sprite;
    LabPuzzle labPuzzle;
    GameManager gameManager;
    AudioPlayer audioPlayer;


    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        labPuzzle = FindObjectOfType<LabPuzzle>();
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Update()
    {

        if (labPuzzle.resetButtons) { sprite.sprite = normalSprite; if (clicked) { StartCoroutine(WaitAndUnclickButton()); } }
    }



    public int GetButtonId()
    {
        return buttonId;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameManager.labPuzzleSolved) { return; }
        if(collision.tag == "Player" && clicked == false && labPuzzle.isClicked == false)
        {
            audioPlayer.PlayButtonClip();
            clicked = true;
            sprite.sprite = clickedSprite;
            labPuzzle.labButtonsId.Add(GetButtonId());
        }
    }


    IEnumerator WaitAndUnclickButton()
    {
        if(clicked == false) { yield break; }
        yield return new WaitForSeconds(2);
        clicked = false;
    }
 

    
}
