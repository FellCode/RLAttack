using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignController : MonoBehaviour, IInteractable
{
    public Conversation convo;


    public void Interact()
    {
        DialogueManager.StartConversation(convo);
    }
    public void ReadSign()
    {   
            
    }
}
