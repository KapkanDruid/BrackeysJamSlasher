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
        private CharacterHandler _character;
        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private CharacterStateMachine _stateMachine;
        private CharacterChaseState _chaseState;
        private float _forcePushKnockback = 5f;
        private float _forcePushKnockdown = 5f;
        private float _forcePushUp = 5f;
        private float _timeKnockdown = 3f;
        private float _timeKnockback = 0.2f;
        private float _maxAirTime = 0.5f;
        private float _comboHoldTime = 5f;
        private float _health;
        private int _hitCount = 0;
        private bool _isKnockedDown = false;

        public float Health => _health;

        [Inject]
        public void Construct(CharacterData data, Rigidbody2D rigidbody, CharacterHandler character, Animator animator, CharacterStateMachine stateMachine)
        {
            _data = data;
            _health = _data.Health;
            _rigidbody = rigidbody;
            _character = character;
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
                await UniTask.Delay(TimeSpan.FromSeconds(_comboHoldTime), cancellationToken: _character.CancellationToken);
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
                    Knockback(_forcePushKnockback).Forget();
                    break;

                case 3:
                    Knockdown(_forcePushKnockdown).Forget();
                    break;
            }
        }

        private async UniTask Knockback(float forsePush)
        {
            float direction = Mathf.Sign(_stateMachine.CurrentTarget.position.x - _character.transform.position.x) * -1f;
            _rigidbody.linearVelocity = Vector2.zero;
            _rigidbody.AddForce(new Vector2(direction, 0f) * forsePush, ForceMode2D.Impulse);
            _animator.SetTrigger(AnimatorHashes.Knockback);
            _stateMachine.SetState<CharacterChaseState>();

            Debug.Log("Enemy knocked back.");
            await UniTask.Delay(TimeSpan.FromSeconds(_timeKnockback), cancellationToken: _character.CancellationToken);
            _rigidbody.linearVelocity = Vector2.zero;

        }

        private async UniTask Knockdown(float forsePush)
        {
            float direction = Mathf.Sign(_stateMachine.CurrentTarget.position.x - _character.transform.position.x) * -1f;
            _animator.SetTrigger(AnimatorHashes.Knockdown);
            Debug.Log("Enemy knocked down.");
            _rigidbody.AddForce(new Vector2(direction, 0f) * forsePush, ForceMode2D.Impulse);
            
            await UniTask.Delay(TimeSpan.FromSeconds(_maxAirTime), cancellationToken: _character.CancellationToken);
            _rigidbody.linearVelocity = Vector2.zero;

            _isKnockedDown = true;
            _character.IsKnocked = _isKnockedDown;
            await UniTask.Delay(TimeSpan.FromSeconds(_timeKnockdown), cancellationToken: _character.CancellationToken);
            _isKnockedDown = false;
            _character.IsKnocked = _isKnockedDown;
            _hitCount = 0;
            Debug.Log("Enemy gets up and attacks.");
        
        }

    }
}