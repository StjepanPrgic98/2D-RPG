using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Sprite normalSprite;
    [SerializeField] Sprite clickedSprite;

    SpriteRenderer spriteRenderer;
    AudioPlayer audioPlayer;

    [HideInInspector] public bool isClicked;


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        spriteRenderer.sprite = clickedSprite;
        isClicked = true;
        audioPlayer.PlayPressurePlateClip();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sprite = normalSprite;
    }
}
