using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;
    
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    
    public Text dialogueText;
    public float letterPause = 0.2f;
    
    private Unit _playerUnit;
    private Unit _enemyUnit;
    private string _currentMessage = "";

    private void Start() 
    {
        state = BattleState.START;
        dialogueText.text = _currentMessage;
        StartCoroutine(SetupBattle());
        
    }


    IEnumerator SetupBattle(){
        GameObject playerGameObject = Instantiate(playerPrefab,playerBattleStation);
        _playerUnit = playerGameObject.GetComponent<Unit>();
        GameObject enemyGameObject = Instantiate(enemyPrefab,enemyBattleStation);
        _enemyUnit = enemyGameObject.GetComponent<Unit>();
        playerHUD.ToggleMenu(false);
        playerHUD.ToggleAttackMenu(false);
        
        _currentMessage = $"A wild {_enemyUnit.unitName} approaches";
        StartCoroutine(TypeText(_currentMessage));



        playerHUD.SetupHUD(_playerUnit);
        enemyHUD.SetupHUD(_enemyUnit);

        playerHUD.attack1.text = _playerUnit.moveSet.getAttackByIndex(0).attackName;
        playerHUD.attack2.text = _playerUnit.moveSet.getAttackByIndex(1).attackName;
        playerHUD.attack3.text = _playerUnit.moveSet.getAttackByIndex(2).attackName;
        playerHUD.attack4.text = _playerUnit.moveSet.getAttackByIndex(3).attackName;

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;

        PlayerTurn();
    }

    /*
    Attack Process:
    First Check Hit
    Check Critical
    Optional: Check Enemy Status Changes
    Apply Damage


    */
    IEnumerator PlayerAttack(int attackIndex){
        playerHUD.ToggleMenu(false);
        playerHUD.ToggleAttackMenu(false);

        Attack currentAttack = _playerUnit.moveSet.getAttackByIndex(attackIndex);
        
        
        //Prüfung ob Direct oder Status Angriff
        //Wenn Status: Apply Effect && Push Message to Queue
        //Unterscheidung Target
        bool isDead = false;
        int critMultiplier = CheckCritMultiplier(currentAttack);
        if(CheckHit(currentAttack)){      
            isDead = _enemyUnit.TakeDamage(currentAttack.damage*critMultiplier);
            _currentMessage = critMultiplier == 2 ?  "CRITICAL HIT" : "Attack successful";
            enemyHUD.SetHp(_enemyUnit.currentHp);

        } else {
            _currentMessage = "You missed";
        }
        StartCoroutine(TypeText(_currentMessage));
        
        yield return new WaitForSeconds(2f);
        
        //ShowAllStatusUpdates
        
        

        if(isDead){
            state = BattleState.WON;
            EndBattle();
        }
        
        //OnAfterTurn
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    IEnumerator PlayerHeal(){
        int amount = Random.Range(1,6);
        _currentMessage = $"{_playerUnit.unitName} heals himself for {amount} points";;
        StartCoroutine(TypeText(_currentMessage));
        _playerUnit.HealSelf(amount);
        playerHUD.SetHp(_playerUnit.currentHp);
        
        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }


   private void PlayerTurn(){
        playerHUD.ToggleMenu(true);
        _currentMessage = "Choose an action:";
        StartCoroutine(TypeText(_currentMessage));
    }

    private IEnumerator EnemyTurn(){
        bool isDead = false;
        int attackIndex = Random.Range(0, 3);
        Attack currentAttack = _enemyUnit.moveSet.getAttackByIndex(attackIndex);


        int critMultiplier = CheckCritMultiplier(currentAttack);
        _currentMessage = $"{_enemyUnit.unitName} attacks!";
        StartCoroutine(TypeText(_currentMessage));

        yield return new WaitForSeconds(2f);

        if(CheckHit(currentAttack)){      
            isDead = _playerUnit.TakeDamage(currentAttack.damage*critMultiplier);
            _currentMessage = critMultiplier == 2 ?  "CRITICAL HIT" : $"{_enemyUnit.unitName} attacked successfully";
            StartCoroutine(TypeText(_currentMessage));
            playerHUD.SetHp(_playerUnit.currentHp);
        } else {
            _currentMessage = $"{_enemyUnit.unitName} missed";
            StartCoroutine(TypeText(_currentMessage));
        }
        
        yield return new WaitForSeconds(1f);

        if(isDead){
            state = BattleState.LOST;
            EndBattle();
        }
        
        //OnAfterTurn
        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    public void OnAttackButton(){
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        playerHUD.ToggleMenu(false);
        playerHUD.ToggleAttackMenu(true);

    }

    public void OnMoveButton(int index)
    {
        if (state != BattleState.PLAYERTURN) return;
        StartCoroutine(PlayerAttack(index));
    }

    public void OnHealButton(){
        if(state != BattleState.PLAYERTURN)return;
        StartCoroutine(PlayerHeal());
    }

    private void EndBattle()
    {
        dialogueText.text = state switch
        {
            BattleState.WON => "You won the battle!",
            BattleState.LOST => "You Lost. Go Cry",
            _ => dialogueText.text
        };

        SceneManager.LoadScene("Overworld");
    }

    private static bool CheckHit(Attack currentAttack)
    {
        return Random.Range(1,101) <= currentAttack.hitChance;
    }

    private static int CheckCritMultiplier(Attack currentAttack){
        return Random.Range(1,101) <= currentAttack.critChance ? 2 : 1;
    }

    private IEnumerator TypeText(string message)
    {
        dialogueText.text = string.Empty;
        foreach (char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterPause);
        }
    }
}
