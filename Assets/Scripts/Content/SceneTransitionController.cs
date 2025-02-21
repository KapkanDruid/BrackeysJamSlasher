using UnityEngine;

namespace Assets.Scripts.Content
{
    public class SceneTransitionController
    {
        private SceneResources _resources;
        private SceneTransiter _sceneTransiter;
        private Canvas _canvas;

        public SceneTransitionController(SceneResources resources, Canvas canvas)
        {
            _resources = resources;
            _canvas = canvas;
        }

        public void Initialize()
        {
            if (_sceneTransiter == null)
            {
                _sceneTransiter = GameObject.Instantiate(_resources.SceneTransiter, _canvas.transform);
            }
        }

        public void ChangeScene(string sceneName)
        {
            if (_sceneTransiter == null)
                return;

            _sceneTransiter.SwitchToScene(sceneName);
        }
    }
}
