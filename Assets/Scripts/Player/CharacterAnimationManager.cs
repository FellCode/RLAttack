using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAnimationManager : MonoBehaviour
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    [SerializeField]
    private PlayerCharacterController playerCharacterController;
    private static readonly int Direction = Animator.StringToHash("Direction");
    

    public void UpdatePlayerAnimation(Vector2 dir)
    {
        if (dir.Equals(Vector2.zero)) return;
        playerCharacterController.PlayerAnimator.SetInteger(Direction, MapVectorToAnimation(dir));
        playerCharacterController.PlayerAnimator.SetBool(IsMoving, dir.magnitude > 0);
    }
    
    
    private static int MapVectorToAnimation(Vector2 dir)
    {
        switch ((int)dir.x)
        {
            case -1:
                return 3;
            case 1:
                return 2;
        }

        switch ((int)dir.y)
        {
            case 1:
                return 1;
            case -1:
                return 0;
            default:
                return 99;
        }
    }
}