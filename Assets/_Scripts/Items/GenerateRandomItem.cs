using System.Collections.Generic;
using System.Linq;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.Items
{
    public class GenerateRandomItem : MonoBehaviour
    {
        public static int ItemsCount { get; private set; }

        [SerializeField] private HealGemItem _healGemItem;
        [SerializeField] private DamageGemItem _damageGemItem;
        [SerializeField] private ProtectionGemItem _protectionGemItem;
        [SerializeField] private SecondMoveGemItem _secondMoveGemItem;

        private static readonly List<BaseItem> _itemsList = new List<BaseItem>();
        private static Dictionary<Gem, BaseItem> _itemsDictionary;

        private void Awake()
        {
            _itemsDictionary = new Dictionary<Gem, BaseItem>
            {
                {Gem.Heal, _healGemItem},
                {Gem.Damage, _damageGemItem},
                {Gem.Protection, _protectionGemItem},
                {Gem.SecondMove, _secondMoveGemItem}
            };
        }

        public static BaseItem Generate()
        {
            var randomGem = GetRandomGem();
            var randomItem = CreateRandomItem(randomGem);
            AddItem(randomItem);

            return randomItem;
        }

        public static void RemoveItem(BaseItem item)
        {
            _itemsList.Remove(item);

            ItemsCount--;
        }

        // public static BaseItem GetItem(Gem gem)
        // {
        //     return _itemsList.FirstOrDefault(item => item.Gem == gem);
        // }

        private static BaseItem CreateRandomItem(Gem gem)
        {
            _itemsDictionary.TryGetValue(gem, out var item);
            return item;
        }

        private static Gem GetRandomGem()
        {
            return (Gem) Random.Range(3, 7);
        }

        private static void AddItem(BaseItem item)
        {
            _itemsList.Add(item);

            ItemsCount++;
        }
    }
}