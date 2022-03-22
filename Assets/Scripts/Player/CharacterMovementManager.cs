using UnityEngine;
using UnityEngine.Serialization;

public class CharacterMovementManager : MonoBehaviour
{ 
    [SerializeField]
    private PlayerCharacterController playerCharacterController;
    private void FixedUpdate()
    {
        Vector2 movement = playerCharacterController.characterInputManager.Movement;
        playerCharacterController.characterAnimationManager.UpdatePlayerAnimation(movement);
        playerCharacterController.PlayerRigidBody2D.velocity = playerCharacterController.speed * movement;
    }
}