using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CrosshairDisplay : MonoBehaviour
    {
        [SerializeField] private Sprite _defaultCrosshair;
        private Image _crosshairImage;

        private void Awake()
        {
            _crosshairImage = GetComponent<Image>();
            ResetCrosshair();
        }

        public void SetCrosshair(Sprite newCrosshair)
        {
            _crosshairImage.sprite = newCrosshair;
        }

        public void ResetCrosshair()
        {
            _crosshairImage.sprite = _defaultCrosshair;
        }
    }
}