using UnityEngine;

namespace Assets.Scripts.Content
{
    public class PopupTextController
    {
        private CustomPool<PopupText> _damageTextPool;

        public PopupTextController(SceneResources sceneResources)
        {
            _damageTextPool = new CustomPool<PopupText>(sceneResources.DamageTextPrefab, sceneResources.DamageTextPoolSize, "DamageTextPool");
        }

        public void ShowDamage(Vector2 position, int damage)
        {
            var textObject = _damageTextPool.Get();
            textObject.Activate(position, damage.ToString());
        }
    }
}
