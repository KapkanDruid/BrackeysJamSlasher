using System;
using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    [Serializable]
    public class CharacterData
    {
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private Flags _flags;
        [SerializeField] private EntityFlags _enemyFlag;

        public float Speed => _characterConfig.Speed;
        public float Health => _characterConfig.Health;
        public float AttackCooldown => _characterConfig.AttackCooldown;
        public float RangeAttackCooldown => _characterConfig.RangeAttackCooldown;
        public float SensorRadius => _characterConfig.SensorRadius;
        public float ForcePushKnockback => _characterConfig.ForcePushKnockback;
        public float ForcePushKnockdown => _characterConfig.ForcePushKnockdown;
        public float TimeKnockback => _characterConfig.TimeKnockback;
        public float TimeKnockdown => _characterConfig.TimeKnockdown;
        public float MaxAirTime => _characterConfig.MaxAirTime;
        public float ComboHoldTime => _characterConfig.ComboHoldTime;
        public int Damage => _characterConfig.Damage;
        public int RangeDamage => _characterConfig.RangeDamage;
        public int SplashDamage => _characterConfig.SplashDamage;
        public bool Boss => _characterConfig.Boss;
        public bool MeatBoss => _characterConfig.MeatBoss;
        public bool HasRangeAttack => _characterConfig.HasRangeAttack;
        public Transform CharacterTransform => _characterTransform;
        public Flags Flags => _flags;
        public EntityFlags EnemyFlag => _enemyFlag;
        public Vector2 HitColliderSize => _characterConfig.HitColliderSize;
        public Vector2 HitColliderOffset => _characterConfig.HitColliderOffset;
        public Vector2 HitSplashColliderSize => _characterConfig.HitSplashColliderSize;
        public Vector2 HitSplashColliderOffset => _characterConfig.HitSplashColliderOffset;
        public IEntity ThisEntity { get; set; }
    }
}

