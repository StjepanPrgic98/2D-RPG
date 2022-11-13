using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    [SerializeField] PlayerMovement player;

    bool positionIsSet;
    private void Update()
    {
        if(transform.position != player.transform.position && positionIsSet == false)
        {
            transform.position = player.transform.position;
            positionIsSet = true;
        }
    }

    IEnumerator WaitAndSetPositionToPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = player.transform.position;
    }
}
