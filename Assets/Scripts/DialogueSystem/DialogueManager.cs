using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogueText, navButtonText;
    public Image speakerSprite;

    private int currentIndex;
    private static DialogueManager instance;

    private Conversation currentConvo;
    private Animator Animator;
    private DialogueState state = DialogueState.DONE;
    private PlayerCharacterController PlayerController;

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
        Animator = GetComponent<Animator>();
        PlayerController = Player.GetComponent<PlayerCharacterController>();
    }

    public void Update()
    {
        //if (Input.GetKey(KeyCode.E))
        //{
        //    ReadNext();
        //} 
    }


    public static void StartConversation(Conversation convo)
    {
       if (instance.state.Equals(DialogueState.DONE))
        {
            instance.PlayerController.enabled = false;
            instance.Animator.SetBool("isOpen", true);
            instance.currentIndex = 0;
            instance.currentConvo = convo;
            instance.speakerName.text = "";
            instance.dialogueText.text = "";
            instance.navButtonText.text = "V";
            instance.state = DialogueState.PROGRESS;
        }
    }

    public static bool IsOngoing()
    {
        return instance.state.Equals(DialogueState.PROGRESS);
    }

    public void ReadNext()
    {
        if (state.Equals(DialogueState.TYPING))
        {
            return;
        }

        state = DialogueState.PROGRESS;
        if (currentConvo == null)
            return;

        if (currentIndex > currentConvo.GetLength())
        {
            Animator.SetBool("isOpen", false);
            PlayerController.enabled = true;
            state = DialogueState.DONE;
            return;
        }

        if(currentIndex == currentConvo.GetLength()){
            navButtonText.text = "X";
        }

        string Dialogue = currentConvo.GetLineByIndex(currentIndex).dialogue;
        string SpeakerName = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
        Sprite SpeakerSprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();

        speakerName.text = SpeakerName;
        speakerSprite.sprite = SpeakerSprite;
        StartCoroutine(TypeText(Dialogue));
      
        currentIndex++;
    }


    public IEnumerator TypeText(string text)
    {
        state = DialogueState.TYPING;
        dialogueText.text = "";
        bool complete = false;
        int index = 0;

        while (!complete)
        {
            dialogueText.text += text[index];
            yield return new WaitForSeconds(0.02f);

            if(index == text.Length - 1)
            {
                complete = true;
            }
            else
            {
                index++;
            }
        }
        state = DialogueState.PROGRESS;
    }

    private enum DialogueState
    {
        START,
        PROGRESS,
        DONE,
        TYPING
    }
}


