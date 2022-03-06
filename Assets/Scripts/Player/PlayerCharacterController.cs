using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCharacterController : MonoBehaviour
{
    public float speed;
    public InventoryObject inventory;
    
    private static PlayerCharacterController _instance;
    private readonly List<Collider2D> _colliders = new List<Collider2D>();
    private const string DirectionAnimation = "Direction";
    
    private Animator _animator;
    private PlayerAnimationManager _playerAnimationManager;
    private Rigidbody2D _playerRigidBody2D;
    private InputMaster _inputMaster;
    private bool _movementIsAllowed = true;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnValidate()
    {
        _inputMaster = new InputMaster();
        _inputMaster.Player.Enable();
        _animator = GetComponent<Animator>();
        _playerAnimationManager = new PlayerAnimationManager(_animator);
        _playerRigidBody2D = GetComponent<Rigidbody2D>();
        
        
        //ToDo: Player Spawning auslagern
        transform.localPosition = SceneData.PlayerPosition;
    }

    private void OnEnable()
    {
        _inputMaster.Player.Interact.performed += Interact;
        _inputMaster.Player.Save.performed += SaveInventory;
        _inputMaster.Player.Load.performed += LoadInventory;
    }
    
    private void OnDisable()
    {
        _inputMaster.Player.Interact.performed -= Interact;
        _inputMaster.Player.Save.performed -= SaveInventory;
        _inputMaster.Player.Load.performed -= LoadInventory;
    }

    private void SaveInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        inventory.Safe();
    }
    
    
    private void LoadInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        inventory.Load();
    }


    private void FixedUpdate()
    {
        if (!_movementIsAllowed) return;
        Vector2 movement = _inputMaster.Player.Move.ReadValue<Vector2>();
        _playerAnimationManager.UpdatePlayerAnimation(DirectionAnimation, movement);
        _playerRigidBody2D.velocity = speed * movement;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_colliders.Contains(other) || !other.CompareTag("Interactable")) return;
        
        _colliders.Add(other);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        _colliders.Remove(other);
    }

    public bool CharIsMoving(){
        return _inputMaster.Player.Move.ReadValue<Vector2>() != Vector2.zero;
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }

    private void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed || _colliders.Count <= 0) return;
        IInteractable interactable = FindClosestInteractable();

        interactable?.Interact();

    }
    
    //TODO: Auslagern in Service
    private IInteractable FindClosestInteractable()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Collider2D currentCollider in _colliders)
        {
            Vector3 diff = currentCollider.gameObject.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (!(curDistance < distance)) continue;
            closest = currentCollider.gameObject;
            distance = curDistance;
        }
        return closest.GetComponent<IInteractable>();
    }

    public static void AddItemToPlayerInventoryStatic(ItemObject obj, int amount){
        _instance.inventory.AddItem(obj,amount);
    }

    public void SetMovementIsAllowed(bool allowed)
    {
        _movementIsAllowed = allowed;
    }
}
