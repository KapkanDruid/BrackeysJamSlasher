using UnityEngine;

namespace Assets.Scripts.Content
{
    public static class AnimatorHashes
    {
        public static readonly int SimpleAttackTrigger = Animator.StringToHash("SimpleAttack");
        public static readonly int ArealAttackTrigger = Animator.StringToHash("ArealAttack");
        public static readonly int SpikeAttackTrigger = Animator.StringToHash("SpikeAttack");
        public static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public static readonly int JumpTrigger = Animator.StringToHash("Jump");
        public static readonly int IsLanding = Animator.StringToHash("IsLanding");
        public static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        public static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    }
}
