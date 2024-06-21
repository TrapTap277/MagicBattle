using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Shooting;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.LostScene
{
    public abstract class BaseUseMagic : MonoBehaviour, IUseMagic, ISetGem
    {
        protected static readonly Dictionary<ShootIn, Transform> AbilitySpawnPositions =
            new Dictionary<ShootIn, Transform>();

        protected List<GameObject> Abilities = new List<GameObject>();
        protected GameObject CustomAbility;

        private static Gem _attack;
        private ShootIn _shootIn;
        private Transform _spawnPositions;

        private void Awake()
        {
            // ClearDictionary();
            Init();
            SetGem(Gem.None);
        }

        public async void Use()
        {
            if (_attack != Gem.None && _attack != Gem.TrueAttack) return;
            if (Abilities.Count <= 0) return;
            var newAbility = InstantiateRandomAbility();
            await Task.Delay(4000);
            var explosion = InitExplosion();
            DestroyEffects(newAbility, explosion);
        }

        public void SetGem(Gem gem)
        {
            _attack = gem;
        }

        private void SetShootIn(ShootIn shootIn)
        {
            _shootIn = shootIn;
        }

        private protected abstract void Init();

        private GameObject InstantiateRandomAbility()
        {
            if (Abilities.Count == 0) return null;
            var getRandomAbility = Random.Range(0, Abilities.Count);

            var newAbility = Instantiate(Abilities[getRandomAbility], GetAbilitySpawnPositions(_shootIn).position,
                GetAbilitySpawnPositions(_shootIn).rotation);
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

        private void InstantiateCustomAbility() // Using in Animation
        {
            if (CustomAbility == null) return;

            Instantiate(CustomAbility, GetAbilitySpawnPositions(_shootIn).position,
                Quaternion.identity);
        }

        private static Transform GetAbilitySpawnPositions(ShootIn shootIn)
        {
            return shootIn == ShootIn.NoOne ? AbilitySpawnPositions[ShootIn.Enemy] : AbilitySpawnPositions[shootIn];
        }

        private void RemoveUsedAbility(int getRandomAbility)
        {
            if (_attack != Gem.TrueAttack)
                Abilities.RemoveAt(getRandomAbility);
        }

        private static void ClearDictionary()
        {
            AbilitySpawnPositions.Clear();
        }
        
        private void OnEnable()
        {
            BaseShoot.OnSetShootIn += SetShootIn;
        }

        private void OnDisable()
        {
            BaseShoot.OnSetShootIn -= SetShootIn;
        }
    }
}