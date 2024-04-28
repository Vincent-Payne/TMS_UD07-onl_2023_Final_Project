using Platformer.Player;
using System;
using Platformer.Common;
using Platformer.Pickables;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace Platformer.Player
{
    public class PlayerController : MonoBehaviour, IDamageable
    {
        [Header("Health System")]
        [SerializeField]
        private PlayerHealthBarManager _playerHealthBarManager;

        [SerializeField]
        private float _currentHealth;

        [SerializeField]
        private float _maxHealth;

        [Header("Cherry System")]
        [SerializeField]
        private PlayerCherryBarManager _playerCherryBarManager;

        [SerializeField]
        private int _currentCherry;

        [SerializeField]
        private int _maxCherry;

        [Header("Invulnerability System")]
        [SerializeField]
        private float _invulnerabilityTime;

        [SerializeField]
        private SpriteRenderer _invulnerabilityView;

        [SerializeField]
        private float _invulnerabilityAfterHitCooldown = 1f;

        [Header("Combat System")]
        [SerializeField]
        private PlayerAttackController _playerAttackController;

        [Header("Other")]
        [SerializeField]
        private PlayerView _playerView;

        [SerializeField]
        private PlayerPhysics _playerPhysics;



        private bool _invulnerableAfterHit;
        private bool _invulnerableByCherry;
        private bool _isRunningLeft;
        private bool _isRunningRight;
        private bool _isJumping;
        private bool _isDead;


        public static event Action OnCurrentHealthChanged;
        public event Action OnDied;
        public event Action<IPickable> OnPickableCollected;

        private void Awake()
        {
            _playerPhysics.OnCollided += OnCollided;
        }

        private void Start()
        {
            _playerHealthBarManager.CurrentHealth = _currentHealth;
            _playerHealthBarManager.MaxHealth = _maxHealth;
            _playerHealthBarManager.DrawHearts();

            _playerCherryBarManager.CurrentCherry = _currentCherry;
            _playerCherryBarManager.MaxCherry = _maxCherry;
            _playerCherryBarManager.DrawCherries();

            _playerPhysics.OnCollided += OnCollided;
            GameManager.OnCurrentCherryChanged += CherryIncrease;
            _invulnerabilityView.enabled = false;
        }

        private void OnDestroy()
        {
            _playerPhysics.OnCollided -= OnCollided;
            GameManager.OnCurrentCherryChanged -= CherryIncrease;
        }


        private void Update()
        {
            if (!_isDead)
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
                    if (_playerCherryBarManager.CurrentCherry != 0 && !_invulnerableByCherry)
                    {
                        SoundManager.Sound_Manager.PlayPlayerUseCherrySound();
                        _invulnerableByCherry = true;
                        _invulnerabilityView.enabled = true;
                        StartCoroutine(DisableInvulnerabilityByCherry(_invulnerabilityTime));
                        CherryDecrease();
                    }
                }

                if (Input.GetKey(KeyCode.Space))
                {
                    _playerAttackController.PlayerAttack();
                }

                _playerView.Tick(_playerPhysics.Velocity, _playerPhysics.IsOnGround);
            }
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
            if (collidedObject.TryGetComponent<IDamageSource>(out var damageSource) && !_invulnerableAfterHit && !_invulnerableByCherry && !_isDead)
            {
                _playerHealthBarManager.CurrentHealth -= damageSource.Damage;

                if (_playerHealthBarManager.CurrentHealth < 1)
                {
                    //Death
                    StartCoroutine(PlayerDeathCoroutine(2f));
                }
                else
                {
                    // Hit
                    _playerPhysics.Disable();
                    _playerView.PlayHitAnimation(OnHitAnimationFinished);
                    SoundManager.Sound_Manager.PlayPlayerHitByEnemySound();
                    _invulnerableAfterHit = true;
                    StartCoroutine(DisableInvulnerabilityAfterHitCoroutine(_invulnerabilityAfterHitCooldown));
                }

                OnCurrentHealthChanged?.Invoke();
            }
        }

        private void CherryIncrease()
        {
            if (_playerCherryBarManager.CurrentCherry < _playerCherryBarManager.MaxCherry) _playerCherryBarManager.CurrentCherry++;
            else return;
            _playerCherryBarManager.DrawCherries();
        }
        private void CherryDecrease()
        {
            _playerCherryBarManager.CurrentCherry--;
            _playerCherryBarManager.DrawCherries();
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
        private IEnumerator DisableInvulnerabilityAfterHitCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            _invulnerableAfterHit = false;
        }
        private IEnumerator DisableInvulnerabilityByCherry(float time)
        {
            yield return new WaitForSeconds(time);
            _invulnerableByCherry = false;
            _invulnerabilityView.enabled = false;
        }

        private IEnumerator PlayerDeathCoroutine(float time)
        {
            _isDead = true;
            _playerPhysics.Disable();
            _playerView.PlayDieAnimation(OnDieAnimationFinished);
            SoundManager.Sound_Manager.PlayPlayerDeathSound();
            yield return new WaitForSeconds(time);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}