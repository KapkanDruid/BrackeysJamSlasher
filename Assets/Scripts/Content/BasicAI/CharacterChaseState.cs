using TMPro;
using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterChaseState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterSensor _sensor;
        private readonly Animator _animator;
        private Transform _target;
        private float _distanceToChase = 0.1f;

        public CharacterChaseState(CharacterHandler character, CharacterStateMachine stateMachine, CharacterSensor sensor, Animator animator)
        {
            _character = character;
            _stateMachine = stateMachine;
            _sensor = sensor;
            _animator = animator;
        }

        public void EnterState()
        {
            _target = _sensor.TargetTransform; 
        }

        public void UpdateState()
        {
            if (_stateMachine.CurrentTarget == null)
            {
                _stateMachine.SetState<CharacterPatrolState>();
            }

            if (Vector2.Distance(_character.transform.position, _target.position) > _distanceToChase && !_character.IsKnocked)
            {
                _animator.SetBool(AnimatorHashes.IsMoving, true);
                if (_target == null) return;
                _character.MoveTo(_target.position);
            }
            else if (Vector2.Distance(_character.transform.position, _target.position) <= _distanceToChase && !_character.IsKnocked)
            {
                _animator.SetBool(AnimatorHashes.IsMoving, false);
                
                _stateMachine.SetState<CharacterAttackState>();
            }
        }

        public void ExitState()
        {

        }
    }
}
