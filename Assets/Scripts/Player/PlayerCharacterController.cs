using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    private const string DIRECTION_ANIMATION = "Direction";

    public float speed;
    public InventoryObject Inventory;
    private Animator Animator;

    private static PlayerCharacterController instance;
    private IInputHandler CharacterInput;
    private PlayerAnimationManager PlayerAnimationManager;
    private Rigidbody2D PlayerRigidBody2D;

    private void Awake()
    {
        if (instance == null)
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
        transform.localPosition = SceneData.playerPosition;
        CharacterInput = GetComponent<CharacterInput>();
        PlayerAnimationManager = new PlayerAnimationManager(Animator);
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
            PlayerAnimationManager.UpdatePlayerAnimation(DIRECTION_ANIMATION, CharacterInput.Dir);
            PlayerRigidBody2D.velocity = speed * CharacterInput.Dir;

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Saving Stuff!");
            Inventory.Safe();
        }

        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            Debug.Log("Loading Stuff!");
            Inventory.Load();
        }
    }

    public bool CharIsMoving(){
        return CharacterInput.IsMoving;
    }

    private void OnApplicationQuit()
    {
        Inventory.Container.Clear();
    }
}
