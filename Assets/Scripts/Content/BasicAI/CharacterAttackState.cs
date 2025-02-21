using Assets.Scripts.Architecture;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterAttackState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly Animator _animator;
        private readonly CancellationToken _cancellationToken;
        private readonly CharacterData _data;
        private IDamageable _damageable;
        private CharacterStateMachine _stateMachine;

        public CharacterAttackState(CharacterHandler character, Animator animator, CancellationToken cancellationToken, CharacterStateMachine stateMachine)
        {
            _character = character;
            _animator = animator;
            _cancellationToken = cancellationToken;
            _data = _character.CharacterData;
            _stateMachine = stateMachine;
            
        }

        public void EnterState()
        {
            Attack();
        }

        public void UpdateState()
        {            
            
        }

        public void ExitState()
        {
        
        }

        public void GetTarget(IDamageable target)
        {
            _damageable = target;   
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

        private void Attack()
        {
            if (!_character.IsKnocked)
            {
                _animator.SetBool(AnimatorHashes.IsAttacking, true);
                _damageable.TakeDamage(_data.Damage);

                AttackTimer().Forget();
                _stateMachine.SetState<CharacterChaseState>();
            }
        }
    }
}