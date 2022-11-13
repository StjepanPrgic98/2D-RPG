using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
    PlayerMovement player;
    Vector3 facingLeft = new Vector3(-1, 1, 1);
    Vector3 facingRight = new Vector3(1, 1, 1);
    Vector3 timerScaleGoingLeft = new Vector3(-0.0005f, 0.0005f, 0.0005f);
    Vector3 timerScaleGoingRight = new Vector3(0.0005f, 0.0005f, 0.0005f);


    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }



    private void Update()
    {
        SetScale();
    }


    void SetScale()
    {
        if(player.transform.localScale == facingLeft)
        {
            transform.localScale = timerScaleGoingLeft;
        }
        if(player.transform.localScale == facingRight)
        {
            transform.localScale = timerScaleGoingRight;
        }
    }
}
