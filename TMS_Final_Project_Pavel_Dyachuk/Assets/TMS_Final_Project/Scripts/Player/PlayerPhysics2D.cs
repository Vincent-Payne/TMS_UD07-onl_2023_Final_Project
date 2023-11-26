using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerPhysics2D : PlayerPhysics
    {
        // Need to set LayerMask in Awake
        private static int PlayerLayerIndex;
        private static int PlayerGhostLayerIndex;

        public override event Action<GameObject> OnCollided;

        public override Vector2 Velocity => _rigidbody2D.velocity;
        public override bool IsOnGround => _isOnGround;

        [SerializeField]
        private Rigidbody2D _rigidbody2D;

        //[SerializeField]
        private string _groundTag = "Ground";

        //[SerializeField]
        private string _trampolineTag = "Trampoline";

        //[SerializeField]
        private string _movingGroundTag= "MovingGround";

        //[SerializeField]
        private string _jumpVulnerableEnemyTag = "JumpVulnerableEnemy";

        [SerializeField]
        private float _moveForce = 3000f;

        [SerializeField]
        private float _jumpForce = 1200f;


        //private Collider2D _collider2D;

        private bool _isOnGround;

        private readonly List<GameObject> _collidedGroundObjects = new List<GameObject>();

        private void Awake()
        {
            PlayerLayerIndex = LayerMask.NameToLayer("Player");
            PlayerGhostLayerIndex = LayerMask.NameToLayer("PlayerGhost");

            //_collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnCollided?.Invoke(other.gameObject);
            //if (other.gameObject.CompareTag(_jumpVulnerableEnemyTag) && (gameObject.transform.position.y > other.transform.position.y))
            //{
            //    Destroy(other.gameObject);
            //    _rigidbody2D.velocity = Vector2.zero;
            //    _rigidbody2D.velocity = Vector3.zero;
            //    _rigidbody2D.angularVelocity = 0;
            //    _rigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            //}
            //else
            //{
            //    OnCollided?.Invoke(other.gameObject);
            //}
        }

        private void OnTriggerExit2D(Collider2D other)
        {
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if ((col.gameObject.CompareTag(_trampolineTag)) && (gameObject.transform.position.y > col.transform.position.y))
            {
                //Debug.Log("I'm on trampoline");
                SoundManager.Sound_Manager.PlayTrampolineSound();
                _rigidbody2D.AddForce(_jumpForce * 0f * col.transform.up, ForceMode2D.Force);
                _rigidbody2D.AddForce(_jumpForce * 2f * col.transform.up, ForceMode2D.Impulse);
            }

            if (col.gameObject.CompareTag(_groundTag))
            {
                //Debug.Log("I'm on ground");
                //Transform player object coordinates with platform collider coordinates
                _rigidbody2D.transform.parent = col.transform;

                if (_collidedGroundObjects.Contains(col.gameObject))
                {
                    return;
                }

                _collidedGroundObjects.Add(col.gameObject);

                _isOnGround = true;
            }

            OnCollided?.Invoke(col.gameObject);
        }

        private void OnCollisionStay2D(Collision2D col)
        {
            OnCollided?.Invoke(col.gameObject);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            OnCollided?.Invoke(col.gameObject);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag(_groundTag))
            {
                if (_collidedGroundObjects.Contains(other.gameObject))
                {
                    _collidedGroundObjects.Remove(other.gameObject);
                }

                if (_collidedGroundObjects.Count == 0)
                {
                    _isOnGround = false;
                    _rigidbody2D.transform.parent = null;
                }
            }
        }

        public override void Enable()
        {
            gameObject.layer = PlayerLayerIndex;
        }

        public override void Disable()
        {
            gameObject.layer = PlayerGhostLayerIndex;
        }

        public override void Tick(bool isRunningLeft, bool isRunningRight, bool isJumping)
        {
            if (isRunningLeft)
            {
                _rigidbody2D.AddForce(new Vector2(-_moveForce, 0f), ForceMode2D.Force);
            }
            else if (isRunningRight)
            {
                _rigidbody2D.AddForce(new Vector2(_moveForce, 0f), ForceMode2D.Force);
            }

            if (isJumping && _isOnGround)
            {
                SoundManager.Sound_Manager.PlayPlayerJumpSound();
                _rigidbody2D.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}