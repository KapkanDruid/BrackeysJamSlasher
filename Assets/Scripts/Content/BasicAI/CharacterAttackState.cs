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
        private Transform _entityTransform;
        private CharacterStateMachine _stateMachine;
        private RaycastHit2D[] _hits;
        private IDamageable _damageable;
        private bool _canAttack;
        private bool _isTargetLost;

        public CharacterAttackState(CharacterHandler character, Animator animator, CancellationToken cancellationToken, CharacterStateMachine stateMachine)
        {
            _character = character;
            _animator = animator;
            _cancellationToken = cancellationToken;
            _data = _character.CharacterDatas;
            _stateMachine = stateMachine;
        }

        public void EnterState()
        {
            _canAttack = true;
        }

        public void UpdateState()
        {
            RaycastAttackArea();
            HitTarget();
            CheckDistanceToTarget();
            Attack();
        }

        public void ExitState()
        {
        
        }
       
        private void CheckDistanceToTarget()
        {
            if (_entityTransform == null)
                return;

            if (Vector2.Distance(_entityTransform.position, (Vector2)_character.transform.position + _data.HitColliderOffset) > _data.HitColliderSize.x && !_character.IsKnocked)
            {
                _stateMachine.SetState<CharacterChaseState>();
                Debug.Log("переход в состояние преследования");
                _isTargetLost = true;
            }
        }


        private void RaycastAttackArea()
        {
            Vector2 origin = (Vector2)_data.CharacterTransform.position + _data.HitColliderOffset;
            Vector2 direction = Vector2.down;
            Vector2 size = _data.HitColliderSize;

            _hits = Physics2D.BoxCastAll(origin, size, 0, direction);
        }

        private async UniTask AttackTimer()
        {
            try
            {
                await UniTask.WaitForSeconds(_character.CharacterDatas.AttackCooldown, cancellationToken: _cancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            _canAttack = true;
        }

        private void HitTarget()
        {
            if (!_canAttack)
                return;
            _animator.SetTrigger(AnimatorHashes.SpikeAttackTrigger);

            CheckRaycast(_hits);
        }

        private void CheckRaycast(RaycastHit2D[] hits)
        {

            int count = hits.Length;
            for (int i = 0; i < count; i++)
            {
                if (!hits[i].collider.TryGetComponent(out IEntity entity))
                    continue;

                if (entity == _data.ThisEntity)
                    continue;

                Flags flags = entity.ProvideComponent<Flags>();

                if (flags == null)
                    continue;

                if (!flags.Contain(_data.EnemyFlag))
                    continue;

                _damageable = entity.ProvideComponent<IDamageable>();

                if (_damageable == null)
                    continue;

                _entityTransform = entity.ProvideComponent<Transform>();
            }
        }

        private void Attack()
        {
            if (_isTargetLost && !_character.IsKnocked)
            {
                _animator.SetBool(AnimatorHashes.IsAttacking, true);
                _damageable.TakeDamage(_data.Damage);

                _canAttack = false;
                AttackTimer().Forget();
            }
        }
    }
}