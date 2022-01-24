using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Debuff Object", menuName = "Inventory System/Items/Debuff")]
public class DebuffObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Debuff;
    }
}
