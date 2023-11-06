using System;
using System.Collections;
using Unity.Collections;
using UnityEngine;

namespace Platformer.Player
{
    public class Health : MonoBehaviour
    {
        public event Action<int> OnNumLivesChanged;
        public event Action OnInvulnerabilityEnabled;
        public event Action OnInvulnerabilityDisabled;


        [SerializeField]
        private int _startNumLives;

        [SerializeField]
        private float _invulnerabilityDuration;


        public int NumLives
        {
            get => _numLives;
            private set
            {
                _numLives = Mathf.Clamp(value, 0, int.MaxValue);
                OnNumLivesChanged?.Invoke(_numLives);
            }
        }

        public bool IsDead => _numLives == 0;
        public bool IsInvulnerable => _isInvulnerable;

        private int _numLives;
        private bool _isInvulnerable;
        private Coroutine _invulnerabilityCoroutine;


        public void Init()
        {
            DisableInvulnerability();

            NumLives = _startNumLives;
        }

        public bool TakeDamage(int damage = 1)
        {
            if (_isInvulnerable || IsDead)
                return false;

            NumLives -= damage;
            if (NumLives > 0) { SoundManager.Sound_Manager.PlayPlayerHitByEnemySound(); }

            if (NumLives == 0) { SoundManager.Sound_Manager.PlayPlayerDeathSound(); }

            if (!IsDead)
            {
                EnableInvulnerability();
            }

            return true;
        }

        public void Revive()
        {
            Init();

            EnableInvulnerability();
        }

        public void EnableInvulnerability()
        {
            _isInvulnerable = true;

            OnInvulnerabilityEnabled?.Invoke();

            if (_invulnerabilityCoroutine != null)
            {
                StopCoroutine(_invulnerabilityCoroutine);
            }

            _invulnerabilityCoroutine = StartCoroutine(DisableInvulnerabilityCoroutine());
        }

        private void DisableInvulnerability()
        {
            _isInvulnerable = false;

            OnInvulnerabilityDisabled?.Invoke();

            if (_invulnerabilityCoroutine != null)
            {
                StopCoroutine(_invulnerabilityCoroutine);
                _invulnerabilityCoroutine = null;
            }
        }

        private IEnumerator DisableInvulnerabilityCoroutine()
        {
            yield return new WaitForSeconds(_invulnerabilityDuration);

            DisableInvulnerability();
        }
    }
}