using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="new Moveset", menuName="Moves/New MoveSet")]
public class MoveSet : ScriptableObject
{
    [SerializeField] private Attack[] allAttacks;

    public Attack getAttackByIndex(int index)
    {
        return allAttacks[index];
    }
}
