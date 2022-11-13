using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] string locationOfWarp;

    bool doTeleport;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {          
            doTeleport = true;
        }
    }

    public void ResetDoTeleport()
    {
        doTeleport = false;
    }


    public bool DoTeleport()
    {
        return doTeleport;
    }
    public string GetLocationOfWarp()
    {
        return locationOfWarp;
    }


}
