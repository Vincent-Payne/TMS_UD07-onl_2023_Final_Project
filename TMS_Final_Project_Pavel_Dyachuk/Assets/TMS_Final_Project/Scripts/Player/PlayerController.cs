using System;
using Platformer.Common;
using Platformer.Pickables;
using UnityEngine;

namespace Platformer.Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        public event Action<int> OnNumLivesChanged;
        public event Action OnDied;
        public event Action<IPickable> OnPickableCollected;


        [SerializeField]
        private Health _health;

        [SerializeField]
        private PlayerView _playerView;

        [SerializeField]
        private PlayerPhysics _playerPhysics;

        [SerializeField]
        private SpriteRenderer _invulnerabilityView;


        public int NumLives => _health.NumLives;
        public bool IsInvulnerable => _health.IsInvulnerable;


        private bool _isRunningLeft;
        private bool _isRunningRight;
        private bool _isJumping;


        private void Awake()
        {
            // _playerPhysics.OnCollided += OnCollided;
        }

        private void Start()
        {
            _health.OnNumLivesChanged += OnHealthNumLivesChanged;
            _health.OnInvulnerabilityEnabled += OnInvulnerabilityEnabled;
            _health.OnInvulnerabilityDisabled += OnInvulnerabilityDisabled;

            _playerPhysics.OnCollided += OnCollided;

            _health.Init();
        }

        private void OnDestroy()
        {
            _health.OnNumLivesChanged -= OnHealthNumLivesChanged;
            _health.OnInvulnerabilityEnabled -= OnInvulnerabilityEnabled;
            _health.OnInvulnerabilityDisabled -= OnInvulnerabilityDisabled;

            _playerPhysics.OnCollided -= OnCollided;
        }


        private void Update()
        {
            if (_health.IsDead)
            {
                return;
            }

            if (Input.GetKey(KeyCode.A))
            {
                _isRunningLeft = true;
            }

            if (Input.GetKey(KeyCode.D))
            {
                _isRunningRight = true;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                _isJumping = true;
            }

            _playerView.Tick(_playerPhysics.Velocity, _playerPhysics.IsOnGround);
        }

        private void FixedUpdate()
        {
            _playerPhysics.Tick(_isRunningLeft, _isRunningRight, _isJumping);

            // Reset input for movement
            _isRunningLeft = false;
            _isRunningRight = false;
            _isJumping = false;
        }


        private void OnCollided(GameObject collidedObject)
        {
            if (collidedObject.TryGetComponent<IDamageSource>(out var damageSource))
            {
                var isDamageTaken = _health.TakeDamage(damageSource.Damage);

                if (!isDamageTaken)
                {
                    return;
                }

                if (_health.IsDead)
                {
                    // Die
                    _playerView.PlayDieAnimation(OnDieAnimationFinished);
                }
                else
                {
                    // Hit
                    _playerView.PlayHitAnimation(OnHitAnimationFinished);
                }
            }
        }

        private void OnHealthNumLivesChanged(int numLives)
        {
            OnNumLivesChanged?.Invoke(numLives);
        }

        private void OnInvulnerabilityEnabled()
        {
            _playerPhysics.Disable();

            _invulnerabilityView.enabled = true;
        }

        private void OnInvulnerabilityDisabled()
        {
            _playerPhysics.Enable();

            _invulnerabilityView.enabled = false;
        }

        private void OnHitAnimationFinished()
        {
        }

        private void OnDieAnimationFinished()
        {
            OnDied?.Invoke();
        }

        public void Pick(IPickable pickable)
        {
            OnPickableCollected?.Invoke(pickable);
        }

        public void EnableInvulnerability()
        {
            _health.EnableInvulnerability();
        }

        public void Revive()
        {
            _health.Revive();
        }
    }
}