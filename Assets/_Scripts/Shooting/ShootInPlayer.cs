using UnityEngine;

namespace _Scripts.Shooting
{
    public class ShootInPlayer : IShoot
    {
        private Animator _animator;
        
        public ShootInPlayer(Animator animator)
        {
            _animator = animator;
        }
        
        public void Shoot()
        {
            _animator.CrossFade("Metalstaff01FastSpin", 2);
            
            Debug.Log("Attack in player");
        }
    }
}