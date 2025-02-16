using System;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    [Serializable]
    public class PlayerData : IJumpData
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _viewObjectTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _shadowTransform;
        [SerializeField] private Flags _flags;

        public float Speed => _playerConfig.Speed;
        public float MaxHealth => _playerConfig.MaxHealth;
        public float JumpHeight => _playerConfig.JumpHeight;
        public float JumpDuration => _playerConfig.JumpDuration;

        public Transform ShadowTransform => _shadowTransform;
        public Transform PlayerTransform => _playerTransform;
        public Flags Flags => _flags;
        public Transform ViewObjectTransform => _viewObjectTransform;

    }
}