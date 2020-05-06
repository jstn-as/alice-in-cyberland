using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private ReloadLevel _reload;
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Player"))
        {
            _reload.Reload();
        }
    }
}
