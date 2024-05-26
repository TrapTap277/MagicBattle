using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts.Shooting
{
    public class ShootButtons : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Button _buttonToShootInEnemy;
        [SerializeField] private Button _buttonToShootInYou;

        private void Awake()
        {
            _buttonToShootInEnemy.onClick.AddListener(ShootInEnemy);
            _buttonToShootInYou.onClick.AddListener(ShootInYou);
        }

        private void ShootInEnemy()
        {
            IShoot enemy = new ShootInEnemy(_animator);
            enemy.Shoot();
        }

        private void ShootInYou()
        {
            IShoot player = new ShootInPlayer(_animator);
            player.Shoot();
        }
    }
}