using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class AddUseButton : MonoBehaviour, IInstantiate
    {
        [SerializeField] private Button _useButtonPrefab;
        [SerializeField] private Transform _useButtonParent;

        private bool _isDone;

        public void Instantiate()
        {
            if (_isDone == false) Instantiate(_useButtonPrefab, _useButtonParent);

            _isDone = true;
        }
    }
}