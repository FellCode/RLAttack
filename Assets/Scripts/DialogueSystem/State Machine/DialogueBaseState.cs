using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueBaseState
{
    protected DialogueManager DialogueManager;

    public DialogueBaseState(DialogueManager dialogueManager)
    {
        DialogueManager = dialogueManager;
    }

    public virtual void Read()
    {
        return;
    }

    public virtual void Start()
    {
        return;
    }

    public virtual void Interact()
    {
        return;
    }
}
