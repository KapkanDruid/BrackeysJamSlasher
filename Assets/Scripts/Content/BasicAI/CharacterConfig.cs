using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Slasher/CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _speed;

        [Header("Attack System")]
        [SerializeField] private int _damage;
        [SerializeField] private float _attackCooldown = 1.5f;

        public float Speed => _speed;
        public float AttackCooldown => _attackCooldown;
        public int Damage => _damage;
    }
}