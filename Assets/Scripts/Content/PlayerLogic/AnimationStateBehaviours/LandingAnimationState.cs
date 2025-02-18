using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    public class LandingAnimationState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorHashes.IsLanding, true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool(AnimatorHashes.IsLanding, false);
        }
    }
}