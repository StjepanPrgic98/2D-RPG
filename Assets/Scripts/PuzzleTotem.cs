using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTotem : MonoBehaviour
{
    GameManager gameManager;
    SpriteRenderer spriteRenderer;
    PuzzleUnderTower puzzle;
    AudioPlayer audioPlayer;
    BoxCollider2D boxCollider;
    Rigidbody2D rb;


    [SerializeField] string id;
    [SerializeField] GameObject icon1;
    [SerializeField] GameObject icon2;
    [SerializeField] GameObject icon3;
    [SerializeField] GameObject icon4;


    [HideInInspector] public bool setInPlace;

    Vector3 totem1CompletedPosition = new Vector3(4.884f, 0.306f, 0);
    Vector3 totem2CompletedPosition = new Vector3(5.766f, 0.313f, 0);
    Vector3 totem3CompletedPosition = new Vector3(5.767f, -0.902f, 0);
    Vector3 totem4CompletedPosition = new Vector3(4.879f, -0.907f, 0);
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        puzzle = FindObjectOfType<PuzzleUnderTower>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (gameManager.underTowerPuzzleSolved)
        {
            if(id == "1")
            {
                transform.position = totem1CompletedPosition;
                IconsCompleted();
            }
            if (id == "2")
            {
                transform.position = totem2CompletedPosition;
                IconsCompleted();
            }
            if (id == "3")
            {
                transform.position = totem3CompletedPosition;
                IconsCompleted();
            }
            if (id == "4")
            {
                transform.position = totem4CompletedPosition;
                IconsCompleted();
            }
            rb.mass = 9999;
            return;
        }
    }
    private void Update()
    {
        
        if (setInPlace)
        {
            rb.mass = 9999;

        }
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" || collision.tag == "PuzzleTotem1" || collision.tag == "PuzzleTotem2" || collision.tag == "PuzzleTotem3" || collision.tag == "PuzzleTotem4")
        {
            spriteRenderer.sortingOrder = 1;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        spriteRenderer.sortingOrder = -1;
    }

    void IconsCompleted()
    {
        icon1.SetActive(true);
        icon2.SetActive(true);
        icon3.SetActive(true);
        icon4.SetActive(true);
    }
    public void TurnIconsOn()
    {
        StartCoroutine(WaitAndTurnOnIcons(0.5f));
    }
    IEnumerator WaitAndTurnOnIcons(float delay)
    {
        icon1.SetActive(true);
        audioPlayer.PlayTotemBeepClip();
        yield return new WaitForSeconds(delay);
        icon2.SetActive(true);
        audioPlayer.PlayTotemBeepClip();
        yield return new WaitForSeconds(delay);
        icon3.SetActive(true);
        audioPlayer.PlayTotemBeepClip();
        yield return new WaitForSeconds(delay);
        icon4.SetActive(true);
        audioPlayer.PlayTotemEndClip();
    }
}
