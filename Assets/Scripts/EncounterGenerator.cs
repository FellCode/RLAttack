using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EncounterGenerator : MonoBehaviour
{
  const int DefaultEncounterThreshold = 10;

  private int _currentEncounterThreshold = DefaultEncounterThreshold;
  private int _immunityCounter;
 
  private int _nextUpdate=1;

  private PlayerCharacterController _playerController;
  public GameObject player;


  

  private void Start() {
      _immunityCounter = SceneData.ImmunityCounter;
      _playerController = player.GetComponent<PlayerCharacterController>();
  }

  private void Update()
  {
      if (!_playerController.CharIsMoving()) return;
      if (!(Time.time >= _nextUpdate)) return;
      
      _nextUpdate=Mathf.FloorToInt(Time.time)+1;
      CheckEncounter();
  }
  
  
  private void CheckEncounter()
  {   
      var value = Random.Range(0, 100);

      if (value < _currentEncounterThreshold && !IsImmune())
      {
        StartEncounter();
      }
      else
      {
        _currentEncounterThreshold += 1;
        if(_immunityCounter > 0){
            _immunityCounter--;
        } else {
            _immunityCounter=0;
        }
      }
  }

  private bool IsImmune(){
      return _immunityCounter > 0;
  }

  void StartEncounter(){
    //Play Encounter Animation
    //Screen Transition
    _currentEncounterThreshold = DefaultEncounterThreshold;
    SceneData.ImmunityCounter = 3;
    SceneData.PlayerPosition = player.transform.position;
    SceneManager.LoadScene("Combat");
    }
}
