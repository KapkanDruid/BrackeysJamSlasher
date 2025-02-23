using Assets.Scripts.Content.CoreProgression;
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
        [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
        [SerializeField] private Transform _damageTextPoint;
        [SerializeField] private EntityFlags _enemyFlag;

        private IEntity _thisEntity;
        private CancellationToken cancellationToken;

        public float Speed => _playerConfig.Speed;
        public float MaxHealth => StaticData.MaxPlayerHealth;
        public float HealPercent => StaticData.PlayerHealPercent;
        public float JumpHeight => _playerConfig.JumpHeight;
        public float JumpDuration => _playerConfig.JumpDuration;
        public float CriticalChance => StaticData.CriticalMultiplier;
        public float DodgeChancePercent => StaticData.PlayerDodgeChance;
        public float CriticalMultiplier => StaticData.CriticalMultiplier;
        public float InvincibleFramesDuration => _playerConfig.InvincibleFramesDuration;

        public CancellationToken CancellationToken { get => cancellationToken; set => cancellationToken = value; }
        public Vector2 WeaponColliderOffset => _playerConfig.Weapon.ColliderOffset;
        public Vector2 WeaponColliderSize => _playerConfig.Weapon.ColliderSize;

        public Flags Flags => _flags;
        public Transform PlayerTransform => _playerTransform;
        public Transform ShadowTransform => _shadowTransform;
        public Transform DamageTextPoint => _damageTextPoint;
        public Transform JumpObjectTransform => _jumpObjectTransform;
        public Sprite[] WeaponSprites => StaticData.Weapon.WeaponSprites;
        public SpriteRenderer WeaponSpriteRenderer => _weaponSpriteRenderer;
        public PlayerWeapon CurrentPlayerWeapon => StaticData.Weapon;
        public float Damage => StaticData.Damage;

        public EntityFlags EnemyFlag => _enemyFlag;
        public IEntity ThisEntity { get => _thisEntity; set => _thisEntity = value; }
    }
}