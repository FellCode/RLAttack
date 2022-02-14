using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProgressState : DialogueBaseState
{
    public override void EnterState(DialogueStateManager dialogue){
        PlayerController.enabled = false; //Durch Methode ersetzen, die auch Bewegung stoppt
        Animator.SetBool("isOpen", true);
        currentIndex = 0;
        currentConvo = convo;
        speakerName.text = "";
        dialogueText.text = "";
        navButtonText.text = "V";
        State.EnterState()
    }

    public override void UpdateState(DialogueStateManager dialogue){

    }

    public override void Interact(DialogueStateManager dialogue){

    }
}
