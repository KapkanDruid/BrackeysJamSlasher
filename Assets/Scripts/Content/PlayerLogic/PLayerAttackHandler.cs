using System;
using Assets.Scripts.Architecture;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerAttackHandler : IDisposable, IGizmosDrawer
    {
        private readonly PlayerData _data;
        private readonly Animator _animator;
        private readonly InputSystemActions _inputActions;
        private readonly CharacterJumpHandler _jumpHandler;
        private readonly SpriteRenderer _weaponSpriteRenderer;
        private readonly PlayerMoveHandler _playerMoveHandler;
        private readonly PlayerHealthHandler _playerHealthHandler;
        private readonly AnimatorEventHandler _animatorEventHandler;
        private readonly PlayerAttackAnimationController _playerAttackAnimationController;

        private bool _canDrawAttackCollider;
        private Vector2 ColliderOffset => new Vector2(_data.WeaponColliderOffset.x * _playerMoveHandler.CurrentOrientation, _data.WeaponColliderOffset.y);

        public PlayerAttackHandler(
            InputSystemActions inputActions,
            PlayerAttackAnimationController playerAttackAnimationController,
            PlayerData data,
            Animator animator,
            CharacterJumpHandler jumpHandler,
            AnimatorEventHandler animatorEventHandler,
            PlayerHealthHandler playerHealthHandler,
            PlayerMoveHandler playerMoveHandler)
        {
            _data = data;
            _animator = animator;
            _jumpHandler = jumpHandler;
            _inputActions = inputActions;
            _animatorEventHandler = animatorEventHandler;
            _playerAttackAnimationController = playerAttackAnimationController;

            _weaponSpriteRenderer = _data.WeaponSpriteRenderer;

            _inputActions.Player.Attack.performed += OnAttack;

            _animatorEventHandler.OnAnimationHit += OnAnimationHit;
            _playerHealthHandler = playerHealthHandler;
            _playerMoveHandler = playerMoveHandler;
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (!IsAttackAllowed())
                return;

            _playerAttackAnimationController.PlayAttackAnimation(_data.CurrentPlayerWeapon);

            if (_data.WeaponSprite != null)
            {
                _weaponSpriteRenderer.sprite = _data.WeaponSprite;
            }
        }

        private bool IsAttackAllowed()
        {
            if (_animator.GetBool(AnimatorHashes.IsAttacking))
                return false;

            if (!_jumpHandler.IsGrounded)
                return false;

            if (_playerHealthHandler.IsDead)
                return false;

            return true;
        }

        private void OnAnimationHit()
        {
            EnableAttackCollider();
            _canDrawAttackCollider = true;

            DisableColliderDrawing().Forget();
        }

        private void EnableAttackCollider()
        {
            Vector2 origin = (Vector2)_data.PlayerTransform.position + ColliderOffset;

            Vector2 size = _data.WeaponColliderSize;
            Vector2 direction = Vector2.zero;

            RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, 0, direction);

            int count = hits.Length;
            for (int i = 0; i < count; i++)
            {
                if (!hits[i].collider.TryGetComponent(out IEntity entity))
                    continue;

                if (entity == _data.ThisEntity)
                    continue;

                Flags flags = entity.ProvideComponent<Flags>();

                if (flags == null)
                    continue;

                if (!flags.Contain(_data.EnemyFlag))
                    continue;

                IDamageable damageable = entity.ProvideComponent<IDamageable>();

                damageable.TakeDamage(_data.CurrentPlayerWeapon.Damage);
            }
        }

        public void Dispose()
        {
            _inputActions.Player.Attack.performed -= OnAttack;
            _animatorEventHandler.OnAnimationHit -= OnAnimationHit;
        }

        private async UniTask DisableColliderDrawing()
        {
            try
            {
                await UniTask.WaitForSeconds(0.05f, cancellationToken: _data.CancellationToken);
            }
            catch (OperationCanceledException)
            {
                return;
            }

            _canDrawAttackCollider = false;
        }

        public void OnDrawGizmos()
        {
            if (_data == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)_data.PlayerTransform.position + ColliderOffset, _data.WeaponColliderSize);

            if (!_canDrawAttackCollider)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawCube((Vector2)_data.PlayerTransform.position + ColliderOffset, _data.WeaponColliderSize);
        }
    }
}
