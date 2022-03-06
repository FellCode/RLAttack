using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Unit : MonoBehaviour
{
   public float shakeDuration;
   public float shakeMagnitude;
   public string unitName;
   public int unitLevel;

   public int maxHp;
   public int currentHp;

   public MoveSet moveSet;
   private Queue<string> _statusUpdates;

   private Condition condition = ConditionsDB.Conditions[ConditionID.NONE];

   public bool TakeDamage(int dmg){
      currentHp -= dmg;
      StartCoroutine(Shake(shakeDuration,shakeMagnitude));
      return currentHp <=0;
   }

   public void HealSelf(int amount){
      currentHp += amount;
      if(currentHp >= maxHp){
         currentHp = maxHp;
      }
   }

   public void SetCondition(ConditionID conditionId)
   {
      condition = ConditionsDB.Conditions[conditionId];
   }

   public string GetNextStatusUpdate()
   {
      return _statusUpdates.Dequeue();
   }

   public void SetNewStatusUpdate(string message)
   {
     _statusUpdates.Enqueue(message);  
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
}
