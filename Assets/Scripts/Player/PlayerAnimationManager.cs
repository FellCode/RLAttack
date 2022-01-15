using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : IAnimationManager
{
    private Animator PlayerAnimator;
    public PlayerAnimationManager(Animator PlayerAnimator)
    {
        this.PlayerAnimator = PlayerAnimator;
    }

    public void UpdatePlayerAnimation(string animation, Vector2 dir)
    {
        if (!dir.Equals(Vector2.zero))
        {
            PlayerAnimator.SetInteger(animation, mapVectorToAnimation(dir));
            PlayerAnimator.SetBool("IsMoving", dir.magnitude > 0);
        }
    }

    private int mapVectorToAnimation(Vector2 dir)
    {
        if ((int)dir.x == -1)
        {
            return 3;
        }
        else if ((int)dir.x == 1)
        {
            return 2;
        }

        if ((int)dir.y == 1)
        {
            return 1;

        }
        else if ((int)dir.y == -1)
        {
            return 0;
        }

        return 99;
    }
}
