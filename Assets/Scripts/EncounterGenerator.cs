using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterGenerator : MonoBehaviour
{
      // This is the starting probability of an encounter
  const int DEFAULT_ENCOUNTER_THRESHOLD = 10;
  
  // Set the current probability to the default value
  private int currentEncounterThreshold = DEFAULT_ENCOUNTER_THRESHOLD;


  
  
  public void checkEncouter()
  {
      // Pick a number between 0 and 100
      int value = Random.Range(0, 200);
      Debug.Log(value);
      // Check if the number is below the current threshold
      if (value < currentEncounterThreshold)
      {
        // If it is, then start an encounter, and set the threshold back to the default value for next time.
        startEncounter();
        currentEncounterThreshold = DEFAULT_ENCOUNTER_THRESHOLD;
      }
      else
      {
        // We weren't below the threshold this time, so let's increase it
        currentEncounterThreshold += 1;
      }
  }


  void startEncounter(){
      //Play Encounter Animation
      //Screen Transition
      SceneManager.LoadScene("Combat");
  }
}
