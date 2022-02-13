using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueBaseState
{
    public abstract void EnterState(DialogueStateManager dialogue);

    public abstract void UpdateState(DialogueStateManager dialogue);

    public abstract void Interact(DialogueStateManager dialogue);
}
