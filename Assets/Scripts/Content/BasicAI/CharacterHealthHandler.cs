using Assets.Scripts.Architecture;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterHealthHandler : IDamageable
    {
        private CharacterData _data;
        private CharacterHandler _character;
        private PopupTextController _popupTextController;
        private Rigidbody2D _rigidbody;
        private AudioController _audioController;
        private Animator _animator;
        private CharacterStateMachine _stateMachine;
        private EnemyDeadHandler _enemyDeadHandler;
        private float _health;
        private int _hitCount = 0;
        private bool _isKnockedDown = false;
        private bool _isDead = false;

        public float Health => _health;

        [Inject]
        public void Construct(CharacterData data,
                              Rigidbody2D rigidbody,
                              CharacterHandler character,
                              Animator animator,
                              CharacterStateMachine stateMachine,
                              PopupTextController popupTextController,
                              AudioController audioController,
                              EnemyDeadHandler enemyDeadHandler)
        {
            _data = data;
            _health = _data.Health;
            _rigidbody = rigidbody;
            _character = character;
            _animator = animator;
            _stateMachine = stateMachine;
            _popupTextController = popupTextController;
            _audioController = audioController;
            _enemyDeadHandler = enemyDeadHandler;
            _enemyDeadHandler.Initialize(_character.transform);
        }

        public void TakeDamage(float damage, Action callback)
        {
            if (_isDead)
                return;

            if (_isKnockedDown)
                return;

            callback?.Invoke();

            _health -= damage;
            PlaySaund();
            _popupTextController.ShowDamage(_data.DamageTextPoint.position, damage);
            if ( _health <= 0)
            {
                _enemyDeadHandler.Death();
                _isDead = true;
                _animator.SetBool(AnimatorHashes.IsDead, _isDead);
                return;
            }
            _hitCount++;
            ProcessHitReaction();

            ResetHitCountTimer().Forget();

        }

        private void PlaySaund()
        {
            if (_data.Cupcake)
            {
                _audioController.PlayOneShot(AudioController.SoundEffects.CakeHit);
            }
            if (_data.Sausage)
            {
                _audioController.PlayOneShot(AudioController.SoundEffects.SausageHit);
            }
            if (_data.BreadBoss)
            {
                _audioController.PlayOneShot(AudioController.SoundEffects.BreadHit);
            }
            if (_data.MeatBoss)
            {
                _audioController.PlayOneShot(AudioController.SoundEffects.SausageHit);
            }
        }

        private async UniTask ResetHitCountTimer()
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_data.ComboHoldTime), cancellationToken: _character.CancellationToken);
                _hitCount = 0;
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
                    SetAnimatorTrigger(AnimatorHashes.TakeDamageTrigger);
                    _stateMachine.SetState<CharacterChaseState>();
                    break;

                case 2:
                    Knockback(_data.ForcePushKnockback).Forget();
                    break;

                case 3:
                    Knockdown(_data.ForcePushKnockdown).Forget();
                    break;
            }
        }

        private async UniTask Knockback(float forsePush)
        {
            float direction = GetDirection();
            ResetVelocity();
            _rigidbody.AddForce(new Vector2(direction, 0f) * forsePush, ForceMode2D.Impulse);

            SetAnimatorTrigger(AnimatorHashes.Knockback);
            _stateMachine.SetState<CharacterChaseState>();

            await AwaitDelay(_data.TimeKnockback);
            ResetVelocity();

            _stateMachine.SetState<CharacterChaseState>();
        }

        private async UniTask Knockdown(float forsePush)
        {
            float direction = GetDirection();

            SetAnimatorTrigger(AnimatorHashes.Knockdown);

            _rigidbody.AddForce(new Vector2(direction, 0f) * forsePush, ForceMode2D.Impulse);

            await AwaitDelay(_data.MaxAirTime);

            ResetVelocity();

            _isKnockedDown = true;
            _character.IsKnocked = _isKnockedDown;

            await AwaitDelay(_data.TimeKnockdown);

            _isKnockedDown = false;
            _character.IsKnocked = _isKnockedDown;
            _hitCount = 0;

            _stateMachine.SetState<CharacterChaseState>();
        }

        private void SetAnimatorTrigger(int name)
        {
            _animator.SetTrigger(name);
        }

        private async Task AwaitDelay(float time)
        {
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _character.CancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
        }

        private void ResetVelocity()
        {
            _rigidbody.linearVelocity = Vector2.zero;
        }

        private float GetDirection()
        {
            return Mathf.Sign(_stateMachine.CurrentTarget.position.x - _character.transform.position.x) * -1f;
        }

    }
}