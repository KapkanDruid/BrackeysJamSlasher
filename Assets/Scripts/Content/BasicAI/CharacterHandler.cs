using Assets.Scripts.Content.PlayerLogic;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content.BasicAI
{
    public class CharacterHandler : MonoBehaviour, IEntity
    {
        [SerializeField] private CharacterData _data;
        [SerializeField] private ProjectileTrapData _projectileData;


        private CustomPool<DirectProjectile> _projectilePool;

        private EnemyDeadHandler _enemyDeadHandler;
        private CharacterHealthHandler _healthHandler;
        private CancellationToken _cancellationToken;
        private bool _isKnocked = false;
        private bool _isReadyToShoot;
        private int _currentOrientation = 1;

        public CancellationToken CancellationToken => _cancellationToken;
        public CharacterData CharacterData => _data;
        public bool IsKnocked { get => _isKnocked; set => _isKnocked = value; }
        public int CurrentOrientation => _currentOrientation;


        [Inject]
        public void Construct(CharacterHealthHandler healthHandler, EnemyDeadHandler enemyDeadHandler)
        {
            _healthHandler = healthHandler;
            _data.ThisEntity = this;
            _enemyDeadHandler = enemyDeadHandler;
            _enemyDeadHandler.Initialize(transform);

            _cancellationToken = this.GetCancellationTokenOnDestroy();
            _currentOrientation = Mathf.Clamp(Mathf.RoundToInt(transform.localScale.x), -1, 1);

            _projectilePool = new CustomPool<DirectProjectile>(_projectileData.ProjectilePrefab, _projectileData.ProjectilePoolCount, "Guts");

            _isReadyToShoot = true;

        }

        public void MoveTo(Vector3 target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _data.Speed * Time.deltaTime);
        }

        public T ProvideComponent<T>() where T : class
        {
            if (_data.Flags is T flags)
                return flags;

            if (_healthHandler is T healthHandler)
                return healthHandler;
            
            if (transform is T characterTransform)
                return characterTransform;

            if (_enemyDeadHandler is T deadHandler)
                return deadHandler;

            return null;
        }

        public void SetOrientation(int direction)
        {
            Vector3 rightOrientation = new Vector3(1, transform.localScale.y, transform.localScale.z);
            Vector3 leftOrientation = new Vector3(-1, transform.localScale.y, transform.localScale.z);

            if (direction > 0)
            {
                _currentOrientation = 1;
                transform.localScale = rightOrientation;
            }
            else if (direction < 0)
            {
                _currentOrientation = -1;
                transform.localScale = leftOrientation;
            }
        }

        public void Shoot(Vector2 direction)
        {
            if (!_isReadyToShoot)
                return;

            if (direction == Vector2.zero)
                return;

            _isReadyToShoot = false;
            var projectile = _projectilePool.Get();
            projectile.Prepare(_projectileData.ShootPoint.position, direction, _projectileData.ProjectileData);

            RechargeRangeAttack().Forget();

            
        }

        private async UniTaskVoid RechargeRangeAttack()
        {
            try
            {
                await UniTask.WaitForSeconds(_projectileData.FireRate, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException)
            {
                return;
            }

            _isReadyToShoot = true;
        }

        private void Update()
        {
            if (_healthHandler.Health == 0)
            {
                _enemyDeadHandler.Death();
            }
            
        }
    }
}

