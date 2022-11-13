using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintSigns : MonoBehaviour
{
    [SerializeField] string hint;

    [SerializeField] TextMeshProUGUI hintText;
    [SerializeField] GameObject hintTextObject;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //hintTextObject.SetActive(true);
            hintText.text = hint;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //hintTextObject.SetActive(false);
            hintText.text = "";
        }
    }

}
