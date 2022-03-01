using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueStateManager : MonoBehaviour
{
    protected DialogueBaseState State;


    public void SetState(DialogueBaseState state){
        State = state;
        State.Start();
    }
}
