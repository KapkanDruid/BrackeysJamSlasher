using Assets.Scripts.Architecture;
using Assets.Scripts.Content.CoreProgression;
using Assets.Scripts.Content.PlayerLogic;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Content
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] private ProgressCardsConfig _progressCardsConfig;
        [SerializeField] private List<EntitySubList> _enemyWaves;
        [SerializeField] private List<float> _wavesDelay;
        [SerializeField] private List<Transform> _spawnPoints;
        [SerializeField] private List<GameObject> _walls;

        private StopwatchTimer _timer;
        private PlayerController _playerController;
        private PlayerProgressController _playerProgressController;

        private EnemyDeadHandler[] _enemies;
        private bool _isComplete;
        private bool _combatStarted;

        [Inject]
        private void Construct(StopwatchTimer timer, PlayerController playerController, PlayerProgressController playerProgressController)
        {
            _timer = timer;
            _playerController = playerController;
            _playerProgressController = playerProgressController;
        }

        private void Start()
        {
            List<EnemyDeadHandler> enemies = new();

            foreach (var wave in _enemyWaves)
            {
                foreach (var entity in wave.Entities)
                {
                    var enemy = entity.ProvideComponent<EnemyDeadHandler>();

                    if (enemy != null)
                        enemies.Add(enemy);
                }
            }

            _isComplete = false;

            foreach (var wall in _walls)
                wall.SetActive(false);

            _enemies = enemies.ToArray();
        }

        private void Update()
        {
            CheckForEndCondition();
        }

        private void CheckForEndCondition()
        {
            if (_isComplete)
                return;

            for (int i = 0; i < _enemies.Length; i++)
            {
                if (!_enemies[i].IsDead)
                {
                    return;
                }
            }

            _isComplete = true;

            EndCombat();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_combatStarted)
                return;

            if (!PlayerEnterCondition.IsPlayer(collision.gameObject))
                return;

            StartCombat();
        }

        private void StartCombat()
        {
            foreach (var wall in _walls)
                wall.SetActive(true);

            SpawnEnemiesAsync().Forget();

            _combatStarted = true;

            _timer.StartTimer();
        }

        private async UniTask SpawnEnemiesAsync()
        {
            if (_combatStarted)
                return;

            for (int i = 0; i < _enemyWaves.Count; i++)
            {
                IEntity[] entities = _enemyWaves[i].Entities.ToArray();

                float waveDelay = (i < _wavesDelay.Count) ? _wavesDelay[i] : _wavesDelay[_wavesDelay.Count - 1];

                for (int j = 0; j < entities.Length; j++)
                {
                    var enemyTransform = entities[j].ProvideComponent<Transform>();

                    if (enemyTransform != null)
                        SpawnEnemy(enemyTransform);
                }

                try
                {
                    await UniTask.WaitForSeconds(waveDelay, cancellationToken: this.GetCancellationTokenOnDestroy());
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }

        private void SpawnEnemy(Transform enemyTransform)
        {
            int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Count - 1);

            enemyTransform.position = _spawnPoints[randomIndex].position;

            enemyTransform.gameObject.SetActive(true);
        }

        private void EndCombat()
        {
            foreach (var wall in _walls)
                wall.SetActive(false);

            _playerController.Heal();

            _timer.Stop();

            EndTimer().Forget();
        }

        private async UniTask EndTimer()
        {
            try
            {
                await UniTask.WaitForSeconds(2, cancellationToken: this.GetCancellationTokenOnDestroy());
            }
            catch (OperationCanceledException)
            {
                return;
            }
            float endTime = _timer.ResetTimer();

            StaticData.Score += Mathf.RoundToInt(200 + (60 - endTime) * 50);

            _playerProgressController.ShowProgressCards(_progressCardsConfig, endTime);
        }
    }
}
