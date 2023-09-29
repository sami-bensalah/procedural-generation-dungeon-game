using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject buttonHighlight;

    public void OnPointerEnter(PointerEventData pointerEventData) 
    {
        buttonHighlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData pointerEventData) 
    {
        buttonHighlight.SetActive(false);
    }
}
