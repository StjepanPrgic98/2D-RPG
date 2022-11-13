using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] bool smallDoor;
    [SerializeField] bool largeDoor;
    [SerializeField] bool wornDownDoor;
    [SerializeField] bool dungeonDoor;
    [SerializeField] bool otherSideDoor1;
    [SerializeField] bool otherSideDoor2;
    [SerializeField] bool runeDoor;
    [HideInInspector] public bool isOpen;


    [HideInInspector] public bool smallDoorOpen;
    [HideInInspector] public bool largeDoorOpen;
    [HideInInspector] public bool wornDoorOpen;
    [HideInInspector] public bool dungeonDoorOpen;
    [HideInInspector] public bool otherSide1DoorOpen;
    [HideInInspector] public bool otherSide2DoorOpen;
    [HideInInspector] public bool runeDoorOpen;

    



    OpenAndCloseDoors[] doors;
    BoxCollider2D boxCollider;
    GameManager gameManager;
    AudioPlayer audioPlayer;



    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        doors = FindObjectsOfType<OpenAndCloseDoors>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Start()
    {
        boxCollider.enabled = true;
        OpenDoors();
    }

    private void Update()
    {
        SetDoorCollider();
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameManager = FindObjectOfType<GameManager>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        doors = FindObjectsOfType<OpenAndCloseDoors>();
        if (smallDoor && gameManager.playerHasSmallKey)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsDoorSmall())
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                    gameManager.smallDoorOpen = true;
                }
                
            }
        }
        if(smallDoor && gameManager.playerHasSmallKey == false)
        {
            canvas.SetActive(true);
            text.text = "Locked! \n Small key required!";
            audioPlayer.PlayLockedDoorClip();
        }
        if(largeDoor && gameManager.playerHasLargeKey)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsDoorLarge())
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                    gameManager.largeDoorOpen = true;
                }

            }
        }
        if(largeDoor && gameManager.playerHasLargeKey == false)
        {
            canvas.SetActive(true);
            text.text = "Locked! \n Large Key Required!";
            audioPlayer.PlayLockedDoorClip();
        }
        if(wornDownDoor && gameManager.playerHasWornDownKey)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsWornDownDoor())
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                    gameManager.wornDoorOpen = true;
                }

            }
        }
        if(wornDownDoor && gameManager.playerHasWornDownKey == false)
        {
            canvas.SetActive(true);
            text.text = "Locked! \n Worn Down Key Required!";
            audioPlayer.PlayLockedDoorClip();
        }
        if(dungeonDoor && gameManager.playerHasDungeonKey)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsDungeonDoor())
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                    gameManager.dungeonDoorOpen = true;
                }

            }
        }
        if(dungeonDoor && gameManager.playerHasDungeonKey == false)
        {
            canvas.SetActive(true);
            text.text = "Locked! \n Dungeon Key Required";
            audioPlayer.PlayLockedDoorClip();
        }
        if (otherSideDoor1 && gameManager.otherSideKey1)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsOtherSideDoor1())
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                    gameManager.otherSide1DoorOpen = true;
                }

            }
        }
        if (otherSideDoor1 && gameManager.otherSideKey1 == false)
        {
            canvas.SetActive(true);
            text.text = "Does not open from this side!";
            audioPlayer.PlayLockedDoorClip();
        }
        if (otherSideDoor2 && gameManager.otherSideKey2)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsOtherSideDoor2())
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                    gameManager.otherSide2DoorOpen = true;
                }

            }
        }
        if (otherSideDoor2 && gameManager.otherSideKey2 == false)
        {
            canvas.SetActive(true);
            text.text = "Does not open from this side!";
            audioPlayer.PlayLockedDoorClip();
        }
        if (runeDoor && gameManager.playerHasRuneKey)
        {
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsRuneDoor())
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                    gameManager.runeDoorOpen = true;
                }

            }
        }
        if (runeDoor && gameManager.playerHasRuneKey == false)
        {
            canvas.SetActive(true);
            text.text = "Locked! \n Rune Key Required";
            audioPlayer.PlayLockedDoorClip();
        }


    }


    void SetDoorCollider()
    {
        if(gameManager.otherSide1DoorOpen == false && otherSideDoor1)
        {
            boxCollider.enabled = true;
        }
        else if(gameManager.otherSide2DoorOpen == false && otherSideDoor2)
        {
            boxCollider.enabled = true;
        }
    }


    void OpenDoors()
    {
        gameManager = FindObjectOfType<GameManager>();
        doors = FindObjectsOfType<OpenAndCloseDoors>();
        if (gameManager.smallDoorOpen && smallDoor)
        {
            gameManager.sceneChange = true;
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsDoorSmall() && smallDoorOpen == false)
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                }

            }
            smallDoorOpen = true;
        }
        if (gameManager.largeDoorOpen && largeDoor)
        {
            gameManager.sceneChange = true;
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsDoorLarge() && largeDoorOpen == false)
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                }

            }
            largeDoorOpen = true;
        }
        if (gameManager.wornDoorOpen && wornDoorOpen)
        {
            gameManager.sceneChange = true;
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsWornDownDoor() && wornDoorOpen == false)
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                }
            }
            wornDoorOpen = true;
        }
        if (gameManager.dungeonDoorOpen && dungeonDoor)
        {
            gameManager.sceneChange = true;
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsDungeonDoor() && dungeonDoorOpen == false)
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                }
            }
            dungeonDoor = true;
        }
        if (gameManager.otherSide1DoorOpen && otherSideDoor1)
        {
            gameManager.sceneChange = true;
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsOtherSideDoor1() && otherSide1DoorOpen == false)
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                }

            }
            otherSide1DoorOpen = true;
        }
        if (gameManager.otherSide2DoorOpen && otherSideDoor2)
        {
            gameManager.sceneChange = true;
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsOtherSideDoor2() && otherSide2DoorOpen == false)
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                }

            }
            otherSide2DoorOpen = true;
        }
        if(gameManager.runeDoorOpen && runeDoor)
        {
            gameManager.sceneChange = true;
            for (int i = 0; i < doors.Length; i++)
            {
                if (doors[i].IsRuneDoor() && runeDoorOpen == false)
                {
                    doors[i].OpenDoor();
                    boxCollider.enabled = false;
                }
            }
            runeDoorOpen = true;
        }
        gameManager.sceneChange = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canvas.SetActive(false);
    }
}
