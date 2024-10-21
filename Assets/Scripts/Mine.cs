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
        // ���� ���� �� ���� ������������, ���������, ��������� �� ����� � �������
        if (!_isTriggered)
        {
            CheckForPlayerInRange();
        }
    }

    // ���������, ���� �� ����� � ���� �������� ����
    private void CheckForPlayerInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (var hitCollider in hitColliders)
        {
            // ���������, ��� ������ �������� ������� (�� ���� "Player")
            if (hitCollider.GetComponent<Player>())
            {
                // ���������� ����
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
            Debug.LogWarning("����� ���������! ���� ������������!");
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
            _mineEffect.Play(); // ���������� �����
        }

        foreach (Collider hit in hitPlayers)
        {
            if (hit.GetComponent<Player>())
            {
                Health playerHealth = hit.GetComponent<Health>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(_damage); // ������� ���� ������
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // ���������� ������ ������ � ���������
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}
