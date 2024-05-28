using System;
using System.Collections.Generic;
using _Scripts.Enemy;
using UnityEngine;

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

        private void Start()
        {
            GenerateMagicAttacks();
        }

        public void GenerateMagicAttacks()
        {
            ResetStats();
            EnemyEnterInIdleState();
            
            int randomAttackCount = UnityEngine.Random.Range(2, 7);
            
            for (int i = 0; i < randomAttackCount; i++)
            {
                AttacksType type;

                int randomAttack = UnityEngine.Random.Range(0, 2);

                if (randomAttack == 0)
                {
                    type = AttacksType.Blue;
                    BlueAttack++;
                }

                else
                {
                    type = AttacksType.Red;
                    RedAttack++;
                }

                AttackCount++;
                Typies.Add(type);
            }
            
            OnCreatedUI?.Invoke();
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
            AttacksType type = Typies[0];
            UsedAttacks.Add(type);
            Typies.RemoveAt(0);
            AttackCount--;
            
            if(Typies.Count <= 0)
                GenerateMagicAttacks();
            
            return type;
        }
}

    public enum AttacksType
    {
        Red,
        Blue
    }
}