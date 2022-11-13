using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CantCollectYet : MonoBehaviour
{
    CircleCollider2D circleCollider;
    bool triedToCollect;
    [SerializeField] Boss boss;

    private void Awake()
    {
        circleCollider = FindObjectOfType<CircleCollider2D>();
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (boss.IsBossAlive())
        {
            circleCollider.enabled = true;
            triedToCollect = true;
        }
        else
        {
            Destroy(gameObject);
        }      
    }
    public bool TriedToCollect()
    {
        return triedToCollect;
    }

}
