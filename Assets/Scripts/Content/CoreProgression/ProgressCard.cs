using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Content.CoreProgression
{
    public class ProgressCard : MonoBehaviour
    {
        private ProgressCardData _data;

        [SerializeField] private Image _mainImage;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _iconText;
        [SerializeField] private Button _button;

        [SerializeField] private RectTransform[] _enableObjectsA;
        [SerializeField] private RectTransform[] _enableObjectsB;
        [SerializeField] private RectTransform[] _enableObjectsC;

        private SuccessRate _currentSuccessRate;
        private float _currentIncreaseValue;

        public event Action<ProgressValue> OnProgressDetermined;

        private ProgressValue _currentProgressValue;
        public Button Button => _button;

        private void Start()
        {
            _button.onClick.AddListener(() => OnProgressDetermined?.Invoke(_currentProgressValue));
        }

        public void Enable(SuccessRate successRate, ProgressCardData data)
        {
            _currentSuccessRate = successRate;
            _data = data;

            _button.interactable = true;

            foreach (var value in _data.ProgressValues)
            {
                if (value.SuccessRate == _currentSuccessRate)
                {
                    _mainImage.sprite = value.RateSprite;
                    _iconText.text = value.CardText;
                    _currentIncreaseValue = value.IncreaseValue;
                }
            }

            _icon.sprite = _data.ValueIcon;
            _currentProgressValue = new ProgressValue(_data.ProgressType, _currentIncreaseValue);

            DisableArray(_enableObjectsA);
            DisableArray(_enableObjectsB);
            DisableArray(_enableObjectsC);

            switch (_currentSuccessRate)
            {
                case SuccessRate.A:
                    EnableArray(_enableObjectsA);
                    break;

                case SuccessRate.B:
                    EnableArray(_enableObjectsB);
                    break;

                case SuccessRate.C:
                    EnableArray(_enableObjectsC);
                    break;
            }
        }

        private void EnableArray(RectTransform[] rectTransforms)
        {
            foreach (var reactTransform in rectTransforms)
                reactTransform.gameObject.SetActive(true);
        }

        private void DisableArray(RectTransform[] rectTransforms)
        {
            foreach (var reactTransform in rectTransforms)
                reactTransform.gameObject.SetActive(false);
        }

        public void Disable()
        {
            _button.interactable = false;
        }
    }
}
