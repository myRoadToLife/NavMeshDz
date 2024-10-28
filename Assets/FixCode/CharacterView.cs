using UnityEngine;

namespace FixCode
{
    public class CharacterView : MonoBehaviour
    {
        private const float DeadZone = 0.1f;

        private readonly int _isRunningKey = Animator.StringToHash("isRunning");
        private readonly int _isInjuredKey = Animator.StringToHash("isInjured");
        private readonly int _isDancingKey = Animator.StringToHash("isDance");

        private Character _character;
        private Animator _animator;
        private Health _health;

        public void Initialize(Character character)
        {
            _animator = GetComponent<Animator>();
            _character = character;
        }


        private void Update()
        {
            if (_character != null && _character.Agent != null)
            {
                _animator.SetBool(_isRunningKey, _character.Agent.velocity.sqrMagnitude >= DeadZone);

                if (_character.IsInjured() && _character.Agent.velocity.sqrMagnitude > DeadZone)
                {
                    _animator.SetBool(_isInjuredKey, true);
                }

                if (_character.Agent.velocity.sqrMagnitude <= DeadZone)
                {
                    _animator.SetBool(_isInjuredKey, false);
                }
            }
        }
    }
}