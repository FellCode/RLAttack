using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStateManager : MonoBehaviour
{
    DialogueBaseState currentState;
    public DialogueIdleState IdleState = new DialogueIdleState();
    public DialogueProgressState ProgressState = new DialogueProgressState();
    public DialogueTypingState TypingState = new DialogueTypingState();

    public int currentIndex;

    public Conversation currentConvo;

     void Start() {
        currentState = IdleState;
        currentState.EnterState(this);
    }

     void Update() {
        currentState.UpdateState(this);
    }

    public void SwitchState(DialogueBaseState state){
        currentState = state;
        currentState.EnterState(this);
    }
}
