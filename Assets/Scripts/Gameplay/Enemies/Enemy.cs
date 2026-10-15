using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace WaveTestTask.Gameplay.Enemies
{
    public class Enemy : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Enemy> Died;

        private bool _isDead;

        public void ResetState()
        {
            _isDead = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isDead)
                return;

            _isDead = true;
            Died?.Invoke(this);
        }
    }
}

