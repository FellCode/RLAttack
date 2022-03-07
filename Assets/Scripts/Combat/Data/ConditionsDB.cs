using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsDB
{

    public static Dictionary<ConditionID, Condition> Conditions { get; set; } =
        new Dictionary<ConditionID, Condition>()
        {
            {
            ConditionID.BSF,
            new Condition()
                {
                    Description = "Ziel erleidet eine Alkoholvergiftung",
                    Name = "Besoffen",
                    OnAfterTurn = unit =>
                    {
                        unit.TakeDamage(unit.maxHp / 8);
                    },
                    StartMessage = "erleidet eine Alkoholvergiftung"
                }
            }
        };
}

public enum ConditionID
{
    NONE,BSF,FHR,BNM,BLD,DST
}