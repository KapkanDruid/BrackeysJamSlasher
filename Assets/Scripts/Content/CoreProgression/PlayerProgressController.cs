using Assets.Scripts.Content.PlayerLogic;
using UnityEngine;

namespace Assets.Scripts.Content.CoreProgression
{
    public class PlayerProgressController
    {
        private ProgressCardsPopup _cardsPopup;
        private SceneResources _sceneResources;

        public PlayerProgressController(ProgressCardsPopup cardsPopup, SceneResources sceneResources)
        {
            _cardsPopup = cardsPopup;
            _sceneResources = sceneResources;
        }

        private SuccessRate _successValue;

        public void ShowProgressCards(ProgressCardsConfig progressCardsConfig, float combatTime)
        {
            DetermineSuccessRate(combatTime);
            _cardsPopup.Show(_successValue, progressCardsConfig);
        }

        private void DetermineSuccessRate(float time)
        {
            if (time >= 0 && time < _sceneResources.MaxATime)
                _successValue = SuccessRate.A;
            else if (time >= _sceneResources.MaxATime && time < _sceneResources.MaxBTime)
                _successValue = SuccessRate.B;
            else if (time >= _sceneResources.MaxBTime)
                _successValue = SuccessRate.C;
            else
                Debug.LogError("Failed to DetermineSuccessRate");
        }

        public void DetermineProgress(ProgressValue progressValue)
        {
            //StaticData.ExecuteProgress(progressValue);
            Debug.Log("Progress " + progressValue.IncreaseValue + " Type " + progressValue.ProgressType);
        }

    }

    public static class StaticData
    {
        private static PlayerConfig _playerConfig;
        private static PlayerData _playerData;

        private static int _weaponLevel = 1;
        private static float _weaponDamage = _playerData.CurrentPlayerWeapon.Damage;
        private static float _playerMaxHealth = _playerConfig.MaxHealth;
        private static float _playerHealPercent = _playerConfig.HealPercent;
        private static float _playerCriticalChance = _playerConfig.CriticalChance;
        private static float _playerDodgeChance = _playerConfig.DodgeChancePercent;
        private static float _criticalMultiplier = _playerConfig.CriticalMultiplier;

        public static int WeaponLevel => _weaponLevel;
        public static float MaxPlayerHealth => _playerMaxHealth;
        public static float PlayerHealPercent => _playerHealPercent;
        public static float PlayerCriticalChance => _playerCriticalChance;
        public static float PlayerDodgeChance => _playerDodgeChance;
        public static float CriticalMultiplier => _criticalMultiplier;

        public static void ExecuteProgress(ProgressValue progressValue)
        {
            switch (progressValue.ProgressType)
            {
                case ProgressType.MaxHealth:
                    _playerMaxHealth += progressValue.IncreaseValue;
                    break;

                case ProgressType.Healing:
                    _playerHealPercent += progressValue.IncreaseValue;
                    break;

                case ProgressType.WeaponDamage:

                    break;

                case ProgressType.CriticalChance:

                    break;

                case ProgressType.DodgeChance:

                    break;

                default:
                    break;
            }
        }
    }
}
