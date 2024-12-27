using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialougeControl : MonoBehaviour
{

    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> moveTexts;

    [SerializeField] Color highlightColor;

    [SerializeField] BattleSystem battleSystem;
    [SerializeField] PokeMoves PokeMoves;

    [SerializeField] Text ppText;
    [SerializeField] Text typeText;
   

    public void EnableDialogText(bool enabled)
    {
        battleSystem.dialogueText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled) { 
        
        actionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled)
    {
        moveSelector.SetActive(enabled);
        moveDetails.SetActive(enabled);
        PokeMoves.PlayMoves();
    }

    //public void UpdateMoveDetails(string moveName, string moveType, string maxPP)
    //{
    //    moveTexts[1].text = moveName;
    //    ppText.text = "PP:" + maxPP;
    //    typeText.text = "Type " + moveType;
    //}


    public void UpdateMoveDetails(List<string> moves)
    {

        ppText.text = "PP:" + PokeMoves.maxxPP;
        typeText.text = "Type " + PokeMoves.type;

        for (int i = 0; i < moves.Count; i++)
        {
            
            if(i < moveTexts.Count)
            {
                moveTexts[i].text = moves[i];

            }
        }

    }

}
