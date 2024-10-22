using UnityEngine;

public class Mine : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _damage;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _timeThreshold = 2f;

    [Header("Effects")]
    [SerializeField] private ParticleSystem _mineEffect;

    private ViewCharacter _view;

    private bool _showGizmos = false;
    private bool _isTriggered = false;
    private float _timeInRange = 0f;

    public void Initialize(ViewCharacter view)
    {
        _view = view;
    }

    private void Update()
    {
        if (!_isTriggered)
            CheckForPlayerInRange();
    }

    private void CheckForPlayerInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);

        bool playerInRange = false;

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Character>())
            {
                playerInRange = true;
                _timeInRange += Time.deltaTime;

                _showGizmos = true;

                if (_timeInRange >= _timeThreshold && !_isTriggered)
                {
                    Explode();
                }

                break;
            }
        }

        if (!playerInRange)
        {
            _timeInRange = 0f;
            _showGizmos = false;
        }
    }

    private void Explode()
    {
        if (!_isTriggered)
        {
            _isTriggered = true;

            Collider[] hitPlayers = Physics.OverlapSphere(transform.position, _explosionRadius);

            if (_mineEffect != null)
            {
                _mineEffect.Play();
            }

            foreach (Collider hit in hitPlayers)
            {
                if (hit.GetComponent<Character>())
                {
                    Health playerHealth = hit.GetComponentInChildren<Health>();

                    if (playerHealth != null)
                    {
                        playerHealth.TakeDamage(_damage);

                        _view.UpdateHealthText();
                    }
                }
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        if (_showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _explosionRadius);
        }
    }
}
