using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTypingState : DialogueBaseState
{   
    private string currentText;
    public DialogueTypingState(DialogueManager dialogueManager) : base(dialogueManager)
    {
    }

    public override void Start(){
        currentText = DialogueManager.currentConvo.GetLineByIndex(DialogueManager.currentIndex).dialogue;
        DialogueManager.Interface.SetDialogueText("");
        DialogueManager.StartCoroutine(Type());

       
    }

    public override IEnumerator Type()
    { 
        foreach (char c in currentText){
            DialogueManager.Interface.SetDialogueText( DialogueManager.Interface.GetDialogueText() + c);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
