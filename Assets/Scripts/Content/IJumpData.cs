using UnityEngine;

namespace Assets.Scripts.Content
{
    public interface IJumpData
    {
        public float JumpHeight { get; }
        public float JumpDuration { get; }
        public Transform ViewObjectTransform { get; }
        public Transform ShadowTransform { get; }
    }
}
