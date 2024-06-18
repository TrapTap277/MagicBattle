using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.EndGame;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class MoveBox : MonoBehaviour, IMoveBox
    {
        public static event Action<Transform> OnChangedCameraRotation;
        public static event Action OnChangedCameraRotationToDefault;

        private const string SpawnPositionsPlayer = "SpawnPositionsForPlayer";
        private const string SpawnPositionsEnemy = "SpawnPositionsEnemy";
        private const string EndPositions = "EndPositions";

        private Transform _spawnPositionForPlayer;
        private Transform _spawnPositionForEnemy;
        private Transform _endPositions;
        private Transform _boxPositions;

        private Dictionary<string, Transform> _positions = new Dictionary<string, Transform>();

        private void Start()
        {
            InitDictionary();

            MoveUp();
        }

        public void InitPositions(Transform spawnPositionsForPlayer, Transform spawnPositionsForEnemy,
            Transform endPositions)
        {
            _spawnPositionForPlayer = spawnPositionsForPlayer;
            _spawnPositionForEnemy = spawnPositionsForEnemy;
            _endPositions = endPositions;
        }

        public void ExitFromBox(WhoWon whoWon)
        {
            var spawnPositions = new[] {SpawnPositionsPlayer, SpawnPositionsEnemy};

            foreach (var key in spawnPositions) MoveDown(key, whoWon);
        }

        private async void MoveDown(string key, WhoWon whoWon)
        {
            if (gameObject == null || key == null || !gameObject.TryGetComponent(out MoveBox moveBox)) return;
            MoveBoxWithTween.MoveBox(gameObject.transform, moveBox.GetTransform(key));

            await Task.Delay(1000);

            if (whoWon == WhoWon.Player)
                InvokeAction(OnChangedCameraRotationToDefault);
        }

        private void MoveUp()
        {
            MoveBoxWithTween.MoveBox(gameObject.transform, GetTransform(EndPositions));
            InvokeAction(OnChangedCameraRotation, GetTransform(EndPositions));
        }

        private static void InvokeAction(Action<Transform> action, Transform transform = null)
        {
            action?.Invoke(transform);
        }

        private static void InvokeAction(Action action)
        {
            action?.Invoke();
        }

        private Transform GetTransform(string key)
        {
            return _positions.TryGetValue(key, out var transformValue) ? transformValue : null;
        }

        private void InitDictionary()
        {
            _positions = new Dictionary<string, Transform>
            {
                {SpawnPositionsPlayer, _spawnPositionForPlayer},
                {SpawnPositionsEnemy, _spawnPositionForEnemy},
                {EndPositions, _endPositions}
            };
        }

    }
}