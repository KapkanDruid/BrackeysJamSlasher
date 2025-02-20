using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    [CreateAssetMenu(fileName = "PlayerWeapon", menuName = "Slasher/PlayerWeapon")]
    public class PlayerWeapon : ScriptableObject
    {
        [SerializeField] private WeaponAnimationType _playerWeaponType;
        [SerializeField] private Sprite _weaponSprite;
        [SerializeField] private float _damage;
        [SerializeField] private WeaponCollider _weaponCollider;

        public WeaponAnimationType PlayerWeaponType => _playerWeaponType;
        public Sprite WeaponSprite => _weaponSprite;
        public float Damage => _damage;
        public Vector2 ColliderSize => _weaponCollider.ColliderSize;
        public Vector2 ColliderOffset => _weaponCollider.ColliderOffset;
    }
}