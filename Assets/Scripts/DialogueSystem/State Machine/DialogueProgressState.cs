using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProgressState : DialogueBaseState
{
    public DialogueProgressState(DialogueManager dialogueManager) : base(dialogueManager)
    {
    }

    public override void Start(){
        DialogueManager.dialogueInterface.ShowDialogueWindow(true);
        Interact();
    }

    public override void Interact(){
        
        if (DialogueManager.currentIndex > DialogueManager.currentConvo.GetLength())
        {
            DialogueManager.dialogueInterface.ShowDialogueWindow(false);
            DialogueManager.GetPlayerController().enabled = true;
            DialogueManager.SetState(new DialogueIdleState(DialogueManager));
        }

        if(DialogueManager.currentIndex == DialogueManager.currentConvo.GetLength()){
            DialogueManager.dialogueInterface.SetNavButton("X");
        }
        
        DialogueLine currentLine = DialogueManager.currentConvo.GetLineByIndex(DialogueManager.currentIndex);

        DialogueManager.dialogueInterface.SetSpeakerName(currentLine.speaker.name);
        DialogueManager.dialogueInterface.SetSpeakerSprite(currentLine.speaker.GetSprite());
        DialogueManager.SetState(new DialogueTypingState(DialogueManager));
      
        DialogueManager.currentIndex ++;
    }
}
