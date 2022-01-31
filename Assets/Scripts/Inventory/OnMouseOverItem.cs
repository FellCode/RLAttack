using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseOverItem : MonoBehaviour
{

    public string onHoverText;
    void OnMouseEnter()
    {
        //If your mouse hovers over the GameObject with the script attached, output this message
        Tooltip.ShowTooltip_Static(onHoverText);

    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Tooltip.HideTooltip_Static();
    }
}
