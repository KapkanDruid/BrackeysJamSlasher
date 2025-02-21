using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content
{
    public class ProjectileTrap : MonoBehaviour
    {
        [SerializeField] private ProjectileTrapData _data;
        private MainSceneBootstrap _sceneBootstrap;

        private CustomPool<DirectProjectile> _projectilePool;

        private bool _isReadyToShoot;

        [Inject]
        private void Construct(MainSceneBootstrap mainSceneBootstrap)
        {
            _sceneBootstrap = mainSceneBootstrap;

            _sceneBootstrap.OnServicesInitialized += Initialize;
        }

        private void Initialize()
        {
            _projectilePool = new CustomPool<DirectProjectile>(_data.ProjectilePrefab, _data.ProjectilePoolCount, "ProjectileTrapProjectiles");

            _isReadyToShoot = true;
        }

        private void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!_isReadyToShoot)
                return;

            _isReadyToShoot = false;

            var projectile = _projectilePool.Get();
            projectile.Prepare(_data.ShootPoint.position, _data.ShootDirection, _data.ProjectileData);

            RechargeRangeAttack().Forget();
        }

        private async UniTaskVoid RechargeRangeAttack()
        {
            try
            {
                await UniTask.WaitForSeconds(_data.FireRate, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException)
            {
                return;
            }

            _isReadyToShoot = true;
        }

        private void OnDestroy()
        {
            if (_sceneBootstrap != null)
                _sceneBootstrap.OnServicesInitialized -= Initialize;
        }
    }
}
