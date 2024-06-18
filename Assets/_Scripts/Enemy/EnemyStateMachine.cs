using System.Collections.Generic;
using _Scripts.AttackMoveStateMachine;
using _Scripts.Attacks;
using _Scripts.Die;
using _Scripts.Health;
using _Scripts.Items;
using _Scripts.Shooting;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyStateMachine : MonoBehaviour
    {
        #region States

        public readonly EnemyIdleState IdleState = new EnemyIdleState();
        public readonly EnemyAttackState AttackState = new EnemyAttackState();
        public readonly EnemyUseItemState UseItemState = new EnemyUseItemState();
        private EnemyBaseState _enemyCurrentState;

        #endregion

        public MoveTurn MoveTurn { get; private set; }
        public readonly BaseUseItem UseItem = new EnemyUseItem();
        public EnemyHealth EnemyHealth;
        public MoveTransition MoveTransition;
        public ShootInvoker ShotInvoker;
        public MagicAttackStorage Storage;

        public readonly Dictionary<Gem, BaseItem> UsedItems = new Dictionary<Gem, BaseItem>();

        [HideInInspector] public float PercentToAttackInPlayer;
        [HideInInspector] public float RandomNumber;

        [HideInInspector] public bool IsStopped;
        [HideInInspector] public bool IsDied;

        private void Awake()
        {
            _enemyCurrentState = IdleState;
            MoveTurn = MoveTurn.Player;
            _enemyCurrentState.Enter(this);
        }

        public void SwitchState(EnemyBaseState state)
        {
            if (IsStopped && state != IdleState && MoveTurn != MoveTurn.Player) return;
            _enemyCurrentState.Exit(this);
            _enemyCurrentState = state;
            _enemyCurrentState.Enter(this);
        }

        public void AddUsedItems(Gem gem, BaseItem item)
        {
            if (!UsedItems.ContainsKey(gem))
                UsedItems.Add(gem, item);
        }

        public void SetMoveTurn(MoveTurn turn)
        {
            MoveTurn = turn;
        }

        public void CalculatePercent()
        {
            PercentToAttackInPlayer = (float) Storage.BlueAttack / Storage.AttackCount *
                                      100;
            RandomNumber = Random.Range(0, 100);
        }

        private void ChangeIsDied(bool isDied)
        {
            IsDied = isDied;
        } 

        private void Stop()
        {
            IsStopped = true;
        }       
        
        private void StopIsStopped()
        {
            IsStopped = false;
        }

        private void OnEnable()
        {
            DieManager.OnBlockedTransition += Stop;
            ShootInvoker.OnStoppedIsStopped += StopIsStopped;
            HealthBase.OnDied += ChangeIsDied;
        }

        private void OnDisable()
        {
            DieManager.OnBlockedTransition -= Stop;
            ShootInvoker.OnStoppedIsStopped -= StopIsStopped;
            HealthBase.OnDied -= ChangeIsDied;
        }
    }
}