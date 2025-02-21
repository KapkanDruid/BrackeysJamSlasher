using UnityEngine;

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

        [Header("Movement")]
        [SerializeField] private float _speed;

        [Header("Attack System")]
        [SerializeField] private int _damage;
        [SerializeField] private float _attackCooldown = 1.5f;
        [SerializeField] private float _attackDelay = 0.5f;
        [SerializeField] private Vector2 _hitColliderSize;
        [SerializeField] private Vector2 _hitColliderOffset;

        [Header("Sensor System")]
        [SerializeField] private float _sensorRadius;

        public float Speed => _speed;
        public float Health => _health;
        public float AttackCooldown => _attackCooldown;
        public float SensorRadius => _sensorRadius;
        public float ForcePushKnockback => _forcePushKnockback;
        public float ForcePushKnockdown => _forcePushKnockdown;
        public float TimeKnockback => _timeKnockback;
        public float TimeKnockdown => _timeKnockdown;
        public float MaxAirTime => _maxAirTime;
        public float ComboHoldTime => _comboHoldTime;
        public float AttackDelay => _attackDelay;
        public int Damage => _damage;
        public Vector2 HitColliderSize => _hitColliderSize;
        public Vector2 HitColliderOffset => _hitColliderOffset;
    }
}