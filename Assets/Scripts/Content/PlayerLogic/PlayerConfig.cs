using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Slasher/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHealth;

    public float Speed => _speed;
    public float MaxHealth => _maxHealth;
}
