using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class TakeDamageAnimationState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorHashes.IsTakingDamage, true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorHashes.IsTakingDamage, false);
        }
    }
}