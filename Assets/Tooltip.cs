using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip instance;

    [SerializeField]
    private Camera uiCamera;
    private Text TooltipText;
    private RectTransform BackgroundTextTransform;

    private ReportMousePosition MousePositionHandler;

    private void Awake()
    {
        instance = this;
        BackgroundTextTransform = transform.Find("Background").GetComponent<RectTransform>();
        TooltipText = transform.Find("Text").GetComponent<Text>();
        MousePositionHandler = GetComponent<ReportMousePosition>();
        gameObject.SetActive(false);
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);
        TooltipText.text = tooltipString;
        float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(TooltipText.preferredWidth + textPaddingSize*2f, TooltipText.preferredHeight + textPaddingSize * 2f);
        BackgroundTextTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), MousePositionHandler.getMousePosition(), uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        instance.HideTooltip();
    }
}
