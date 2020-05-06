using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponIcon : MonoBehaviour
    {
        private Image _weaponImage;
        private void Awake()
        {
            _weaponImage = GetComponent<Image>();
        }

        public void SetSprite(Sprite image)
        {
            _weaponImage.sprite = image;
            _weaponImage.SetNativeSize();
            SetVisible(true);
        }

        public void SetVisible(bool visible)
        {
            _weaponImage.enabled = visible;
        }
    }
}
