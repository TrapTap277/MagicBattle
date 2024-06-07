﻿using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Enemy;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts
{
    public class MagicAttackStorage : MonoBehaviour
    {
        public static event Action OnCreatedUI;

        public readonly List<AttacksType> Typies = new List<AttacksType>();
        public readonly List<AttacksType> UsedAttacks = new List<AttacksType>();

        public int AttackCount { get; private set; }
        public int RedAttack { get; private set; }
        public int BlueAttack { get; private set; }

        [SerializeField] private EnemyStateMachine _stateMachine;

        private AttacksType _attacksType;

        private void Start()
        {
            GenerateMagicAttacks();
        }

        public void GenerateMagicAttacks()
        {
            ResetStats();
            EnemyEnterInIdleState();

            var randomAttackCount = Random.Range(2, 7);

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
                Typies.Add(_attacksType);
            }

            if (BlueAttack == 0)
            {
                _attacksType = AttacksType.Blue;
                BlueAttack++;
                AttackCount++;
                Typies.Add(_attacksType);
            }

            if (RedAttack == 0)
            {
                _attacksType = AttacksType.Red;
                RedAttack++;
                AttackCount++;
                Typies.Add(_attacksType);
            }

            OnCreatedUI?.Invoke();

            PrintAttacks();
        }

        private void ResetStats()
        {
            RedAttack = 0;
            BlueAttack = 0;
            AttackCount = 0;
            Typies.Clear();
            UsedAttacks.Clear();
        }

        private void EnemyEnterInIdleState()
        {
            _stateMachine.SwitchState(_stateMachine.IdleState);
        }

        public AttacksType GetFirstType()
        {
            var type = Typies[0];

            StartCoroutine(RemoveAttackWithTime(type));

            return type;
        }

        private IEnumerator RemoveAttackWithTime(AttacksType type)
        {
            yield return new WaitForSeconds(0.5f);

            UsedAttacks.Add(type);
            Typies.RemoveAt(0);
            AttackCount--;

            if (Typies.Count <= 0) GenerateMagicAttacks();
        }

        private void PrintAttacks()
        {
            var attacksToDebug = "";

            foreach (var attacks in Typies)
            {
                var attack = attacks == AttacksType.Red ? "R" : "B";
                attacksToDebug += attack;
            }

            Debug.Log(attacksToDebug);
        }
    }

    public enum AttacksType
    {
        Red,
        Blue
    }
}