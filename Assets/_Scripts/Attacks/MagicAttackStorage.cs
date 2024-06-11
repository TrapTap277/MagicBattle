using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BoxWithItems;
using _Scripts.Die;
using _Scripts.Enemy;
using _Scripts.Shooting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Attacks
{
    public class MagicAttackStorage : MonoBehaviour
    {
        public static event Action OnCreatedUI;

        public List<AttacksType> _typies = new List<AttacksType>();
        private readonly List<AttacksType> _usedAttacks = new List<AttacksType>();

        public int AttackCount { get; private set; }
        private int RedAttack { get; set; }
        public int BlueAttack { get; private set; }

        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private CreateBox _createBox;
        private const int MINAttackCount = 2;
        private const int MaXttackCount = 7;

        private AttacksType _attacksType;

        private void Start()
        {
            GenerateMagicAttacks();
        }

        public void GenerateMagicAttacks()
        {
            ResetStats();
            EnemyEnterInIdleState();

            var randomAttackCount = SetAttackCount();

            AddAttacks(randomAttackCount);

            if(BlueAttack == 0 || RedAttack == 0)
                NoAttackOfType();

            OnCreatedUI?.Invoke();

            CreateBoxWithItems();
            PrintAttacks();
        }

        public AttacksType GetFirstType()
        {
            var type = _typies[0];

            return type;
        }

        private void AddAttacks(int randomAttackCount)
        {
            for (var i = 0; i < randomAttackCount; i++)
            {
                var randomAttack = Random.Range(0, 2);

                if (randomAttack == 0)
                {
                    _attacksType = AttacksType.Blue;
                    BlueAttack++;
                }

                else
                {
                    _attacksType = AttacksType.Red;
                    RedAttack++;
                }

                AddAttackType();
                AttackCount++;
            }
        }

        private void NoAttackOfType()
        {
            if (BlueAttack == 0)
            {
                _attacksType = AttacksType.Blue;
                BlueAttack++;
                AttackCount++;
                AddAttackType();
                Debug.Log(BlueAttack);
            }

            if (RedAttack != 0) return;
            _attacksType = AttacksType.Red;
            RedAttack++;
            AttackCount++;
            Debug.Log(BlueAttack);
            AddAttackType();
        }

        private void AddAttackType()
        {
            _typies.Add(_attacksType);
        }

        private static int SetAttackCount()
        {
            var randomAttackCount = Random.Range(MINAttackCount, MaXttackCount);
            return randomAttackCount;
        }

        private void ResetStats()
        {
            RedAttack = 0;
            BlueAttack = 0;
            AttackCount = 0;
            _typies.Clear();
            _usedAttacks.Clear();
        }

        private void CreateBoxWithItems()
        {
            if (DieCounter.IsSomeoneDied()) _createBox.CreateAndMove();
        }

        private void EnemyEnterInIdleState()
        {
            ResetSecondMove(_stateMachine);

            _stateMachine.SetMoveTurn(MoveTurn.Player);
            _stateMachine.SwitchState(_stateMachine.IdleState);
        }

        private static void ResetSecondMove(EnemyStateMachine stateMachine)
        {
            IEnemyStateSwitcher[] resetSecondMove =
            {
                new SwitchEnemyStateWithShootingInEnemy(stateMachine),
                new SwitchEnemyStateWithShootingInPlayer(stateMachine)
            };

            foreach (var reset in resetSecondMove) reset.ResetSecondMove();
        }

        public void RemoveAttack(AttacksType type)
        {
            _usedAttacks.Add(type);
            _typies.RemoveAt(0);

            DecreaseAttackCount(type);

            if (_typies.Count <= 0) GenerateMagicAttacks();
        }

        private void DecreaseAttackCount(AttacksType type)
        {
            AttackCount--;

            if (type == AttacksType.Blue)
                BlueAttack--;

            else
                RedAttack--;
        }

        private void PrintAttacks()
        {
            var attacksToDebug = _typies.Select(attacks => attacks == AttacksType.Red ? "R" : "B")
                .Aggregate("", (current, attack) => current + attack);

            Debug.Log(attacksToDebug);
        }
    }

    public enum AttacksType
    {
        Red,
        Blue
    }
}