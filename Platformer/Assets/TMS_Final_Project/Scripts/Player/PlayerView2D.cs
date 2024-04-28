using System;
using System.Collections;
using UnityEngine;
namespace Platformer.Player
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerView2D : PlayerView
    {
        private static float VELOCITY_THRESHOLD = 0.1f;


        [SerializeField]
        private string _jumpAnimationName = "Jump";

        [SerializeField]
        private string _fallAnimationName = "Fall";

        [SerializeField]
        private string _runAnimationName = "Run";

        [SerializeField]
        private string _idleAnimationName = "Idle";

        [SerializeField]
        private string _hitAnimationName = "Hit";

        [SerializeField]
        private string _dieAnimationName = "Death";

        [SerializeField]
        private string _attackAnimationName1 = "Attack1";

        [SerializeField]
        private float _hitAnimationDuration = 0.5f;

        [SerializeField]
        private float _dieAnimationDuration = 2f;

        [SerializeField]
        private float _attack1AnimationDuration = 0.5f;


        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private GameObject _player2DGameObject;

        private bool _isAnimationOverriden;
        private Action _onAnimationFinished;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _player2DGameObject = transform.parent.gameObject;
        }

        public override void Tick(Vector2 velocity, bool isOnGround)
        {
            if (_isAnimationOverriden)
            {
                return;
            }

            if (velocity.x > VELOCITY_THRESHOLD)
            {
                _player2DGameObject.transform.localScale = new Vector3(1, 1, 1);
                //_spriteRenderer.flipX = false;
            }
            else if (velocity.x < -VELOCITY_THRESHOLD)
            {
                _player2DGameObject.transform.localScale = new Vector3 (-1, 1, 1);
                //_spriteRenderer.flipX = true;
            }

            if (!isOnGround)
            {
                if (velocity.y > VELOCITY_THRESHOLD)
                {
                    _animator.Play(_jumpAnimationName);
                }
                else if (velocity.y < -VELOCITY_THRESHOLD)
                {
                    _animator.Play(_fallAnimationName);
                }
            }
            else if (velocity.x > VELOCITY_THRESHOLD)
            {
                _animator.Play(_runAnimationName);
            }
            else if (velocity.x < -VELOCITY_THRESHOLD)
            {
                _animator.Play(_runAnimationName);
            }
            else
            {
                _animator.Play(_idleAnimationName);
            }
        }

        public override void PlayAttackAnimation(Action onAnimationFinished)
        {
            _isAnimationOverriden = true;
            _onAnimationFinished = onAnimationFinished;

            StartCoroutine(PlayAttackAnimationCoroutine());
        }

        public override void PlayHitAnimation(Action onAnimationFinished)
        {
            _isAnimationOverriden = true;
            _onAnimationFinished = onAnimationFinished;

            StartCoroutine(PlayHitAnimationCoroutine());
        }

        public override void PlayDieAnimation(Action onAnimationFinished)
        {
            _isAnimationOverriden = true;
            _onAnimationFinished = onAnimationFinished;

            StartCoroutine(PlayDieAnimationCoroutine());
        }


        private IEnumerator PlayHitAnimationCoroutine()
        {
            _animator.Play(_hitAnimationName);

            yield return new WaitForSeconds(_hitAnimationDuration);

            _isAnimationOverriden = false;
            _onAnimationFinished?.Invoke();
        }

        private IEnumerator PlayDieAnimationCoroutine()
        {
            _animator.Play(_dieAnimationName);

            yield return new WaitForSeconds(_dieAnimationDuration);

            _isAnimationOverriden = false;
            _onAnimationFinished?.Invoke();
        }

        private IEnumerator PlayAttackAnimationCoroutine()
        {
            _animator.Play(_attackAnimationName1);
            yield return new WaitForSeconds(_attack1AnimationDuration);
            _isAnimationOverriden = false;
            _onAnimationFinished?.Invoke();
        }
    }
}