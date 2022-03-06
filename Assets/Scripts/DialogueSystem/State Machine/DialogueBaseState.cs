using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueBaseState
{
    protected readonly DialogueManager DialogueManager;

    protected DialogueBaseState(DialogueManager dialogueManager)
    {
        DialogueManager = dialogueManager;
    }

    protected virtual IEnumerator Type()
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
