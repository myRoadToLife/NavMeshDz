using UnityEngine;
using UnityEngine.AI;

public class Movement
{

    public void Move(NavMeshAgent agent, Camera camera)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Ground ground = hitInfo.collider.GetComponent<Ground>();

            if (ground != null)
            {
                agent.SetDestination(hitInfo.point);
            }
        }
    }
}
