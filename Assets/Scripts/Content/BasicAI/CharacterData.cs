using Assets.Scripts.Content.CoreProgression;
using System;
using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    [Serializable]
    public class CharacterData
    {
        [SerializeField] private CharacterConfig _characterConfig;
        [SerializeField] private Transform _damageTextPoint;
        [SerializeField] private Transform _characterTransform;
        [SerializeField] private Flags _flags;
        [SerializeField] private EntityFlags _enemyFlag;
        [SerializeField] private TypeCharacter _typeCharacter;

        public float Speed => _characterConfig.Speed;
        public float Health => DetermineHealth(_typeCharacter);
        public float AttackCooldown => _characterConfig.AttackCooldown;
        public float RangeAttackCooldown => _characterConfig.RangeAttackCooldown;
        public float SensorRadius => _characterConfig.SensorRadius;
        public float ForcePushKnockback => _characterConfig.ForcePushKnockback;
        public float ForcePushKnockdown => _characterConfig.ForcePushKnockdown;
        public float TimeKnockback => _characterConfig.TimeKnockback;
        public float TimeKnockdown => _characterConfig.TimeKnockdown;
        public float MaxAirTime => _characterConfig.MaxAirTime;
        public float ComboHoldTime => _characterConfig.ComboHoldTime;
        public float Damage => DetermineDamage(_typeCharacter);
        public float RangeDamage => DetermineRangeDamage(_typeCharacter);
        public float SplashDamage => DetermineSplashDamage(_typeCharacter);
        public bool Boss => _characterConfig.Boss;
        public bool Cupcake => _characterConfig.Cupcaka;
        public bool Sausage => _characterConfig.Sausage;
        public bool BreadBoss => _characterConfig.BreadBoss;
        public bool MeatBoss => _characterConfig.MeatBoss;
        public bool HasRangeAttack => _characterConfig.HasRangeAttack;
        public Transform CharacterTransform => _characterTransform;
        public Transform DamageTextPoint => _damageTextPoint;
        public Flags Flags => _flags;
        public EntityFlags EnemyFlag => _enemyFlag;
        public Vector2 HitColliderSize => _characterConfig.HitColliderSize;
        public Vector2 HitColliderOffset => _characterConfig.HitColliderOffset;
        public Vector2 HitSplashColliderSize => _characterConfig.HitSplashColliderSize;
        public Vector2 HitSplashColliderOffset => _characterConfig.HitSplashColliderOffset;
        public IEntity ThisEntity { get; set; }

        public float WaveMovementStrength => _characterConfig.WaveMovementStrength;
        public float WaveMovementFrequency => _characterConfig.WaveMovementFrequency;

        private float DetermineDamage(TypeCharacter typeCharacter)
        {
            float damage = 0f;
            switch (typeCharacter)
            {
                case TypeCharacter.Cupcake:
                    damage = StaticData.DamageCupcake;
                    break;

                case TypeCharacter.Sausage:
                    damage = StaticData.DamageSausage;
                    break;

                case TypeCharacter.Bread:
                    damage = StaticData.DamageBread;
                    break;

                case TypeCharacter.Meat:
                    damage = StaticData.DamageMeat;
                    break;

                default:
                    break;
            }

            return damage;
        }

        private float DetermineRangeDamage(TypeCharacter typeCharacter)
        {
            float rangeDamage = 0f;
            switch (typeCharacter)
            {
                case TypeCharacter.Cupcake:
                    rangeDamage = StaticData.DamageRangeCupcake;
                    break;

                case TypeCharacter.Sausage:
                    rangeDamage = StaticData.DamageRangeSausage;
                    break;

                case TypeCharacter.Bread:
                    rangeDamage = StaticData.DamageRangeBread;
                    break;

                case TypeCharacter.Meat:
                    rangeDamage = StaticData.DamageRangeMeat;
                    break;

                default:
                    break;
            }

            return rangeDamage;
        }

        private float DetermineSplashDamage(TypeCharacter typeCharacter)
        {
            float splashDamage = 0f;
            switch (typeCharacter)
            {
                case TypeCharacter.Cupcake:
                    splashDamage = StaticData.DamageSplashCupcake;
                    break;

                case TypeCharacter.Sausage:
                    splashDamage = StaticData.DamageSplashSausage;
                    break;

                case TypeCharacter.Bread:
                    splashDamage = StaticData.DamageSplashBread;
                    break;

                case TypeCharacter.Meat:
                    splashDamage = StaticData.DamageSplashMeat;
                    break;

                default:
                    break;
            }

            return splashDamage;
        }

        private float DetermineHealth(TypeCharacter typeCharacter)
        {
            float health = 0f;
            switch (typeCharacter)
            {
                case TypeCharacter.Cupcake:
                    health = StaticData.MaxCupcakeHealth;
                    break;

                case TypeCharacter.Sausage:
                    health = StaticData.MaxSausageHealth;
                    break;

                case TypeCharacter.Bread:
                    health = StaticData.MaxBreadHealth;
                    break;

                case TypeCharacter.Meat:
                    health = StaticData.MaxMeatHealth;
                    break;

                default:
                    break;
            }
            Debug.Log($"{health}, {typeCharacter}, {StaticData.MaxCupcakeHealth}");
            return health;
        }

        public enum TypeCharacter
        {
            Cupcake,
            Sausage,
            Bread,
            Meat
        }
    }
}

