using System;
using _Scripts.Enemy;
using UnityEngine;

namespace _Scripts.Staff
{
    public class ChangePositions : MonoBehaviour, ISetPositions
    {
        [SerializeField] private Transform _staffGemPositions;
        [SerializeField] private Transform _scytheGemPositions;

        private void Start()
        {
            SetPositions(MoveTurn.Player);
        }

        public void SetPositions(MoveTurn positions)
        {
            SetParent(positions);
            ResetSetTransform();
        }

        private void ResetSetTransform()
        {
            gameObject.transform.localPosition = Vector3.zero;
        }

        private void SetParent(MoveTurn positions)
        {
            switch (positions)
            {
                case MoveTurn.Player:
                    gameObject.transform.SetParent(_staffGemPositions);
                    break;
                case MoveTurn.Enemy:
                    gameObject.transform.SetParent(_scytheGemPositions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(positions), positions, null);
            }
        }
    }
}