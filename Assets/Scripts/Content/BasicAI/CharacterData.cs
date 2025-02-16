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

        public float Speed => _characterConfig.Speed;
        public float AttackCooldown => _characterConfig.AttackCooldown;
        public int Damage => _characterConfig.Damage;
        public Transform CharacterTransform => _characterTransform;
        public Flags Flags => _flags;
    }
}

