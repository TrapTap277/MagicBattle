using _Scripts.Health;
using UnityEngine;

namespace _Scripts.Particles
{
    public class ParticleDealDamage : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out HealthBase healthBase)) healthBase.TakeDamage(20);
        }
    }
}