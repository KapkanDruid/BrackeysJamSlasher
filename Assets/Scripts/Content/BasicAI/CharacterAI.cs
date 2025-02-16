using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterAI : MonoBehaviour
    {
        [SerializeField] private Transform[] patrolPoints;
        [SerializeField] private Flags _characterFlag;

        private ICharacterState _currentState;
        private CharacterPatrolState _patrolState;
        private CharacterChaseState _chaseState;
        private CharacterHandler _characterHandler;
        private Transform _currentTarget;

        [Inject]
        public void Construct(CharacterHandler characterHandler)
        {
            _characterHandler = characterHandler;
            _patrolState = new CharacterPatrolState(characterHandler, patrolPoints);
            _chaseState = new CharacterChaseState(characterHandler);

            SetState(_patrolState);
        }

        private void Update()
        {
            _currentState?.UpdateState();
        }

        public void SetState(ICharacterState newState)
        {
            _currentState?.ExitState();
            _currentState = newState;
            _currentState.EnterState();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEntity entity))
            {
                Debug.Log($"{other} вошёл в область, его флаг {entity.ProvideComponent<Flags>()}");

                Flags flags = entity.ProvideComponent<Flags>();

                if (flags != _characterFlag)
                {
                    if (_currentState is CharacterChaseState && _currentTarget != null) return;

                    _currentTarget = other.transform;
                    _chaseState.SetTarget(other.transform);
                    SetState(_chaseState);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.transform == _currentTarget)
            {
                _currentTarget = null;
                SetState(_patrolState);
            }
        }
    }
}