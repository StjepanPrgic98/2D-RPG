using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderBuffs : MonoBehaviour
{
    [SerializeField] bool protect;
    [SerializeField] bool oneHitShield;
    [SerializeField] bool darkShield;
    [SerializeField] bool reflectShield;
    Vector3 darkShieldPosition = new Vector3(0, 0.45f, 0);

    Defender defender;

    private void Awake()
    {
        defender = FindObjectOfType<Defender>();
    }



    private void Update()
    {
        if(protect && defender.hasProtect == false)
        {
            Destroy(gameObject);
        }
        if(oneHitShield && defender.hasOneHitShield == false)
        {
            Destroy(gameObject);
        }
        if(darkShield && defender.hasDarkShield == false)
        {
            Destroy(gameObject);
        }
        if(reflectShield && defender.hasReflectShield == false)
        {
            Destroy(gameObject);
        }
        if(darkShield && defender.hasDarkShield == true)
        {

            transform.position = defender.transform.position + darkShieldPosition;
        }
        else
        {
            transform.position = defender.transform.position;
        }
    }
}
