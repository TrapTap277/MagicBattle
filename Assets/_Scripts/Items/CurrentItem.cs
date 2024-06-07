using _Scripts.Shooting;
using _Scripts.Staff;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Items
{
    public class CurrentItem : MonoBehaviour
    {
        private static GameObject _useButtonGameObject;
        private static Button _useButton;
        private BaseItem _item;
        private ShootInvoker _invoker;
        private ISetSecondMove _secondMove;
        private GameObject _frame;

        private void Awake()
        {
            _useButtonGameObject = GameObject.FindGameObjectWithTag("UseButton");
            _useButton = _useButtonGameObject.GetComponent<Button>();
            _invoker = FindObjectOfType<ShootInvoker>();
            gameObject.GetComponent<Button>().onClick.AddListener(() => InitAndShow(false));
            _frame = gameObject.GetComponentInChildren<SquareFrame>().gameObject;
        }

        private void Start()
        {
            SetSkin();
        }

        public void SetItem(BaseItem item)
        {
            _item = item;
        }

        public void DestroyItem()
        {
            Destroy(gameObject);
        }

        private void InitAndShow(bool isUsedByEnemy)
        {
            InitUseItem();            
            ShowFrame(true);
        }

        private void InitUseItem()
        {
            UseItem.Init(_useButton, _item, this);
            UseItem.DeInitButton();
            UseItem.InitButton();

            if (_item.Gem == Gem.SecondMove)
            {
                _secondMove = (SecondMoveGemItem) _item;
                _invoker.SetSecondMove(_secondMove.Get());
            }
        }
        
        private void ShowFrame(bool isShow)
        {
            var activator = new FramesActivator(_frame.gameObject);
            activator.ShowOrFadeFrame(isShow);
        }

        private void SetSkin()
        {
            gameObject.GetComponent<Image>().sprite = _item.Sprite;
        }
    }
}