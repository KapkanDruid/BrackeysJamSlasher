using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class AttackAnimationState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorHashes.IsAttacking, true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorHashes.IsAttacking, false);
        }
    }
}