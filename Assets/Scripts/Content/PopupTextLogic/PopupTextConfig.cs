using UnityEngine;

namespace Assets.Scripts.Content
{
    [CreateAssetMenu(fileName = "PopupTextConfig", menuName = "Slasher/PopupTextConfig")]
    public class PopupTextConfig : ScriptableObject
    {
        [SerializeField] private float _startScale;
        [SerializeField] private float _scaleMultiplier;
        [SerializeField] private float _scaleChangeTime;
        [SerializeField] private float _fadeTime;
        [SerializeField] private Vector3 _fadePosition;

        public float ScaleMultiplier => _scaleMultiplier;
        public float ScaleChangeTime => _scaleChangeTime;
        public Vector3 FadePosition => _fadePosition;
        public float FadeTime => _fadeTime;
        public float StartScale => _startScale;
    }
}
