using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueProgressState : DialogueBaseState
{
    public DialogueProgressState(DialogueManager dialogueManager) : base(dialogueManager)
    {
    }

    public override void Start(){
        DialogueManager.Interface.ShowDialogueWindow(true);
        Interact();
    }

    public override void Interact(){
        DialogueLine currentLine = DialogueManager.currentConvo.GetLineByIndex(DialogueManager.currentIndex);
        if (DialogueManager.currentIndex > DialogueManager.currentConvo.GetLength())
        {
            DialogueManager.Interface.ShowDialogueWindow(false);
            DialogueManager.getPlayerController().enabled = true;
            DialogueManager.SetState(new DialogueIdleState(DialogueManager));
        }

        if(DialogueManager.currentIndex == DialogueManager.currentConvo.GetLength()){
            DialogueManager.Interface.SetNavButton("X");
        }

        DialogueManager.Interface.SetSpeakerName(currentLine.speaker.name);
        DialogueManager.Interface.SetSpeakerSprite(currentLine.speaker.GetSprite());
        DialogueManager.StartCoroutine(Type());
      
        DialogueManager.currentIndex ++;
    }
}
