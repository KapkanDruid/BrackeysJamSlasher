using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public sealed class PlayerController : MonoBehaviour, IEntity
    {
        [SerializeField] private PlayerData _playerData;

        public PlayerData PlayerData => _playerData;

        public T ProvideComponent<T>() where T : class
        {
            if (_playerData.Flags is T flags)
                return flags;

            return null;
        }
    }
}