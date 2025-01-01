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

    public List<Text> moveTexts;

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

    
    public void UpdateMoveDetails(List<string> moves)
    {
   
        for (int i = 0; i < moves.Count; i++)
        {
            
            if( i < moveTexts.Count)
            {
                moveTexts[i].text = moves[i];
                

            }
        }

    }

}
