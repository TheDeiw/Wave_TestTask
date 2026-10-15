using System;
using System.Collections.Generic;
using UnityEngine;

namespace WaveTestTask.Gameplay.Waves
{
    [CreateAssetMenu(menuName = "Wave Test Task/Waves Config", fileName = "WavesConfig")]
    public class WavesConfig : ScriptableObject
    {
        [SerializeField] private WaveSettings[] waves = Array.Empty<WaveSettings>();

        public IReadOnlyList<WaveSettings> Waves => waves;
    }
}