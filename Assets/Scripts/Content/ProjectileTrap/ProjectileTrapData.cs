using System;
using UnityEngine;

namespace Assets.Scripts.Content
{
    [Serializable]
    public class ProjectileTrapData
    {
        [SerializeField] private DirectProjectile _projectilePrefab;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Vector2 _shootDirection;
        [SerializeField] private int _projectilePoolCount;
        [SerializeField] private float _fireRate;

        [SerializeField] private DirectProjectileData _projectileData;

        public DirectProjectile ProjectilePrefab => _projectilePrefab;
        public DirectProjectileData ProjectileData => _projectileData;
        public int ProjectilePoolCount => _projectilePoolCount;
        public Transform ShootPoint => _shootPoint;
        public Vector2 ShootDirection => _shootDirection;
        public float FireRate => _fireRate;
    }
}
