using System;
using System.Threading.Tasks;
using _Scripts.Attacks;
using _Scripts.Die;
using _Scripts.EndGame;
using _Scripts.Items;
using _Scripts.LostScene;
using _Scripts.Staff;
using _Scripts.Stats;
using _Scripts.UI;
using UnityEngine;

namespace _Scripts.BoxWithItems
{
    public class BoxWithItems : MonoBehaviour
    {
        public static event Action<bool> OnBlocked;
        public static event Action OnGeneratedAttacks;
        private int _itemsCount;
        private CreateItemsUI _createItemsUI;
        private IOpenCloseManager _boxManager;
        private IMoveBox _moveBox;
        private IEnableDisableManager _magicAttacks;
        private IInstantiate _instantiateUseButton;
        private IStaffAnimationController _staffAnimationController;
        public void InitBox(IMoveBox moveBox)
        {
            _instantiateUseButton = FindObjectOfType<AddUseButton>();
            _createItemsUI = FindObjectOfType<CreateItemsUI>();
            _magicAttacks = FindObjectOfType<AttackShowAndFade>();
            SetItemCount();
            
            _moveBox = moveBox;
            _magicAttacks?.Fade();
            _staffAnimationController = FindObjectOfType<StaffSwitchAnimation>();

            SetItemCount();

            _moveBox = moveBox;
            _magicAttacks?.Fade();
            _staffAnimationController?.SwitchAnimation(StaffAnimations.DissolveStaff);
            OnBlocked?.Invoke(true);
        }

        private async void OnMouseDown()
        {
            SetItemCount();
            InitBoxManager();
            _instantiateUseButton?.Instantiate();
            _boxManager?.Open();
            _createItemsUI.CreateWithItemsCount(_itemsCount);
            await Task.Delay(1500);
            _boxManager?.Close();
            await Task.Delay(2000);
            _moveBox?.ExitFromBox(WhoWon.Player);
            await Task.Delay(6000);
            OnBlocked?.Invoke(false);
            OnGeneratedAttacks?.Invoke();
            _staffAnimationController?.SwitchAnimation(StaffAnimations.UnDissolveStaff);
            await Task.Delay(3000);
            DestroyBox();
        }

        private void DestroyBox()
        {
            if (gameObject != null)
                Destroy(gameObject);
        }

        private void InitBoxManager()
        {
            _boxManager = gameObject.GetComponent<BoxAnimationController>();
        }

        private void SetItemCount()
        {
            _itemsCount = 2;
            if (DieManager.EnemyDieCount == 2 || DieManager.PlayerDieCount == 2)
                _itemsCount = 4;
        }
    }
}