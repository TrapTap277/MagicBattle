using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class UseMagic : MonoBehaviour, IUseMagic
    {
        [SerializeField] private List<GameObject> _abilities = new List<GameObject>();
        [SerializeField] private GameObject _openPortalAbility;
        [SerializeField] private Transform _staffPositions;

        public async void Use()
        {
            if (_abilities.Count <= 0) return;
            var newAbility = InstantiateRandomAbility();
            await Task.Delay(4000);
            var explosion = InitExplosion();
            DestroyEffects(newAbility, explosion);
        }

        public void CreateOpenPortal()
        {
            InstantiateOpenPortal();
        }

        private GameObject InstantiateRandomAbility()
        {
            if (_abilities.Count == 0) return null;
            var getRandomAbility = Random.Range(0, _abilities.Count);
            
            var newAbility = Instantiate(_abilities[getRandomAbility], _staffPositions.transform.position,
                Quaternion.identity);
            RemoveUsedAbility(getRandomAbility);
            Debug.LogError(newAbility.name);
            return newAbility;
        }

        private void InstantiateOpenPortal()
        {
            if(_openPortalAbility == null) return;
            
            Instantiate(_openPortalAbility, _staffPositions.transform.position,
                Quaternion.identity);
        }

        private void RemoveUsedAbility(int getRandomAbility)
        {
            _abilities.RemoveAt(getRandomAbility);
        }

        private static EffectExplosion InitExplosion()
        {
            var explosion = FindObjectOfType<EffectExplosion>();
            return explosion;
        }

        private static void DestroyEffects(GameObject newAbility, EffectExplosion explosion)
        {
            if (newAbility != null) Destroy(newAbility);
            if (explosion != null) Destroy(explosion);
        }
    }
}