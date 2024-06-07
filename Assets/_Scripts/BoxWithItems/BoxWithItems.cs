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
            _animator.SetBool(Open, true);
            
            //todo action give items
            
            _createItemsUI.Create(10);
            
            await Task.Delay(6000);
            
            _animator.SetBool(Open, false);

            await Task.Delay(2000);

            _createBox.ExitFromBox();
        }
    }
}  