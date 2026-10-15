using System;
using UnityEngine;
using UnityEngine.Pool;

namespace WaveTestTask.Gameplay.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Enemy enemyPrefab;
        [Header("Enemy Pool Settings")]
        [SerializeField] private Vector2 spawnAreaSize = new(10f, 5f);
        [SerializeField, Min(1)] private int poolCapacity = 10;

        public event Action EnemyKilled;

        private ObjectPool<Enemy> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<Enemy>(
                createFunc: CreateEnemy,
                actionOnGet: OnGetEnemy,
                actionOnRelease: OnReleaseEnemy,
                actionOnDestroy: DestroyEnemy,
                collectionCheck: true,
                defaultCapacity: poolCapacity);
        }

        public void Spawn()
        {
            Enemy enemy = _pool.Get();
            enemy.transform.position = GetRandomPosition();
        }
        private Enemy CreateEnemy()
        {
            Enemy enemy = Instantiate(enemyPrefab, transform);
            enemy.Died += OnEnemyDied;
            return enemy;
        }
        private void OnGetEnemy(Enemy enemy)
        {
            enemy.ResetState();
            enemy.gameObject.SetActive(true);
        }

        private void OnReleaseEnemy(Enemy enemy)
        {
            enemy.gameObject.SetActive(false);
        }

        private void DestroyEnemy(Enemy enemy)
        {
            enemy.Died -= OnEnemyDied;
            Destroy(enemy.gameObject);
        }

        private void OnEnemyDied(Enemy enemy)
        {
            _pool.Release(enemy);
            EnemyKilled?.Invoke();
        }

        private Vector3 GetRandomPosition()
        {
            Vector2 halfSize = spawnAreaSize * 0.5f;
            var offset = new Vector3(
                UnityEngine.Random.Range(-halfSize.x, halfSize.x),
                UnityEngine.Random.Range(-halfSize.y, halfSize.y),
                0f);

            return transform.position + offset;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0f));
        }
    }
}