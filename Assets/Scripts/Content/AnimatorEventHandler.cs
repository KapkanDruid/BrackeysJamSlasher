using System;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public class AnimatorEventHandler : MonoBehaviour
    {
        public event Action OnAnimationHit;

        public void HitEventHandle()
        {
            OnAnimationHit?.Invoke();
        }
    }
}
