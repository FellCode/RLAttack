using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogueText, navButtonText;
    public Image speakerSprite;

    public Animator Animator;

    public void SetSpeakerName(string name){
        speakerName.text = name;
    }

    public void SetNavButton(string text){
        navButtonText.text = text;
    }

    public void SetDialogueText(string line){
        dialogueText.text = line;
    }


    public string GetDialogueText(){
        return dialogueText.text;
    }

    public void SetSpeakerSprite(Sprite sprite){
        speakerSprite.sprite = sprite;
    }

    public void ShowDialogueWindow(bool show){
        Animator.SetBool("isOpen", show);
    }
}
