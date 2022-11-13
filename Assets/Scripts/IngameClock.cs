using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameClock : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeText;


    private void Start()
    {
        
    }

    private void Update()
    {
        var time = System.DateTime.Now;
        timeText.text = time.ToString();
    }
}
