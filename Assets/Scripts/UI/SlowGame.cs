using System;
using UnityEngine;

namespace UI
{
    public class SlowGame : MonoBehaviour
    {
        [SerializeField, Range(0, 1)] private float _speed, _minTime;

        private void Update()
        {
            var timeScale = Time.timeScale;
            timeScale -= _speed * Time.unscaledDeltaTime;
            timeScale = Mathf.Clamp(timeScale, _minTime, 1);
            Time.timeScale = timeScale;
        }

        private void OnDestroy()
        {
            Time.timeScale = 1;
        }
    }
}
