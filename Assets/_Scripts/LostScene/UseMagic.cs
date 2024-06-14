using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class UseMagic : MonoBehaviour, IUseMagic, ISetStaffPositions, ISetGem
    {
        [SerializeField] private List<GameObject> _abilities = new List<GameObject>();
        [SerializeField] private GameObject _openPortalAbility;
        [SerializeField] private Transform _staffPositions;

        private Gem _attack;

        private void Awake()
        {
            SetGem(Gem.None);
        }

        public async void Use()
        {
            if (_attack != Gem.None && _attack != Gem.TrueAttack) return;
            if (_abilities.Count <= 0) return;
            var newAbility = InstantiateRandomAbility();
            await Task.Delay(4000);
            var explosion = InitExplosion();
            DestroyEffects(newAbility, explosion);
        }

        public void SetGem(Gem gem)
        {
            _attack = gem;
        }

        public void SetPositions(Transform positions)
        {
            _staffPositions = positions;
        }

        private GameObject InstantiateRandomAbility()
        {
            if (_abilities.Count == 0) return null;
            var getRandomAbility = Random.Range(0, _abilities.Count);

            var newAbility = Instantiate(_abilities[getRandomAbility], _staffPositions.transform.position,
                _staffPositions.rotation);
            RemoveUsedAbility(getRandomAbility);
            return newAbility;
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

        private void InstantiateOpenPortal() // Using in Animation(Open Portal)
        {
            if (_openPortalAbility == null) return;

            Instantiate(_openPortalAbility, _staffPositions.transform.position,
                Quaternion.identity);
        }

        private void RemoveUsedAbility(int getRandomAbility)
        {
            if(_attack != Gem.TrueAttack)
                _abilities.RemoveAt(getRandomAbility);
        }
    }
}