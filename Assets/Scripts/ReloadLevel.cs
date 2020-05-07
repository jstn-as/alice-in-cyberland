using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ReloadLevel : MonoBehaviour
{
    [SerializeField] private InputActionReference _reloadAction;

    private void OnEnable()
    {
        _reloadAction.action.Enable();
    }

    private void OnDisable()
    {
        _reloadAction.action.Disable();
    }

    private void Awake()
    {
        _reloadAction.action.performed += OnReload;
    }

    private void OnDestroy()
    {
        _reloadAction.action.performed -= OnReload;
    }

    private void OnReload(InputAction.CallbackContext obj)
    {
        Reload();
    }

    public void Reload()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }
}
