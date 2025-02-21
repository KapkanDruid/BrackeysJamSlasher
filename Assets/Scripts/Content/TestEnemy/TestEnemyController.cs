using Assets.Scripts.Architecture;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.TestEnemy
{
    public class TestEnemyController : MonoBehaviour, IEntity, IDamageable
    {
        [SerializeField] private float _currentHealth;
        [SerializeField] private Transform _damageTextPoint;
        [SerializeField] private Flags _flags;

        private EnemyDeadHandler _enemyDeadHandler;
        private PopupTextController _popupTextController;

        private bool _isDead;

        [Inject]
        private void Construct(PopupTextController popupTextController)
        {
            _popupTextController = popupTextController;

            _enemyDeadHandler = new EnemyDeadHandler(null);

            _enemyDeadHandler.Initialize(transform);
        }

        public void TakeDamage(float damage, Action callBack)
        {
            if (_isDead)
                return;

            _currentHealth -= damage;

            _popupTextController.ShowDamage(_damageTextPoint.position, damage);

            callBack?.Invoke();

            if (_currentHealth <= 0)
            {
                _isDead = true;
                _currentHealth = 0;
                _enemyDeadHandler.Death();
            }
        }

        public T ProvideComponent<T>() where T : class
        {
            if (transform is T objectTransform)
                return objectTransform;

            if (_enemyDeadHandler is T deadHandler)
                return deadHandler;

            if (typeof(T) == typeof(IDamageable))
                return this as T;

            if (_flags is T flags)
                return flags;

            return null;
        }

    }
}
