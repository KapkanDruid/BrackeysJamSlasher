using Assets.Scripts.Content.CoreProgression;
using System;
using UnityEngine;

namespace Assets.Scripts.Content
{
    [Serializable]
    public class DirectProjectileData
    {
        [SerializeField] private EntityFlags _enemyFlag;
        [SerializeField] private float _damage;
        [SerializeField] private float _speed;
        [SerializeField] private float _lifeTime;

        public EntityFlags EnemyFlag => _enemyFlag;
        public float Damage => StaticData.DamageRangeSausage;
        public float Speed => _speed;
        public float LifeTime => _lifeTime;
    }
}
