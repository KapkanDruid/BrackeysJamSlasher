﻿using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterChaseState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private Transform _target;
        private float _distanceToChase = 0.7f;

        public CharacterChaseState(CharacterHandler character, CharacterStateMachine stateMachine)
        {
            _character = character;
            _stateMachine = stateMachine;
        }

        public void EnterState()
        {
            _target = _stateMachine.CurrentTarget;
        }

        public void UpdateState()
        {
            if (_stateMachine.CurrentTarget == null)
            {
                Debug.Log("Цель для преследования отсутствует");
                _stateMachine.SetState<CharacterPatrolState>();
            }

            if (Vector2.Distance(_character.transform.position, _target.position) > _distanceToChase)
            {
                if (_target == null) return;
                _character.MoveTo(_target.position);
            }
            else if (Vector2.Distance(_character.transform.position, _target.position) <= _distanceToChase)
            {
                Debug.Log("переход в состояние атаки");
                _stateMachine.SetState<CharacterAttackState>();
            }
        }

        public void ExitState()
        {

        }
    }
}
