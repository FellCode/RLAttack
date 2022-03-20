using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="new Moveset", menuName="Moves/New MoveSet")]
public class MoveSet : ScriptableObject
{
    [SerializeField] private MoveBase[] allAttacks;

    public MoveBase getMoveByIndex(int index)
    {
        return allAttacks[index];
    }
}
