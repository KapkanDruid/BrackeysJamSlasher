using UnityEngine;

namespace Assets.Scripts.Content.CoreProgression
{
    [CreateAssetMenu(fileName = "ProgressCardsConfig", menuName = "Slasher/ProgressCardsConfig")]
    public class ProgressCardsConfig : ScriptableObject
    {
        [SerializeField] private ProgressCardData _leftCardData;
        [SerializeField] private ProgressCardData _centralCardData;
        [SerializeField] private ProgressCardData _rightCardData;

        public ProgressCardData LeftCardData => _leftCardData;
        public ProgressCardData CentralCardData => _centralCardData;
        public ProgressCardData RightCardData => _rightCardData;
    }
}
