using UnityEngine;

namespace Platformer.Common
{
    public class Follower : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        [SerializeField]
        private float _speed = 1f;

        [SerializeField]
        private Vector3 _offset;


        private void Update()
        {
            if (_target == null)
                return;

            var position = transform.position;

            var targetPosition = _target.position;
            targetPosition.z = position.z;
            targetPosition += _offset;

            transform.position = Vector3.Lerp(position, targetPosition, _speed * Time.deltaTime);
        }


        public void Init(Transform target)
        {
            _target = target;

            var position = _target.position;
            transform.position = new Vector3(position.x + _offset.x, position.y + _offset.y, transform.position.z);
        }
    }
}