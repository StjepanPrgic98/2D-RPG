using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Button : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite clickedSprite;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject textObject;
    [SerializeField] GameObject button;
    [SerializeField] bool buttonAtStart;

    [HideInInspector] public bool levelPulled;
    [HideInInspector] public bool isClicked;
    bool wasClicked;
    [HideInInspector] public bool nearButton;
   

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            textObject.SetActive(false);
            text.text = "";
            button.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if(levelPulled == false && isClicked == false)
            {
                textObject.SetActive(true);
                text.text = "No power!";
            }
            else if(levelPulled && wasClicked)
            {
                textObject.SetActive(true);
                text.text = "Doesnt work anymore!";
            }
            else if(levelPulled && isClicked == false)
            {
                button.SetActive(true);
                if (buttonAtStart)
                {
                    nearButton = true;
                    Debug.Log("Near button");
                }
                
            }
           
            
        }
        
    }

    public void ClickButton()
    {
        isClicked = true;
        spriteRenderer.sprite = clickedSprite;
        button.SetActive(false);
        StartCoroutine(WaitAndUnClickButton(1));
    }

    



    IEnumerator WaitAndUnClickButton(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.sprite = normalSprite;
        isClicked = false;
        wasClicked = true;

    }
}
