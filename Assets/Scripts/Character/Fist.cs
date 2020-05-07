using UnityEngine;

namespace Character
{
    public class Fist : MonoBehaviour
    {
        [SerializeField] private int _damage;
        private void OnTriggerEnter(Collider other)
        {
            // Check for a health script.
            var healthScript = other.GetComponent<CharacterHealth>();
            if (!healthScript)
                return;
            // Ignore self.
            foreach (Transform children in other.transform)
            {
                if (transform == children)
                    return;
            }
            // Deal damage.
            healthScript.Damage(_damage);
        }
    }
}
