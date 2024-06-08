using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.BoxWithItems;
using _Scripts.Die;
using _Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class MagicAttackStorage : MonoBehaviour
    {
        public static event Action OnCreatedUI;

        private readonly List<AttacksType> _typies = new List<AttacksType>();
        private readonly List<AttacksType> _usedAttacks = new List<AttacksType>();

        public int AttackCount { get; private set; }
        public int RedAttack { get; private set; }
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

            NoAttackOfType();

            OnCreatedUI?.Invoke();

            CreateBoxWithItems();
            PrintAttacks();
        }

        private void NoAttackOfType()
        {
            if (BlueAttack == 0)
            {
                _attacksType = AttacksType.Blue;
                AddAttacksCount();
                TypiesAddAttack();
            }

            if (RedAttack != 0) return;
            _attacksType = AttacksType.Red;
            AddAttacksCount();
            TypiesAddAttack();
        }

        private void TypiesAddAttack()
        {
            _typies.Add(_attacksType);
        }

        private void AddAttacksCount()
        {
            RedAttack++;
            AttackCount++;
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

                AttackCount++;
                TypiesAddAttack();
            }
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
            _stateMachine.SwitchState(_stateMachine.IdleState);
        }

        public AttacksType GetFirstType()
        {
            var type = _typies[0];

            StartCoroutine(RemoveAttackWithTime(type));

            return type;
        }

        private IEnumerator RemoveAttackWithTime(AttacksType type)
        {
            yield return new WaitForSeconds(0.5f);

            _usedAttacks.Add(type);
            _typies.RemoveAt(0);
            AttackCount--;

            if (_typies.Count <= 0) GenerateMagicAttacks();
        }

        private void PrintAttacks()
        {
            var attacksToDebug = _typies.Select(attacks => attacks == AttacksType.Red ? "R" : "B").Aggregate("", (current, attack) => current + attack);

            Debug.Log(attacksToDebug);
        }
    }

    public enum AttacksType
    {
        Red,
        Blue
    }
}