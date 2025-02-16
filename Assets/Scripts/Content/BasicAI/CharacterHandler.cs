using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterHandler : MonoBehaviour, IEntity
    {
        [SerializeField] private CharacterData _characterData;

        private CharacterHealthHandler _healthHandler;

        public CharacterData CharacterData => _characterData;

        [Inject]
        public void Construct(CharacterHealthHandler healthHandler)
        {
            _healthHandler = healthHandler;
        }

        public void MoveTo(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _characterData.Speed * Time.deltaTime);
        }

        public void Attack()
        {
            Debug.Log($"{gameObject.name} атакует!");
        }


        public T ProvideComponent<T>() where T : class
        {
            if (_characterData.Flags is T flags)
                return flags;

            return null;
        }

        public void TakeDamage(float damage)
        {
            _healthHandler.TakeDamage(damage);
        }
    }
}

