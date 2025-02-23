using Assets.Scripts.Architecture;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content
{
    public class SceneChangeTrigger : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        private SceneTransiter _sceneTransitionController;

        [Inject]
        private void Construct(SceneTransiter sceneTransitionController)
        {
            _sceneTransitionController = sceneTransitionController;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!PlayerEnterCondition.IsPlayer(collision.gameObject))
                return;

            _sceneTransitionController.SwitchToScene(_sceneName);
        }
    }
}
