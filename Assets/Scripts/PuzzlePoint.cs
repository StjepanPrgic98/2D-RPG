using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzlePoint : MonoBehaviour
{
    GameManager gameManager;
    PuzzleUnderTower puzzle;
    PuzzleTotem[] puzzleTotems;
    [SerializeField] string id;
    List<PuzzleTotem> totems = new List<PuzzleTotem>();

    private void Awake()
    {
        puzzle = FindObjectOfType<PuzzleUnderTower>();
        puzzleTotems = FindObjectsOfType<PuzzleTotem>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        if (gameManager.underTowerPuzzleSolved) { gameObject.SetActive(false); return; }
        totems = puzzleTotems.ToList();
        for (int i = 0; i < puzzleTotems.Length; i++)
        {
            if (puzzleTotems[i].tag == "PuzzleTotem1")
            {
                totems[0] = puzzleTotems[i];
            }
            if (puzzleTotems[i].tag == "PuzzleTotem2")
            {
                totems[1] = puzzleTotems[i];
            }
            if (puzzleTotems[i].tag == "PuzzleTotem3")
            {
                totems[2] = puzzleTotems[i];
            }
            if (puzzleTotems[i].tag == "PuzzleTotem4")
            {
                totems[3] = puzzleTotems[i];
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PuzzleTotem1" && id == "1")
        {
            puzzle.totemPositions.Add(id);
            totems[0].TurnIconsOn();
            totems[0].setInPlace = true;
        }
        else if(collision.tag == "PuzzleTotem2" && id == "2")
        {
            puzzle.totemPositions.Add(id);
            totems[1].TurnIconsOn();
            totems[1].setInPlace = true;
        }
        else if (collision.tag == "PuzzleTotem3" && id == "3")
        {
            puzzle.totemPositions.Add(id);
            totems[2].TurnIconsOn();
            totems[2].setInPlace = true;
        }
        else if (collision.tag == "PuzzleTotem4" && id == "4")
        {
            puzzle.totemPositions.Add(id);
            totems[3].TurnIconsOn();
            totems[3].setInPlace = true;
        }
        else if(collision.tag == "Player") { }
        else
        {
            puzzle.KillPlayer();
        }
    }
}
