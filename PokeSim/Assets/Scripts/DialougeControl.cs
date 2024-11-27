using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeControl : MonoBehaviour
{

    [SerializeField] GameObject actionSelector;
    [SerializeField] GameObject moveSelector;
    [SerializeField] GameObject moveDetails;

    [SerializeField] List<Text> actionTexts;
    [SerializeField] List<Text> moveTexts;

     public Text ppText;
     public Text typeText;

    [SerializeField] Color highlightColor;

    [SerializeField] BattleSystem battleSystem;

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
    }

    //public void UpdateActionSelection(int selectedAction)
    //{
    //    for (int i = 0; i < actionTexts.Count; i++)
    //    {
    //        if (i == selectedAction)
    //        {
    //            actionTexts[i].color = highlightColor;

    //        }
    //        else
    //        {
    //            actionTexts[i].color = Color.black;
    //        }
    //    }
    //}
}
