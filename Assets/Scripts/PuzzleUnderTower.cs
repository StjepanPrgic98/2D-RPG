using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleUnderTower : MonoBehaviour
{
    GameManager gameManager;
    [HideInInspector] public List<string> totemPositions = new List<string>();


    [SerializeField] GameObject darkSoul;

    [HideInInspector] public int target;
    [SerializeField] GameObject chest1;
    [SerializeField] GameObject chest2;
    [SerializeField] GameObject chest3;
    [SerializeField] GameObject chest4;
    [SerializeField] GameObject chest5;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        if (gameManager.underTowerPuzzleSolved)
        {
            chest1.GetComponent<SpriteRenderer>().enabled = true;
            chest1.GetComponent<BoxCollider2D>().enabled = true;
            chest2.GetComponent<SpriteRenderer>().enabled = true;
            chest2.GetComponent<BoxCollider2D>().enabled = true;
            chest3.GetComponent<SpriteRenderer>().enabled = true;
            chest3.GetComponent<BoxCollider2D>().enabled = true;
            chest4.GetComponent<SpriteRenderer>().enabled = true;
            chest4.GetComponent<BoxCollider2D>().enabled = true;
            chest5.GetComponent<SpriteRenderer>().enabled = true;
            chest5.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            chest1.GetComponent<SpriteRenderer>().enabled = false;
            chest1.GetComponent<BoxCollider2D>().enabled = false;
            chest2.GetComponent<SpriteRenderer>().enabled = false;
            chest2.GetComponent<BoxCollider2D>().enabled = false;
            chest3.GetComponent<SpriteRenderer>().enabled = false;
            chest3.GetComponent<BoxCollider2D>().enabled = false;
            chest4.GetComponent<SpriteRenderer>().enabled = false;
            chest4.GetComponent<BoxCollider2D>().enabled = false;
            chest5.GetComponent<SpriteRenderer>().enabled = false;
            chest5.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Update()
    {
        if (gameManager.underTowerPuzzleSolved) { return; }
        CheckIfTotemPositionIsCorrect();
    }

    void CheckIfTotemPositionIsCorrect()
    {
        if(totemPositions.Count == 4)
        {
            if(totemPositions[0] == "3" && totemPositions[1] == "4" && totemPositions[2] == "2" && totemPositions[3] == "1")
            {
                totemPositions.Clear();
                gameManager.underTowerPuzzleSolved = true;
                SpawnChestsWithDarkSouls();
            }
            else
            {
                totemPositions.Clear();
                KillPlayer();
            }
        }
    }
    public void SpawnChestsWithDarkSouls()
    {
        StartCoroutine(WaitAndSpawnDarkSouls(1));
    }
    IEnumerator WaitAndSpawnDarkSouls(float delay)
    {
        yield return new WaitForSeconds(3);
        target = 1;
        GameObject darkSoulsEffect1 = Instantiate(darkSoul, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        target = 2;
        GameObject darkSoulsEffect2 = Instantiate(darkSoul, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        target = 3;
        GameObject darkSoulsEffect3 = Instantiate(darkSoul, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        target = 4;
        GameObject darkSoulsEffect4 = Instantiate(darkSoul, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(delay);
        target = 5;
        GameObject darkSoulsEffect5 = Instantiate(darkSoul, transform.position, Quaternion.identity);

    }

    public void KillPlayer()
    {
        GameObject darkSoulsEffect = Instantiate(darkSoul, transform.position, Quaternion.identity);
    }
    
}
