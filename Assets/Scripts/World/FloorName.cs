using UnityEngine;
using UnityEngine.UI;

namespace World
{
    public class FloorName : MonoBehaviour
    {
        [SerializeField] private Text _floorText;

        public void SetName(string newName)
        {
            _floorText.text = newName;
        }
    }
}
