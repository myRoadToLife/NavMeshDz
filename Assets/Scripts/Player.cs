using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Camera _camera;

    private Movement _movement;
    private Health _healh;

    private void Start()
    {
        _movement = new Movement();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _movement.Move(_agent, _camera);
        }
    }
}