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

        private void Update()
        {
            if (Health.Value <= 0f)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(int damage) => Health.Reduce(damage);

        public bool IsInjured()
        {
            bool ingered = Health.Value <= Health.MaxValue * 0.3f;
            return ingered;
        }
    }
}