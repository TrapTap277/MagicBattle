using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class BoxAnimationController : MonoBehaviour, IOpenCloseManager
    {
        private static readonly int OpenBox = Animator.StringToHash("Open");
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Open()
        {
            _animator.SetBool(OpenBox, true);
            RemoveBoxCollider();
        }

        public void Close()
        {
            _animator.SetBool(OpenBox, false);
        }
        
        private void RemoveBoxCollider()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}