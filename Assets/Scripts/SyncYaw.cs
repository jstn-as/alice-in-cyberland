using UnityEngine;

public class SyncYaw : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField, Range(0, 1)] private float _speed;

    private void Update()
    {
        var t = transform;
        var rotation = t.rotation;
        var targetRotation = rotation;
        var cameraRotation = _camera.rotation;
        targetRotation.y = cameraRotation.y;
        targetRotation.w = cameraRotation.w;
        var smooth = Quaternion.Lerp(rotation, targetRotation, _speed);
        transform.rotation = smooth;
    }
}
