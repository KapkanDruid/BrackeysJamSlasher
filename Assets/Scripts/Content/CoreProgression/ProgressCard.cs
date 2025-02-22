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
                    _iconText.text = value.IncreaseValue.ToString();
                    _currentIncreaseValue = value.IncreaseValue;
                }
            }

            _icon.sprite = _data.ValueIcon;
            _currentProgressValue = new ProgressValue(_data.ProgressType, _currentIncreaseValue);

            switch (_currentSuccessRate)
            {
                case SuccessRate.A:
                    foreach (var reactTransform in _enableObjectsA)
                        reactTransform.gameObject.SetActive(true);
                    break;

                case SuccessRate.B:
                    foreach (var reactTransform in _enableObjectsB)
                        reactTransform.gameObject.SetActive(true);
                    break;

                case SuccessRate.C:
                    foreach (var reactTransform in _enableObjectsC)
                        reactTransform.gameObject.SetActive(true);
                    break;
            }
        }
        public void Disable()
        {
            _button.interactable = false;
        }
    }
}
