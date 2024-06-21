using System.Collections.Generic;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Animations
{
    public class EnemyAnimationSwitcher : BaseAnimationSwitcher, ISwitchAnimation<EnemyAnimations>
    {
        private readonly List<EnemyAnimations> _randomAttack = new List<EnemyAnimations>();

        private const string Idle = "Idle";
        private const string Attack = "Attack";
        private const string Attack2 = "Attack2";
        private const string MagicAttack = "MagicAttack";
        private const string Death = "Death";

        private int _idleState;
        private int _attack;
        private int _attack2;
        private int _magicAttack;
        private int _death;

        private Dictionary<EnemyAnimations, int> _enemyAnimations = new Dictionary<EnemyAnimations, int>();
        private Animator _staffAnimator;

        private void Awake()
        {
            _staffAnimator = GetComponent<Animator>();

            Init(_staffAnimator);
        }

        public void SwitchAnimation(EnemyAnimations staffAnimations)
        {
            var value = GetStaffAnimation(staffAnimations);
            CrossFade(value);
        }

        public EnemyAnimations SetRandomAttackAnimation()
        {
            var randomIndex = Random.Range(0, _randomAttack.Count);
            return _randomAttack[randomIndex];
        }

        protected override void InitHashes()
        {
            _idleState = Animator.StringToHash(Idle);
            _attack = Animator.StringToHash(Attack);
            _attack2 = Animator.StringToHash(Attack2);
            _magicAttack = Animator.StringToHash(MagicAttack);
            _death = Animator.StringToHash(Death);
        }

        protected override void InitDictionary()
        {
            _enemyAnimations = new Dictionary<EnemyAnimations, int>
            {
                {EnemyAnimations.Idle, _idleState},
                {EnemyAnimations.FirstAttack, _attack},
                {EnemyAnimations.SecondAttack, _attack2},
                {EnemyAnimations.MagicAttack, _magicAttack},
                {EnemyAnimations.Death, _death}
            };
        }

        protected override void InitList()
        {
            _randomAttack.Add(EnemyAnimations.FirstAttack);
            _randomAttack.Add(EnemyAnimations.SecondAttack);
            _randomAttack.Add(EnemyAnimations.MagicAttack);
        }

        private int GetStaffAnimation(EnemyAnimations staffAnimations)
        {
            _enemyAnimations.TryGetValue(staffAnimations, out var value);
            return value;
        }
    }
}