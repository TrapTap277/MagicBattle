using System.Collections;
using _Scripts.EndGame;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class CreateBox : MonoBehaviour
    {
        [SerializeField] private GameObject _boxPrefab;
        [SerializeField] private Transform _spawnPositionForPlayer;
        [SerializeField] private Transform _spawnPositionForEnemy;
        [SerializeField] private Transform _endPositions;

        private IOpenCloseManager _boxManager;
        private IMoveBox _moveBox;

        public void CreateAndMove()
        {
            var spawnPositionKeys = new[] {_spawnPositionForPlayer, _spawnPositionForEnemy};

            foreach (var key in spawnPositionKeys)
            {
                var box = InstantiateBox(key);
                box.GetComponent<MoveBox>().InitPositions(_spawnPositionForEnemy, _spawnPositionForEnemy, _endPositions);

                if (key == _spawnPositionForEnemy)
                {
                    _boxManager = box.GetComponent<BoxAnimationController>();
                    _moveBox = box.GetComponent<MoveBox>();
                    StartCoroutine(EnemyOpensBox());
                }

                else
                {
                    box.InitBox(box.GetComponent<MoveBox>());
                }
            }
        }

        private BoxWithItems InstantiateBox(Transform positions)
        {
            return Instantiate(_boxPrefab, positions.position, positions.rotation).GetComponent<BoxWithItems>();
        }

        private IEnumerator EnemyOpensBox()
        {
            yield return new WaitForSeconds(3);
            _boxManager?.Open();
            yield return new WaitForSeconds(3);
            _boxManager?.Close();
            yield return new WaitForSeconds(2);
            _moveBox?.ExitFromBox(WhoWon.Enemy);
        }
    }
}