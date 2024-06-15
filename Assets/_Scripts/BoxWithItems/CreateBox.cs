using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class CreateBox : MonoBehaviour
    {
        [SerializeField] private GameObject _boxPrefab;
        [SerializeField] private Transform _spawnPositionForPlayer;
        [SerializeField] private Transform _spawnPositionForEnemy;
        [SerializeField] private Transform _endPositions;

        public void CreateAndMove()
        {
            var spawnPositionKeys = new[] {_spawnPositionForPlayer, _spawnPositionForEnemy};

            foreach (var key in spawnPositionKeys)
            {
                var box = InstantiateBox(key);
                box.InitPositions(_spawnPositionForEnemy, _spawnPositionForEnemy, _endPositions);
            }
        }

        private BoxWithItems InstantiateBox(Transform positions)
        {
            return Instantiate(_boxPrefab, positions.position, positions.rotation).GetComponent<BoxWithItems>();
        }
    }
}