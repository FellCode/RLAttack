using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private List<Collider2D> colliders = new List<Collider2D>();
    private InputMaster inputMaster;
    

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

        inputMaster = new InputMaster();
        inputMaster.Player.Enable();
        inputMaster.Player.Interact.performed += Interact;
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
        Vector2 Movement = inputMaster.Player.Move.ReadValue<Vector2>();
        PlayerAnimationManager.UpdatePlayerAnimation(DIRECTION_ANIMATION, Movement);
        PlayerRigidBody2D.velocity = speed * Movement;

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    Debug.Log("Saving Stuff!");
        //    Inventory.Safe();
        //}

        //if (Input.GetKey(KeyCode.KeypadEnter))
        //{
        //    Debug.Log("Loading Stuff!");
        //    Inventory.Load();
        //}
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
         if (!colliders.Contains(other) && other.CompareTag("Interactable")) 
         {  
             Debug.Log("Collided");
             colliders.Add(other); 
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        colliders.Remove(other);
    }

    public bool CharIsMoving(){
        return CharacterInput.IsMoving;
    }

    private void OnApplicationQuit()
    {
        Inventory.Container.Clear();
    }

    public List<Collider2D> GetColliders () 
    {
        return colliders; 
    }

    public void Interact(InputAction.CallbackContext context){
        if(context.performed && colliders.Count > 0){
            IInteractable interactable = FindClosestInteractable();
                interactable.Interact();
            
        }

    }


    public IInteractable FindClosestInteractable()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (Collider2D collider in colliders)
        {
            Vector3 diff = collider.gameObject.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = collider.gameObject;
                distance = curDistance;
            }
        }
        return closest.GetComponent<IInteractable>();
    }

    public static void AddItemToPlayerInventoryStatic(ItemObject obj, int amount){
        instance.Inventory.AddItem(obj,amount);
    }
}
