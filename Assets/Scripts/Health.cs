using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 50f;
    private float _currentHealth;
    private float _damage;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    internal void TakeDamage(float damage)
    {
        _currentHealth -= damage;
    }
}
