﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _Scripts.Items;
using _Scripts.Staff;
using UnityEngine;

namespace _Scripts.Enemy
{
    public class EnemyUseItemState : EnemyBaseState
    {
        private static readonly List<BaseItem> _items = new List<BaseItem>();

        public override async void Enter(EnemyStateMachine enemyStateMachine)
        {
            //Animation 
            //await Task.Delay(2000);

            await Task.Delay(1000);
            ChooseItem(enemyStateMachine);
            await Task.Delay(1000);

            enemyStateMachine.SwitchState(enemyStateMachine.AttackState);
        }

        public override void Exit(EnemyStateMachine enemyStateMachine)
        {
            //Animation
        }

        private static void ChooseItem(EnemyStateMachine enemyStateMachine)
        {
            var percentToAttack = (float) enemyStateMachine.Storage.BlueAttack / enemyStateMachine.Storage.AttackCount *
                                  100;
            var randomNumber = Random.Range(0, 100);

            if (enemyStateMachine.EnemyHealth.Health <= 60 && GetItem(Gem.Heal) != null && !CheckForItem(enemyStateMachine, Gem.Heal))
            {
                InitAndRemove(enemyStateMachine, Gem.Heal);
                AddUsedItem(enemyStateMachine, Gem.Heal);
                // Animation 
            }

            if (randomNumber < percentToAttack && GetItem(Gem.Damage) != null && !CheckForItem(enemyStateMachine, Gem.Damage))
            {
                InitAndRemove(enemyStateMachine, Gem.Damage);
                AddUsedItem(enemyStateMachine, Gem.Damage);

                // Animation 
            }

            if (randomNumber > percentToAttack && GetItem(Gem.Protection) != null && !CheckForItem(enemyStateMachine, Gem.Protection))
            {
                InitAndRemove(enemyStateMachine, Gem.Protection);
                AddUsedItem(enemyStateMachine, Gem.Protection);

                // Animation 
            }

            if (enemyStateMachine.Storage.AttackCount >= 2 && GetItem(Gem.SecondMove) != null && !CheckForItem(enemyStateMachine, Gem.SecondMove))
            {
                InitAndRemove(enemyStateMachine, Gem.SecondMove);
                AddUsedItem(enemyStateMachine, Gem.SecondMove);

                // Animation 
            }
        }

        private static bool CheckForItem(EnemyStateMachine enemyStateMachine,Gem gem)
        {
            Debug.Log(enemyStateMachine.UsedItems.ContainsKey(gem));
            return enemyStateMachine.UsedItems.ContainsKey(gem);
        }

        private static void AddUsedItem(EnemyStateMachine enemyStateMachine, Gem gem)
        {
            enemyStateMachine.AddUsedItems(gem, GetItem(gem));
        }

        private static void InitAndRemove(EnemyStateMachine enemyStateMachine, Gem gem)
        {
            enemyStateMachine.UseItem.Init(GetItem(gem));
            RemoveItem(GetItem(gem));
        }

        public static void AddItems(BaseItem item)
        {
            if (item != null) _items.Add(item);
        }

        private static void RemoveItem(BaseItem item)
        {
            if (item != null) _items.Remove(item);
        }

        private static BaseItem GetItem(Gem gem)
        {
            return _items.FirstOrDefault(item => item.Gem == gem);
        }
    }
}