using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class CharacterInputManager : MonoBehaviour, IInputHandler
{
    public bool IsMoving { get; private set; }
    public Vector2 Movement { get; private set; }
    
    [SerializeField]
    private PlayerCharacterController playerCharacterController;


    private void Awake()
    {
        playerCharacterController.InputMaster.Player.Enable();
    }
    
    
    private void OnEnable()
    {
        playerCharacterController.InputMaster.Player.Interact.performed += playerCharacterController.Interact;
        playerCharacterController.InputMaster.Player.Save.performed += playerCharacterController.SaveInventory;
        playerCharacterController.InputMaster.Player.Load.performed += playerCharacterController.LoadInventory;
    }
    
    private void OnDisable()
    {
        playerCharacterController.InputMaster.Player.Interact.performed -= playerCharacterController.Interact;
        playerCharacterController.InputMaster.Player.Save.performed -= playerCharacterController.SaveInventory;
        playerCharacterController.InputMaster.Player.Load.performed -= playerCharacterController.LoadInventory;
    }

    internal bool CharacterIsMoving()
    {
        return playerCharacterController.InputMaster.Player.Move.ReadValue<Vector2>() != Vector2.zero;
    }

    private void Update()
    {
        Movement = playerCharacterController.InputMaster.Player.Move.ReadValue<Vector2>();
    }
}
