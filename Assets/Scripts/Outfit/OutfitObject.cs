using UnityEngine;

namespace Outfit
{
    [CreateAssetMenu(fileName = "Outfit", menuName = "ScriptableObject/Outfit", order = 0)]
    public class OutfitObject : ScriptableObject
    {
        [SerializeField] private Material _bodyMaterial;
        [SerializeField] private Material _glowMaterial;

        public Material GetBodyMaterial()
        {
            return _bodyMaterial;
        }

        public Material GetGlowMaterial()
        {
            return _glowMaterial;
        }
    }
}