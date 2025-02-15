using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Slasher/PlayerConfig")]
public class PlayerConfig : ScriptableObject
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
}
