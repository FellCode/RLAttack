using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private static Tooltip _instance;

    [SerializeField]
    private Camera uiCamera;
    private Text _tooltipText;
    private RectTransform _backgroundTextTransform;

    private ReportMousePosition _mousePositionHandler;

    private void Awake()
    {
        _instance = this;
        _backgroundTextTransform = transform.Find("Background").GetComponent<RectTransform>();
        _tooltipText = transform.Find("Text").GetComponent<Text>();
        _mousePositionHandler = GetComponent<ReportMousePosition>();
        gameObject.SetActive(false);
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);
        _tooltipText.text = tooltipString;
        const float textPaddingSize = 4f;
        Vector2 backgroundSize = new Vector2(_tooltipText.preferredWidth + textPaddingSize*2f, _tooltipText.preferredHeight + textPaddingSize * 2f);
        _backgroundTextTransform.sizeDelta = backgroundSize;
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), _mousePositionHandler.getMousePosition(), uiCamera, out var localPoint);
        transform.localPosition = localPoint;
    }

    public static void ShowTooltip_Static(string tooltipString)
    {
        _instance.ShowTooltip(tooltipString);
    }

    public static void HideTooltip_Static()
    {
        _instance.HideTooltip();
    }
}
