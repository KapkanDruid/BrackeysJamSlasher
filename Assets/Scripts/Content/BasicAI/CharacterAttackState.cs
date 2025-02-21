using Assets.Scripts.Architecture;
using Assets.Scripts.Content.PlayerLogic;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterAttackState : ICharacterState, IDisposable
    {
        private readonly CharacterHandler _character;
        private readonly Animator _animator;
        private readonly CancellationToken _cancellationToken;
        private readonly CharacterData _data;
        private readonly AnimatorEventHandler _animatorEventHandler;
        private IDamageable _damageable;
        private CharacterStateMachine _stateMachine;

        private Vector2 ColliderOffset => new Vector2(_character.CharacterData.HitColliderOffset.x * _character.CurrentOrientation, _character.CharacterData.HitColliderOffset.y);

        public CharacterAttackState(CharacterHandler character, Animator animator, CancellationToken cancellationToken, CharacterStateMachine stateMachine, AnimatorEventHandler animatorEventHandler)
        {
            _character = character;
            _animator = animator;
            _cancellationToken = cancellationToken;
            _data = _character.CharacterData;
            _stateMachine = stateMachine;
            _animatorEventHandler = animatorEventHandler;

            _animatorEventHandler.OnAnimationHit += OnAnimationHit;

        }

        public void EnterState()
        {
            StartAttack();
            _damageable = null;
        }

        public void UpdateState()
        {

        }

        public void ExitState()
        {

        }
        public void Dispose()
        {
            _animatorEventHandler.OnAnimationHit -= OnAnimationHit;
        }

        private async void StartAttack()
        {
            _animator.SetTrigger(AnimatorHashes.SpikeAttackTrigger);
            await AttackTimer();
        }

        private void OnAnimationHit()
        {
            if (!_character.IsKnocked)
            {
                Attack();
            }
        }

        private async UniTask AttackTimer()
        {
            try
            {
                await UniTask.WaitForSeconds(_data.AttackCooldown, cancellationToken: _cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }

        }
        private void CheckRaycast()
        {
            Vector2 origin = (Vector2)_character.transform.position + ColliderOffset;
            Vector2 direction = Vector2.zero;
            Vector2 size = _character.CharacterData.HitColliderSize;

            var _hits = Physics2D.BoxCastAll(origin, size, 0, direction);

            int count = _hits.Length;
            for (int i = 0; i < count; i++)
            {
                if (!_hits[i].collider.TryGetComponent(out IEntity entity))
                    continue;

                if (entity == _character.CharacterData.ThisEntity)
                    continue;

                Flags flags = entity.ProvideComponent<Flags>();

                if (flags == null)
                    continue;

                if (!flags.Contain(_character.CharacterData.EnemyFlag))
                    continue;

                _damageable = entity.ProvideComponent<IDamageable>();
            }
        }

        private void Attack()
        {
            CheckRaycast();
            if (_damageable == null)
            {
                _stateMachine.SetState<CharacterChaseState>();
                return;
            }
            _damageable.TakeDamage(_data.Damage);
            _stateMachine.SetState<CharacterChaseState>();

        }

    }
}