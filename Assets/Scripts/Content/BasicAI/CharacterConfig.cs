﻿using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Slasher/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [Header("Health System")]
        [SerializeField] private float _health;
        [SerializeField] private float _forcePushKnockback = 5f;
        [SerializeField] private float _forcePushKnockdown = 5f;
        [SerializeField] private float _timeKnockback = 0.2f;
        [SerializeField] private float _timeKnockdown = 3f;
        [SerializeField] private float _maxAirTime = 0.5f;
        [SerializeField] private float _comboHoldTime = 5f;
        [SerializeField] private bool _cupcake;
        [SerializeField] private bool _sausage;
        [SerializeField] private bool _breadBoss;
        [SerializeField] private bool _meatBoss;

        [Header("Movement")]
        [SerializeField] private float _speed;

        [Header("Attack System")]
        [Header("Melee")]
        [SerializeField] private int _damage;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private Vector2 _hitColliderSize;
        [SerializeField] private Vector2 _hitColliderOffset;
        [Header("Range")]
        [SerializeField] private bool _hasRangeAttack;
        [SerializeField] private int _rangeDamage;
        [SerializeField] private float _rangeAttackCooldown;
        [SerializeField] private float _attackRange;
        [Header("Boss")]
        [SerializeField] private bool _boss;
        [SerializeField] private int _splashDamage;
        [SerializeField] private Vector2 _hitSplashColliderSize;
        [SerializeField] private Vector2 _hitSplashColliderOffset;

        [Header("Sensor System")]
        [SerializeField] private float _sensorRadius;

        [Header("Random movement")]
        [SerializeField] private float _waveMovementStrength;
        [SerializeField] private float _waveMovementFrequency;

        public float Speed => _speed;
        public float Health => _health;
        public float AttackCooldown => _attackCooldown;
        public float RangeAttackCooldown => _rangeAttackCooldown;
        public float SensorRadius => _sensorRadius;
        public float ForcePushKnockback => _forcePushKnockback;
        public float ForcePushKnockdown => _forcePushKnockdown;
        public float TimeKnockback => _timeKnockback;
        public float TimeKnockdown => _timeKnockdown;
        public float MaxAirTime => _maxAirTime;
        public float ComboHoldTime => _comboHoldTime;
        public int Damage => _damage;
        public int RangeDamage => _rangeDamage;
        public int SplashDamage => _splashDamage;
        public bool Boss => _boss;
        public bool Cupcaka => _cupcake;
        public bool Sausage => _sausage;
        public bool BreadBoss => _breadBoss;
        public bool MeatBoss => _meatBoss;
        public bool HasRangeAttack => _hasRangeAttack;
        public Vector2 HitColliderSize => _hitColliderSize;
        public Vector2 HitColliderOffset => _hitColliderOffset;
        public Vector2 HitSplashColliderSize => _hitSplashColliderSize;
        public Vector2 HitSplashColliderOffset => _hitSplashColliderOffset;
        public float WaveMovementStrength => _waveMovementStrength;
        public float WaveMovementFrequency => _waveMovementFrequency;
    }
}