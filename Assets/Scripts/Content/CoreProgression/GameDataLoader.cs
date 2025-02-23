using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Content.CoreProgression
{
    public class GameDataLoader
    {
        private SceneResources _sceneResources;

        public GameDataLoader(SceneResources sceneResources)
        {
            _sceneResources = sceneResources;
        }

        public void Initialize()
        {
            if (SceneManager.GetActiveScene().name == _sceneResources.FirstLevel)
            {
                StaticData.Initialize(_sceneResources.PlayerConfig, _sceneResources.CupcakeConfig, _sceneResources.SausageConfig, _sceneResources.BreadConfig, _sceneResources.MeatConfig);
                Debug.Log("Data reset");
            }
        }
    }

}
