﻿using System;
using Assets.Scripts.Architecture;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerAttackHandler : IDisposable, IGizmosDrawer
    {
        private readonly PlayerData _data;
        private readonly Animator _animator;
        private readonly InputSystemActions _inputActions;
        private readonly SpriteRenderer _weaponSpriteRenderer;
        private readonly CharacterJumpHandler _jumpHandler;
        private readonly PlayerAttackAnimationController _playerAttackAnimationController;

        private bool _isAttacking;

        public PlayerAttackHandler(
            InputSystemActions inputActions,
            PlayerAttackAnimationController playerAttackAnimationController,
            PlayerData data,
            Animator animator,
            CharacterJumpHandler jumpHandler)
        {
            _data = data;
            _animator = animator;
            _jumpHandler = jumpHandler;
            _inputActions = inputActions;
            _playerAttackAnimationController = playerAttackAnimationController;

            _weaponSpriteRenderer = _data.WeaponSpriteRenderer;

            _inputActions.Player.Attack.performed += OnAttack;

            _inputActions.Player.Attack.canceled += context => _isAttacking = false;
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (!IsAttackAllowed())
                return;

            _isAttacking = true;

            _playerAttackAnimationController.PlayAttackAnimation(_data.CurrentPlayerWeapon);

            if (_data.WeaponSprite != null)
            {
                _weaponSpriteRenderer.sprite = _data.WeaponSprite;
            }

            EnableAttackCollider();
        }

        private bool IsAttackAllowed()
        {
            if (_animator.GetBool(AnimatorHashes.IsAttacking))
                return false;

            if (!_jumpHandler.IsGrounded)
                return false;

            return true;
        }

        private void EnableAttackCollider()
        {
            Vector2 origin = (Vector2)_data.PlayerTransform.position + _data.WeaponColliderOffset;
            Vector2 size = _data.WeaponColliderSize;
            Vector2 direction = Vector2.down;

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

        public void OnDrawGizmos()
        {
            if (_data == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)_data.PlayerTransform.position + _data.WeaponColliderOffset, _data.WeaponColliderSize);

            if (!_isAttacking)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawCube((Vector2)_data.PlayerTransform.position + _data.WeaponColliderOffset, _data.WeaponColliderSize);
        }

        public void Dispose()
        {
            _inputActions.Player.Attack.performed -= OnAttack;
        }

    }
}
