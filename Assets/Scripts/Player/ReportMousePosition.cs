using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ReportMousePosition : MonoBehaviour
{   
    private Vector2 _mousePosition;
    private PointerEventData _pointerEventData;
    [SerializeField]
    private EventSystem eventSystem;
    [SerializeField]
    private GraphicRaycaster raycaster;

    // Update is called once per frame
    void Update()
    {
         _mousePosition = Mouse.current.position.ReadValue();
         
         _pointerEventData = new PointerEventData(eventSystem)
         {
             position = _mousePosition
         };
         
         List<RaycastResult> results = new List<RaycastResult>();
         
         raycaster.Raycast(_pointerEventData, results);
 
         if(results.Count > 0) {
            Tooltip.ShowTooltip_Static(results[0].gameObject.GetComponent<OnMouseOverItem>().onHoverText);
         }
         
         else
         {
             Tooltip.HideTooltip_Static();
         }
    }

    public Vector2 GetMousePosition()
    {
        return _mousePosition;
    }
}
