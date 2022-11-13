using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : MonoBehaviour
{
    [SerializeField] string Id;

    [SerializeField] GameObject object1;
    [SerializeField] GameObject object2;
    [SerializeField] GameObject object3;
    [SerializeField] GameObject object4;
    [SerializeField] GameObject object5;
    [SerializeField] GameObject object6;

    [SerializeField] GameObject energyBall;
    [SerializeField] GameObject energyBallLeft;

    [SerializeField] string direction;

    GameManager gameManager;
    PlayerMovement player;
    SpriteRenderer spriteRenderer;
    AudioPlayer audioPlayer;

    Vector3 aboveTotem = new Vector3(0, 0.5f, 0);

    bool isActivated;
    bool objectsOn;
    bool isTouching;

    bool energyBallSpawned;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    private void Update()
    {
        if (isActivated && objectsOn == false && isTouching == false)
        {
            objectsOn = true;
            object1.SetActive(true);
            object2.SetActive(true);
            object3.SetActive(true);
            object4.SetActive(true);
            object5.SetActive(true);
            object6.SetActive(true);
        }
        if (gameManager.totemsSolved && energyBallSpawned == false && gameManager.timesHitByEnergyBall < 9 && gameManager.energyBallSpawned == false)
        {
            energyBallSpawned = true;
            SpawnEneryBall(direction);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isActivated) { return; }
        isTouching = true;
        isActivated = true;
        gameManager.activatedTotems.Add(Id);
        StartCoroutine(WaitAndActivateObjects());
        if(gameManager.activatedTotems.Count == 9)
        {
            gameManager.totemsSolved = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            spriteRenderer.sortingOrder = -3;
        }
    }


    public string GetId()
    {
        return Id;
    }
    public void SetActivated()
    {
        isActivated = true;
    }


    void SpawnEneryBall(string direction)
    {
        if(direction == "left")
        {
            GameObject eneryBallEffect = Instantiate(energyBall, transform.position + aboveTotem, Quaternion.identity);
        }
        else
        {
            GameObject eneryBallEffect = Instantiate(energyBall, transform.position + aboveTotem, Quaternion.identity);
        }

    }

    IEnumerator WaitAndActivateObjects()
    {
        yield return new WaitForSeconds(0.5f);
        object1.SetActive(true);
        audioPlayer.PlayTotemBeepClip();
        yield return new WaitForSeconds(0.5f);
        object2.SetActive(true);
        audioPlayer.PlayTotemBeepClip();
        yield return new WaitForSeconds(0.5f);
        object3.SetActive(true);
        audioPlayer.PlayTotemBeepClip();
        yield return new WaitForSeconds(0.5f);
        object4.SetActive(true);
        audioPlayer.PlayTotemBeepClip();
        yield return new WaitForSeconds(0.5f);
        object5.SetActive(true);
        audioPlayer.PlayTotemBeepClip();
        yield return new WaitForSeconds(0.5f);
        object6.SetActive(true);
        audioPlayer.PlayTotemEndClip();
    }
}
