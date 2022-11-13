using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSignsVisited : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] string id;
    [SerializeField] bool isSign;
    [SerializeField] bool isArea;

    bool visited;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void SetVisited()
    {
        visited = true;
    }
    public bool IsVisited()
    {
        return visited;
    }
    public bool IsArea()
    {
        return isArea;
    }
    public bool IsSign()
    {
        return isSign;
    }
    public string GetId()
    {
        return id;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (visited) { return; }
        if(collision.tag == "Player")
        {
            visited = true;
            if (isSign)
            {
                gameManager.visitedSigns.Add(id);
            }
            if (isArea)
            {
                gameManager.visitedAreas.Add(id);
            }
        }
    }






}
