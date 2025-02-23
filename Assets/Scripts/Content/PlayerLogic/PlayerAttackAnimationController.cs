using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class PlayerAttackAnimationController
    {
        private Animator _animator;
        private CharacterJumpHandler _jumpHandler;

        public PlayerAttackAnimationController(Animator animator, CharacterJumpHandler jumpHandler)
        {
            _animator = animator;
            _jumpHandler = jumpHandler;
        }

        public void PlayAttackAnimation(PlayerWeapon playerWeapon)
        {
            WeaponAnimationType animationType = playerWeapon.PlayerWeaponType;

            if (!_jumpHandler.IsGrounded)
                return;

            switch (animationType)
            {
                case WeaponAnimationType.Spike:
                    _animator.SetTrigger(AnimatorHashes.SpikeAttackTrigger);
                    break;
                case WeaponAnimationType.Areal:
                    _animator.SetTrigger(AnimatorHashes.ArealAttackTrigger);
                    break;
                case WeaponAnimationType.Simple:
                    _animator.SetTrigger(AnimatorHashes.SimpleAttackTrigger);
                    break;
            }

        }
    }
}
