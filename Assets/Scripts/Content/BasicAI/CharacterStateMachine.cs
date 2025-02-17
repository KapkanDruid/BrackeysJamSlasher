using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterStateMachine : MonoBehaviour
    {
        [SerializeField] private Transform[] _patrolPoints;
        [SerializeField] private Flags _characterFlag;

        private ICharacterState _currentState;
        private List<ICharacterState> _states = new();
        private CharacterAttackState _attackState;
        private Transform _currentTarget;

        public Flags CharacterFlag => _characterFlag;
        public Transform CurrentTarget => _currentTarget;

        [Inject]
        public void Construct(CharacterHandler characterHandler, CharacterSensor sensor)
        {
            Vector2[] patrolPoints2D = ConvertTransformToVector2();

            _attackState = new CharacterAttackState(characterHandler, this);

            _states.Add(new CharacterPatrolState(characterHandler, this, patrolPoints2D, sensor));
            _states.Add(_attackState);
            _states.Add(new CharacterChaseState(characterHandler, this, sensor));


            SetState<CharacterPatrolState>();
        }

        public void SetState<T>() where T : ICharacterState
        {
            _currentState?.ExitState();

            foreach (var state in _states)
            {
                if (state is T)
                    _currentState = (T)state;
            }

            _currentState.EnterState();
        }

        private Vector2[] ConvertTransformToVector2()
        {
            Vector2[] patrolPoints2D = new Vector2[_patrolPoints.Length];
            for (int i = 0; i < _patrolPoints.Length; i++)
            {
                patrolPoints2D[i] = _patrolPoints[i].position;
            }

            return patrolPoints2D;
        }

        private void Update()
        {
            _currentState?.UpdateState();
        }
    }
}
