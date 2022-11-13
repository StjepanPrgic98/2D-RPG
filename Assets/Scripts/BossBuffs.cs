using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBuffs : MonoBehaviour
{
    Boss boss;
    [SerializeField] bool bossShield;
    [SerializeField] bool reflectShield;

    private void Awake()
    {
        boss = FindObjectOfType<Boss>();
    }



    private void Update()
    {
        if(bossShield && boss.BossHasShield() == false)
        {
            Destroy(gameObject);
        }
        if(reflectShield && boss.BossHasReflectShield() == false)
        {
            Destroy(gameObject);
        }
    }
}
