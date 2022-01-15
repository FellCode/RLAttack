using UnityEngine;

public interface ICharacterInput
{
    Vector2 Dir { get; }
    bool IsMoving { get; }
}