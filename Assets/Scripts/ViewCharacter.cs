using System;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class ViewCharacter : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Marker _markerPrefab;
    [SerializeField] private Health _health;

    [Header("Components")]
    [SerializeField] private TMP_Text _textHealth;
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _agent;

    private Marker _currentMarker;
    private Mine _mine;

    private readonly int _isRunningKey = Animator.StringToHash("isRunning");
    private readonly int _isInjuredKey = Animator.StringToHash("isInjured");
    private readonly int _isDancingKey = Animator.StringToHash("isDance");

    private const int InjuredLayerIndex = 1;
    private const int _weightOn = 1;
    private const int _weightOff = 0;

    private float _healthMultiplier = 0.3f;


    private void Start()
    {
        _textHealth.text = _health.CurrentHealth.ToString();
    }

    private void Update()
    {
        HandleInjuryAndMovementState();
    }

    public void CreateNewMarker(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            _currentMarker = Instantiate(_markerPrefab, hitInfo.point, Quaternion.identity);
        }
    }

    public void DestroyOldMarker()
    {
        if (_currentMarker != null)
        {
            Destroy(_currentMarker.gameObject);
        }
    }

    public void UpdateHealthText()
    {
        _textHealth.text = $"Health: {_health.CurrentHealth}";
    }

    public void StartRunning()
    {
        _animator.SetBool(_isRunningKey, true);
    }

    public void StopRunning()
    {
        _animator.SetBool(_isRunningKey, false);
    }

    public void StartDancing()
    {
        _animator.SetBool(_isDancingKey, true);
    }
    public void StopDancing()
    {
        _animator.SetBool(_isDancingKey, false);
    }

    public void HandleInjuryAndMovementState()  //тут я поздно понял, что это уже и не вьюшка получается, а логика...
    {
        float currentHealth = _health.CurrentHealth;
        float threshold = _health.MaxHealth * _healthMultiplier;

        bool isMoving = _agent.pathPending == false && _agent.remainingDistance > _agent.stoppingDistance;

        if (currentHealth <= threshold && isMoving)
        {
            _animator.SetBool(_isRunningKey, true);
            _animator.SetLayerWeight(InjuredLayerIndex, _weightOn);
        }
        else
        {
            _animator.SetBool(_isRunningKey, isMoving);
            _animator.SetLayerWeight(InjuredLayerIndex, _weightOff);
        }
    }

}
