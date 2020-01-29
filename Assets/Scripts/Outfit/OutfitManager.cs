using UnityEngine;

namespace Outfit
{
    public class OutfitManager : MonoBehaviour
    {
        [SerializeField] private OutfitObject _outfit;
        [SerializeField] private Renderer _bodyRenderer;
        [SerializeField] private Renderer _glowRenderer;

        private void ApplyOutfit()
        {
            _bodyRenderer.sharedMaterial = _outfit.GetBodyMaterial();
            _glowRenderer.sharedMaterial = _outfit.GetGlowMaterial();
        }

        private void Awake()
        {
            ApplyOutfit();
        }
    }
}