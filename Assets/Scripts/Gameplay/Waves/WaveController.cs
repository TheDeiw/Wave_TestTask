using System;
using System.Collections;
using UnityEngine;
using WaveTestTask.Gameplay.Enemies;

namespace WaveTestTask.Gameplay.Waves
{
    public class WaveController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WavesConfig config;
        [SerializeField] private EnemySpawner spawner;

        public event Action<int> WaveStarted;
        public event Action<int> AliveCountChanged;
        public event Action AllWavesCompleted;

        private int _aliveCount;
        private WaitUntil _waitAllEnemiesDead;

        private void Awake()
        {
            if (!IsSetupValid())
            {
                enabled = false;
                return;
            }

            _waitAllEnemiesDead = new WaitUntil(() => _aliveCount == 0);
        }

        private void OnEnable()
        {
            spawner.EnemyKilled += OnEnemyKilled;
        }

        private void OnDisable()
        {
            spawner.EnemyKilled -= OnEnemyKilled;
        }

        private void Start()
        {
            StartCoroutine(RunWaves());
        }

        private IEnumerator RunWaves()
        {
            for (int i = 0; i < config.Waves.Count; i++)
            {
                WaveSettings wave = config.Waves[i];
                WaveStarted?.Invoke(i+1);

                var spawnInterval = new WaitForSeconds(wave.SpawnInterval);
                for (int j = 0; j < wave.EnemyCount; j++)
                {
                    SpawnEnemy();

                    if (j < wave.EnemyCount - 1)
                        yield return spawnInterval;
                }
                yield return _waitAllEnemiesDead;

                bool isLastWave = i == config.Waves.Count - 1;
                if (!isLastWave)
                    yield return new WaitForSeconds(wave.DelayBeforeNextWave);
            }
            
            AllWavesCompleted?.Invoke();
        }
        
        private void SpawnEnemy()
        {
            spawner.Spawn();
            _aliveCount++;
            AliveCountChanged?.Invoke(_aliveCount);
        }

        private void OnEnemyKilled()
        {
            _aliveCount--;
            AliveCountChanged?.Invoke(_aliveCount);
        }

        private bool IsSetupValid()
        {
            if (config == null || config.Waves.Count == 0)
            {
                Debug.LogError($"{nameof(WaveController)}: waves config is missing or empty.", this);
                return false;
            }

            if (spawner == null)
            {
                Debug.LogError($"{nameof(WaveController)}: enemy spawner is not assigned.", this);
                return false;
            }

            return true;
        }
    }
}
