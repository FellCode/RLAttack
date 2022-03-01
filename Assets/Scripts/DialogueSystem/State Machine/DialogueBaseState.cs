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

    public virtual IEnumerator Type()
    {
        yield break;
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
