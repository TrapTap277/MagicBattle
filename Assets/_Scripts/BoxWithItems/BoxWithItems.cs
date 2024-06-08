using System.Threading.Tasks;
using _Scripts.Items;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class BoxWithItems : MonoBehaviour
    {
        private CreateItemsUI _createItemsUI;
        private CreateBox _createBox;
        private Animator _animator;

        private static readonly int Open = Animator.StringToHash("Open");

        void Start()
        {
            _animator = GetComponent<Animator>();
            _createBox = FindObjectOfType<CreateBox>();
            _createItemsUI = FindObjectOfType<CreateItemsUI>();
        }

        private async void OnMouseDown()
        {
            RemoveBoxCollider();
            
            SetAnimation(true);
            
            //todo action give items
            
            _createItemsUI.Create(10);
            
            await Task.Delay(6000);
            
            SetAnimation(false);

            await Task.Delay(2000);

            _createBox.ExitFromBox();
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