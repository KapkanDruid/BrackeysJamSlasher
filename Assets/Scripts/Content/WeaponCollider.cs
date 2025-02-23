using UnityEngine;

namespace Assets.Scripts.Content
{
    [CreateAssetMenu(fileName = "WeaponCollider", menuName = "Slasher/WeaponCollider")]
    public class WeaponCollider : ScriptableObject
    {
        [SerializeField] private Vector2 _colliderSize;
        [SerializeField] private Vector2 _colliderOffset;

        public Vector2 ColliderSize => _colliderSize;
        public Vector2 ColliderOffset => _colliderOffset;
    }


}