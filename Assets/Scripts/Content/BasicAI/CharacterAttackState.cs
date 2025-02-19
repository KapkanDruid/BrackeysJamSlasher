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
        private bool _canAttack;

        public CharacterAttackState(CharacterHandler character, Animator animator, CancellationToken cancellationToken)
        {
            _character = character;
            _animator = animator;
            _cancellationToken = cancellationToken;
            _data = _character.CharacterDatas;
        }

        public void EnterState()
        {
            _canAttack = true;
        }

        public void UpdateState()
        {
            HitTarget();
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
            Vector2 origin = (Vector2)_data.CharacterTransform.position + _data.HitColliderOffset;
            Vector2 direction = Vector2.down;
            Vector2 size = _data.HitColliderSize;

            RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, 0, direction);

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

                IDamageable damageable = entity.ProvideComponent<IDamageable>();

                if (damageable == null)
                    continue;

                _animator.SetBool(AnimatorHashes.IsAttacking, true);
                damageable.TakeDamage(_data.Damage);

                _canAttack = false;
                AttackTimer().Forget();
            }
        }

        public void ExitState()
        {

        }

    }
}