using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
   public float shakeDuration;
   public float shakeMagnitude;
   public string unitName;
   public int unitLevel;

   public int damage;
   public int maxHP;
   public int currentHP;

   public int baseCritChance;

   public bool TakeDamage(int dmg){
      currentHP -= dmg;
      StartCoroutine(Shake(shakeDuration,shakeMagnitude));
      if(currentHP <=0)
         return true;
      else
         return false;
   }

   public void HealSelf(int amount){
      currentHP += amount;
      if(currentHP >= maxHP){
         currentHP = maxHP;
      }
   }

   IEnumerator Shake(float duration, float magnitude){
      Vector3 originalPosition = transform.localPosition;

      float elapsed = 0.0f;
      playHitSound();
      
      while(elapsed < duration){
         float x = Random.Range(-1f,1f) * magnitude;
         float y = Random.Range(-1f,1f) * magnitude;

         transform.localPosition = new Vector3(x,originalPosition.z);
         
         elapsed += Time.deltaTime;

         yield return null;
         
      }

      transform.localPosition = originalPosition;
   }

   void playHitSound(){

   }
}
