using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Content
{
    public class PausePopup : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private float _showSpeed = 0.5f;

        private RectTransform _rectTransform;

        private Vector2 _startPosition;
        private Vector2 _targetPosition;

        public void Initialize(Action exitAction, Action resumeAction)
        {
            _rectTransform = GetComponent<RectTransform>();

            _resumeButton.onClick.AddListener(() => resumeAction?.Invoke());
            _exitButton.onClick.AddListener(() => exitAction?.Invoke());

            _targetPosition = _rectTransform.anchoredPosition;

            _startPosition = _targetPosition - new Vector2(0, Screen.height + _rectTransform.rect.height / 2);

            _rectTransform.anchoredPosition = _startPosition;

            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);

            _rectTransform.DOAnchorPos(_targetPosition, _showSpeed)
                .SetEase(Ease.OutExpo).SetUpdate(true);

            Time.timeScale = 0;
        }

        public void Hide()
        {
            Time.timeScale = 1;

            _rectTransform.DOAnchorPos(_startPosition, _showSpeed)
                .SetEase(Ease.OutExpo).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveAllListeners();
            _resumeButton.onClick.RemoveAllListeners();
        }
    }
}
