using UnityEngine;

namespace _Scripts.Shooting
{
    public class ShootInEnemy : IShoot
    {
        private MagicAttackStorage _attackStorage;
        private Animator _animator;

        public ShootInEnemy(Animator animator, MagicAttackStorage attackStorage)
        {
            _animator = animator;
            _attackStorage = attackStorage;
        }

        public void Shoot()
        {
            _animator.CrossFade("Metalstaff01SlowSpin", 2);

            if (_attackStorage.GetFirstType() == AttacksType.Blue)
            {
                Debug.Log("Attack in Enemy");
                //Create Spell
            }

            else
            {
                //Передать посох
            }

        }
    }
}