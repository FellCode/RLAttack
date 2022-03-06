using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueStateManager : MonoBehaviour
{
    private DialogueBaseState _state;


    public void SetState(DialogueBaseState state){
        _state = state;
        _state.Start();
    }
}
