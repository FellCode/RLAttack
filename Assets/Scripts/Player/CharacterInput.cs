using UnityEngine;
using UnityEngine.InputSystem;
public class CharacterInput : MonoBehaviour, IInputHandler
{
    public bool IsMoving { get; private set; }
    public Vector2 Movement { get; private set; }

    private void Update()
    {
        //Movement = controls.Player.Move.ReadValue<Vector2>();
        IsMoving = !Movement.Equals(Vector2.zero);
    }



}
