using System;
using _Scripts.Enemy;
using UnityEngine;

namespace _Scripts.Staff
{
    public class StaffChangePositions : MonoBehaviour, ISetPositions
    {
        private Vector3 _startPosition;
        private Quaternion _startRotation;

        private void Awake()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        public void SetPositions(MoveTurn positions)
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
        }
    }
}