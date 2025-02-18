using System;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    [Serializable]
    public class PlayerData : IJumpData
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _jumpObjectTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _shadowTransform;
        [SerializeField] private Flags _flags;
        [SerializeField] private PlayerWeapon _currentPlayerWeapon;
        [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
        [SerializeField] private EntityFlags _enemyFlag;

        private IEntity _thisEntity;

        public float Speed => _playerConfig.Speed;
        public float MaxHealth => _playerConfig.MaxHealth;
        public float JumpHeight => _playerConfig.JumpHeight;
        public float JumpDuration => _playerConfig.JumpDuration;

        public Vector2 WeaponColliderSize => _currentPlayerWeapon.ColliderSize;
        public Vector2 WeaponColliderOffset => _currentPlayerWeapon.ColliderOffset;

        public Flags Flags => _flags;
        public Sprite WeaponSprite => _currentPlayerWeapon.WeaponSprite;
        public Transform PlayerTransform => _playerTransform;
        public Transform ShadowTransform => _shadowTransform;
        public Transform JumpObjectTransform => _jumpObjectTransform;
        public SpriteRenderer WeaponSpriteRenderer => _weaponSpriteRenderer;
        public PlayerWeapon CurrentPlayerWeapon { get => _currentPlayerWeapon; set => _currentPlayerWeapon = value; }

        public EntityFlags EnemyFlag => _enemyFlag;
        public IEntity ThisEntity { get => _thisEntity; set => _thisEntity = value; }
    }
}