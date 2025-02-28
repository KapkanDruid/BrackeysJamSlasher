﻿using UnityEngine;

namespace Assets.Scripts.Content
{
    public class GameEndController
    {
        private SceneTransiter _transitionController;
        private SceneResources _sceneResources;
        private GameLoosePopup _loosePopup;
        private Canvas _canvas;

        public GameEndController(Canvas canvas, SceneTransiter transitionController, SceneResources sceneResources)
        {
            _canvas = canvas;
            _transitionController = transitionController;
            _sceneResources = sceneResources;
        }

        public void Initialize()
        {
            _loosePopup = GameObject.Instantiate(_sceneResources.GameLoosePopup, _canvas.transform);
            _loosePopup.Initialize(() => _transitionController.SwitchToScene(_sceneResources.MainMenu), () => _transitionController.SwitchToScene(_sceneResources.FirstLevel));
        }

        public void OnPlayerDeath()
        {
            _loosePopup.Show();
        }
    }

    public class HUDController
    {
        private SceneResources _sceneResources;
        private Canvas _canvas;
        private HeadUpDisplay _display;

        public HUDController(SceneResources sceneResources, Canvas canvas)
        {
            _sceneResources = sceneResources;
            _canvas = canvas;

            _display = GameObject.Instantiate(_sceneResources.HUDPrefab, _canvas.transform);
        }

 
    }
}
