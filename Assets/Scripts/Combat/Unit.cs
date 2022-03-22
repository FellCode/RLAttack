using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
   public class Unit : MonoBehaviour
   {
      public float shakeDuration;
      public float shakeMagnitude;
      public string unitName;
      public int unitLevel;

      public int maxHp;
      public int currentHp;

      public MoveSet moveSet;
      public Queue<string> StatusUpdates { get; private set; } = new Queue<string>();

      private Condition Condition { get;  set; }

      public BattleHUD battleHudReference;

      public bool TakeDamage(int dmg){
         currentHp -= dmg;
         StartCoroutine(Shake(shakeDuration,shakeMagnitude));
         return currentHp <=0;
      }
      public void SetCondition(ConditionID conditionId)
      {
         Condition = ConditionsDB.Conditions[conditionId];
         StatusUpdates.Enqueue($"{unitName} {Condition.StartMessage}");
      }
      public void SetBattleHud(BattleHUD battleHud)
      {
         battleHudReference = battleHud;
      }

      private IEnumerator Shake(float duration, float magnitude){
         var originalPosition = transform.localPosition;
         var elapsed = 0.0f;

         while(elapsed < duration){
            var x = Random.Range(-1f,1f) * magnitude;
            var y = Random.Range(-1f,1f) * magnitude;

            transform.localPosition = new Vector3(x,originalPosition.z);
         
            elapsed += Time.deltaTime;

            yield return null;
         }
      
         transform.localPosition = originalPosition;
      }

      public void OnAfterTurn()
      {
         Condition?.OnAfterTurn?.Invoke(this);
      }

      public MoveBase GetRandomMove()
      {
         //return moveSet.getMoveByIndex(Random.Range(0, 3));
         return moveSet.getMoveByIndex(0);
      }
   }
}
