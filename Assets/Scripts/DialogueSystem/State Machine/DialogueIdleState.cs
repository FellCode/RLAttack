using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueIdleState : DialogueBaseState
{
    public DialogueIdleState(DialogueManager dialogueManager) : base(dialogueManager)
    {
    }

    public override void Start(){
        DialogueManager.GetPlayerController().SetMovementIsAllowed(true); //Durch Methode ersetzen, die auch Bewegung stoppt
        DialogueManager.currentIndex = 0;
        DialogueManager.dialogueInterface.SetSpeakerName("");
        DialogueManager.dialogueInterface.SetDialogueText("");
        DialogueManager.dialogueInterface.SetNavButton("V");
    }
    public override void Interact(){
        DialogueManager.SetState(new DialogueProgressState(DialogueManager));
    }
}
