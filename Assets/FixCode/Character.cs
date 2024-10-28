using UnityEngine;
using UnityEngine.AI;

namespace FixCode
{
    public class Character : MonoBehaviour, IDamageble
    {
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        public Health Health { get; private set; }

        public void Initialize(Health health)
        {
            Health = health;
        }

        public void TakeDamage(int damage) => Health.Reduce(damage);

        private void Update()
        {
            if (Health.Value <= 0f)
            {
                Destroy(gameObject);
            }
        }

    }
}