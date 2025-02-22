using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Content
{
    public class StopwatchTimer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;
        [SerializeField] private float _showSpeed = 0.5f;

        private float _elapsedTime = 0f;
        private bool _isRunning = false;
        private RectTransform _rectTransform;
        private Vector2 _targetPosition;
        private Vector2 _startPosition;

        public float ElapsedTime  => _elapsedTime;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            _targetPosition = _rectTransform.anchoredPosition;

            _startPosition = _targetPosition + new Vector2(0, _rectTransform.rect.height * 2);

            _rectTransform.anchoredPosition = _startPosition;

            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_isRunning)
            {
                _elapsedTime += Time.deltaTime;
                UpdateTimerText();
            }
        }

        public void StartTimer()
        {
            UpdateTimerText();
            _isRunning = true;
            Show();
        }

        private void Show()
        {
            gameObject.SetActive(true);

            _rectTransform.DOAnchorPos(_targetPosition, _showSpeed)
                .SetEase(Ease.OutExpo);
        }

        private void Hide()
        {
            _rectTransform.DOAnchorPos(_startPosition, _showSpeed)
                .SetEase(Ease.OutExpo).OnComplete(() => gameObject.SetActive(false));
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public float ResetTimer()
        {
            Hide();
            _isRunning = false;
            var passedTime = _elapsedTime;

            _elapsedTime = 0f;

            return passedTime;
        }

        private void UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(_elapsedTime / 60);
            int seconds = Mathf.FloorToInt(_elapsedTime % 60);
            _timerText.text = $"{minutes:D2}:{seconds:D2}"; // Формат MM:SS
        }
    }
}
