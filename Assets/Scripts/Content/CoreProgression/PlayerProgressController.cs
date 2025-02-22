using Assets.Scripts.Architecture;
using UnityEngine;

namespace Assets.Scripts.Content.CoreProgression
{
    public class PlayerProgressController
    {
        private ProgressCardsPopup _cardsPopup;
        private SceneResources _sceneResources;
        private InputSystemActions _inputActions;

        public PlayerProgressController(ProgressCardsPopup cardsPopup, SceneResources sceneResources, InputSystemActions inputActions)
        {
            _cardsPopup = cardsPopup;
            _sceneResources = sceneResources;
            _inputActions = inputActions;

            _inputActions.Player.Debug.performed += context => StaticData.DebugData();
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
            StaticData.ExecuteProgress(progressValue, _successValue);
            //Debug.Log("Progress " + progressValue.IncreaseValue + " Type " + progressValue.ProgressType);
        }
    }

}
