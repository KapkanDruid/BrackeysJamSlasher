using UnityEngine;

namespace Assets.Scripts.Content
{
    public class PopupTextController
    {
        private CustomPool<PopupText> _damageTextPool;
        private CustomPool<PopupText> _healTextPool;
        private CustomPool<PopupText> _dodgeTextPool;
        private SceneResources _sceneResources;

        public PopupTextController(SceneResources sceneResources)
        {
            _sceneResources = sceneResources;
        }

        public void Initialize()
        {
            _damageTextPool = new CustomPool<PopupText>(_sceneResources.DamageTextPrefab, _sceneResources.DamageTextPoolSize, "DamageTextPool");
            _healTextPool = new CustomPool<PopupText>(_sceneResources.HealTextPrefab, 2, "HealTextPool");
            _dodgeTextPool = new CustomPool<PopupText>(_sceneResources.DodgeTextPrefab, 2, "DodgeTextPool");
        }

        public void ShowDamage(Vector2 position, float damage)
        {
            var roundedDamage = Mathf.RoundToInt(damage);

            var textObject = _damageTextPool.Get();

            textObject.Activate(position, roundedDamage.ToString());
        }

        public void ShowHeal(Vector2 position, float heal)
        {
            var roundedHeal = Mathf.RoundToInt(heal);

            var textObject = _healTextPool.Get();

            textObject.Activate(position, roundedHeal.ToString());
        }

        public void ShowDodge(Vector2 position)
        {
            var textObject = _dodgeTextPool.Get();

            textObject.Activate(position, "DODGE!");
        }
    }
}
