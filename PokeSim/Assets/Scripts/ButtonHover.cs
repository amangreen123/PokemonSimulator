using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    public GameObject ppText;
    public GameObject powerText;


    // Start is called before the first frame update
    void Start()
    {
        ppText.SetActive(false);
        powerText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ppText.SetActive(true);
        powerText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ppText.SetActive(false);
        powerText.SetActive(false);
    }
}