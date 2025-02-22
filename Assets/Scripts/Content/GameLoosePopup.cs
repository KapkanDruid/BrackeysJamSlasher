using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Content
{
    public class GameLoosePopup : MonoBehaviour
    {
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private float _showSpeed = 0.5f;

        private RectTransform _rectTransform;

        private Vector2 _startPosition;
        private Vector2 _targetPosition;

        public void Initialize(Action exitAction, Action retryAction)
        {
            _rectTransform = GetComponent<RectTransform>();

            _tryAgainButton.onClick.AddListener(() => retryAction?.Invoke());
            _exitButton.onClick.AddListener(() => exitAction?.Invoke());

            _targetPosition = _rectTransform.anchoredPosition;

            _startPosition = _targetPosition - new Vector2(0, Screen.height);

            _rectTransform.anchoredPosition = _startPosition;

            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            _rectTransform.DOAnchorPos(_targetPosition, _showSpeed)
                .SetEase(Ease.OutExpo);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveAllListeners();
            _tryAgainButton.onClick.RemoveAllListeners();
        }
    }
}
