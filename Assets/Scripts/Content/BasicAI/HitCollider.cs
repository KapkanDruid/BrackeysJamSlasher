using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class HitCollider : MonoBehaviour
    {
        private CharacterHandler _characterHandler;
        private GameObject _currentTarget;
        private Collider2D _collider2D;
        private bool _hasAttacked = false;

        public GameObject CurrentTarget => _currentTarget;

        [Inject]
        private void Construt(CharacterHandler characterHandler)
        {
            _characterHandler = characterHandler;
            _collider2D = GetComponent<Collider2D>();
            DisableHitCollider();
        }

        public void EnableHitCollider()
        {
            gameObject.SetActive(true);
            _hasAttacked = false;
        }

        public void DisableHitCollider() => gameObject.SetActive(false);

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("HurtCollider"))
            {
                if (other.GetComponentInParent<IDamageable>() is IDamageable damageable)
                {
                    _currentTarget = other.gameObject;
                    if (!_hasAttacked)
                    {
                        Debug.Log($"{gameObject.name} атакует - {other.gameObject.name}");
                        damageable.TakeDamage(_characterHandler.CharacterData.Damage);
                        _hasAttacked = true;
                        return;
                    }
                }
                _currentTarget = null;
            }
            _currentTarget = null;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _currentTarget = null;
        }
    }
}