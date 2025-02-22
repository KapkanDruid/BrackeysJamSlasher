using System;
using System.Threading;
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
        [SerializeField] private Transform _damageTextPoint;
        [SerializeField] private EntityFlags _enemyFlag;

        private IEntity _thisEntity;
        private CancellationToken cancellationToken;

        public float Speed => _playerConfig.Speed;
        public float MaxHealth => _playerConfig.MaxHealth;
        public float HealPercent => _playerConfig.HealPercent;
        public float JumpHeight => _playerConfig.JumpHeight;
        public float JumpDuration => _playerConfig.JumpDuration;
        public float CriticalChance => _playerConfig.CriticalChance;
        public float DodgeChancePercent => _playerConfig.DodgeChancePercent;
        public float CriticalMultiplier => _playerConfig.CriticalMultiplier;
        public float InvincibleFramesDuration => _playerConfig.InvincibleFramesDuration;

        public CancellationToken CancellationToken { get => cancellationToken; set => cancellationToken = value; }
        public Vector2 WeaponColliderOffset => _currentPlayerWeapon.ColliderOffset;
        public Vector2 WeaponColliderSize => _currentPlayerWeapon.ColliderSize;

        public Flags Flags => _flags;
        public Transform PlayerTransform => _playerTransform;
        public Transform ShadowTransform => _shadowTransform;
        public Transform DamageTextPoint => _damageTextPoint;
        public Transform JumpObjectTransform => _jumpObjectTransform;
        public Sprite[] WeaponSprites => _currentPlayerWeapon.WeaponSprites;
        public SpriteRenderer WeaponSpriteRenderer => _weaponSpriteRenderer;
        public PlayerWeapon CurrentPlayerWeapon { get => _currentPlayerWeapon; set => _currentPlayerWeapon = value; }

        public EntityFlags EnemyFlag => _enemyFlag;
        public IEntity ThisEntity { get => _thisEntity; set => _thisEntity = value; }
    }
}