using UnityEngine;

public interface IInputHandler
{
    Vector2 Dir { get; }
    bool IsMoving { get; }
}