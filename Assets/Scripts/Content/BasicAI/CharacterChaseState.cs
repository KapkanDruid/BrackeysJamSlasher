using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterChaseState : ICharacterState
    {
        private readonly CharacterHandler _character;
        private Transform _target;

        public CharacterChaseState(CharacterHandler character)
        {
            _character = character;
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void EnterState()
        {
            Debug.Log($"{_character.name} вошел в режим атаки.");
        }

        public void UpdateState()
        {
            if (_target == null) return;
            _character.MoveTo(_target.position);

            if (Vector3.Distance(_character.transform.position, _target.position) < 2f)
            {
                _character.Attack();
            }
        }

        public void ExitState()
        {
            Debug.Log($"{_character.name} покидает режим атаки.");
        }
    }
}