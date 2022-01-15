using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    private const string DIRECTION_ANIMATION = "Direction";

    public float speed;
    private Animator Animator;

    private static TopDownCharacterController instance;
    private CharacterInput CharacterInput;
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
            GetComponent<Rigidbody2D>().velocity = speed * CharacterInput.Dir;
    }

    public bool CharIsMoving(){
        return CharacterInput.IsMoving;
    }
}
