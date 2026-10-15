using TMPro;
using UnityEngine;
using WaveTestTask.Gameplay.Waves;

namespace WaveTestTask.UI
{
    public class WaveUI : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private WaveController waveController;

        [Header("UI Elements")]
        [SerializeField] private TMP_Text waveText;
        [SerializeField] private TMP_Text aliveEnemiesText;
        [SerializeField] private GameObject gameCompletedPanel;

        private void Awake()
        {
            gameCompletedPanel.SetActive(false);
        }

        private void OnEnable()
        {
            waveController.WaveStarted += OnWaveStarted;
            waveController.AliveCountChanged += OnAliveCountChanged;
            waveController.AllWavesCompleted += OnAllWavesCompleted;
        }

        private void OnDisable()
        {
            waveController.WaveStarted -= OnWaveStarted;
            waveController.AliveCountChanged -= OnAliveCountChanged;
            waveController.AllWavesCompleted -= OnAllWavesCompleted;
        }

        private void OnWaveStarted(int waveNumber)
        {
            waveText.text = $"Wave: {waveNumber}";
        }

        private void OnAliveCountChanged(int aliveCount)
        {
            aliveEnemiesText.text = $"Enemies alive: {aliveCount}";
        }

        private void OnAllWavesCompleted()
        {
            gameCompletedPanel.SetActive(true);
        }
    }
}