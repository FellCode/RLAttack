using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState {START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{
    

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public BattleState state;

    Unit playerUnit;
    Unit enemyUnit;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Text dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }


    IEnumerator SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab,playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab,enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches";

        playerHUD.SetupHUD(playerUnit);
        enemyHUD.SetupHUD(enemyUnit);

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
    IEnumerator PlayerAttack(){
        bool isDead = false;
        int critMultiplier = checkCritMultiplier(playerUnit);
        string hitText;
        if(checkHit(95)){      
            isDead = enemyUnit.TakeDamage(playerUnit.damage*critMultiplier);
            hitText = critMultiplier == 2 ?  "CRITICAL HIT" : "Attack successful";
            dialogueText.text = hitText;
            enemyHUD.SetHP(enemyUnit.currentHP);
        } else {
            dialogueText.text = "You missed";
        }
        
        yield return new WaitForSeconds(2f);

        if(isDead){
            state = BattleState.WON;
            EndBattle();
        }
        else {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal(){
        int amount = Random.Range(1,6);
        dialogueText.text = playerUnit.unitName + " heals himself for " + amount + " points";
        playerUnit.HealSelf(amount);
        playerHUD.SetHP(playerUnit.currentHP);
        
        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }


    void PlayerTurn(){
        playerHUD.ToggleMenu(true);
        dialogueText.text = "Choose an action:";
    }

    IEnumerator EnemyTurn(){
        bool isDead = false;
        int critMultiplier = checkCritMultiplier(enemyUnit);
        string hitText;
        playerHUD.ToggleMenu(false);
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(2f);

        if(checkHit(80)){      
            isDead = playerUnit.TakeDamage(enemyUnit.damage*critMultiplier);
            hitText = critMultiplier == 2 ?  "CRITICAL HIT" : enemyUnit.unitName + " attacked successfully";
            dialogueText.text = hitText;
            playerHUD.SetHP(enemyUnit.currentHP);
        } else {
            dialogueText.text = enemyUnit.unitName + " missed";
        }
        
        yield return new WaitForSeconds(1f);

        if(isDead){
            state = BattleState.LOST;
            EndBattle();
        }  
        else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    public void OnAttackButton(){
        if(state != BattleState.PLAYERTURN){
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton(){
        if(state != BattleState.PLAYERTURN){
            return;
        }
        StartCoroutine(PlayerHeal());
    }

    public void EndBattle(){
        if(state == BattleState.WON){
            dialogueText.text = "You won the battle!";
        } else if(state == BattleState.LOST) {
            dialogueText.text = "You Lost. Go Cry";
        }

        SceneManager.LoadScene("Overworld");
    }

    bool checkHit(int hitChance){
        return Random.Range(1,101) <= hitChance;
    }

    int checkCritMultiplier(Unit unit){
        return Random.Range(1,101) <= unit.baseCritChance ? 2 : 1;
    }
    
}
