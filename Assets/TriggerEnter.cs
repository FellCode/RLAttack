using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnter : MonoBehaviour
{

    public Conversation convo;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueManager.StartConversation(convo); 
    }
}
