using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterInput : MonoBehaviour, IInputHandler
{
    public bool IsMoving { get; private set; }
    public Vector2 Movement { get; private set; }

    public InputAction PlayerControls;

    private void OnEnable()
    {
        PlayerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerControls.Disable();
    }


    private void Update()
    {
        Movement = PlayerControls.ReadValue<Vector2>();
        IsMoving = !Movement.Equals(Vector2.zero);
    }



}
