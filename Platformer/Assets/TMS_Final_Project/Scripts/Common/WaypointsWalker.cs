using UnityEngine;

public class WaypointsWalker : MonoBehaviour
{
    [SerializeField]
    protected Transform[] _waypoints;

    [SerializeField]
    protected float _moveSpeed;


    private int _nextWaypointIndex;
    private Transform _nextWaypoint;


    private void Start()
    {
        if (_waypoints.Length > 0)
        {
            _nextWaypointIndex = 0;
            _nextWaypoint = _waypoints[_nextWaypointIndex];
        }
    }

    private void Update()
    {
        if (_nextWaypoint != null && _waypoints.Length > 0)
        {
            //Was Lerp
            transform.position = Vector3.MoveTowards(transform.position, _nextWaypoint.position, _moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _nextWaypoint.position) < 0.2f)
            {
                _nextWaypointIndex += 1;
                if (_nextWaypointIndex >= _waypoints.Length)
                {
                    _nextWaypointIndex = 0;
                }

                _nextWaypoint = _waypoints[_nextWaypointIndex];
            }
        }
    }
}