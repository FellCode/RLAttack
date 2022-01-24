using UnityEngine;

public class CharacterInput : MonoBehaviour, IInputHandler
{
    public bool IsMoving { get; private set; }
    public Vector2 Dir { get; private set; }

    void Update()
    {
        IsMoving = false;
        Dir = Vector2.zero;
        Vector2 CurrentDir = Vector2.zero;

        #region character Movement
        if (Input.GetKey(KeyCode.A))
        {
            CurrentDir.x = -1;
            IsMoving = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            CurrentDir.x = 1;
            IsMoving = true;
        }

        if (Input.GetKey(KeyCode.W))
        {
            CurrentDir.y = 1;
            IsMoving = true;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            CurrentDir.y = -1;
            IsMoving = true;
        }

        CurrentDir.Normalize();
        Dir = CurrentDir;

        #endregion
    }
}
