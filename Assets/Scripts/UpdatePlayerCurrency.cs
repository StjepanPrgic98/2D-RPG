using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdatePlayerCurrency : MonoBehaviour
{
    GameManager gameManager;
    TextMeshProUGUI[] textObject;
    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        textObject = FindObjectsOfType<TextMeshProUGUI>();
    }   



    private void Update()
    {
        UpdateCurrency();
    }


    void UpdateCurrency()
    {
        for (int i = 0; i < textObject.Length; i++)
        {
            if (textObject[i].tag == "Currency")
            {
                textObject[i].text = "Gold: " + gameManager.MoneyOwned() + "\n" + "Magic gems: " + gameManager.MagicGemsOwned() + "\n" + "Sharp gems: " + gameManager.SharpGemsOwned() + "\n" + "Special gems: " + gameManager.SpecialGemsOwned();
            }
        }
        
    }
}
