using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts
{
    public class MagicAttackStorage : MonoBehaviour
    {
        public readonly List<AttacksType> Typies = new List<AttacksType>();
        public readonly List<AttacksType> UsedAttacks = new List<AttacksType>();

        private const int MaximumAttacksCount = 10;
        private int _currentAttacksCount;

        private void Start()
        {
            GenerateMagicAttacks();
        }

        public void GenerateMagicAttacks()
        {
            for (int i = 0; i < MaximumAttacksCount; i++)
            {
                AttacksType type;

                int randomAttack = UnityEngine.Random.Range(0, 2);

                if (randomAttack == 0)
                    type = AttacksType.Blue;

                else
                {
                    type = AttacksType.Red;
                }

                Typies.Add(type);
            }
        }

        public AttacksType GetFirstType()
        {
            AttacksType type = Typies[0];
            UsedAttacks.Add(type);
            Typies.RemoveAt(0);
            return type;
        }
}

    public enum AttacksType
    {
        Red,
        Blue
    }
}