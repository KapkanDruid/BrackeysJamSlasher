using UnityEngine;

namespace Assets.Scripts.Content.PlayerLogic
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Slasher/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _maxHealth;
        [SerializeField, Range(0, 100)] private float _healPercent;
        [SerializeField] private float _jumpHeight;
        [SerializeField] private float _jumpDuration;
        [SerializeField, Range(0,100)] private float _criticalChancePercent;
        [SerializeField] private float _criticalMultiplier;
        [SerializeField] private float _invincibleFramesDuration;
        [SerializeField, Range(0, 100)] private float _dodgeChancePercent;

        public float Speed => _speed;
        public float MaxHealth => _maxHealth;
        public float HealPercent => _healPercent;
        public float JumpHeight => _jumpHeight;
        public float CriticalChance => _criticalChancePercent;
        public float CriticalMultiplier => _criticalMultiplier;
        public float JumpDuration => _jumpDuration;
        public float DodgeChancePercent => _dodgeChancePercent;
        public float InvincibleFramesDuration => _invincibleFramesDuration;
    }
}