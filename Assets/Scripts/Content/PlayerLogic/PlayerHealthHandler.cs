using Assets.Scripts.Architecture;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerHealthHandler : IDamageable
    {
        private readonly PlayerData _playerData;

        private float _currentHealth;

        public PlayerHealthHandler(PlayerData playerData)
        {
            _playerData = playerData;

            _currentHealth = _playerData.MaxHealth;
        }

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            //take damage event
            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                //death event
            }
        }

        public void Heal(float healValue)
        {
            _currentHealth += healValue;

            if (_currentHealth > _playerData.MaxHealth)
            {
                _currentHealth = _playerData.MaxHealth;
            }
        }
    }
}
