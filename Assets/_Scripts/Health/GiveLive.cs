using UnityEngine;

namespace _Scripts.Health
{
    public class GiveLive : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private PlayerHealth _playerHealth;
        
        public void RestoreHealth(float health)
        {
            _enemyHealth.RestoreHealth(health);
            _playerHealth.RestoreHealth(health);
        }
    }
}