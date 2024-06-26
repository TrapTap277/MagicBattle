﻿using System.Threading.Tasks;
using _Scripts.AttackMoveStateMachine;
using _Scripts.Attacks;
using _Scripts.DialogueSystem;
using _Scripts.Health;
using _Scripts.Shooting;
using _Scripts.Staff;
using _Scripts.Stats;
using UnityEngine;

namespace _Scripts.StartGame
{
    public class GameStartManager : MonoBehaviour
    {
        [SerializeField] private int _countDialogues;

        private IGenerateMagicAttacks _generateMagicAttacks;
        private IEnableDisableManager _enableDisableManager;
        private ISwitchDialogue _switchDialogue;
        private IInit _initStaffGemChanger;
        private IInit _initShootInvoker;
        private HealthBase[] _initHealth;

        private void Awake()
        {
            InitInterfaces();
        }

        private void Start()
        {
            StartGame();
        }

        private async void StartGame()
        {
            await _switchDialogue?.SwitchDialogue(DialogueAnswerType.General, _countDialogues);
            _switchDialogue?.Fade();
            await Task.Delay(1000);
            ShowHealth();
            await Task.Delay(1200);
            _initStaffGemChanger?.Init();
            await Task.Delay(1000);
            _initShootInvoker?.Init();
            _enableDisableManager?.Show();
            await Task.Delay(500);
            _generateMagicAttacks?.GenerateMagicAttacks();
        }

        private void ShowHealth()
        {
            foreach (var health in _initHealth)
            {
                health.Init();
                health.Show();
            }
        }

        private void InitInterfaces()
        {
            _enableDisableManager = FindObjectOfType<AttackButtonsController>();
            _switchDialogue = FindObjectOfType<DialogueStorage>();
            _generateMagicAttacks = FindObjectOfType<MagicAttackStorage>();
            _initHealth = FindObjectsOfType<HealthBase>();
            _initShootInvoker = FindObjectOfType<ShootInvoker>();
            _initStaffGemChanger = FindObjectOfType<StaffGemChanger>();
        }
    }
}