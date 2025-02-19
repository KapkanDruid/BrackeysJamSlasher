using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public class PopupText : MonoBehaviour
    {
        [SerializeField] private PopupTextConfig _animatorConfig;
        [SerializeField] private TextMeshPro _textMeshPro;
        [SerializeField] private RectTransform _rectTransform;

        private Sequence _tweenSequence;

        private Vector2 _prefabScale;

        private void Start()
        {
            _prefabScale = _rectTransform.sizeDelta;
        }

        public void Activate(Vector2 position, string massage)
        {
            _textMeshPro.rectTransform.position = position;
            _textMeshPro.text = massage;

            _rectTransform.localScale = Vector3.one * _animatorConfig.StartScale;

            Color newColor = _textMeshPro.color;
            newColor.a = 1f;
            _textMeshPro.color = newColor;

            _tweenSequence = DOTween.Sequence()
                .Append(_rectTransform.DOScale(_animatorConfig.ScaleMultiplier, _animatorConfig.ScaleChangeTime))
                .Append(_textMeshPro.DOFade(0, _animatorConfig.FadeTime))
                .Join(_rectTransform.DOMove(_rectTransform.position + _animatorConfig.FadePosition, _animatorConfig.FadeTime))
                .AppendCallback(OnAnimationEnd);
        }

        private void OnAnimationEnd() => gameObject.SetActive(false);

        private void OnDisable() => _tweenSequence?.Kill();

        private void OnDestroy() => _tweenSequence?.Kill();
    }
}
