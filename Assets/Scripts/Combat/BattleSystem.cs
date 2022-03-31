using System.Collections;
using UnityEngine;

namespace Combat
{
    public enum BattleState
    {
        WON,
        LOST
    }

    public class BattleSystem : MonoBehaviour
    {
        private static readonly string CHOOSE_MESSAGE = "Choose an action:";
        private static readonly string RUN_MESSAGE = "You ran away";
        private static readonly string CRIT_MESSAGE = "CRITICAL HIT!";
        private static readonly string HIT_MESSAGE = "Attack successful";
        private static readonly string MISSED_MESSAGE = "You missed";
        private static readonly string WON_MESSAGE = "You won!";
        private static readonly string LOST_MESSAGE = "You Lost.Go Cry!";
        public GameObject playerPrefab;
        public GameObject enemyPrefab;

        public Transform playerBattleStation;
        public Transform enemyBattleStation;

        public BattleState state;

        public BattleHUD playerHUD;
        public BattleHUD enemyHUD;

        public CombatDialogueManager dialogueText;
        private Unit _enemyUnit;

        private LevelLoaderScript _levelLoader;

        private Unit _playerUnit;

        private void Start()
        {
            StartCoroutine(SetupBattle());
            _levelLoader = GameObject.Find("LevelLoader").GetComponent<LevelLoaderScript>();
        }

        private IEnumerator SetupBattle()
        {
            var playerGameObject = Instantiate(playerPrefab, playerBattleStation);
            _playerUnit = playerGameObject.GetComponent<Unit>();
            _playerUnit.SetBattleHud(playerHUD);

            var enemyGameObject = Instantiate(enemyPrefab, enemyBattleStation);
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
            yield return dialogueText.TypeText(message);

            StartCoroutine(PlayerTurn());
        }

        private IEnumerator PlayerTurn()
        {
            yield return dialogueText.TypeText(CHOOSE_MESSAGE);
            playerHUD.ToggleMenu(true);
        }

        private IEnumerator PlayerAttack(int attackIndex)
        {
            playerHUD.ToggleMenu(false);
            playerHUD.ToggleAttackMenu(false);

            var playerMove = _playerUnit.moveSet.getMoveByIndex(attackIndex);

            yield return ApplyMove(playerMove, _playerUnit, _enemyUnit);

            StartCoroutine(EnemyTurn());
        }

        private IEnumerator ApplyMove(MoveBase move, Unit source, Unit target)
        {
            yield return dialogueText.TypeText($"{source.unitName} setzt {move.name} ein");

            if (move.Category() == Category.DIRECT) RunDamage(move, target);

            if (move.Category() == Category.STATUS) yield return RunMoveEffect(move, source, target);

            source.OnAfterTurn();
            yield return ShowAllStatusChanges(source);
            source.battleHudReference.SetHp(source.currentHp);
            CheckBattleOver();
        }

        private void RunDamage(MoveBase move, Unit target)
        {
            var damageResult = CalculateDamage(move);
            target.TakeDamage(damageResult.Damage);
            target.battleHudReference.SetHp(target.currentHp);
            StartCoroutine(dialogueText.TypeText(damageResult.AttackResultString));
        }

        private IEnumerator EnemyTurn()
        {
            var enemyMove = _enemyUnit.GetRandomMove();
            yield return ApplyMove(enemyMove, _enemyUnit, _playerUnit);
            StartCoroutine(PlayerTurn());
        }

        private void EndBattle()
        {
            var endText = DecideEndText();
            dialogueText.SetText(endText);
            StartCoroutine(_levelLoader.BackToOverworldScene());
        }

        private IEnumerator RunMoveEffect(MoveBase move, Unit source, Unit target)
        {
            var effects = move.Effects();
            if (effects.Status != ConditionID.NONE) target.SetCondition(effects.Status);

            yield return ShowAllStatusChanges(source);
            yield return ShowAllStatusChanges(target);
        }

        private IEnumerator ShowAllStatusChanges(Unit unit)
        {
            while (unit.StatusUpdates.Count > 0)
            {
                var message = unit.StatusUpdates.Dequeue();
                yield return dialogueText.TypeText(message);
            }
        }

        public void OnAttackButton()
        {
            playerHUD.ToggleMenu(false);
            playerHUD.ToggleAttackMenu(true);
        }

        public void OnMoveButton(int index)
        {
            StartCoroutine(PlayerAttack(index));
        }

        public void OnRunButton()
        {
            StartCoroutine(ApplyRun());
        }

        private IEnumerator ApplyRun()
        {
            yield return dialogueText.TypeText(RUN_MESSAGE);
            StartCoroutine(_levelLoader.BackToOverworldScene());
        }


        private static bool CheckHit(MoveBase move)
        {
            return Random.Range(1, 101) <= move.Accuracy();
        }

        private static int CheckCritMultiplier(MoveBase move)
        {
            return Random.Range(1, 101) <= move.CritChance() ? 2 : 1;
        }

        private static DamageResult CalculateDamage(MoveBase move)
        {
            if (!CheckHit(move)) return new DamageResult(0, MISSED_MESSAGE);
            var critMultiplier = CheckCritMultiplier(move);
            var message = critMultiplier == 2 ? CRIT_MESSAGE : HIT_MESSAGE;
            var damage = move.Power() * critMultiplier;
            return new DamageResult(damage, message);
        }

        private string DecideEndText()
        {
            return state == BattleState.WON ? WON_MESSAGE : LOST_MESSAGE;
        }

        private void CheckBattleOver()
        {
            if (_playerUnit.UnitIsDead())
            {
                state = BattleState.LOST;
                EndBattle();
            }

            if (_enemyUnit.UnitIsDead())
            {
                state = BattleState.WON;
                EndBattle();
            }
        }
    }


    internal class DamageResult
    {
        public DamageResult(int damage, string attackResultString)
        {
            Damage = damage;
            AttackResultString = attackResultString;
        }

        public string AttackResultString { get; set; }

        public int Damage { get; set; }
    }
}