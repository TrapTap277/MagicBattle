using _Scripts.Health;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Particles
{
    public class ParticleCollision : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out HealthBase healthBase)) healthBase.TakeDamage(20);
            if (other.gameObject.TryGetComponent(out DissolveDoor dissolveDoor)) dissolveDoor.Dissolve();
        }
    }
}