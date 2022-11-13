using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHour : MonoBehaviour
{
    [SerializeField] int hour;

    BigClockPuzzle bigClockPuzzle;


    private void Awake()
    {
        bigClockPuzzle = FindObjectOfType<BigClockPuzzle>();
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            bigClockPuzzle.hours.Add(hour);
            Debug.Log(hour);
        }
    }
}
