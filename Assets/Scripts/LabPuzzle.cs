using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LabPuzzle : MonoBehaviour
{
    [HideInInspector] public List<int> labButtonsId;
    [HideInInspector] public bool resetButtons;

    [HideInInspector] public bool isClicked;
    [SerializeField] TextMeshProUGUI labText;
    [SerializeField] GameObject textObject;
    AudioPlayer audioPlayer;


    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    private void Update()
    {
       if(labButtonsId.Count == 4)
        {
           
            if (labButtonsId[0] == 1 && labButtonsId[1] == 2 && labButtonsId[2] == 3 && labButtonsId[3] == 4)
            {
                isClicked = true;
                ShowText(2, "Something happened!");
                audioPlayer.PlaySidequestDoneClip();
                ResetAndClear();              
            }
            else
            {
                ShowText(2, "Something went wrong!");
                ResetAndClear();
            }

        }

        
    }


    void ShowText(float delay, string text)
    {
        textObject.SetActive(true);
        labText.text = text;
        StartCoroutine(WaitAndTurnTextOff(delay));
    }
    private void ResetAndClear()
    {
        resetButtons = true;
        labButtonsId.Clear();
        StartCoroutine(WaitAndTurnOffResetButtons());
    }

    IEnumerator WaitAndTurnOffResetButtons()
    {
        yield return new WaitForSeconds(1);
        resetButtons = false;
    }

    IEnumerator WaitAndTurnTextOff(float delay)
    {
        yield return new WaitForSeconds(delay);
        textObject.SetActive(false);
        labText.text = "";
    }



}
