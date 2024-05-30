using System.Threading.Tasks;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class BoxWithItems : MonoBehaviour
    {
        private CreateBox _createBox;
        private Animator _animator;
        void Start()
        {
            _animator = GetComponent<Animator>();
            _createBox = FindObjectOfType<CreateBox>();
        }
        
        private async void OnMouseDown()
        {
            _animator.SetBool("Open", true);
            
            await Task.Delay(6000);
            
            _animator.SetBool("Open", false);

            await Task.Delay(2000);

            _createBox.ExitFromBox();
        }
    }
}  