using System;
using UnityEngine;

namespace WaveTestTask.Gameplay.Waves
{
    [Serializable] public struct WaveSettings
    {
        [SerializeField, Min(1)]
        [Tooltip("The number of enemies to spawn in this wave.")]
        private int enemyCount;

        [SerializeField, Min(0f)]
        [Tooltip("Seconds between two enemy spawns.")]
        private float spawnInterval;

        [SerializeField, Min(0f)]
        [Tooltip("Seconds to wait after this wave is cleared before the next one starts.")]
        private float delayBeforeNextWave;

        public int EnemyCount => enemyCount;
        public float SpawnInterval => spawnInterval;
        public float DelayBeforeNextWave => delayBeforeNextWave;
    }
}
