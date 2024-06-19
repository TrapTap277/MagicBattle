using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Items
{
    public class CurrentItem : MonoBehaviour
    {
        private static GameObject _useButtonGameObject;
        private static Button _useButton;
        private BaseItem _item;
        private GameObject _frame;

        private void Awake()
        {
            _useButtonGameObject = GameObject.FindGameObjectWithTag("UseButton");
            _useButton = _useButtonGameObject.GetComponent<Button>();
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
            BaseUseItem playerUseItemBase = new PlayerUseItem();
            var playerInitAndDeinit = (IInitAndDeinitUseItem) playerUseItemBase;
            playerUseItemBase.Init(_item, this, _useButton);
            playerInitAndDeinit.Deinit();
            playerInitAndDeinit.Init();
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