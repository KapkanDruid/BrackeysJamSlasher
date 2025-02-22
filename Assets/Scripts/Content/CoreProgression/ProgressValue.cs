namespace Assets.Scripts.Content.CoreProgression
{
    public class ProgressValue
    {
        public readonly ProgressType ProgressType;
        public readonly float IncreaseValue;

        public ProgressValue(ProgressType progressType, float increaseValue)
        {
            ProgressType = progressType;
            IncreaseValue = increaseValue;
        }
    }
}
