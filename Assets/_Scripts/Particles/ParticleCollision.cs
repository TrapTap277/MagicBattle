using System;
using System.Threading.Tasks;
using _Scripts.Health;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.Particles
{
    public class ParticleCollision : MonoBehaviour
    {
        public static event Action OnResetedProperties;

        [SerializeField] private int _timeToDissolve;

        private async void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out HealthBase healthBase))
            {
                healthBase.TakeDamage(20);
                OnResetedProperties?.Invoke();
            }

            if (!other.gameObject.TryGetComponent(out DissolveDoor dissolveDoor)) return;
            await Task.Delay(_timeToDissolve);
            dissolveDoor.Dissolve();
        }
    }
}