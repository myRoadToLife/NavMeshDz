using UnityEngine;

namespace FixCode
{
    public class Mine : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int _damage;
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private float _timeThreshold = 2f;
        private Character _character;

        private float _timeInRange = 0f;
        private bool _hasExploded = false;


        [Header("Effects")]
        [SerializeField] private ParticleSystem _mineEffect;
        private bool _showGizmos = false;

        private void OnTriggerStay(Collider other)
        {
            if (_hasExploded)
                return;

            _character = other.GetComponent<Character>();
            _collider = GetComponent<SphereCollider>();

            _showGizmos = true;
            _timeInRange += Time.deltaTime;

            if (_character != null && _timeInRange >= _timeThreshold)
            {
                _mineEffect.Play();
                _character.TakeDamage(_damage);
                _timeInRange = 0f;
                _hasExploded = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _character = other.GetComponent<Character>();
            if (_character != null)
            {
                _hasExploded = false;
                _showGizmos = false;
                _timeInRange = 0f;
            }
        }

        private void OnDrawGizmos()
        {
            if (_showGizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, _collider.radius);
            }
        }
    }
}

