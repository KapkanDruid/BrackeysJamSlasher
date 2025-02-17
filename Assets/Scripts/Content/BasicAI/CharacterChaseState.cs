﻿using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterChaseState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterSensor _sensor;
        private Transform _target;
        private float _distanceToChase = 0.7f;

        public CharacterChaseState(CharacterHandler character, CharacterStateMachine stateMachine, CharacterSensor sensor)
        {
            _character = character;
            _stateMachine = stateMachine;
            _sensor = sensor;
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

            if (Vector2.Distance(_character.transform.position, _target.position) > _distanceToChase)
            {
                if (_target == null) return;
                _character.MoveTo(_target.position);
            }
            else if (Vector2.Distance(_character.transform.position, _target.position) <= _distanceToChase)
            {
                _stateMachine.SetState<CharacterAttackState>();
            }
        }

        public void ExitState()
        {

        }
    }
}
