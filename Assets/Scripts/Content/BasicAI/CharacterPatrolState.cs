using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterPatrolState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterSensor _sensor;
        private readonly Vector2[] _patrolPoints;
        private int _currentPointIndex;

        public CharacterPatrolState(CharacterHandler character, CharacterStateMachine stateMachine, Vector2[] patrolPoints, CharacterSensor sensor)
        {
            _character = character;
            _stateMachine = stateMachine;
            _patrolPoints = patrolPoints;
            _sensor = sensor;
        }

        public void EnterState()
        {
            _sensor.TargetDetected += OnTargetDetected;
        }

        public void UpdateState()
        {
            if (_patrolPoints.Length == 0) return;

            Vector2 targetPoint = _patrolPoints[_currentPointIndex];
            _character.MoveTo(targetPoint);

            if (Vector2.Distance(_character.transform.position, targetPoint) < 1f)
            {
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
            }
        }

        private void OnTargetDetected()
        {
            _stateMachine.SetState<CharacterChaseState>();
        }

        public void ExitState()
        {
            _sensor.TargetDetected -= OnTargetDetected;
        }
    }
}