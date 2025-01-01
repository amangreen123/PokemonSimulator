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
    public Text buttonText;



    // Start is called before the first frame update
    void Start()
    {
        ppText.SetActive(false);
        typeText.SetActive(false);
    }


    //Compare available moves list with poke move data list
    //if the move name is in the list then show the move details

    public void setMoveData (string name, string type, string pp)
    {
        PM.name = name;
        PM.type = type;
        PM.maxxPP = pp;

        Debug.Log("Name: " + PM.name + " Type: " + PM.type + " PP: " + PM.maxxPP);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
       buttonName = GetComponentInChildren<Text>().text;

        //loop through pokedata list and compare the name of the move with the button name
        for (int i = 0; i < PM.pokeMoves.Count; i++)
        {
            if (PM.pokeMoves[i].name == buttonName)
            {
                PM.name = PM.pokeMoves[i].name;
                PM.type = PM.pokeMoves[i].type;
                PM.maxxPP = PM.pokeMoves[i].maxxPP;
                PM.movePower = PM.pokeMoves[i].movePower;
                PM.moveLevel = PM.pokeMoves[i].moveLevel;
                PM.accuracy = PM.pokeMoves[i].accuracy;

                ppText.GetComponent<Text>().text = "PP: " + PM.pokeMoves[i].maxxPP;
                typeText.GetComponent<Text>().text = "Type: " + PM.pokeMoves[i].type;
            }
        }

        ppText.SetActive(true);
        typeText.SetActive(true);

        //showing the proper
        Debug.Log("Hovered Button Text: " + buttonName);

        //need to get the text of the button
        Debug.Log(eventData.pointerEnter.name);

        //PM.showMovesDetails(buttonName);
        //Debug.Log("Cursor Entering " + buttonName + " GameObject");

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ppText.SetActive(true);
        typeText.SetActive(true);

        ppText.GetComponent<Text>().text = "PP: " + "Nothing";
        typeText.GetComponent<Text>().text = "Type: " + " nothing";
        Debug.Log("leaving " + eventData.pointerEnter.name);
    }

}