﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterAttackState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterData _data;
        private GameObject _target;
        private float _nextAttackTime;
        private float _attackCooldown;
        private bool _canAttack;

        public CharacterAttackState(CharacterHandler character, CharacterStateMachine stateMachine)
        {
            _character = character;
            _attackCooldown = _character.CharacterData.AttackCooldown;
            _data = _character.CharacterData;
            _stateMachine = stateMachine;
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
            await UniTask.WaitForSeconds(_character.CharacterData.AttackCooldown);

            _canAttack = true;
        }

        private void HitTarget()
        {
            if (!_canAttack)
                return;

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