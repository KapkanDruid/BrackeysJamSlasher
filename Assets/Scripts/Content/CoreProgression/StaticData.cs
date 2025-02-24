using Assets.Scripts.Content.BasicAI;
using Assets.Scripts.Content.PlayerLogic;
using System;
using UnityEngine;

namespace Assets.Scripts.Content.CoreProgression
{
    public static class StaticData
    {
        private static PlayerConfig _playerConfig;
        private static CharacterConfig _cupcakeConfig;
        private static CharacterConfig _sausageConfig;
        private static CharacterConfig _breadConfig;
        private static CharacterConfig _meatConfig;

        private static float _damage;

        private static float _damageCupcake;
        private static float _damageSplashCupcake;
        private static float _damageRangeCupcake;
        private static float _cupcakeMaxHealth;

        private static float _damageSausage;
        private static float _damageSplashSausage;
        private static float _damageRangeSausage;
        private static float _sausageMaxHealth;
        
        private static float _damageBread;
        private static float _damageSplashBread;
        private static float _damageRangeBread;
        private static float _breadMaxHealth;
        
        private static float _damageMeat;
        private static float _damageSplashMeat;
        private static float _damageRangeMeat;
        private static float _meatMaxHealth;

        private static PlayerWeapon _weapon;
        private static float _playerMaxHealth;
        private static float _playerHealPercent;
        private static float _playerCriticalChance;
        private static float _playerDodgeChance;
        private static float _criticalMultiplier;

        private static float _currentPlayerHP;

        private static float _increase10 = 1.1f;
        private static float _increase25 = 1.25f;
        private static float _increase50 = 1.5f;

        public static PlayerWeapon Weapon => _weapon;
        public static float MaxPlayerHealth => _playerMaxHealth;
        public static float PlayerHealPercent => _playerHealPercent;
        public static float PlayerCriticalChance => _playerCriticalChance;
        public static float PlayerDodgeChance => _playerDodgeChance;
        public static float CriticalMultiplier => _criticalMultiplier;

        public static float CurrentPlayerHP { get => _currentPlayerHP; set => _currentPlayerHP = value; }
        public static float Damage => _damage;

        public static float MaxCupcakeHealth => _cupcakeMaxHealth;
        public static float DamageCupcake => _damageCupcake;
        public static float DamageSplashCupcake => _damageSplashCupcake;
        public static float DamageRangeCupcake => _damageRangeCupcake;

        public static float MaxSausageHealth => _sausageMaxHealth;
        public static float DamageSausage => _damageSausage;
        public static float DamageSplashSausage => _damageSplashSausage;
        public static float DamageRangeSausage => _damageRangeSausage;

        public static float MaxBreadHealth => _breadMaxHealth;
        public static float DamageBread => _damageBread;
        public static float DamageSplashBread => _damageSplashBread;
        public static float DamageRangeBread => _damageRangeBread;

        public static float MaxMeatHealth => _meatMaxHealth;
        public static float DamageMeat => _damageMeat;
        public static float DamageSplashMeat => _damageSplashMeat;
        public static float DamageRangeMeat => _damageRangeMeat;


        public static event Action OnMaxHealthChanged;

        public static void Initialize(PlayerConfig playerConfig,
                                      CharacterConfig cupcakeConfig,
                                      CharacterConfig sausageConfig,
                                      CharacterConfig breadConfig,
                                      CharacterConfig meatConfig)
        {
            _playerConfig = playerConfig;
            _cupcakeConfig = cupcakeConfig;
            _sausageConfig = sausageConfig;
            _breadConfig = breadConfig;
            _meatConfig = meatConfig;

            _weapon = _playerConfig.Weapon;
            _damage = _weapon.Damage;

            _damageCupcake = _cupcakeConfig.Damage;
            _damageSplashCupcake = _cupcakeConfig.SplashDamage;
            _damageRangeCupcake = _cupcakeConfig.RangeDamage;
            _cupcakeMaxHealth = _cupcakeConfig.Health;
            
            _damageSausage = _sausageConfig.Damage;
            _damageSplashSausage = _sausageConfig.SplashDamage;
            _damageRangeSausage = _sausageConfig.RangeDamage;
            _sausageMaxHealth = _sausageConfig.Health;
            
            _damageBread = _breadConfig.Damage;
            _damageSplashBread = _breadConfig.SplashDamage;
            _damageRangeBread = _breadConfig.RangeDamage;
            _breadMaxHealth = _breadConfig.Health;
            
            _damageMeat = _meatConfig.Damage;
            _damageSplashMeat = _meatConfig.SplashDamage;
            _damageRangeMeat = _meatConfig.RangeDamage;
            _meatMaxHealth = _meatConfig.Health;

            _playerMaxHealth = _playerConfig.MaxHealth;
            _playerHealPercent = _playerConfig.HealPercent;
            _playerCriticalChance = _playerConfig.CriticalChance;
            _playerDodgeChance = _playerConfig.DodgeChancePercent;
            _criticalMultiplier = _playerConfig.CriticalMultiplier;

            _currentPlayerHP = _playerMaxHealth;

            OnMaxHealthChanged?.Invoke();
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

            switch (successRate)
            {
                case SuccessRate.A:
                    _damageCupcake *= _increase50;
                    _damageSplashCupcake *= _increase50;
                    _damageRangeCupcake *= _increase50;
                    
                    _damageSausage *= _increase50;
                    _damageSplashSausage *= _increase50;
                    _damageRangeSausage *= _increase50;
                    
                    _damageBread *= _increase50;
                    _damageSplashBread *= _increase50;
                    _damageRangeBread *= _increase50;
                    
                    _damageMeat *= _increase50;
                    _damageSplashMeat *= _increase50;
                    _damageRangeMeat *= _increase50;
                    break;

                case SuccessRate.B:
                    _cupcakeMaxHealth *= _increase25;
                    
                    _sausageMaxHealth *= _increase25;
                    
                    _breadMaxHealth *= _increase25;
                    
                    _meatMaxHealth *= _increase25;
                    break;

                case SuccessRate.C:
                    _damageCupcake *= _increase25;
                    _damageSplashCupcake *= _increase25;
                    _damageRangeCupcake *= _increase25;

                    _cupcakeMaxHealth *= _increase10;
                    

                    _damageSausage *= _increase25;
                    _damageSplashSausage *= _increase25;
                    _damageRangeSausage *= _increase25;

                    _sausageMaxHealth *= _increase10;
                    

                    _damageBread *= _increase25;
                    _damageSplashBread *= _increase25;
                    _damageRangeBread *= _increase25;

                    _breadMaxHealth *= _increase10;
                    

                    _damageMeat *= _increase25;
                    _damageSplashMeat *= _increase25;
                    _damageRangeMeat *= _increase25;

                    _meatMaxHealth *= _increase10;
                    break;

                default:
                    break;
            }

            //change enemy data
        }

        public static void DebugData()
        {
            Debug.Log($"_cupcakeHealth = {_cupcakeMaxHealth}");
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
