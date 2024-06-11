using UnityEngine.UI;

namespace _Scripts.Items
{
    public class EnemyUseItem : BaseUseItem
    {
        public override void Init(BaseItem item, CurrentItem currentItem = null, Button useButton = null)
        {
            base.Init(item);
            base.InitItemBoolProperties();
            
            base.Use(true);
        }
    }
}