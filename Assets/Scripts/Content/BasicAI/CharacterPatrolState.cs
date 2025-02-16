using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterPatrolState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private readonly Transform[] _patrolPoints;
        private int _currentPointIndex;

        public CharacterPatrolState(CharacterHandler character, Transform[] patrolPoints)
        {
            _character = character;
            _patrolPoints = patrolPoints;
        }

        public void EnterState()
        {
            Debug.Log($"{_character.name} вошел в режим патрулирования.");
        }

        public void UpdateState()
        {
            if (_patrolPoints.Length == 0) return;

            Transform targetPoint = _patrolPoints[_currentPointIndex];
            _character.MoveTo(targetPoint.position);

            if (Vector3.Distance(_character.transform.position, targetPoint.position) < 1f)
            {
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Length;
            }
        }

        public void ExitState()
        {
            Debug.Log($"{_character.name} покидает режим патрулирования.");
        }
    }
}