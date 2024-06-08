using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Items;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class BoxWithItems : MonoBehaviour
    {
        public static event Action<Transform> OnChangedCameraRotation;
        public static event Action OnChangedCameraRotationToDefault;

        private Transform _spawnPositionForPlayer;
        private Transform _spawnPositionForEnemy;
        private Transform _endPositions;
        private Transform _boxPositions;

        private Dictionary<string, Transform> _positions;

        private const string SpawnPositionsPlayer = "SpawnPositionsForPlayer";
        private const string SpawnPositionsEnemy = "SpawnPositionsEnemy";
        private const string EndPositions = "EndPositions";

        private static readonly int Open = Animator.StringToHash("Open");

        private CreateItemsUI _createItemsUI;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _createItemsUI = FindObjectOfType<CreateItemsUI>();

            _positions = new Dictionary<string, Transform>
            {
                {SpawnPositionsPlayer, _spawnPositionForPlayer},
                {SpawnPositionsEnemy, _spawnPositionForEnemy},
                {EndPositions, _endPositions},
            };

            MoveUp(EndPositions);
        }

        private async void OnMouseDown()
        {
            RemoveBoxCollider();

            SetAnimation(true);

            //todo action give items

            _createItemsUI.Create(10);

            await Task.Delay(2000);

            SetAnimation(false);

            await Task.Delay(2000);

            ExitFromBox();
        }

        public void InitPositions(Transform spawnPositionsForPlayer, Transform spawnPositionsForEnemy, Transform endPositions)
        {
            _spawnPositionForPlayer = spawnPositionsForPlayer;
            _spawnPositionForEnemy = spawnPositionsForEnemy;
            _endPositions = endPositions;
        }
        
        private void MoveUp(string key)
        {
            MoveBoxWithTween.MoveBox(gameObject.transform, GetTransform(key));
            InvokeAction(OnChangedCameraRotation, GetTransform(EndPositions));
        }

        private void MoveDown(string key)
        {
            MoveBoxWithTween.MoveBox(gameObject.transform, GetTransform(key));
            DestroyBox();
            InvokeAction(OnChangedCameraRotationToDefault);
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

        private void DestroyBox()
        {
            Destroy(gameObject, 3f);
        }

        private void ExitFromBox()
        {
            var spawnPositions = new[] {SpawnPositionsPlayer, SpawnPositionsEnemy};

            foreach (var key in spawnPositions) MoveDown(key);
        }

        private void SetAnimation(bool isOpened)
        {
            _animator.SetBool(Open, isOpened);
        }

        private void RemoveBoxCollider()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}