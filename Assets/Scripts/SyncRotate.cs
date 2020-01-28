using UnityEngine;

public class SyncRotate : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _targetTransform;

    private void Update()
    {
        transform.eulerAngles = _targetTransform.eulerAngles + _offset;
    }
}
