using System.Collections.Generic;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class EnemyUseMagic : BaseUseMagic
    {
        [SerializeField] private List<GameObject> _abilities = new List<GameObject>();
        [SerializeField] private GameObject _customAbility;
        [SerializeField] private Transform _staffPositions;
        [SerializeField] private Transform _scythePositions;

        private protected override void Init()
        {
            Abilities = _abilities;
            CustomAbility = _customAbility;

            AbilitySpawnPositions.Clear();

            AbilitySpawnPositions[ShootIn.Enemy] = _staffPositions;
            AbilitySpawnPositions[ShootIn.Player] = _scythePositions;
        }
    }
}