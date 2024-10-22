using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
    public float CurrentHealth { get; private set; }

    [SerializeField] private ViewCharacter _view;

    private bool _isDead = false;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    internal void TakeDamage(float damage)
    {
        if (_isDead)
            return;

        CurrentHealth -= damage;

        CurrentHealth = Mathf.Max(0, CurrentHealth);

        _view.UpdateHealthText();

        if (CurrentHealth <= 0 && !_isDead)
            Die();
    }

    private void Die()
    {
        _isDead = true;

        Debug.Log("Игрок погиб!");
        Destroy(transform.parent.gameObject);
    }
}
