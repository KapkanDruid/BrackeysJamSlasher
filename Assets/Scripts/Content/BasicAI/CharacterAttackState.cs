using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterAttackState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private readonly HitCollider _hitCollider;
        private GameObject _target;
        private float _nextAttackTime;
        private float _attackCooldown;

        public CharacterAttackState(CharacterHandler character, CharacterStateMachine stateMachine, HitCollider hitCollider)
        {
            _character = character;
            _attackCooldown = _character.CharacterData.AttackCooldown;
            _stateMachine = stateMachine;
            _hitCollider = hitCollider;
        }

        public void EnterState()
        {
        }

        public void UpdateState()
        {
            if (Time.time >= _nextAttackTime)
            {
                _target = _hitCollider.CurrentTarget;

                _hitCollider.EnableHitCollider();
                Debug.Log("Колайдер хита включился");

                if (_target == null)
                {
                    Debug.Log("Цели нет");
                    _stateMachine.SetState<CharacterChaseState>();
                    return;
                }

                _nextAttackTime = Time.time + _attackCooldown;
                _hitCollider.DisableHitCollider();
                Debug.Log("Колайдер хита выключился");

            }
        }

        public void ExitState()
        {

        }

    }
}