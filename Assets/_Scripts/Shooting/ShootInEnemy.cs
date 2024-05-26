using UnityEngine;

namespace _Scripts.Shooting
{
    public class ShootInEnemy : IShoot
    {
        private Animator _animator;

        public ShootInEnemy(Animator animator)
        {
            _animator = animator;
        }
        
        public void Shoot()
        {
            _animator.CrossFade("Metalstaff01SlowSpin", 2);
            
            Debug.Log("Attack in Enemy");
        }
    }
}