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
        private CharacterChaseState _chaseState;
        private CharacterHandler _characterHandler;
        private Transform _currentTarget;
        private HitCollider _hitCollider;

        public Flags CharacterFlag => _characterFlag;
        public Transform CurrentTarget => _currentTarget;

        [Inject]
        public void Construct(CharacterHandler characterHandler, HitCollider hitCollider)
        {
            _characterHandler = characterHandler;
            _hitCollider = hitCollider;

            Vector2[] patrolPoints2D = ConvertTransformToVector2();

            _attackState = new CharacterAttackState(characterHandler, this, _hitCollider);

            _states.Add(new CharacterPatrolState(characterHandler, this, patrolPoints2D));
            _states.Add(_attackState);
            _states.Add(new CharacterChaseState(characterHandler, this));


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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Character"))
            {
                if (other.TryGetComponent(out IEntity entity))
                {
                    Flags flags = entity.ProvideComponent<Flags>();

                    if (flags != _characterFlag)
                    {
                        if (_currentState is CharacterChaseState && _currentTarget != null) return;

                        _currentTarget = other.transform;
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.transform == _currentTarget)
            {
                _currentTarget = null;
            }
        }

    }
}
