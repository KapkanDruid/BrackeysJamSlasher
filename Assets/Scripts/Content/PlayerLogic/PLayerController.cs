using Assets.Scripts.Architecture;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;

        public PlayerData PlayerData => _playerData;
    }
}