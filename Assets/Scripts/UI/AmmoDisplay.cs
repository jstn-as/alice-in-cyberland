using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        [SerializeField] private Text _ammoText;
        [SerializeField] private Text _magazineText;
        [SerializeField] private GameObject _boxText;

        public void UpdateAmmo(int newAmmo)
        {
            _ammoText.text = $"{newAmmo}";
        }

        public void UpdateMaxAmmo(int newMaxAmmo)
        {
            _magazineText.text = $"{newMaxAmmo}";
        }

        public void SetVisible(bool visible)
        {
            _boxText.SetActive(visible);
        }
    }
}
