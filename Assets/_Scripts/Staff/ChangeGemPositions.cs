using System;
using _Scripts.Enemy;
using UnityEngine;

namespace _Scripts.Staff
{
    public class ChangeGemPositions : MonoBehaviour, ISetPositions
    {
        [SerializeField] private Transform staffGemPositions;
        [SerializeField] private Transform scytheGemPositions;

        private void Start()
        {
            SetPositions(MoveTurn.Player);
        }

        public void SetPositions(MoveTurn positions)
        {
            SetParent(positions);
            ResetTransform();
        }

        private void ResetTransform()
        {
            gameObject.transform.localPosition = Vector3.zero;
        }

        private void SetParent(MoveTurn positions)
        {
            switch (positions)
            {
                case MoveTurn.Player:
                    gameObject.transform.SetParent(staffGemPositions);
                    break;
                case MoveTurn.Enemy:
                    gameObject.transform.SetParent(scytheGemPositions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(positions), positions, null);
            }
        }
    }
}