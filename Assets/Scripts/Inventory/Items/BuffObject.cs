using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Buff Object", menuName = "Inventory System/Items/Buff")]
public class BuffObject : ItemObject
{
    public void Awake()
    {
        type = ItemType.Buff;
    }
}
