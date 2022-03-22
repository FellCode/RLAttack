using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Combat
{
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

        private void Start() 
        {
            state = BattleState.START;
            dialogueText.text = string.Empty;
            StartCoroutine(SetupBattle());
        
        }


        private IEnumerator SetupBattle(){
            GameObject playerGameObject = Instantiate(playerPrefab,playerBattleStation);
            _playerUnit = playerGameObject.GetComponent<Unit>();
            _playerUnit.SetBattleHud(playerHUD);
            
            GameObject enemyGameObject = Instantiate(enemyPrefab,enemyBattleStation);
            _enemyUnit = enemyGameObject.GetComponent<Unit>();
            _enemyUnit.SetBattleHud(enemyHUD);
            
            playerHUD.ToggleMenu(false);
            playerHUD.ToggleAttackMenu(false);
            
            _playerUnit.battleHudReference.SetupHUD(_playerUnit);
            _enemyUnit.battleHudReference.SetupHUD(_enemyUnit);
            
            playerHUD.attack1.text = _playerUnit.moveSet.getMoveByIndex(0).name;
            //playerHUD.attack2.text = _playerUnit.moveSet.getMoveByIndex(1).name;
            //playerHUD.attack3.text = _playerUnit.moveSet.getMoveByIndex(2).name;
            //playerHUD.attack4.text = _playerUnit.moveSet.getMoveByIndex(3).name;
        
            var message = $"A wild {_enemyUnit.unitName} approaches";
            yield return TypeText(message);

            PlayerTurn();
        }
        
        private IEnumerator PlayerAttack(int attackIndex){
            playerHUD.ToggleMenu(false);
            playerHUD.ToggleAttackMenu(false);

            MoveBase playerMove = _playerUnit.moveSet.getMoveByIndex(attackIndex);

            yield return ApplyMove(playerMove, _playerUnit, _enemyUnit);

            StopAllCoroutines();
            StartCoroutine(EnemyTurn());

        }

        private IEnumerator ApplyMove(MoveBase move, Unit source, Unit target)
        {
            bool isDead = false;
            
            yield return TypeText($"{source.unitName} setzt {move.name} ein");

            if (move.Category() == Category.DIRECT)
            {
                Tuple<int, string> damageResult = CalculateDamage(move);
                isDead = target.TakeDamage(damageResult.Item1);
                target.battleHudReference.SetHp(target.currentHp);
                StartCoroutine(TypeText(damageResult.Item2));
            }
            
            if(move.Category() == Category.STATUS)
            {
                yield return RunMoveEffect(move, _playerUnit, _enemyUnit);
            }
            
            source.OnAfterTurn();
            yield return ShowAllStatusChanges(source);
            source.battleHudReference.SetHp(source.currentHp);

            if (!isDead) yield break;
        
            state = BattleState.WON;
            EndBattle();
        }

        private Tuple<int, string> CalculateDamage(MoveBase move)
        {
            if (!CheckHit(move)) return new Tuple<int, string>(0, "You missed") ;
            int critMultiplier = CheckCritMultiplier(move);
            var message = critMultiplier == 2 ? "CRITICAL HIT" : "Attack successful";
            int damage = move.Power()*critMultiplier;
            return new Tuple<int, string>(damage, message);
        }

        private IEnumerator RunMoveEffect(MoveBase move, Unit source, Unit target)
        {
            var effects = move.Effects();
            if (effects.Status != ConditionID.NONE)
            {
                target.SetCondition(effects.Status);
            }

            yield return ShowAllStatusChanges(source);
            yield return ShowAllStatusChanges(target);
        }

        private IEnumerator ShowAllStatusChanges(Unit unit)
        {
            while (unit.StatusUpdates.Count > 0)
            {
                var message = unit.StatusUpdates.Dequeue();
                yield return TypeText(message);
                
            }
        }

        private void PlayerTurn(){
            playerHUD.ToggleMenu(true);
            const string message = "Choose an action:";
            dialogueText.text = message;
        }

        private IEnumerator EnemyTurn()
        {
            MoveBase enemyMove = _enemyUnit.GetRandomMove();
            yield return ApplyMove(enemyMove, _enemyUnit, _playerUnit);
            PlayerTurn();
        }

        public void OnAttackButton(){
            playerHUD.ToggleMenu(false);
            playerHUD.ToggleAttackMenu(true);
        }

        public void OnMoveButton(int index)
        {
            StartCoroutine(PlayerAttack(index));
        }

        public void OnRunButton()
        {
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            StartCoroutine(TypeText("You ran away"));
            SceneManager.LoadScene("Overworld");
            yield break;
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

        private static bool CheckHit(MoveBase move)
        {
            return Random.Range(1,101) <= move.Accuracy();
        }

        private static int CheckCritMultiplier(MoveBase move)
        {
            return Random.Range(1,101) <= move.CritChance() ? 2 : 1;
        }

        private IEnumerator TypeText(string message)
        {
            dialogueText.text = string.Empty;
            foreach (char letter in message)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(letterPause);
            }
            
            yield return new WaitForSeconds(1f);
        }
    }
}