using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    [CreateAssetMenu(fileName = "PlayerWeapon", menuName = "Slasher/PlayerWeapon")]
    public class PlayerWeapon : ScriptableObject
    {
        [SerializeField] private WeaponAnimationType _playerWeaponType;
        [SerializeField] private Sprite[] _weaponSprites;
        [SerializeField] private float _damage;
        [SerializeField] private WeaponCollider _weaponCollider;
        [SerializeField] private float _additiveDamagePerLevel;
        [SerializeField] private float _additiveDamageForA;
        [SerializeField] private float _additiveDamageForB;
        [SerializeField] private float _additiveDamageForC;

        private int _currentLevel = 1;

        public WeaponAnimationType PlayerWeaponType => _playerWeaponType;
        public Sprite[] WeaponSprites => _weaponSprites;
        public Vector2 ColliderSize => _weaponCollider.ColliderSize;
        public Vector2 ColliderOffset => _weaponCollider.ColliderOffset;
        public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
        public float AdditiveDamageForA => _additiveDamageForA;
        public float AdditiveDamageForB => _additiveDamageForB;
        public float AdditiveDamageForC => _additiveDamageForC;
        public float AdditiveDamagePerLevel => _additiveDamagePerLevel;
        public float Damage { get => _damage; set => _damage = value; }
    }
}