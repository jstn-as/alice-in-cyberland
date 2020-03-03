using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AmmoDisplay : MonoBehaviour
    {
        [SerializeField] private Text _ammoText;
        [SerializeField] private Text _magazineText;

        public void UpdateAmmo(int newAmmo)
        {
            _ammoText.text = $"{newAmmo}";
        }

        public void UpdateMaxAmmo(int newMaxAmmo)
        {
            _magazineText.text = $"{newMaxAmmo}";
        }
    }
}
