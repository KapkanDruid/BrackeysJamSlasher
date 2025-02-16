using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterPatrolState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private readonly Vector2[] _patrolPoints;
        private int _currentPointIndex;

        public CharacterPatrolState(CharacterHandler character, CharacterStateMachine stateMachine, Vector2[] patrolPoints)
        {
            _character = character;
            _stateMachine = stateMachine;
            _patrolPoints = patrolPoints;
        }

        public void EnterState()
        {

        }

        public void UpdateState()
        {
            if (_stateMachine.CurrentTarget != null)
            {
                _stateMachine.SetState<CharacterChaseState>();
            }
            
            if (_patrolPoints.Length == 0) return;

            Vector2 targetPoint = _patrolPoints[_currentPointIndex];
            _character.MoveTo(targetPoint);

            if (Vector2.Distance(_character.transform.position, targetPoint) < 1f)
            {
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
            }
        }

        public void ExitState()
        {

        }
    }
}