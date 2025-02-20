using Assets.Scripts.Architecture;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterChaseState : ICharacterState, IGizmosDrawer
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterAttackState _attackState;
        private readonly CharacterSensor _sensor;
        private readonly Animator _animator;
        private Transform _target;
        private RaycastHit2D[] _hits;
        private IDamageable _damageable;
        private Transform _entityTransform;
        private float _distanceToChase = 0.4f;

        private Vector2 ColliderOffset => new Vector2(_character.CharacterData.HitColliderOffset.x * _character.CurrentOrientation, _character.CharacterData.HitColliderOffset.y);

        public CharacterChaseState(CharacterHandler character, CharacterStateMachine stateMachine, CharacterSensor sensor, Animator animator, CharacterAttackState attackState, GizmosDrawer gizmosDrawer)
        {
            _character = character;
            _stateMachine = stateMachine;
            _sensor = sensor;
            _animator = animator;
            _attackState = attackState;
            gizmosDrawer.AddGizmosDrawer(this);
        }

        public void EnterState()
        {
            _target = _sensor.TargetTransform; 
        }

        public void UpdateState()
        {
            SetDirection();

            _stateMachine.SetTarget(_target);

            if (_stateMachine.CurrentTarget == null)
            {
                _stateMachine.SetState<CharacterPatrolState>();
            }

            CheckDistanceToTarget();

            RaycastAttackArea();
            TryHitTarget();
        }

        public void ExitState()
        {

        }

        private void SetDirection()
        {
            float direction = Mathf.Sign(_target.position.x - _character.transform.position.x);
            _character.SetOrientation((int)direction);
        }
        
        private void CheckDistanceToTarget()
        {
            if (Vector2.Distance(_character.transform.position, _target.position) > _distanceToChase && !_character.IsKnocked)
            {
                _animator.SetBool(AnimatorHashes.IsMoving, true);
                if (_target == null) return;
                _character.MoveTo(_target.position);
            }
        }

        private void RaycastAttackArea()
        {
            Vector2 origin = (Vector2)_character.transform.position + ColliderOffset;
            Vector2 direction = Vector2.down;
            Vector2 size = _character.CharacterData.HitColliderSize;

            _hits = Physics2D.BoxCastAll(origin, size, 0, direction);
        }
        
        private void TryHitTarget()
        {
            //if (!_canAttack)
            //    return;
            _animator.SetTrigger(AnimatorHashes.SpikeAttackTrigger);

            CheckRaycast(_hits);
        }

        private void CheckRaycast(RaycastHit2D[] hits)
        {

            int count = hits.Length;
            for (int i = 0; i < count; i++)
            {
                if (!hits[i].collider.TryGetComponent(out IEntity entity))
                    continue;

                if (entity == _character.CharacterData.ThisEntity)
                    continue;

                Flags flags = entity.ProvideComponent<Flags>();

                if (flags == null)
                    continue;

                if (!flags.Contain(_character.CharacterData.EnemyFlag))
                    continue;

                _damageable = entity.ProvideComponent<IDamageable>();

                if (_damageable == null)
                    continue;

                _entityTransform = entity.ProvideComponent<Transform>();

                Debug.Log($"_entityTransform: {_entityTransform.gameObject.name} {_entityTransform == null}");

                if (!_character.IsKnocked)
                {
                    _animator.SetBool(AnimatorHashes.IsMoving, false);

                    _attackState.GetTarget(_damageable);
                    _stateMachine.SetState<CharacterAttackState>();

                }
            }
        }

        public void OnDrawGizmos()
        {
            if (_character.CharacterData == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_character.transform.position, _character.CharacterData.SensorRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)_character.transform.position + ColliderOffset, _character.CharacterData.HitColliderSize);
        }
    }
}
