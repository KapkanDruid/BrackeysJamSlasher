using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content
{
    public sealed class CharacterMoveHandler : ITickable
    {
        private readonly Transform _characterTransform;

        public CharacterMoveHandler(Transform characterTransform) 
        {
            _characterTransform = characterTransform;
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }
    }
}
