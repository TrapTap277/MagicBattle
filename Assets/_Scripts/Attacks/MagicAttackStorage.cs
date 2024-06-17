using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Scripts.BoxWithItems;
using _Scripts.Die;
using _Scripts.Enemy;
using _Scripts.Shooting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.Attacks
{
    public class MagicAttackStorage : MonoBehaviour, IGenerateMagicAttacks
    {
        public static event Action<List<AttacksType>> OnCreatedUI;

        [HideInInspector] public List<AttacksType> Typies = new List<AttacksType>();

        public int AttackCount { get; private set; }
        private int RedAttack { get; set; }
        public int BlueAttack { get; private set; }

        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private CreateBox _createBox;

        private const int MINAttackCount = 2;
        private const int MaXAttackCount = 7;

        private AttacksType _attacksType;

        private bool _isSomeoneDied;

        private void GenerateMagicAttacksAfterReset()
        {
            _isSomeoneDied = true;
            GenerateMagicAttacks();
        }

        public void GenerateMagicAttacks()
        {
            if (DieCounter.IsGameEnded()) return;
            ResetStats();
            EnemyEnterInIdleState();

            var randomAttackCount = SetAttackCount();

            AddAttacks(randomAttackCount);

            if (BlueAttack == 0 || RedAttack == 0)
                NoAttackOfType();

            PrintAttacks();
            var attackList = ShuffleList();

            OnCreatedUI?.Invoke(attackList);
        }

        private List<AttacksType> ShuffleList()
        {
            var shuffledList = new List<AttacksType>(Typies); 
            var count = shuffledList.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var random = Random.Range(i, count);
                var temp = shuffledList[i];
                shuffledList[i] = shuffledList[random];
                shuffledList[random] = temp;
            }

            return shuffledList;
        }

        public AttacksType GetFirstType()
        {
            var type = Typies[0];

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
            }

            if (RedAttack != 0) return;
            _attacksType = AttacksType.Red;
            RedAttack++;
            AttackCount++;
            AddAttackType();
        }

        private void AddAttackType()
        {
            Typies.Add(_attacksType);
        }

        private static int SetAttackCount()
        {
            var randomAttackCount = Random.Range(MINAttackCount, MaXAttackCount);
            return randomAttackCount;
        }

        private void ResetStats()
        {
            RedAttack = 0;
            BlueAttack = 0;
            AttackCount = 0;
            Typies.Clear();
        }

        private void CreateBoxWithItems()
        {
            if (DieCounter.IsGameEnded()) return;
            _createBox.CreateAndMove();
        }

        private void EnemyEnterInIdleState()
        {
            //ResetSecondMove(_stateMachine);

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

        public async void RemoveAttack(AttacksType type)
        {
            Typies.RemoveAt(0);

            DecreaseAttackCount(type);

            if (Typies.Count <= 0 && _isSomeoneDied == false)
            {
                await Task.Delay(2000);
                GenerateMagicAttacks();
            }

            if (Typies.Count <= 0 && _isSomeoneDied)
            {
                await Task.Delay(2000);
                CreateBoxWithItems();
            }
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
            var attacksToDebug = Typies.Select(attacks => attacks == AttacksType.Red ? "R" : "B")
                .Aggregate("", (current, attack) => current + attack);

            Debug.Log(attacksToDebug);
        }

        private void OnEnable()
        {
            DieCounter.OnResetBarriers += CreateBoxWithItems;
            BoxWithItems.BoxWithItems.OnGeneratedBarriers += GenerateMagicAttacksAfterReset;
        }

        private void OnDisable()
        {
            DieCounter.OnResetBarriers -= CreateBoxWithItems;
            BoxWithItems.BoxWithItems.OnGeneratedBarriers -= GenerateMagicAttacksAfterReset;
        }
    }

    public enum AttacksType
    {
        Red,
        Blue
    }
}