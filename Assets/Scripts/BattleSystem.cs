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

    public float letterPause = 0.2f;

    private string currentMessage = "";

    // Start is called before the first frame update
    void Start() 
    {
        state = BattleState.START;
        dialogueText.text = currentMessage;
        StartCoroutine(SetupBattle());
        
    }


    IEnumerator SetupBattle(){
        GameObject playerGO = Instantiate(playerPrefab,playerBattleStation);
        playerUnit = playerGO.GetComponent<Unit>();
        GameObject enemyGO = Instantiate(enemyPrefab,enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();
        playerHUD.ToggleMenu(false);

        currentMessage= "A wild " + enemyUnit.unitName + " approaches";
        StartCoroutine(TypeText(currentMessage));

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
        playerHUD.ToggleMenu(false);
        bool isDead = false;
        int critMultiplier = CheckCritMultiplier(playerUnit);
        if(CheckHit(playerUnit.baseHitChance)){      
            isDead = enemyUnit.TakeDamage(playerUnit.damage*critMultiplier);
            currentMessage = critMultiplier == 2 ?  "CRITICAL HIT" : "Attack successful";
            enemyHUD.SetHP(enemyUnit.currentHP);
        } else {
            currentMessage = "You missed";
        }
        StartCoroutine(TypeText(currentMessage));
        
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
        currentMessage = playerUnit.unitName + " heals himself for " + amount + " points";
        StartCoroutine(TypeText(currentMessage));
        playerUnit.HealSelf(amount);
        playerHUD.SetHP(playerUnit.currentHP);
        
        yield return new WaitForSeconds(1f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }


    void PlayerTurn(){
        playerHUD.ToggleMenu(true);
        currentMessage = "Choose an action:";
        StartCoroutine(TypeText(currentMessage));
    }

    IEnumerator EnemyTurn(){
        bool isDead = false;
        int critMultiplier = CheckCritMultiplier(enemyUnit);
        currentMessage = enemyUnit.unitName + " attacks!";
        StartCoroutine(TypeText(currentMessage));

        yield return new WaitForSeconds(2f);

        if(CheckHit(enemyUnit.baseHitChance)){      
            isDead = playerUnit.TakeDamage(enemyUnit.damage*critMultiplier);
            currentMessage = critMultiplier == 2 ?  "CRITICAL HIT" : enemyUnit.unitName + " attacked successfully";
            StartCoroutine(TypeText(currentMessage));
            playerHUD.SetHP(playerUnit.currentHP);
        } else {
            currentMessage = enemyUnit.unitName + " missed";
            StartCoroutine(TypeText(currentMessage));
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

    bool CheckHit(int hitChance){
        return Random.Range(1,101) <= hitChance;
    }

    int CheckCritMultiplier(Unit unit){
        return Random.Range(1,101) <= unit.baseCritChance ? 2 : 1;
    }

    IEnumerator TypeText (string message) {
        Debug.Log(message);
         dialogueText.text = "";
         foreach (char letter in message.ToCharArray()) {
             dialogueText.text += letter;
             yield return new WaitForSeconds (letterPause);
         }
     }
    
}
