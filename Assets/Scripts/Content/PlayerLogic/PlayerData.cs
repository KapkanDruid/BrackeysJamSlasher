using System;
using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _playerTransform;

        public float Speed => _playerConfig.Speed;
        public Transform PlayerTransform => _playerTransform;
    }
}