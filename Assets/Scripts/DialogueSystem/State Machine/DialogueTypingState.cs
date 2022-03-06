using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTypingState : DialogueBaseState
{   
    private string _currentText;
    public DialogueTypingState(DialogueManager dialogueManager) : base(dialogueManager)
    {
    }

    public override void Start(){
        _currentText = DialogueManager.currentConvo.GetLineByIndex(DialogueManager.currentIndex).dialogue;
        DialogueManager.dialogueInterface.SetDialogueText("");
        DialogueManager.StartCoroutine(Type());

       
    }

    protected override IEnumerator Type()
    { 
        foreach (char c in _currentText){
            DialogueManager.dialogueInterface.SetDialogueText( DialogueManager.dialogueInterface.GetDialogueText() + c);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
