using System;
using System.Threading.Tasks;
using _Scripts.Die;
using _Scripts.EndGame;
using _Scripts.Items;
using _Scripts.LostScene;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class BoxWithItems : MonoBehaviour
    {
        public static event Action OnGeneratedBarriers;

        private int _itemsCount;

        private CreateItemsUI _createItemsUI;
        private IOpenCloseManager _boxManager;
        private IMoveBox _moveBox;

        private void Start()
        {
            _createItemsUI = FindObjectOfType<CreateItemsUI>();

            SetItemCount();
        }

        public void InitBox(IMoveBox moveBox)
        {
            _moveBox = moveBox;
        }

        private async void OnMouseDown()
        {
            SetItemCount();

            InitBoxManager();

            _boxManager?.Open();
            _createItemsUI.CreateWithItemsCount(_itemsCount);

            await Task.Delay(2000);

            _boxManager?.Close();

            await Task.Delay(2000);

            OnGeneratedBarriers?.Invoke();

            _moveBox?.ExitFromBox(WhoWon.Player);
        }

        private void InitBoxManager()
        {
            _boxManager = gameObject.GetComponent<BoxAnimationController>();
        }

        private void SetItemCount()
        {
            _itemsCount = 2;

            if (DieCounter.EnemyDieCount == 2 || DieCounter.PlayerDieCount == 2)
                _itemsCount = 4;
        }
    }
}  