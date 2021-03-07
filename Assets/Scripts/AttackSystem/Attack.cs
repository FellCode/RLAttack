using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Attack", menuName = "Moves/New Attack")]
public class Attack : ScriptableObject
{
    public string attackName;
    public int hitChance;
    public int critChance;
    public int damage;

}
