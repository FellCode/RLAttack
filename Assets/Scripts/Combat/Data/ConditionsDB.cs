using System.Collections;
using System.Collections.Generic;
using Combat;
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
                        unit.StatusUpdates.Enqueue($"{unit.name} erleidet Schaden durch die Alkoholvergiftung");
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