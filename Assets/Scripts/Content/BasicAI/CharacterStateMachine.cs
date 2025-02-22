using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterStateMachine : MonoBehaviour
    {
        [SerializeField] private Transform[] _patrolPoints;

        private Animator _animator;
        private ICharacterState _currentState;
        private List<ICharacterState> _states = new();
        private Transform _currentTarget;

        public Transform CurrentTarget => _currentTarget;

        [Inject]
        public void Construct(CharacterHandler characterHandler, CharacterSensor sensor, Animator animator, GizmosDrawer gizmosDrawer, AnimatorEventHandler animatorEventHandler)
        {
            _animator = animator;

            Vector2[] patrolPoints2D = ConvertTransformToVector2();

            _states.Add(new CharacterAttackState(characterHandler, _animator, characterHandler.CancellationToken, this, animatorEventHandler));
            _states.Add(new CharacterPatrolState(characterHandler, this, patrolPoints2D, sensor));
            _states.Add(new CharacterChaseState(characterHandler, this, sensor, _animator, gizmosDrawer, patrolPoints2D));


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

        public void SetTarget(Transform target)
        {
            _currentTarget = target;
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
