using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts.Die;
using _Scripts.Items;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class BoxWithItems : MonoBehaviour
    {
        public static event Action OnGeneratedBarriers;
        public static event Action<Transform> OnChangedCameraRotation;
        public static event Action OnChangedCameraRotationToDefault;


        private Transform _spawnPositionForPlayer;
        private Transform _spawnPositionForEnemy;
        private Transform _endPositions;
        private Transform _boxPositions;

        private Dictionary<string, Transform> _positions = new Dictionary<string, Transform>();

        private static readonly int Open = Animator.StringToHash("Open");

        private const string SpawnPositionsPlayer = "SpawnPositionsForPlayer";
        private const string SpawnPositionsEnemy = "SpawnPositionsEnemy";
        private const string EndPositions = "EndPositions";

        private int _itemsCount;

        private CreateItemsUI _createItemsUI;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _createItemsUI = FindObjectOfType<CreateItemsUI>();

            InitDictionary();

            MoveUp(EndPositions);
            SetItemCount();
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

        private async void OnMouseDown()
        {
            SetItemCount();

            RemoveBoxCollider();

            SetAnimation(true);
            _createItemsUI.CreateWithItemsCount(_itemsCount);

            await Task.Delay(2000);

            SetAnimation(false);

            await Task.Delay(2000);

            OnGeneratedBarriers?.Invoke();

            ExitFromBox();
        }

        private void SetItemCount()
        {
            _itemsCount = 2;

            if (DieCounter._enemyDieCount == 2 || DieCounter._playerDieCount == 2)
                _itemsCount = 4;
        }

        public void InitPositions(Transform spawnPositionsForPlayer, Transform spawnPositionsForEnemy,
            Transform endPositions)
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