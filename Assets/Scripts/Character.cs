using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Camera _camera;

    [Header("Elements")]
    [SerializeField] private ViewCharacter _view;
    [SerializeField] private Health _health;

    private Movement _movement;
    private float _timeToDance;
    private readonly float _timeToBored = 5f;

    private void Awake()
    {
        _movement = new Movement();
    }

    private void Update()
    {
        HandleInput();
        HandleMovement();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TryMoveCharacter())
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                UpdateViewOnMove(ray);
            }
        }
    }

    private bool TryMoveCharacter()
    {
        if (_movement.Move(_agent, _camera))
        {
            HandleRunning(true);
            HandleDancing(false);

            _view.HandleInjuryAndMovementState();
            return true;
        }
        return false;
    }

    private void HandleMovement()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance)
        {
            if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
            {
                OnCharacterStopped();
            }
        }
        else
        {
            _timeToDance = 0f;
        }
    }

    private void OnCharacterStopped()
    {
        _view.DestroyOldMarker();

        HandleRunning(false);

        _timeToDance += Time.deltaTime;

        if (_timeToDance >= _timeToBored)
        {
            HandleDancing(true);
        }
    }

    private void HandleRunning(bool isRunning)
    {
        if (isRunning)
        {
            _view.StartRunning();
        }
        else
        {
            _view.StopRunning();
        }
    }

    private void HandleDancing(bool isDancing)
    {
        if (isDancing)
        {
            _view.StartDancing();
        }
        else
        {
            _view.StopDancing();
        }
    }

    private void UpdateViewOnMove(Ray ray)
    {
        _view.DestroyOldMarker();
        _view.CreateNewMarker(ray);
    }
}
