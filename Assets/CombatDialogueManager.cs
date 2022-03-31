using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CombatDialogueManager : MonoBehaviour
{
    
    private Text _dialogueText;
    public float letterPause;
    private void Awake()
    {
        _dialogueText=GetComponentInChildren<Text>();
        _dialogueText.text = string.Empty;
    }

    public IEnumerator TypeText(string message)
    {
        _dialogueText.text = string.Empty;
        foreach (char letter in message)
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(letterPause);
        }

        yield return new WaitForSeconds(1f);
    }

    public void SetText(string message)
    {
        _dialogueText.text = message;
    }
}
