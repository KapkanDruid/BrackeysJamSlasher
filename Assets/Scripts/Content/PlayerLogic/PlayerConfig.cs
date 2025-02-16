using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Slasher/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpDuration;

    public float Speed => _speed;
    public float MaxHealth => _maxHealth;
    public float JumpHeight => _jumpHeight;
    public float JumpDuration => _jumpDuration;
}
