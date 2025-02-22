using System;
using UnityEngine;

namespace Assets.Scripts.Content.CoreProgression
{
    [Serializable]
    public class ProgressValueByRate
    {
        [SerializeField] private SuccessRate _successRate;
        [SerializeField] private float _increaseValue;
        [SerializeField] private Sprite _rateSprite;

        public SuccessRate SuccessRate => _successRate;
        public float IncreaseValue => _increaseValue;
        public Sprite RateSprite { get => _rateSprite; set => _rateSprite = value; }
    }
}
