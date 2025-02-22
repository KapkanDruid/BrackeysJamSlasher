using System;
using UnityEngine;

namespace Assets.Scripts.Content.CoreProgression
{
    [Serializable]
    public class ProgressCardData
    {
        [SerializeField] private ProgressType _progressType;
        [SerializeField] private ProgressValueByRate[] _progressValues;
        [SerializeField] private Sprite _valueIcon;

        public ProgressValueByRate[] ProgressValues => _progressValues;
        public ProgressType ProgressType => _progressType;
        public Sprite ValueIcon => _valueIcon;
    }
}
