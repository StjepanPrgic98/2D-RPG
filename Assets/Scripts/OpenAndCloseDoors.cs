using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenAndCloseDoors : MonoBehaviour
{
    [SerializeField] bool isDoorSmall;
    [SerializeField] bool isDoorLarge;
    [SerializeField] bool isDungeonDoor;
    [SerializeField] bool isWornDownDoor;
    [SerializeField] bool isOtherSideDoor1;
    [SerializeField] bool isOtherSideDoor2;
    [SerializeField] bool isRuneDoor;
    [SerializeField] Sprite closedSprite;
    [SerializeField] Sprite openSprite;

    SpriteRenderer spriteRenderer;
    AudioPlayer audioPlayer;
    GameManager gameManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameManager = FindObjectOfType<GameManager>();
    }
    

    public bool IsDoorSmall()
    {
        return isDoorSmall;
    }
    public bool IsRuneDoor()
    {
        return isRuneDoor;
    }
    public bool IsDoorLarge()
    {
        return isDoorLarge;
    }
    public bool IsDungeonDoor()
    {
        return isDungeonDoor;
    }
    public bool IsWornDownDoor()
    {
        return isWornDownDoor;
    }
    public bool IsOtherSideDoor1()
    {
        return isOtherSideDoor1;
    }
    public bool IsOtherSideDoor2()
    {
        return isOtherSideDoor2;
    }

    public void CloseDoor()
    {
        spriteRenderer.sprite = closedSprite;
    }

    public void OpenDoor()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        spriteRenderer.sprite = openSprite;
        if (gameManager.sceneChange) { return; }
        audioPlayer.PlayUnlockedDoorClip();
    }


    
}
