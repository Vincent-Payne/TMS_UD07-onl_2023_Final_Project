using Platformer.Player;
using System;
using Platformer.Common;
using Platformer.Pickables;
using UnityEngine;
using System.Collections;

namespace Platformer.Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private HealthBarManager _healthManager;

        [SerializeField]
        private float _currentHealth, _maxHealth;

        private bool _isInvulnerable;

        [SerializeField]
        private PlayerView _playerView;

        [SerializeField]
        private PlayerPhysics _playerPhysics;

        [SerializeField]
        private SpriteRenderer _invulnerabilityView;

        private bool _isRunningLeft;
        private bool _isRunningRight;
        private bool _isJumping;

        public static event Action OnCurrentHealthChanged;
        public event Action OnDied;
        public event Action<IPickable> OnPickableCollected;

        private void Awake()
        {
             _playerPhysics.OnCollided += OnCollided;
        }

        private void Start()
        {
            _healthManager.CurrentHealth = _currentHealth;
            _healthManager.MaxHealth = _maxHealth;
            _healthManager.DrawHearts();
            _playerPhysics.OnCollided += OnCollided;


        }

        private void OnDestroy()
        {
            _playerPhysics.OnCollided -= OnCollided;
        }


        private void Update()
        {

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

            if (Input.GetKeyDown(KeyCode.Q))
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
            if (collidedObject.TryGetComponent<IDamageSource>(out var damageSource) && !_isInvulnerable)
            {
                _healthManager.CurrentHealth -= damageSource.Damage;
                Debug.Log(_isInvulnerable);

                if (_healthManager.CurrentHealth < 1)
                {
                    // Die
                    _playerPhysics.Disable();
                    _playerView.PlayDieAnimation(OnDieAnimationFinished);
                }
                else
                {
                    // Hit
                    _playerPhysics.Disable();
                    _playerView.PlayHitAnimation(OnHitAnimationFinished);
                }

                OnCurrentHealthChanged?.Invoke();
            }
            //if (collidedObject.TryGetComponent<IDamageSource>(out var damageSource))
            //{
            //    var isDamageTaken = _health.TakeDamage(damageSource.Damage);

            //    if (!isDamageTaken)
            //    {
            //        return;
            //    }

            //    if (_health.IsDead)
            //    {
            //        // Die
            //        _playerView.PlayDieAnimation(OnDieAnimationFinished);
            //    }
            //    else
            //    {
            //        // Hit
            //        _playerView.PlayHitAnimation(OnHitAnimationFinished);
            //    }
            //}
        }

        //private void OnHealthNumLivesChanged(int numLives)
        //{
        //OnHealthNumLivesChanged?.Invoke(numLives);
        //}

        //private void OnInvulnerabilityEnabled()
        //{
        //    _playerPhysics.Disable();

        //    _invulnerabilityView.enabled = true;
        //}

        //private void OnInvulnerabilityDisabled()
        //{
        //    _playerPhysics.Enable();

        //    _invulnerabilityView.enabled = false;
        //}

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

        //public void EnableInvulnerability()
        //{
        //    _health.EnableInvulnerability();
        //}

        //public void Revive()
        //{
        //    _health.Revive();
        //}
    }
}