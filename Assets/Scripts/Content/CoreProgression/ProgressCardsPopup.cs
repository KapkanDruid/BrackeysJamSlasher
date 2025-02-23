using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.CoreProgression
{
    public class ProgressCardsPopup : MonoBehaviour
    {
        [SerializeField] private ProgressCard _leftCard;
        [SerializeField] private ProgressCard _centralCard;
        [SerializeField] private ProgressCard _rightCard;
 
        [SerializeField] private float _showSpeed;

        private PlayerProgressController _progressController;
        private RectTransform _rectTransform;
        private Vector2 _targetPosition;
        private Vector2 _startPosition;

        [Inject]
        private void Construct(PlayerProgressController playerProgressController)
        {
            _progressController = playerProgressController;
        }

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();

            _targetPosition = _rectTransform.anchoredPosition;

            _startPosition = _targetPosition - new Vector2(0, Screen.height);

            _rectTransform.anchoredPosition = _startPosition;

            gameObject.SetActive(false);

            SetProgressValue();
        }

        private void SetProgressValue()
        {
            _leftCard.OnProgressDetermined += OnCardChosen;
            _centralCard.OnProgressDetermined += OnCardChosen;
            _rightCard.OnProgressDetermined += OnCardChosen;
        }

        public void Show(SuccessRate successRate, ProgressCardsConfig progressCardsConfig)
        {
            _leftCard.Enable(successRate, progressCardsConfig.LeftCardData);
            _centralCard.Enable(successRate, progressCardsConfig.CentralCardData);
            _rightCard.Enable(successRate, progressCardsConfig.RightCardData);

            _rectTransform.gameObject.SetActive(true);

            _rectTransform.DOAnchorPos(_targetPosition, _showSpeed)
                .SetEase(Ease.OutExpo).SetUpdate(true);

            Time.timeScale = 0;
        }


        private void OnCardChosen(ProgressValue progressValues)
        {
            _progressController.DetermineProgress(progressValues);

            Time.timeScale = 1;
            Hide();
        }

        private void Hide()
        {
            _rectTransform.DOAnchorPos(_startPosition, _showSpeed)
                .SetEase(Ease.OutExpo);

            _leftCard.Disable();
            _centralCard.Disable();
            _rightCard.Disable();
        }

        private void OnDestroy()
        {
            _leftCard.OnProgressDetermined -= OnCardChosen;
            _centralCard.OnProgressDetermined -= OnCardChosen;
            _rightCard.OnProgressDetermined -= OnCardChosen;
        }
    }
}
