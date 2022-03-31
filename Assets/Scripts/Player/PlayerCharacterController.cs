using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerCharacterController : MonoBehaviour
{
    public float speed;
    public InventoryObject inventory;

    private static PlayerCharacterController _instance;


    internal Animator PlayerAnimator;
    internal Rigidbody2D PlayerRigidBody2D;
    internal InputMaster InputMaster;

    [SerializeField] internal CharacterAnimationManager characterAnimationManager;
    [SerializeField] internal CharacterInputManager characterInputManager;
    [SerializeField] internal CharacterMovementManager characterMovementManager;
    [SerializeField] internal CharacterCollisionManager characterCollisionManager;


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

        InputMaster = new InputMaster();
        PlayerAnimator = GetComponent<Animator>();
        PlayerRigidBody2D = GetComponent<Rigidbody2D>();
        //ToDo: Player Spawning auslagern
        transform.localPosition = SceneData.PlayerPosition;
    }

    internal void SaveInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        inventory.Safe();
    }

    internal void LoadInventory(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        inventory.Load();
    }

    public bool CharIsMoving()
    {
        return characterInputManager.CharacterIsMoving();
    }

    private void OnApplicationQuit()
    {
        inventory.container.Clear();
    }

    internal void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed || characterCollisionManager.Colliders.Count <= 0) return;
        IInteractable interactable = FindClosestInteractable();

        interactable?.Interact();
    }

    //TODO: Auslagern in Service
    private IInteractable FindClosestInteractable()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Collider2D currentCollider in characterCollisionManager.Colliders)
        {
            Vector3 diff = currentCollider.gameObject.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (!(curDistance < distance)) continue;
            closest = currentCollider.gameObject;
            distance = curDistance;
        }

        return closest.GetComponent<IInteractable>();
    }

    public static void AddItemToPlayerInventoryStatic(ItemObject obj, int amount)
    {
        _instance.inventory.AddItem(obj, amount);
    }

    public void ToggleMovement(bool canMove)
    {
        characterMovementManager.enabled = canMove;
    }
}