using Assets.Scripts.Architecture;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterHealthHandler : IDamageable
    {
        private CharacterData _data;
        private CharacterHandler _characterHandler;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private CharacterStateMachine _stateMachine;
        private float _forcePushKnockback = 5f;
        private float _forcePushKnockdown = 5f;
        private float _forcePushUp = 5f;
        private float _timeKnockdown = 1f;
        private float _timeKnockback = 0.2f;
        private float _maxAirTime = 0.5f;
        private float _comboHoldTime = 5f;
        private float _health;
        private int _hitCount = 0;
        private bool _isKnockedDown = false;

        public float Health => _health;

        [Inject]
        public void Construct(CharacterData data, Rigidbody2D rigidbody, CharacterHandler characterHandler, Animator animator, CharacterStateMachine stateMachine)
        {
            _data = data;
            _health = _data.Health;
            _rigidbody = rigidbody;
            _characterHandler = characterHandler;
            _animator = animator;
            _stateMachine = stateMachine;
        }

        public void TakeDamage(float damage, Action callback)
        {
            if (_isKnockedDown) return;

            callback?.Invoke();
            _health -= damage;

            Debug.Log($"Health: {_health}");

            _hitCount++;
            ProcessHitReaction();

            ResetHitCountTimer().Forget();
        }

        private async UniTask ResetHitCountTimer()
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_comboHoldTime), cancellationToken: _characterHandler.CancellationToken);
                _hitCount = 0;
                Debug.Log("Hit count reset");
            }
            catch (OperationCanceledException)
            {
                return;
            }
        }

        private void ProcessHitReaction()
        {
            switch (_hitCount)
            {
                case 1:
                    //_animator.SetTrigger("HitReaction");
                    Debug.Log("Enemy hitted.");
                    break;

                case 2:
                    Knockforce(_forcePushKnockback, false).Forget();
                    break;

                case 3:
                    Knockforce(_forcePushKnockdown, true).Forget();
                    break;
            }
        }

        private async UniTask Knockforce(float forsePush, bool isKnockdown)
        {
            //_animator.SetTrigger("Knockback");
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.right * forsePush, ForceMode2D.Impulse);
            Debug.Log("Enemy knocked back.");
            _stateMachine.SetState<CharacterChaseState>();

            if (!isKnockdown)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_timeKnockback), cancellationToken: _characterHandler.CancellationToken);
            }
            else
            {
                Debug.Log("Enemy knocked down.");
                await UniTask.Delay(TimeSpan.FromSeconds(_timeKnockdown), cancellationToken: _characterHandler.CancellationToken);

                _hitCount = 0;
                Debug.Log("Enemy gets up and attacks.");

            }
            _rigidbody.linearVelocity = Vector2.zero;
        }

    }
}