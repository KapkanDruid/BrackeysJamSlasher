using UnityEngine;

namespace Assets.Scripts.Content
{
    public class PopupTextController
    {
        private CustomPool<PopupText> _damageTextPool;
        private SceneResources _sceneResources;

        public PopupTextController(SceneResources sceneResources)
        {
            _sceneResources = sceneResources;
        }

        public void Initialize()
        {
            _damageTextPool = new CustomPool<PopupText>(_sceneResources.DamageTextPrefab, _sceneResources.DamageTextPoolSize, "DamageTextPool");

        }

        public void ShowDamage(Vector2 position, float damage)
        {
            var roundedDamage = Mathf.RoundToInt(damage);

            var textObject = _damageTextPool.Get();

            textObject.Activate(position, roundedDamage.ToString());
        }
    }
}
