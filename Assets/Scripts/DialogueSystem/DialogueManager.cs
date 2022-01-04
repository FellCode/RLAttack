using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI speakerName, dialogueText, navButtonText;
    public Image speakerSprite;

    private int currentIndex;
    private static DialogueManager instance;

    private Conversation currentConvo;
    private Animator anim;
    private Coroutine typing;
    private DialogueState state = DialogueState.DONE;

    [SerializeField] GameObject player;

    private void Awake()
    {
       if(instance == null)
        {
            instance = this;
            anim = GetComponent<Animator>();
        } 
        else
        {
            Destroy(gameObject); 
        }
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
                    ReadNext();
        } 
    }


    public static void StartConversation(Conversation convo)
    {
       if (instance.state.Equals(DialogueState.DONE))
        {
            instance.player.GetComponent<TopDownCharacterController>().toggleMovement(false);
            instance.anim.SetBool("isOpen", true);
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
            instance.anim.SetBool("isOpen", false);
            instance.player.GetComponent<TopDownCharacterController>().toggleMovement(true);
            state = DialogueState.DONE;
            return;
        }

        if(currentIndex == currentConvo.GetLength()){
            navButtonText.text = "X";
        }

        if(typing == null)
        {
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        }
        else
        {
            instance.StopCoroutine(typing);
            typing = null;
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).dialogue));
        }


        speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
        speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
        
        instance.currentIndex++;
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
        typing = null;
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
