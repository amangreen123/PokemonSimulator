using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor.Search;
using System;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject ppText;
    public GameObject typeText;
    
    public PokeMoves PM;

    private string buttonName = "";
   
    
    void Start()
    {
        ppText.SetActive(false);
        typeText.SetActive(false);

    }

    public void setMoveData (string name, string type, string pp)
    {

        ppText.GetComponent<Text>().text = "PP: " + pp;
        typeText.GetComponent<Text>().text = "Type: " + type;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
         buttonName = GetComponentInChildren<Text>().text;
         PokeMoveData moveData = PM.GetMoveData(buttonName);

         //showing the proper
         //Debug.Log("Hovered Button Text: " + buttonName);


        if (moveData != null)
        {
            setMoveData(moveData.name, moveData.type, moveData.maxxPP);
            ppText.SetActive(true);
            typeText.SetActive(true);
        }
        else
        {
            Debug.LogError($"Move '{buttonName}' not found in cache!");
        }
            

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ppText.SetActive(true);
        typeText.SetActive(true);

        ppText.GetComponent<Text>().text = "PP: " + "Nothing";
        typeText.GetComponent<Text>().text = "Type: " + " nothing";
       
    }

}