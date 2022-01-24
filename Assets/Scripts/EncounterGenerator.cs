using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterGenerator : MonoBehaviour
{
  const int DEFAULT_ENCOUNTER_THRESHOLD = 10;

  private int currentEncounterThreshold = DEFAULT_ENCOUNTER_THRESHOLD;
  private int immunityCounter;
 
  private int nextUpdate=1;

  private PlayerCharacterController playerController;
  public GameObject player;


  

  private void Start() {
      immunityCounter = SceneData.immunityCounter;
      playerController = player.GetComponent<PlayerCharacterController>();
  }

  private void Update() {    
    if(playerController.CharIsMoving()){
      if(Time.time>=nextUpdate){
        nextUpdate=Mathf.FloorToInt(Time.time)+1;
        checkEncouter();
      }
    }
  }
  
  
  private void checkEncouter()
  {   
      int value = Random.Range(0, 100);

      if (value < currentEncounterThreshold && !isImmune())
      {
        startEncounter();
      }
      else
      {
        currentEncounterThreshold += 1;
        if(immunityCounter > 0){
            immunityCounter--;
        } else {
            immunityCounter=0;
        }
      }
  }

  private bool isImmune(){
      return immunityCounter > 0;
  }

  void startEncounter(){
    //Play Encounter Animation
    //Screen Transition
    currentEncounterThreshold = DEFAULT_ENCOUNTER_THRESHOLD;
    SceneData.immunityCounter = 3;
    SceneData.playerPosition = player.transform.position;
    SceneManager.LoadScene("Combat");
    }
}
