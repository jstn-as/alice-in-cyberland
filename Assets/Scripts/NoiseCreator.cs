using UnityEngine;
using Random = UnityEngine.Random;

public class NoiseCreator : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Material _material;

    private void Update()
    {
        var texture = new Texture2D(_size.x, _size.y, TextureFormat.RGBA32, false);
        var colours = new Color[_size.x * _size.y];
        for (var y = 0; y < _size.y; y++)
        {
            for (var x = 0; x < _size.x; x++)
            {
                var randomFloat = Random.Range(0, 255);
                var randomColour = new Color(randomFloat, randomFloat, randomFloat);
                colours[_size.x * y + x] = randomColour;
            }
        }
        texture.SetPixels(colours);
        _material.mainTexture = texture;
    }
}
