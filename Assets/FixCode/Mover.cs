using UnityEngine;
using UnityEngine.AI;

namespace FixCode
{
    public class Mover
    {
        public void ClickToMove(RaycastHit raycastHit, NavMeshAgent meshAgent)
        {
            meshAgent.SetDestination(raycastHit.point);
        }

    }
}
