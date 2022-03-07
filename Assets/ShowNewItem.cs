using System.Collections;
using TMPro;
using UnityEngine;

public class ShowNewItem : MonoBehaviour
{
    private TMP_Text _text;
    private ChestController _chestController;

    private void Awake()
    {
        _chestController = GetComponentInParent<ChestController>();
        _text = GetComponent<TMP_Text>();
    }

    public void ShowFloatingText()
    {
        _text.SetText($"{_chestController.itemAmount}x {_chestController.item.name}");
        _text.CrossFadeAlpha(1.0f, 0.05f, false);
        StartCoroutine(WaitASecond());
    }

    private IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(1);
        _text.SetText(string.Empty);
    }
}