using UnityEngine;

public interface IInputHandler
{
    Vector2 Movement { get; }
    bool IsMoving { get; }
}