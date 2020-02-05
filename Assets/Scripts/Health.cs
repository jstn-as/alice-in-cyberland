using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private Renderer _glowRenderer;
    private static readonly int HealthId = Shader.PropertyToID("_Health");

    public int GetMaxHealth()
    {
        return _maxHealth;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public void ChangeHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        if (_currentHealth <= 0)
        {
            Die();
        }
        // Set the emission of the object to the health.
        var material = _glowRenderer.material;
        material.SetFloat(HealthId,(float)_currentHealth/_maxHealth);
    }

    private void Die()
    {
        print("Die");
        Destroy(gameObject);
    }

    private void Start()
    {
        ChangeHealth(0);
    }
}