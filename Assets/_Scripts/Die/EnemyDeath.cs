using _Scripts.Enemy;
using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Die
{
    public class EnemyDeath : BaseDeath
    {
        [SerializeField] private MagicAttackStorage _attackStorage;
        [SerializeField] private EnemyStateMachine _stateMachine;
        [SerializeField] private GiveLive _giveLive;

        [SerializeField] private CanvasGroup _roundsCounter;

        private void Awake()
        {
            Init();
        }

        protected override void Init()
        {
            AttackStorage = _attackStorage;
            StateMachine = _stateMachine;
            GiveLive = _giveLive;
            RoundsCounter = _roundsCounter;
        }

        private void OnEnable()
        {
            EnemyHealth.OnDied += Death;
        }

        private void OnDisable()
        {
            EnemyHealth.OnDied -= Death;
        }
    }
}