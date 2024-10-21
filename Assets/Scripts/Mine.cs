using System.Collections;
using UnityEngine;

public class Mine : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionDelay;

    [SerializeField] private ParticleSystem _mineEffect;
    private bool _isTriggered = false;

    private void Update()
    {
        // Если мина не была активирована, проверяем, находится ли игрок в радиусе
        if (!_isTriggered)
        {
            CheckForPlayerInRange();
        }
    }

    // Проверяем, есть ли игрок в зоне действия мины
    private void CheckForPlayerInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            // Проверяем, что объект является игроком (по тегу "Player")
            if (hitCollider.GetComponent<Player>())
            {
                // Активируем мину
                ActivateMine();
                break;
            }
        }
    }

    private void ActivateMine()
    {
        if (!_isTriggered)
        {
            _isTriggered = true;
            Debug.LogWarning("Игрок обнаружен! Мина активирована!");
            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(_explosionDelay);
        Explode();
    }

    private void Explode()
    {
        Collider[] hitPlayers = Physics.OverlapSphere(transform.position, _explosionRadius);

        if (_mineEffect != null)
        {
            _mineEffect.Play(); // Визуальный взрыв
        }

        foreach (Collider hit in hitPlayers)
        {
            if (hit.GetComponent<Player>())
            {
                Health playerHealth = hit.GetComponent<Health>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_damage); // Наносим урон игроку
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Отображаем радиус взрыва в редакторе
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
