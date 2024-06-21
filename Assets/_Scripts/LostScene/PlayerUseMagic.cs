using System.Collections.Generic;
using _Scripts.Shooting;
using UnityEngine;

namespace _Scripts.LostScene
{
    public class PlayerUseMagic : BaseUseMagic
    {
        [SerializeField] private List<GameObject> _abilities = new List<GameObject>();
        [SerializeField] private GameObject _openPortalAbility;
        [SerializeField] private Transform _staffPositions;
        [SerializeField] private Transform _scythePositions;

        private protected override void Init()
        {
            Abilities = _abilities;
            CustomAbility = _openPortalAbility;

            AbilitySpawnPositions.Clear();

            AbilitySpawnPositions[ShootIn.Enemy] = _staffPositions;
            AbilitySpawnPositions[ShootIn.Player] = _scythePositions;
        }
    }
}