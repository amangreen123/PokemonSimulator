using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeControl : MonoBehaviour
{

    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> moveTexts;

     public Text ppText;
     public Text typeText;

    [SerializeField] Color highlightColor;

    [SerializeField] BattleSystem battleSystem;
    [SerializeField] PokeMoves PokeMoves;

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

    public void UpdateMoveDetails(string moveName, string moveType, string maxPP)
    {
        moveTexts[1].text = moveName;
        ppText.text = "PP:" + maxPP;
        typeText.text ="Type " +  moveType;
    }

}
