using System;
using UnityEngine;

namespace _Scripts
{
    public class MagicAttackGenerator 
    {
        private readonly MagicAttackStorage _magicAttackStorage = new MagicAttackStorage();

        public void GenerateNewAttacks()
        {
            if (_magicAttackStorage.Typies.Count == 0)
            {
                _magicAttackStorage.GenerateMagicAttacks();
            }
        }
    }
}