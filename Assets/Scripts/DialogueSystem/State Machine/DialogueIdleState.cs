using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIdleState : DialogueBaseState
{
    public override void EnterState(DialogueStateManager dialogue){
        dialogue.currentConvo = null;
        dialogue.currentIndex = 0;
    }

    public override void UpdateState(DialogueStateManager dialogue){

    }

    public override void Interact(DialogueStateManager dialogue){

    }
}
