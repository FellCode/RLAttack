using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogueText, navButtonText;
    public Image speakerSprite;

    public Animator animator;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");


    private void OnValidate()
    {
        animator = GetComponentInParent<Animator>();
    }

    public void SetSpeakerName(string newSpeakerName){
        this.speakerName.text = newSpeakerName;
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
        animator.SetBool(IsOpen, show);
    }
}
