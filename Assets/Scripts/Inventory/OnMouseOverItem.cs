using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnMouseOverItem : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
{

    public string onHoverText;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover");
        //If your mouse hovers over the GameObject with the script attached, output this message
        Tooltip.ShowTooltip_Static(onHoverText);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Tooltip.HideTooltip_Static();
    }
}
