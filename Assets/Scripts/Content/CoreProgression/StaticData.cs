using Assets.Scripts.Content.PlayerLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.Content.CoreProgression
{
    public static class StaticData
    {
        private static PlayerConfig _playerConfig;

        private static float _damage;
        private static PlayerWeapon _weapon;
        private static float _playerMaxHealth;
        private static float _playerHealPercent;
        private static float _playerCriticalChance;
        private static float _playerDodgeChance;
        private static float _criticalMultiplier;

        private static float _currentPlayerHP;

        public static PlayerWeapon Weapon => _weapon;
        public static float MaxPlayerHealth => _playerMaxHealth;
        public static float PlayerHealPercent => _playerHealPercent;
        public static float PlayerCriticalChance => _playerCriticalChance;
        public static float PlayerDodgeChance => _playerDodgeChance;
        public static float CriticalMultiplier => _criticalMultiplier;

        public static float CurrentPlayerHP { get => _currentPlayerHP; set => _currentPlayerHP = value; }
        public static float Damage => _damage;

        public static event Action OnMaxHealthChanged;

        public static void Initialize(PlayerConfig playerConfig)
        {
            _playerConfig = playerConfig;

            _weapon = _playerConfig.Weapon;
            _damage = _weapon.Damage;
            _playerMaxHealth = _playerConfig.MaxHealth;
            _playerHealPercent = _playerConfig.HealPercent;
            _playerCriticalChance = _playerConfig.CriticalChance;
            _playerDodgeChance = _playerConfig.DodgeChancePercent;
            _criticalMultiplier = _playerConfig.CriticalMultiplier;
        }

        public static void ExecuteProgress(ProgressValue progressValue, SuccessRate successRate)
        {
            switch (progressValue.ProgressType)
            {
                case ProgressType.MaxHealth:
                    _playerMaxHealth += progressValue.IncreaseValue;
                    OnMaxHealthChanged?.Invoke();
                    break;

                case ProgressType.Healing:
                    _playerHealPercent += progressValue.IncreaseValue;
                    break;

                case ProgressType.WeaponDamage:
                    _damage += progressValue.IncreaseValue;
                    break;

                case ProgressType.CriticalChance:
                    _criticalMultiplier += progressValue.IncreaseValue;
                    break;

                case ProgressType.DodgeChance:
                    _playerDodgeChance += progressValue.IncreaseValue;
                    break;

                default:
                    break;
            }

            //change enemy data
        }

        public static void DebugData()
        {
            Debug.Log($"_weaponDamage = {_weapon.Damage}");
            Debug.Log($"_playerMaxHealth = {_playerMaxHealth}");
            Debug.Log($"_playerHealPercent = {_playerHealPercent}");
            Debug.Log($"_playerCriticalChance = {_playerCriticalChance}");
            Debug.Log($"_playerDodgeChance = {_playerDodgeChance}");
            Debug.Log($"_criticalMultiplier = {_criticalMultiplier}");
            Debug.Log($"_currentPlayerHP = {_currentPlayerHP}");
        }
    }
}
