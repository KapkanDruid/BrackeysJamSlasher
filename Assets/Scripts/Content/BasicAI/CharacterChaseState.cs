using Assets.Scripts.Architecture;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterChaseState : ICharacterState, IGizmosDrawer
    {
        private readonly CharacterHandler _character;
        private readonly CharacterStateMachine _stateMachine;
        private readonly CharacterSensor _sensor;
        private readonly Animator _animator;
        private readonly Vector2[] _patrolPoints;
        private Transform _target;
        private RaycastHit2D[] _hits;
        private float _distanceToChase = 0.4f;
        private float direction;
        private bool _canMoving;
        private bool _isAttacking;

        private Vector2 ColliderOffset => new Vector2(_character.CharacterData.HitColliderOffset.x * _character.CurrentOrientation, _character.CharacterData.HitColliderOffset.y);
        private Vector2 SplashColliderOffset => new Vector2(_character.CharacterData.HitSplashColliderOffset.x * _character.CurrentOrientation, _character.CharacterData.HitSplashColliderOffset.y);

        public CharacterChaseState(CharacterHandler character,
                                   CharacterStateMachine stateMachine,
                                   CharacterSensor sensor,
                                   Animator animator,
                                   GizmosDrawer gizmosDrawer, 
                                   Vector2[] patrolPoints)
        {
            _character = character;
            _stateMachine = stateMachine;
            _sensor = sensor;
            _animator = animator;
            _patrolPoints = patrolPoints;
            gizmosDrawer.AddGizmosDrawer(this);

        }

        public void EnterState()
        {
            _canMoving = true;
            _isAttacking = false;
        }

        public void UpdateState()
        {
            _target = _sensor.TargetTransform;
            SetDirection();

            _stateMachine.SetTarget(_target);

            if (_stateMachine.CurrentTarget == null)
            {
                _stateMachine.SetState<CharacterPatrolState>();
            }

            RangeAttack();

            CheckDistanceToTarget();
            CheckRaycast();
        }

        public void ExitState()
        {

        }

        public void OnDrawGizmos()
        {
            if (_character.CharacterData == null)
                return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_character.transform.position, _character.CharacterData.SensorRadius);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)_character.transform.position + ColliderOffset, _character.CharacterData.HitColliderSize);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube((Vector2)_character.transform.position + SplashColliderOffset, _character.CharacterData.HitSplashColliderSize);
        }

        private void RangeAttack()
        {
            if (_character.CharacterData.HasRangeAttack)
            {
                if (_character.transform.position.y == _target.position.y)
                {
                    _animator.SetTrigger(AnimatorHashes.RangeAttackTrigger);
                    _character.Shoot(new(_character.CurrentOrientation, 0f));
                }
            }
        }

        private void SetDirection()
        {
            direction = Mathf.Sign(_target.position.x - _character.transform.position.x);
            _character.SetOrientation((int)direction);
        }

        private void CheckDistanceToTarget()
        {
            if (Vector2.Distance(_character.transform.position, _target.position) > _distanceToChase && !_character.IsKnocked)
            {
                if (_canMoving)
                {
                    _animator.SetBool(AnimatorHashes.IsMoving, true);
                    if (_character.CharacterData.HasRangeAttack)
                    {
                        _character.MoveTo(new Vector2(_patrolPoints[0].x, _target.position.y));
                    }
                    else
                    {
                        _character.MoveToRandom(_target.position);
                    }

                }
            }
        }

        private void CheckRaycast()
        {
            Vector2 origin = (Vector2)_character.transform.position + ColliderOffset;
            Vector2 direction = Vector2.zero;
            Vector2 size = _character.CharacterData.HitColliderSize;

            _hits = Physics2D.BoxCastAll(origin, size, 0, direction);

            int count = _hits.Length;
            for (int i = 0; i < count; i++)
            {
                if (!_hits[i].collider.TryGetComponent(out IEntity entity))
                    continue;

                if (entity == _character.CharacterData.ThisEntity)
                    continue;

                Flags flags = entity.ProvideComponent<Flags>();

                if (flags == null)
                    continue;

                if (!flags.Contain(_character.CharacterData.EnemyFlag))
                    continue;

                _canMoving = false;
                _animator.SetBool(AnimatorHashes.IsMoving, false);

                if (_isAttacking)
                    return;
                _isAttacking = true;

                _stateMachine.SetState<CharacterAttackState>();
            }
        }
    }
}
