using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : DialogueStateManager
{
    public int currentIndex;
    private static DialogueManager instance;

    public Conversation currentConvo;
    private PlayerCharacterController PlayerController;

    public DialogueUI Interface;

    [SerializeField] GameObject Player;

    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject); 
        }
    }

    private void Start()
    {
        PlayerController = Player.GetComponent<PlayerCharacterController>();
        SetState(new DialogueIdleState(this));
    }

    public PlayerCharacterController getPlayerController(){
        return PlayerController;
    }

    public static void StartConversation(Conversation convo)
    {
        instance.currentConvo = convo;
        instance.SetState(new DialogueProgressState(instance));
    }
}


