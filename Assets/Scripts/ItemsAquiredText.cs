using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemsAquiredText : MonoBehaviour
{
    [SerializeField] GameObject aquiredItemObject;
    [SerializeField] TextMeshProUGUI aquiredItemText;



    
    public void TurnItemObjectOn(bool argument)
    {
        aquiredItemObject.SetActive(argument);
        StartCoroutine(WaitAndTurnTextOff(2));
    }

    public void SetItemText(string itemFound)
    {
        aquiredItemText.text = itemFound;
    }

    IEnumerator WaitAndTurnTextOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        aquiredItemObject.SetActive(false);
        aquiredItemText.text = "";
    }
}
