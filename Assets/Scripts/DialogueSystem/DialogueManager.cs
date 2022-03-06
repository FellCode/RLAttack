using UnityEngine;


public class DialogueManager : DialogueStateManager
{
    public int currentIndex;
    private static DialogueManager _instance;

    public Conversation currentConvo;
    private PlayerCharacterController _playerController;

    public DialogueUI dialogueInterface;

    [SerializeField] public GameObject player;

    private void Awake()
    {
       if(_instance == null)
       {
           _instance = this;
       } 
       else
       {
           Destroy(gameObject); 
       }
    }

    private void Start()
    {
        _playerController = player.GetComponent<PlayerCharacterController>();
        SetState(new DialogueIdleState(this));
    }

    public PlayerCharacterController GetPlayerController(){
        return _playerController;
    }

    public static void StartConversation(Conversation convo)
    {
        _instance.currentConvo = convo;
        _instance.SetState(new DialogueProgressState(_instance));
    }
}


