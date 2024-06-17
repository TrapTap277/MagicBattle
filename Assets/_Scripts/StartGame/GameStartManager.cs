﻿using System.Collections;
using _Scripts.AttackMoveStateMachine;
using _Scripts.Attacks;
using _Scripts.DialogueSystem;
using _Scripts.EndGame;
using _Scripts.Health;
using _Scripts.Shooting;
using _Scripts.Staff;
using _Scripts.Stats;
using UnityEngine;

namespace _Scripts.StartGame
{
    public class GameStartManager : MonoBehaviour
    {
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
            StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            _switchDialogue?.SwitchDialogue(WhoWon.NoOne);
            yield return new WaitForSeconds(4);
            _switchDialogue?.SwitchDialogue(WhoWon.NoOne);
            yield return new WaitForSeconds(4);
            _switchDialogue?.SwitchDialogue(WhoWon.NoOne);
            yield return new WaitForSeconds(6);
            
            foreach (var health in _initHealth)
            {
                health.Init();
                health.Show();
            }

            yield return new WaitForSeconds(3);
            _generateMagicAttacks?.GenerateMagicAttacks();
            yield return new WaitForSeconds(4);
            _initShootInvoker?.Init();
            yield return new WaitForSeconds(0.1f);
            _enableDisableManager?.Show();
            yield return new WaitForSeconds(1);
            _initStaffGemChanger?.Init();
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