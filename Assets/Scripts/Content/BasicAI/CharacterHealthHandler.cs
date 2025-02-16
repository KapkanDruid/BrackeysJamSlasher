using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterHealthHandler : IDamageable
    {
        private float _health = 100;

        public float Health => _health;

        public void TakeDamage(float damage)
        {
            _health -= damage;
        }
    }
}